using ITIRR.Core.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ITIRR.Core.Interfaces
{
    public interface IVehicleCategoryService
    {
        Task<IEnumerable<VehicleCategory>> GetAllCategoriesAsync();
        Task<IEnumerable<VehicleCategory>> GetActiveCategoriesAsync();
        Task<IEnumerable<VehicleCategory>> GetCategoriesByTypeAsync(Guid vehicleTypeId);
        Task<VehicleCategory?> GetCategoryByIdAsync(Guid id);
        Task<VehicleCategory?> GetCategoryByCodeAsync(string code);
        Task<VehicleCategory> CreateCategoryAsync(VehicleCategory category);
        Task<bool> UpdateCategoryAsync(VehicleCategory category);
        Task<bool> DeleteCategoryAsync(Guid id);
    }
}