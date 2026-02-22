using ITIRR.Core.Entities;

namespace ITIRR.Core.Interfaces
{
    public interface IVehicleListingRepository : IRepository<VehicleListing>
    {
        Task<VehicleListing?> GetByIdWithMediaAsync(Guid id);
        Task<IEnumerable<VehicleListing>> GetByOwnerAsync(string ownerId);
    }
}