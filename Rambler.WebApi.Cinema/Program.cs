using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace Rambler.WebApi.Cinema
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // var configuration = new ConfigurationBuilder()
            //     .AddJsonFile("appsettings.json")
            //     .Build();
            //
            // Log.Logger = new LoggerConfiguration()
            //     .ReadFrom.Configuration(configuration)
            //     .CreateLogger();
            
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); })
                .UseSerilog((hostingContext, loggerConfiguration) => loggerConfiguration
                    .ReadFrom.Configuration(hostingContext.Configuration));
    }
}