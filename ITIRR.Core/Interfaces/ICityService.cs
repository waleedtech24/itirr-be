using ITIRR.Core.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ITIRR.Core.Interfaces
{
    public interface ICityService
    {
        Task<IEnumerable<City>> GetAllCitiesAsync();
        Task<IEnumerable<City>> GetActiveCitiesAsync();
        Task<IEnumerable<City>> GetCitiesByCountryAsync(Guid countryId);
        Task<IEnumerable<City>> GetCitiesByStateAsync(Guid stateId);
        Task<IEnumerable<City>> SearchCitiesAsync(string searchTerm);
        Task<City?> GetCityByIdAsync(Guid id);
        Task<City> CreateCityAsync(City city);
        Task<bool> UpdateCityAsync(City city);
        Task<bool> DeleteCityAsync(Guid id);
    }
}