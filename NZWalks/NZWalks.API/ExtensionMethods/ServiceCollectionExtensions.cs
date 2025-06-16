using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Repositories;
using NZWalks.API.RepositoriesInterface;
using NZWalks.API.Services;
using NZWalks.API.ServicesInterface;
using NZWalks.API.UnitOfWorks;
using NZWalks.API.UnitOfWorkInterface;
using System.Reflection;

namespace NZWalks.API.ExtensionMethods
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services, string connectionString, Assembly migrationAssembly)
        {
            //Resolved here all service dependencies...
            services.AddDbContext<NZWalksDbContext>(options => options.UseSqlServer(connectionString, (m) => m.MigrationsAssembly(migrationAssembly)));
            
            services.AddScoped<INZWalksUnitOfWork, NZWalksUnitOfWork>();
            services.AddScoped<IRegionRepository, RegionRepository>();
            services.AddScoped<IRegionManagementService, RegionManagementService>();
            
            
            //services.AddScoped<IApplicationTime, ApplicationTime>();

            return services;
        }
    }
}
