using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Repositories;
using NZWalks.API.RepositoriesInterface;
using NZWalks.API.Services;
using NZWalks.API.ServicesInterface;
using NZWalks.API.UnitOfWorks;
using NZWalks.API.UnitOfWorkInterface;
using System.Reflection;
using NZWalks.API.Utilities;

namespace NZWalks.API.ExtensionMethods
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services, string connectionString, Assembly migrationAssembly)
        {
            //Resolved here all service dependencies...
            services.AddDbContext<NZWalksDbContext>(options => options.UseSqlServer(connectionString, (m) => m.MigrationsAssembly(migrationAssembly)));
            services.AddDbContext<NZWalksAuthDbContext>(options => options.UseSqlServer(connectionString, (m) => m.MigrationsAssembly(migrationAssembly)));

            services.AddScoped<INZWalksUnitOfWork, NZWalksUnitOfWork>();
            services.AddScoped<IRegionRepository, RegionRepository>();
            services.AddScoped<IRegionManagementService, RegionManagementService>();
            services.AddScoped<IDifficultyRepository, DifficultyRepository>();
            services.AddScoped<IDifficultyManagementService, DifficultyManagementService>();
            services.AddScoped<IApplicationTime, ApplicationTime>();
            services.AddScoped<IWalkRepository, WalkRepository>();
            services.AddScoped<IWalkManagementService, WalkManagementService>();
            
            return services;
        }

        //Seeding data...
        public static IApplicationBuilder UseDatabaseSeeder(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<NZWalksDbContext>();
                DbInitializer.SeedData(dbContext);
            }

            return app;
        }
    }
}
