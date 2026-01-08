using ITIRR.Core.Entities;
using ITIRR.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ITIRR.Services.Services
{
    public class VehicleCategoryService : IVehicleCategoryService
    {
        private readonly IVehicleCategoryRepository _categoryRepository;
        private readonly ILogService _logService;

        public VehicleCategoryService(IVehicleCategoryRepository categoryRepository, ILogService logService)
        {
            _categoryRepository = categoryRepository;
            _logService = logService;
        }

        public async Task<IEnumerable<VehicleCategory>> GetAllCategoriesAsync()
        {
            return await _categoryRepository.GetAllAsync();
        }

        public async Task<IEnumerable<VehicleCategory>> GetActiveCategoriesAsync()
        {
            return await _categoryRepository.GetActiveCategoriesAsync();
        }

        public async Task<IEnumerable<VehicleCategory>> GetCategoriesByTypeAsync(Guid vehicleTypeId)
        {
            return await _categoryRepository.GetByVehicleTypeAsync(vehicleTypeId);
        }

        public async Task<VehicleCategory?> GetCategoryByIdAsync(Guid id)
        {
            return await _categoryRepository.GetByIdAsync(id);
        }

        public async Task<VehicleCategory?> GetCategoryByCodeAsync(string code)
        {
            return await _categoryRepository.GetByCodeAsync(code);
        }

        public async Task<VehicleCategory> CreateCategoryAsync(VehicleCategory category)
        {
            var result = await _categoryRepository.AddAsync(category);
            await _logService.LogInformationAsync("Admin", "Created", "VehicleCategory", result.Id, $"Category {category.Name} created");
            return result;
        }

        public async Task<bool> UpdateCategoryAsync(VehicleCategory category)
        {
            var existing = await _categoryRepository.GetByIdAsync(category.Id);
            if (existing == null)
                return false;

            await _categoryRepository.UpdateAsync(category);
            await _logService.LogInformationAsync("Admin", "Updated", "VehicleCategory", category.Id, $"Category {category.Name} updated");
            return true;
        }

        public async Task<bool> DeleteCategoryAsync(Guid id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null)
                return false;

            await _categoryRepository.SoftDeleteAsync(id);
            await _logService.LogWarningAsync("Admin", "Deleted", "VehicleCategory", id, $"Category {category.Name} deleted");
            return true;
        }
    }
}