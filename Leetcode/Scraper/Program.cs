using GoogleApi;
using LeetcodeApi;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Scraper.Abstractions;
using Serilog;
using System.Threading.Tasks;

namespace Scraper
{
    public class Program
    {
        private static readonly IConfigurationRoot Configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: true)
            .AddUserSecrets<Program>(optional: true)
            .AddEnvironmentVariables(prefix: "LEETCODE")
            .Build();

        public static async Task Main()
        {
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(Configuration)
                .CreateLogger();

            Log.Information("Leetcode scraper started");
            var serviceProvider = BuildServiceProvider();
            
            //var client = serviceProvider.GetService<GoogleSpreadsheetClient>();

            //var names = await client.GetSheetNames();

            //foreach (var name in names)
            //{
            //    Console.WriteLine(name);
            //}

            var scraper = serviceProvider.GetRequiredService<Scraper>();

            //await scraper.UpdateAllCompanySheetsQuestionsAsync();
            
            await scraper.UpdateAllLastSubmittedAsync();

            //Log.Information("Leetcode scraper finished");
        }

        //private static CommandLineBuilder BuildCommandLine()
        //{
        //    var root = new RootCommand(@"$ dotnet run --name 'Joe'"){
        //        new Option<string>("--name"){
        //            IsRequired = true
        //        }
        //    };
        //    root.Handler = CommandHandler.Create<GreeterOptions, IHost>(Run);
        //    return new CommandLineBuilder(root);
        //}

        public static ServiceProvider BuildServiceProvider()
        {
            var services = new ServiceCollection();

            services.AddSingleton<IConfiguration>(Configuration);
            services.AddLogging(builder => builder.AddSerilog());

            services.AddSingleton(sp =>
            {
                var config = Configuration.GetSection("GoogleApi").Get<GoogleSpreadsheetClientConfiguration>();
                return new GoogleSpreadsheetClient(config);
            });

            services.AddSingleton(sp =>
            {
                var config = Configuration.GetSection("LeetcodeApi").Get<LeetcodeClientConfiguration>();
                return new LeetcodeClient(config);
            });

            //services.AddSingleton<ICompanyProvider, ConfigurationCompanyProvider>();

            services.AddSingleton<ICompanyProvider, SheetTitleCompanyProvider>();
            services.AddSingleton<Scraper>();

            return services.BuildServiceProvider();
        }
    }
}