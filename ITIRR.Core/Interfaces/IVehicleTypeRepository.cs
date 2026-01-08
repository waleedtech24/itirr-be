using ITIRR.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ITIRR.Core.Interfaces
{
    public interface IVehicleTypeRepository : IRepository<VehicleType>
    {
        Task<VehicleType?> GetByCodeAsync(string code);
        Task<IEnumerable<VehicleType>> GetActiveTypesAsync();
        Task<VehicleType?> GetTypeWithCategoriesAsync(string code);
    }
}