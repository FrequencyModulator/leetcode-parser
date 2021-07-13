using GoogleApi;
using LeetcodeApi;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Scraper.Abstractions;
using Serilog;
using SubmissionSync;
using System;
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

            //await scraper.UpdateAllCompanySheetsQuestionsAsync();
            await scraper.UpdateAllLastSubmittedAsync();
            await scraper.UpdateMergedView();
            //await scraper.SyncSubmissionsAsync();
            Log.Information("Leetcode scraper finished");
        }   

        public static ServiceProvider BuildServiceProvider()
        {
            var services = new ServiceCollection();

            services.AddLogging(builder => builder.AddSerilog());

            services.AddSingleton(Configuration.GetSection("GoogleApi").Get<GoogleSpreadsheetClientConfiguration>());
            services.AddSingleton<GoogleSpreadsheetClient>();

            services.AddSingleton(Configuration.GetSection("LeetcodeApi").Get<LeetcodeClientConfiguration>());
            services.AddSingleton<LeetcodeClient>();

            services.AddSingleton(Configuration.GetSection("SubmissionSync").Get<SubmissionSyncConfiguration>());
            services.AddSingleton<ISubmissionSync, FolderSync>();

            services.AddSingleton<ConfigurationCompanyProvider>();
            services.AddSingleton<SheetTitleCompanyProvider>();
            services.AddSingleton<ICompanyProvider>(sp =>
            {
                var companyProvider = Configuration.GetValue<string>("LeetcodeApi:CompaniesProvider");

                if (string.Equals(companyProvider, "SpreadsheetTabTitles", StringComparison.OrdinalIgnoreCase))
                    return sp.GetRequiredService<SheetTitleCompanyProvider>();

                return sp.GetRequiredService<ConfigurationCompanyProvider>();
            });

            services.AddSingleton<Scraper>();

            return services.BuildServiceProvider();
        }
    }
}