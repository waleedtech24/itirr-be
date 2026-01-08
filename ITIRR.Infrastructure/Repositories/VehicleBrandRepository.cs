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
    public class VehicleBrandRepository : Repository<VehicleBrand>, IVehicleBrandRepository
    {
        public VehicleBrandRepository(ApplicationDbContext context, ILogService logService)
            : base(context, logService)
        {
        }

        public async Task<IEnumerable<VehicleBrand>> GetByVehicleTypeAsync(Guid vehicleTypeId)
        {
            return await _dbSet
                .Include(vb => vb.VehicleType)
                .Where(vb => vb.VehicleTypeId == vehicleTypeId && !vb.IsDeleted)
                .OrderBy(vb => vb.Name)
                .ToListAsync();
        }

        public async Task<IEnumerable<VehicleBrand>> GetActiveBrandsAsync()
        {
            return await _dbSet
                .Include(vb => vb.VehicleType)
                .Where(vb => vb.IsActive && !vb.IsDeleted)
                .OrderBy(vb => vb.Name)
                .ToListAsync();
        }

        public async Task<VehicleBrand?> GetBrandWithModelsAsync(Guid id)
        {
            return await _dbSet
                .Include(vb => vb.VehicleType)
                .Include(vb => vb.VehicleModels)
                .FirstOrDefaultAsync(vb => vb.Id == id && !vb.IsDeleted);
        }
    }
}