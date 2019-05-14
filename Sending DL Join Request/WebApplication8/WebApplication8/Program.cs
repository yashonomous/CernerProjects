using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace WebApplication8
{
    /// <summary>Entry point of our application.</summary>
    public class Program
    {
        /// <summary>Defines the entry point of the application.</summary>
        /// <param name="args">The arguments.</param>
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        /// <summary><para>Creates the web host builder.</para>
        /// The Startup class is a mandatory for the .NET Core, in which we configure embedded or custom services that our application needs. 
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <returns></returns>
        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseUrls("http://*:56727")
            .UseKestrel(options =>
            {
                options.Limits.MaxRequestBodySize = null;
            }
            )
            .UseDefaultServiceProvider(options =>
                options.ValidateScopes = false);
    }
}
