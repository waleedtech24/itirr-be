using ITIRR.Core.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ITIRR.Core.Interfaces
{
    public interface IVehicleModelService
    {
        Task<IEnumerable<VehicleModel>> GetAllModelsAsync();
        Task<IEnumerable<VehicleModel>> GetActiveModelsAsync();
        Task<IEnumerable<VehicleModel>> GetModelsByBrandAsync(Guid brandId);
        Task<VehicleModel?> GetModelByIdAsync(Guid id);
        Task<VehicleModel?> GetModelWithBrandAsync(Guid id);
        Task<VehicleModel> CreateModelAsync(VehicleModel model);
        Task<bool> UpdateModelAsync(VehicleModel model);
        Task<bool> DeleteModelAsync(Guid id);
    }
}