using Microsoft.Extensions.Configuration;
using Scraper.Abstractions;
using System.Collections.Generic;
using System.Linq;

namespace Scraper
{
    public class ConfigurationCompanyProvider : ICompanyProvider
    {
        private readonly IConfiguration _configuration;

        public ConfigurationCompanyProvider(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IAsyncEnumerable<string> GetCompanies() =>
            _configuration
                .GetSection("LeetcodeApi:Companies")
                .AsEnumerable()
                .Where(c => !string.IsNullOrEmpty(c.Value))
                .Select(c => c.Value)
                .ToAsyncEnumerable();
    }
}
