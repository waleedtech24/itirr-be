using ITIRR.Core.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ITIRR.Core.Interfaces
{
    public interface IVehicleRepository : IRepository<Vehicle>
    {
        Task<IEnumerable<Vehicle>> GetByTypeAsync(Guid vehicleTypeId);
        Task<IEnumerable<Vehicle>> GetByCategoryAsync(Guid categoryId);
        Task<IEnumerable<Vehicle>> GetByOwnerAsync(string ownerId);
        Task<IEnumerable<Vehicle>> GetAvailableVehiclesAsync();
        Task<IEnumerable<Vehicle>> GetUnverifiedVehiclesAsync();
        Task<IEnumerable<Vehicle>> SearchVehiclesAsync(string searchTerm);
        Task<Vehicle?> GetVehicleWithDetailsAsync(Guid id);
    }
}