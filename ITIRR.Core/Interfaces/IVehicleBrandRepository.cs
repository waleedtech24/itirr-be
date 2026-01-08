using ITIRR.Core.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ITIRR.Core.Interfaces
{
    public interface IVehicleBrandRepository : IRepository<VehicleBrand>
    {
        Task<IEnumerable<VehicleBrand>> GetByVehicleTypeAsync(Guid vehicleTypeId);
        Task<IEnumerable<VehicleBrand>> GetActiveBrandsAsync();
        Task<VehicleBrand?> GetBrandWithModelsAsync(Guid id);
    }
}