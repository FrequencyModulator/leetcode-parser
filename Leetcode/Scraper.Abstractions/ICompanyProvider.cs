using System.Collections.Generic;

namespace Scraper.Abstractions
{
    public interface ICompanyProvider
    {
        IAsyncEnumerable<string> GetCompanies();
    }
}
