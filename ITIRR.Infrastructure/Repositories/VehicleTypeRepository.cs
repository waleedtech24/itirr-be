using ITIRR.Core.Entities;
using ITIRR.Core.Interfaces;
using ITIRR.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITIRR.Infrastructure.Repositories
{
    public class VehicleTypeRepository : Repository<VehicleType>, IVehicleTypeRepository
    {
        public VehicleTypeRepository(ApplicationDbContext context, ILogService logService)
            : base(context, logService)
        {
        }

        public async Task<VehicleType?> GetByCodeAsync(string code)
        {
            return await _dbSet
                .FirstOrDefaultAsync(vt => vt.Code == code && !vt.IsDeleted);
        }

        public async Task<IEnumerable<VehicleType>> GetActiveTypesAsync()
        {
            return await _dbSet
                .Where(vt => vt.IsActive && !vt.IsDeleted)
                .OrderBy(vt => vt.DisplayOrder)
                .ToListAsync();
        }

        public async Task<VehicleType?> GetTypeWithCategoriesAsync(string code)
        {
            return await _dbSet
                .Include(vt => vt.VehicleCategories)
                .FirstOrDefaultAsync(vt => vt.Code == code && !vt.IsDeleted);
        }
    }
}