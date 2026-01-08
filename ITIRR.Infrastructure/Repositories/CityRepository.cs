using ITIRR.Core.Entities;
using ITIRR.Core.Interfaces;
using ITIRR.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITIRR.Infrastructure.Repositories
{
    public class CityRepository : Repository<City>, ICityRepository
    {
        public CityRepository(ApplicationDbContext context, ILogService logService)
            : base(context, logService)
        {
        }

        public async Task<IEnumerable<City>> GetByCountryIdAsync(Guid countryId)
        {
            return await _dbSet
                .Include(c => c.Country)
                .Where(c => c.CountryId == countryId && !c.IsDeleted)
                .OrderBy(c => c.Name)
                .ToListAsync();
        }

        public async Task<IEnumerable<City>> GetByStateIdAsync(Guid stateId)
        {
            return await _dbSet
                .Include(c => c.State)
                .Where(c => c.StateId == stateId && !c.IsDeleted)
                .OrderBy(c => c.Name)
                .ToListAsync();
        }

        public async Task<IEnumerable<City>> GetActiveCitiesAsync()
        {
            return await _dbSet
                .Include(c => c.Country)
                .Where(c => c.IsActive && !c.IsDeleted)
                .OrderBy(c => c.Name)
                .ToListAsync();
        }

        public async Task<IEnumerable<City>> SearchCitiesAsync(string searchTerm)
        {
            return await _dbSet
                .Include(c => c.Country)
                .Where(c => c.Name.Contains(searchTerm) && !c.IsDeleted)
                .OrderBy(c => c.Name)
                .Take(20)
                .ToListAsync();
        }
    }
}