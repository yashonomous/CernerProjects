using Contracts;
using LoggerService;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Entities;
using Repository;

namespace WebApplication8.Extensions
{
    /// <summary>This class contains our service extension methods.</summary>
    public static class ServiceExtensions                   //extension methods for services
    {
        /// <summary>Configures the cors policy.</summary>
        /// <param name="services">The services.</param>
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });
        }

        /// <summary>Configures the IIS integration which will help us with the IIS deployment</summary>
        /// <param name="services">The services.</param>
        public static void ConfigureIISIntegration(this IServiceCollection services)        //configuring IIS deployment.
        {
            services.Configure<IISOptions>(options =>
            {

            });
        }

        /// <summary>Configures the logger service.</summary>
        /// <param name="services">The services.</param>
        public static void ConfigureLoggerService(this IServiceCollection services)         //configuring the logger.
        {
            services.AddSingleton<ILoggerManager, LoggerManager>();
        }

        /// <summary>Configures MySQL context.</summary>
        /// <param name="services">The services.</param>
        /// <param name="config">The configuration.</param>
        public static void ConfigureMySqlContext(this IServiceCollection services, IConfiguration config)       //configuring MySql using Db connection string.
        {
            var connectionString = config["mysqlconnection:connectionString"];
            services.AddDbContext<RepositoryContext>(o => o.UseMySql(connectionString));
        }

        /// <summary>Configures the repository wrapper.</summary>
        /// <param name="services">The services.</param>
        public static void ConfigureRepositoryWrapper(this IServiceCollection services)             //configuring the RepositoryWrapper for 
        {
            services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
        }
    }
}
