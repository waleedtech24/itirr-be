using ITIRR.Core.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ITIRR.Core.Interfaces
{
    public interface IVehicleService
    {
        Task<IEnumerable<Vehicle>> GetAllVehiclesAsync();
        Task<IEnumerable<Vehicle>> GetVehiclesByTypeAsync(Guid vehicleTypeId);
        Task<IEnumerable<Vehicle>> GetVehiclesByCategoryAsync(Guid categoryId);
        Task<IEnumerable<Vehicle>> GetVehiclesByOwnerAsync(string ownerId);
        Task<IEnumerable<Vehicle>> GetAvailableVehiclesAsync();
        Task<IEnumerable<Vehicle>> GetUnverifiedVehiclesAsync();
        Task<IEnumerable<Vehicle>> SearchVehiclesAsync(string searchTerm);
        Task<Vehicle?> GetVehicleByIdAsync(Guid id);
        Task<Vehicle?> GetVehicleWithDetailsAsync(Guid id);
        Task<Vehicle> CreateVehicleAsync(Vehicle vehicle);
        Task<bool> UpdateVehicleAsync(Vehicle vehicle);
        Task<bool> DeleteVehicleAsync(Guid id);
        Task<bool> VerifyVehicleAsync(Guid id, string verifiedBy);
        Task<bool> ToggleVehicleAvailabilityAsync(Guid id);
    }
}