using ITIRR.Core.Entities;
using ITIRR.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ITIRR.Services.Services
{
    public class VehicleBrandService : IVehicleBrandService
    {
        private readonly IVehicleBrandRepository _brandRepository;
        private readonly ILogService _logService;

        public VehicleBrandService(IVehicleBrandRepository brandRepository, ILogService logService)
        {
            _brandRepository = brandRepository;
            _logService = logService;
        }

        public async Task<IEnumerable<VehicleBrand>> GetAllBrandsAsync()
        {
            return await _brandRepository.GetAllAsync();
        }

        public async Task<IEnumerable<VehicleBrand>> GetActiveBrandsAsync()
        {
            return await _brandRepository.GetActiveBrandsAsync();
        }

        public async Task<IEnumerable<VehicleBrand>> GetBrandsByTypeAsync(Guid vehicleTypeId)
        {
            return await _brandRepository.GetByVehicleTypeAsync(vehicleTypeId);
        }

        public async Task<VehicleBrand?> GetBrandByIdAsync(Guid id)
        {
            return await _brandRepository.GetByIdAsync(id);
        }

        public async Task<VehicleBrand?> GetBrandWithModelsAsync(Guid id)
        {
            return await _brandRepository.GetBrandWithModelsAsync(id);
        }

        public async Task<VehicleBrand> CreateBrandAsync(VehicleBrand brand)
        {
            var result = await _brandRepository.AddAsync(brand);
            await _logService.LogInformationAsync("Admin", "Created", "VehicleBrand", result.Id, $"Brand {brand.Name} created");
            return result;
        }

        public async Task<bool> UpdateBrandAsync(VehicleBrand brand)
        {
            var existing = await _brandRepository.GetByIdAsync(brand.Id);
            if (existing == null)
                return false;

            await _brandRepository.UpdateAsync(brand);
            await _logService.LogInformationAsync("Admin", "Updated", "VehicleBrand", brand.Id, $"Brand {brand.Name} updated");
            return true;
        }

        public async Task<bool> DeleteBrandAsync(Guid id)
        {
            var brand = await _brandRepository.GetByIdAsync(id);
            if (brand == null)
                return false;

            await _brandRepository.SoftDeleteAsync(id);
            await _logService.LogWarningAsync("Admin", "Deleted", "VehicleBrand", id, $"Brand {brand.Name} deleted");
            return true;
        }
    }
}