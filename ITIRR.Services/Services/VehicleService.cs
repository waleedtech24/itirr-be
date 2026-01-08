using ITIRR.Core.Entities;
using ITIRR.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ITIRR.Services.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly ILogService _logService;

        public VehicleService(IVehicleRepository vehicleRepository, ILogService logService)
        {
            _vehicleRepository = vehicleRepository;
            _logService = logService;
        }

        public async Task<IEnumerable<Vehicle>> GetAllVehiclesAsync()
        {
            return await _vehicleRepository.GetAllAsync();
        }

        public async Task<IEnumerable<Vehicle>> GetVehiclesByTypeAsync(Guid vehicleTypeId)
        {
            return await _vehicleRepository.GetByTypeAsync(vehicleTypeId);
        }

        public async Task<IEnumerable<Vehicle>> GetVehiclesByCategoryAsync(Guid categoryId)
        {
            return await _vehicleRepository.GetByCategoryAsync(categoryId);
        }

        public async Task<IEnumerable<Vehicle>> GetVehiclesByOwnerAsync(string ownerId)
        {
            return await _vehicleRepository.GetByOwnerAsync(ownerId);
        }

        public async Task<IEnumerable<Vehicle>> GetAvailableVehiclesAsync()
        {
            return await _vehicleRepository.GetAvailableVehiclesAsync();
        }

        public async Task<IEnumerable<Vehicle>> GetUnverifiedVehiclesAsync()
        {
            return await _vehicleRepository.GetUnverifiedVehiclesAsync();
        }

        public async Task<IEnumerable<Vehicle>> SearchVehiclesAsync(string searchTerm)
        {
            return await _vehicleRepository.SearchVehiclesAsync(searchTerm);
        }

        public async Task<Vehicle?> GetVehicleByIdAsync(Guid id)
        {
            return await _vehicleRepository.GetByIdAsync(id);
        }

        public async Task<Vehicle?> GetVehicleWithDetailsAsync(Guid id)
        {
            return await _vehicleRepository.GetVehicleWithDetailsAsync(id);
        }

        public async Task<Vehicle> CreateVehicleAsync(Vehicle vehicle)
        {
            vehicle.IsVerified = false;
            vehicle.IsActive = true;

            var result = await _vehicleRepository.AddAsync(vehicle);
            await _logService.LogInformationAsync(
                vehicle.OwnerId,
                "Created",
                "Vehicle",
                result.Id,
                $"Vehicle {vehicle.Title} created by owner"
            );
            return result;
        }

        public async Task<bool> UpdateVehicleAsync(Vehicle vehicle)
        {
            var existing = await _vehicleRepository.GetByIdAsync(vehicle.Id);
            if (existing == null)
                return false;

            await _vehicleRepository.UpdateAsync(vehicle);
            await _logService.LogInformationAsync(
                "Admin",
                "Updated",
                "Vehicle",
                vehicle.Id,
                $"Vehicle {vehicle.Title} updated"
            );
            return true;
        }

        public async Task<bool> DeleteVehicleAsync(Guid id)
        {
            var vehicle = await _vehicleRepository.GetByIdAsync(id);
            if (vehicle == null)
                return false;

            await _vehicleRepository.SoftDeleteAsync(id);
            await _logService.LogWarningAsync(
                "Admin",
                "Deleted",
                "Vehicle",
                id,
                $"Vehicle {vehicle.Title} deleted"
            );
            return true;
        }

        public async Task<bool> VerifyVehicleAsync(Guid id, string verifiedBy)
        {
            var vehicle = await _vehicleRepository.GetByIdAsync(id);
            if (vehicle == null)
                return false;

            vehicle.IsVerified = true;
            vehicle.VerifiedAt = DateTime.UtcNow;
            vehicle.VerifiedBy = verifiedBy;

            await _vehicleRepository.UpdateAsync(vehicle);
            await _logService.LogInformationAsync(
                verifiedBy,
                "Verified",
                "Vehicle",
                id,
                $"Vehicle {vehicle.Title} verified by admin"
            );
            return true;
        }

        public async Task<bool> ToggleVehicleAvailabilityAsync(Guid id)
        {
            var vehicle = await _vehicleRepository.GetByIdAsync(id);
            if (vehicle == null)
                return false;

            vehicle.IsAvailable = !vehicle.IsAvailable;
            await _vehicleRepository.UpdateAsync(vehicle);

            await _logService.LogInformationAsync(
                vehicle.OwnerId,
                "AvailabilityToggled",
                "Vehicle",
                id,
                $"Vehicle {vehicle.Title} availability set to {vehicle.IsAvailable}"
            );
            return true;
        }
    }
}