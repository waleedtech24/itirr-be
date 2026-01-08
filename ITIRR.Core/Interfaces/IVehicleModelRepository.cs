using ITIRR.Core.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ITIRR.Core.Interfaces
{
    public interface IVehicleModelRepository : IRepository<VehicleModel>
    {
        Task<IEnumerable<VehicleModel>> GetByBrandAsync(Guid brandId);
        Task<IEnumerable<VehicleModel>> GetActiveModelsAsync();
        Task<VehicleModel?> GetModelWithBrandAsync(Guid id);
    }
}