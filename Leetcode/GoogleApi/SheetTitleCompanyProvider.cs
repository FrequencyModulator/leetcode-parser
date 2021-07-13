using Scraper.Abstractions;
using System.Collections.Generic;
using System.Linq;

namespace GoogleApi
{
    public class SheetTitleCompanyProvider : ICompanyProvider
    {
        private readonly GoogleSpreadsheetClient _client;

        public SheetTitleCompanyProvider(GoogleSpreadsheetClient client)
        {
            _client = client;
        }

        public IAsyncEnumerable<string> GetCompanies()
            => _client.GetSheetNames().Where(s => !s.StartsWith("["));
    }
}
