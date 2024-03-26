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

            var scraper = serviceProvider.GetRequiredService<Scraper>();

            await scraper.UpdateAllCompanySheetsQuestionsAsync();
            
            //await scraper.UpdateAllLastSubmittedAsync();
        }

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

            services.AddSingleton<ICompanyProvider, SheetTitleCompanyProvider>();
            services.AddSingleton<Scraper>();

            return services.BuildServiceProvider();
        }
    }
}