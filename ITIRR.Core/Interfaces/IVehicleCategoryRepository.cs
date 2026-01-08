using ITIRR.Core.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ITIRR.Core.Interfaces
{
    public interface IVehicleCategoryRepository : IRepository<VehicleCategory>
    {
        Task<IEnumerable<VehicleCategory>> GetByVehicleTypeAsync(Guid vehicleTypeId);
        Task<IEnumerable<VehicleCategory>> GetActiveCategoriesAsync();
        Task<VehicleCategory?> GetByCodeAsync(string code);
    }
}