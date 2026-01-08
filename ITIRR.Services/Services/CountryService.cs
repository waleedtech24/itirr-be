using ITIRR.Core;
using ITIRR.Core.Entities;
using ITIRR.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ITIRR.Services.Services
{
    public class CountryService : ICountryService
    {
        private readonly ICountryRepository _countryRepository;
        private readonly ILogService _logService;

        public CountryService(ICountryRepository countryRepository, ILogService logService)
        {
            _countryRepository = countryRepository;
            _logService = logService;
        }

        public async Task<IEnumerable<Country>> GetAllCountriesAsync()
        {
            return await _countryRepository.GetAllAsync();
        }

        public async Task<IEnumerable<Country>> GetActiveCountriesAsync()
        {
            return await _countryRepository.GetActiveCountriesAsync();
        }

        public async Task<Country?> GetCountryByIdAsync(Guid id)
        {
            return await _countryRepository.GetByIdAsync(id);
        }

        public async Task<Country?> GetCountryByCodeAsync(string code)
        {
            return await _countryRepository.GetByCodeAsync(code);
        }

        public async Task<Country> CreateCountryAsync(Country country)
        {
            // Validation
            if (await _countryRepository.CodeExistsAsync(country.Code))
            {
                throw new InvalidOperationException($"Country with code {country.Code} already exists.");
            }

            var result = await _countryRepository.AddAsync(country);
            await _logService.LogInformationAsync("Admin", "Created", "Country", result.Id, $"Country {country.Name} created");
            return result;
        }

        public async Task<bool> UpdateCountryAsync(Country country)
        {
            var existing = await _countryRepository.GetByIdAsync(country.Id);
            if (existing == null)
                return false;

            await _countryRepository.UpdateAsync(country);
            await _logService.LogInformationAsync("Admin", "Updated", "Country", country.Id, $"Country {country.Name} updated");
            return true;
        }

        public async Task<bool> DeleteCountryAsync(Guid id)
        {
            var country = await _countryRepository.GetByIdAsync(id);
            if (country == null)
                return false;

            await _countryRepository.SoftDeleteAsync(id);
            await _logService.LogWarningAsync("Admin", "Deleted", "Country", id, $"Country {country.Name} deleted");
            return true;
        }

        public async Task<bool> CountryCodeExistsAsync(string code)
        {
            return await _countryRepository.CodeExistsAsync(code);
        }
    }
}