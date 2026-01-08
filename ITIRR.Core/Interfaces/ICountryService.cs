using ITIRR.Core.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ITIRR.Core.Interfaces
{
    public interface ICountryService
    {
        Task<IEnumerable<Country>> GetAllCountriesAsync();
        Task<IEnumerable<Country>> GetActiveCountriesAsync();
        Task<Country?> GetCountryByIdAsync(Guid id);
        Task<Country?> GetCountryByCodeAsync(string code);
        Task<Country> CreateCountryAsync(Country country);
        Task<bool> UpdateCountryAsync(Country country);
        Task<bool> DeleteCountryAsync(Guid id);
        Task<bool> CountryCodeExistsAsync(string code);
    }
}