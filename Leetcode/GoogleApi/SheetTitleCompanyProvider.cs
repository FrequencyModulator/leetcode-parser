using Scraper.Abstractions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GoogleApi
{
    public class SheetTitleCompanyProvider : ICompanyProvider
    {
        private readonly GoogleSpreadsheetClient _client;

        public SheetTitleCompanyProvider(GoogleSpreadsheetClient client)
        {
            _client = client;
        }

        public Task<IEnumerable<string>> GetCompanies() 
            => _client.GetSheetNames();
    }
}
