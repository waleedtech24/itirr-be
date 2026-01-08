using ITIRR.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ITIRR.Core.Interfaces
{
    public interface ICountryRepository : IRepository<Country>
    {
        Task<Country?> GetByCodeAsync(string code);
        Task<IEnumerable<Country>> GetActiveCountriesAsync();
        Task<bool> CodeExistsAsync(string code);
    }
}