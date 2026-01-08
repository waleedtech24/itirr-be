using ITIRR.Core.Entities;
using ITIRR.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ITIRR.Services.Services
{
    public class CityService : ICityService
    {
        private readonly ICityRepository _cityRepository;
        private readonly ILogService _logService;

        public CityService(ICityRepository cityRepository, ILogService logService)
        {
            _cityRepository = cityRepository;
            _logService = logService;
        }

        public async Task<IEnumerable<City>> GetAllCitiesAsync()
        {
            return await _cityRepository.GetAllAsync();
        }

        public async Task<IEnumerable<City>> GetActiveCitiesAsync()
        {
            return await _cityRepository.GetActiveCitiesAsync();
        }

        public async Task<IEnumerable<City>> GetCitiesByCountryAsync(Guid countryId)
        {
            return await _cityRepository.GetByCountryIdAsync(countryId);
        }

        public async Task<IEnumerable<City>> GetCitiesByStateAsync(Guid stateId)
        {
            return await _cityRepository.GetByStateIdAsync(stateId);
        }

        public async Task<IEnumerable<City>> SearchCitiesAsync(string searchTerm)
        {
            return await _cityRepository.SearchCitiesAsync(searchTerm);
        }

        public async Task<City?> GetCityByIdAsync(Guid id)
        {
            return await _cityRepository.GetByIdAsync(id);
        }

        public async Task<City> CreateCityAsync(City city)
        {
            var result = await _cityRepository.AddAsync(city);
            await _logService.LogInformationAsync("Admin", "Created", "City", result.Id, $"City {city.Name} created");
            return result;
        }

        public async Task<bool> UpdateCityAsync(City city)
        {
            var existing = await _cityRepository.GetByIdAsync(city.Id);
            if (existing == null)
                return false;

            await _cityRepository.UpdateAsync(city);
            await _logService.LogInformationAsync("Admin", "Updated", "City", city.Id, $"City {city.Name} updated");
            return true;
        }

        public async Task<bool> DeleteCityAsync(Guid id)
        {
            var city = await _cityRepository.GetByIdAsync(id);
            if (city == null)
                return false;

            await _cityRepository.SoftDeleteAsync(id);
            await _logService.LogWarningAsync("Admin", "Deleted", "City", id, $"City {city.Name} deleted");
            return true;
        }
    }
}