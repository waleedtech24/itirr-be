using ITIRR.Core.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ITIRR.Core.Interfaces
{
    public interface ICityRepository : IRepository<City>
    {
        Task<IEnumerable<City>> GetByCountryIdAsync(Guid countryId);
        Task<IEnumerable<City>> GetByStateIdAsync(Guid stateId);
        Task<IEnumerable<City>> GetActiveCitiesAsync();
        Task<IEnumerable<City>> SearchCitiesAsync(string searchTerm);
    }
}