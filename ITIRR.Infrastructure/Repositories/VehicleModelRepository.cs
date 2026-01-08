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
    public class VehicleModelRepository : Repository<VehicleModel>, IVehicleModelRepository
    {
        public VehicleModelRepository(ApplicationDbContext context, ILogService logService)
            : base(context, logService)
        {
        }

        public async Task<IEnumerable<VehicleModel>> GetByBrandAsync(Guid brandId)
        {
            return await _dbSet
                .Include(vm => vm.VehicleBrand)
                .Where(vm => vm.VehicleBrandId == brandId && !vm.IsDeleted)
                .OrderBy(vm => vm.Name)
                .ToListAsync();
        }

        public async Task<IEnumerable<VehicleModel>> GetActiveModelsAsync()
        {
            return await _dbSet
                .Include(vm => vm.VehicleBrand)
                .Where(vm => vm.IsActive && !vm.IsDeleted)
                .OrderBy(vm => vm.Name)
                .ToListAsync();
        }

        public async Task<VehicleModel?> GetModelWithBrandAsync(Guid id)
        {
            return await _dbSet
                .Include(vm => vm.VehicleBrand)
                    .ThenInclude(vb => vb.VehicleType)
                .FirstOrDefaultAsync(vm => vm.Id == id && !vm.IsDeleted);
        }
    }
}