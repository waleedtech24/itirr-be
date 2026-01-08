using ITIRR.Core.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ITIRR.Core.Interfaces
{
    public interface IVehicleTypeService
    {
        Task<IEnumerable<VehicleType>> GetAllVehicleTypesAsync();
        Task<IEnumerable<VehicleType>> GetActiveVehicleTypesAsync();
        Task<VehicleType?> GetVehicleTypeByIdAsync(Guid id);
        Task<VehicleType?> GetVehicleTypeByCodeAsync(string code);
        Task<VehicleType?> GetVehicleTypeWithCategoriesAsync(string code);
        Task<VehicleType> CreateVehicleTypeAsync(VehicleType vehicleType);
        Task<bool> UpdateVehicleTypeAsync(VehicleType vehicleType);
        Task<bool> DeleteVehicleTypeAsync(Guid id);
    }
}