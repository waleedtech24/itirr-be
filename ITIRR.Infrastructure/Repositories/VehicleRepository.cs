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
    public class VehicleRepository : Repository<Vehicle>, IVehicleRepository
    {
        public VehicleRepository(ApplicationDbContext context, ILogService logService)
           : base(context, logService)
        {
        }

        public async Task<IEnumerable<Vehicle>> GetByTypeAsync(Guid vehicleTypeId)
        {
            return await _dbSet
                .Include(v => v.VehicleType)
                .Include(v => v.VehicleCategory)
                .Include(v => v.VehicleBrand)
                .Include(v => v.VehicleModel)
                .Where(v => v.VehicleTypeId == vehicleTypeId && !v.IsDeleted)
                .OrderByDescending(v => v.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<Vehicle>> GetByCategoryAsync(Guid categoryId)
        {
            return await _dbSet
                .Include(v => v.VehicleType)
                .Include(v => v.VehicleCategory)
                .Include(v => v.VehicleBrand)
                .Include(v => v.VehicleModel)
                .Where(v => v.VehicleCategoryId == categoryId && !v.IsDeleted)
                .OrderByDescending(v => v.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<Vehicle>> GetByOwnerAsync(string ownerId)
        {
            return await _dbSet
                .Include(v => v.VehicleType)
                .Include(v => v.VehicleCategory)
                .Include(v => v.VehicleBrand)
                .Include(v => v.VehicleModel)
                .Where(v => v.OwnerId == ownerId && !v.IsDeleted)
                .OrderByDescending(v => v.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<Vehicle>> GetAvailableVehiclesAsync()
        {
            return await _dbSet
                .Include(v => v.VehicleType)
                .Include(v => v.VehicleCategory)
                .Include(v => v.VehicleBrand)
                .Include(v => v.VehicleModel)
                .Where(v => v.IsAvailable && v.IsActive && v.IsVerified && !v.IsDeleted)
                .OrderByDescending(v => v.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<Vehicle>> GetUnverifiedVehiclesAsync()
        {
            return await _dbSet
                .Include(v => v.VehicleType)
                .Include(v => v.VehicleCategory)
                .Include(v => v.VehicleBrand)
                .Include(v => v.VehicleModel)
                .Include(v => v.Owner)
                .Where(v => !v.IsVerified && v.IsActive && !v.IsDeleted)
                .OrderBy(v => v.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<Vehicle>> SearchVehiclesAsync(string searchTerm)
        {
            return await _dbSet
                .Include(v => v.VehicleType)
                .Include(v => v.VehicleCategory)
                .Include(v => v.VehicleBrand)
                .Include(v => v.VehicleModel)
                .Where(v => (v.Title.Contains(searchTerm) ||
                            v.VehicleBrand.Name.Contains(searchTerm) ||
                            v.VehicleModel.Name.Contains(searchTerm)) &&
                            !v.IsDeleted)
                .OrderByDescending(v => v.CreatedAt)
                .Take(50)
                .ToListAsync();
        }

        public async Task<Vehicle?> GetVehicleWithDetailsAsync(Guid id)
        {
            return await _dbSet
                .Include(v => v.VehicleType)
                .Include(v => v.VehicleCategory)
                .Include(v => v.VehicleBrand)
                .Include(v => v.VehicleModel)
                .Include(v => v.Owner)
                .Include(v => v.VehicleLocations).ThenInclude(vl => vl.City)
                .Include(v => v.VehicleMedia)
                .Include(v => v.Documents).ThenInclude(d => d.DocumentType)
                .Include(v => v.VehicleFeatureMappings).ThenInclude(vfm => vfm.VehicleFeature)
                .FirstOrDefaultAsync(v => v.Id == id && !v.IsDeleted);
        }
    }
}