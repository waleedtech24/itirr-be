using ITIRR.Core.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ITIRR.Core.Interfaces
{
    public interface IVehicleBrandService
    {
        Task<IEnumerable<VehicleBrand>> GetAllBrandsAsync();
        Task<IEnumerable<VehicleBrand>> GetActiveBrandsAsync();
        Task<IEnumerable<VehicleBrand>> GetBrandsByTypeAsync(Guid vehicleTypeId);
        Task<VehicleBrand?> GetBrandByIdAsync(Guid id);
        Task<VehicleBrand?> GetBrandWithModelsAsync(Guid id);
        Task<VehicleBrand> CreateBrandAsync(VehicleBrand brand);
        Task<bool> UpdateBrandAsync(VehicleBrand brand);
        Task<bool> DeleteBrandAsync(Guid id);
    }
}