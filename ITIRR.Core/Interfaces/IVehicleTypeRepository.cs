using ITIRR.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ITIRR.Core.Interfaces
{
    public interface IVehicleTypeRepository : IRepository<VehicleType>
    {
        Task<IEnumerable<VehicleType>> GetActiveVehicleTypesAsync();
        Task<VehicleType?> GetByCodeAsync(string code);
        Task<IEnumerable<VehicleType>> GetActiveTypesAsync();
        Task<VehicleType?> GetTypeWithCategoriesAsync(string code);
        Task<VehicleType?> GetVehicleTypeWithCategoriesAsync(string code);

    }
}