namespace NZWalks.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            #region Bootstrap Logger
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connection = configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found."); ;
            var migrationAssembly = Assembly.GetExecutingAssembly() ?? throw new InvalidOperationException("Migration Assembly not found.");

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug().WriteTo.MSSqlServer(
                    connectionString: connection,
                    sinkOptions: new MSSqlServerSinkOptions { TableName = "ApplicationLogs", AutoCreateSqlTable = true })
                .ReadFrom.Configuration(configuration)
                .CreateBootstrapLogger();
            #endregion

            try
            {
                Log.Information("Application Starting...");

                var builder = WebApplication.CreateBuilder(args);

                #region Serilog Configuration
                builder.Host.UseSerilog((hostBuilderContext, loggerConfiguration) =>
                {
                    loggerConfiguration.MinimumLevel.Debug()
                    .WriteTo.MSSqlServer(
                        connectionString: connection,
                        sinkOptions: new MSSqlServerSinkOptions { TableName = "ApplicationLogs", AutoCreateSqlTable = true })
                    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                    .Enrich.FromLogContext()
                    .ReadFrom.Configuration(builder.Configuration);

                });
                #endregion  

                // Add services to the container.
                builder.Services.RegisterServices(connection, migrationAssembly);

                //This service for automapper
                builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


                builder.Services.AddNZWalksIdentity();

                builder.Services.AddJwtAuthentication(
                    builder.Configuration["Jwt:Key"],
                    builder.Configuration["Jwt:Issuer"],
                    builder.Configuration["Jwt:Audience"]);

                builder.Services.AddCorsPolicy();

                builder.Services.AddControllers(options =>
                {
                    options.SuppressAsyncSuffixInActionNames = false;
                });

                builder.Services.AddEndpointsApiExplorer();
                builder.Services.AddSwaggerGen();
                builder.Services.AddOpenApi();

                var app = builder.Build();

                // Configure the HTTP request pipeline.
                if (app.Environment.IsDevelopment())
                {
                    app.MapOpenApi();
                    app.UseSwagger();
                    app.UseSwaggerUI();
                }

                app.UseDatabaseSeeder();
                await app.SeedInitialDataAsync();

                app.UseHttpsRedirection();
                app.UseCors("AllowSites");
                app.UseAuthentication();
                app.UseAuthorization();

                app.MapControllers();

                await app.RunAsync();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Application Crashed...");
            }
            finally
            {
                await Log.CloseAndFlushAsync();
            }
        }
    }
}
