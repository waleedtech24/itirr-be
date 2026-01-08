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
    public class VehicleCategoryRepository : Repository<VehicleCategory>, IVehicleCategoryRepository
    {
        public VehicleCategoryRepository(ApplicationDbContext context, ILogService logService)
            : base(context, logService)
        {
        }

        public async Task<IEnumerable<VehicleCategory>> GetByVehicleTypeAsync(Guid vehicleTypeId)
        {
            return await _dbSet
                .Include(vc => vc.VehicleType)
                .Where(vc => vc.VehicleTypeId == vehicleTypeId && !vc.IsDeleted)
                .OrderBy(vc => vc.DisplayOrder)
                .ToListAsync();
        }

        public async Task<IEnumerable<VehicleCategory>> GetActiveCategoriesAsync()
        {
            return await _dbSet
                .Include(vc => vc.VehicleType)
                .Where(vc => vc.IsActive && !vc.IsDeleted)
                .OrderBy(vc => vc.DisplayOrder)
                .ToListAsync();
        }

        public async Task<VehicleCategory?> GetByCodeAsync(string code)
        {
            return await _dbSet
                .Include(vc => vc.VehicleType)
                .FirstOrDefaultAsync(vc => vc.Code == code && !vc.IsDeleted);
        }
    }
}