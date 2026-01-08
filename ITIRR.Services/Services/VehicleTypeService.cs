using ITIRR.Core.Entities;
using ITIRR.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ITIRR.Services.Services
{
    public class VehicleTypeService : IVehicleTypeService
    {
        private readonly IVehicleTypeRepository _vehicleTypeRepository;
        private readonly ILogService _logService;

        public VehicleTypeService(IVehicleTypeRepository vehicleTypeRepository, ILogService logService)
        {
            _vehicleTypeRepository = vehicleTypeRepository;
            _logService = logService;
        }

        public async Task<IEnumerable<VehicleType>> GetAllVehicleTypesAsync()
        {
            return await _vehicleTypeRepository.GetAllAsync();
        }

        public async Task<IEnumerable<VehicleType>> GetActiveVehicleTypesAsync()
        {
            return await _vehicleTypeRepository.GetActiveTypesAsync();
        }

        public async Task<VehicleType?> GetVehicleTypeByIdAsync(Guid id)
        {
            return await _vehicleTypeRepository.GetByIdAsync(id);
        }

        public async Task<VehicleType?> GetVehicleTypeByCodeAsync(string code)
        {
            return await _vehicleTypeRepository.GetByCodeAsync(code);
        }

        public async Task<VehicleType?> GetVehicleTypeWithCategoriesAsync(string code)
        {
            return await _vehicleTypeRepository.GetTypeWithCategoriesAsync(code);
        }

        public async Task<VehicleType> CreateVehicleTypeAsync(VehicleType vehicleType)
        {
            var result = await _vehicleTypeRepository.AddAsync(vehicleType);
            await _logService.LogInformationAsync("Admin", "Created", "VehicleType", result.Id, $"Vehicle Type {vehicleType.Name} created");
            return result;
        }

        public async Task<bool> UpdateVehicleTypeAsync(VehicleType vehicleType)
        {
            var existing = await _vehicleTypeRepository.GetByIdAsync(vehicleType.Id);
            if (existing == null)
                return false;

            await _vehicleTypeRepository.UpdateAsync(vehicleType);
            await _logService.LogInformationAsync("Admin", "Updated", "VehicleType", vehicleType.Id, $"Vehicle Type {vehicleType.Name} updated");
            return true;
        }

        public async Task<bool> DeleteVehicleTypeAsync(Guid id)
        {
            var vehicleType = await _vehicleTypeRepository.GetByIdAsync(id);
            if (vehicleType == null)
                return false;

            await _vehicleTypeRepository.SoftDeleteAsync(id);
            await _logService.LogWarningAsync("Admin", "Deleted", "VehicleType", id, $"Vehicle Type {vehicleType.Name} deleted");
            return true;
        }
    }
}