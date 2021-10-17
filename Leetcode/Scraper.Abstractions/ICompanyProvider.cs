using System.Collections.Generic;
using System.Threading.Tasks;

namespace Scraper.Abstractions
{
    public interface ICompanyProvider
    {
        Task<IEnumerable<string>> GetCompanies();
    }
}
