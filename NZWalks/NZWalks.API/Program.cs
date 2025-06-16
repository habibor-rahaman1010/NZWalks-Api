using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;
using System.Reflection;

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

            var connection = configuration.GetConnectionString("DefaultConnection");
            var migrationAssembly = Assembly.GetExecutingAssembly().FullName;

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

                //Dbcontext registerd here...
                builder.Services.AddDbContext<NZWalksDbContext>(options => 
                    options.UseSqlServer(connection, (m) => m.MigrationsAssembly(migrationAssembly)));

                // Add services to the container.
                builder.Services.AddControllers();
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

                app.UseHttpsRedirection();

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
