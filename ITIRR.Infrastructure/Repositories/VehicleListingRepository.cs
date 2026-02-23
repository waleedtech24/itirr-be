using ITIRR.Core.Entities;
using ITIRR.Core.Interfaces;
using ITIRR.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ITIRR.Infrastructure.Repositories
{
    public class VehicleListingRepository : Repository<VehicleListing>, IVehicleListingRepository
    {
        public VehicleListingRepository(ApplicationDbContext context, ILogService logService) : base(context, logService) { }

        public async Task<VehicleListing?> GetByIdWithMediaAsync(Guid id)
        {
            return await _context.VehicleListings
                .Include(v => v.Media)
                .FirstOrDefaultAsync(v => v.Id == id && !v.IsDeleted);
        }

        public async Task<IEnumerable<VehicleListing>> GetByOwnerAsync(string ownerId)
        {
            return await _context.VehicleListings
                .Include(v => v.Media)
                .Where(v => v.OwnerId == ownerId && !v.IsDeleted)
                .OrderByDescending(v => v.CreatedAt)
                .ToListAsync();
        }
    }
}