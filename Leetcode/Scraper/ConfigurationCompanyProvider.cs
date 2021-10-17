using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Scraper.Abstractions
{
    public class ConfigurationCompanyProvider : ICompanyProvider
    {
        private readonly IConfiguration _configuration;

        public ConfigurationCompanyProvider(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Task<IEnumerable<string>> GetCompanies() =>
            Task.FromResult(
                _configuration
                    .GetSection("LeetcodeApi:Companies")
                    .AsEnumerable()
                    .Where(c => !string.IsNullOrEmpty(c.Value))
                    .Select(c => c.Value)
                );
    }
}
