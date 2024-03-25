using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace MiddleWareIIsChange
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

            var outputTemplateConsole = configuration.GetValue<string>("Serilog:WriteTo:0:outputTemplate");
            var pathToWrite = configuration.GetValue<string>("Serilog:WriteTo:1:Args:path");
            var outputTemplateFile = configuration.GetValue<string>("Serilog:WriteTo:1:outputTemplate");


            Log.Logger = new LoggerConfiguration()
            .Enrich.FromLogContext()
            .Enrich.WithCorrelationId()
            .WriteTo.Console(outputTemplate: outputTemplateConsole)
            .WriteTo.File(pathToWrite, outputTemplate: outputTemplateFile)
            .CreateLogger();

            //Log.Logger = new LoggerConfiguration()
            //    .ReadFrom.Configuration(configuration)
            //    .CreateLogger();

            Log.Information("Starting up");


            

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .UseSerilog();
    }
}