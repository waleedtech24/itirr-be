using ITIRR.Core.Entities;
using ITIRR.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ITIRR.Services.Services
{
    public class VehicleModelService : IVehicleModelService
    {
        private readonly IVehicleModelRepository _modelRepository;
        private readonly ILogService _logService;

        public VehicleModelService(IVehicleModelRepository modelRepository, ILogService logService)
        {
            _modelRepository = modelRepository;
            _logService = logService;
        }

        public async Task<IEnumerable<VehicleModel>> GetAllModelsAsync()
        {
            return await _modelRepository.GetAllAsync();
        }

        public async Task<IEnumerable<VehicleModel>> GetActiveModelsAsync()
        {
            return await _modelRepository.GetActiveModelsAsync();
        }

        public async Task<IEnumerable<VehicleModel>> GetModelsByBrandAsync(Guid brandId)
        {
            return await _modelRepository.GetByBrandAsync(brandId);
        }

        public async Task<VehicleModel?> GetModelByIdAsync(Guid id)
        {
            return await _modelRepository.GetByIdAsync(id);
        }

        public async Task<VehicleModel?> GetModelWithBrandAsync(Guid id)
        {
            return await _modelRepository.GetModelWithBrandAsync(id);
        }

        public async Task<VehicleModel> CreateModelAsync(VehicleModel model)
        {
            var result = await _modelRepository.AddAsync(model);
            await _logService.LogInformationAsync("Admin", "Created", "VehicleModel", result.Id, $"Model {model.Name} created");
            return result;
        }

        public async Task<bool> UpdateModelAsync(VehicleModel model)
        {
            var existing = await _modelRepository.GetByIdAsync(model.Id);
            if (existing == null)
                return false;

            await _modelRepository.UpdateAsync(model);
            await _logService.LogInformationAsync("Admin", "Updated", "VehicleModel", model.Id, $"Model {model.Name} updated");
            return true;
        }

        public async Task<bool> DeleteModelAsync(Guid id)
        {
            var model = await _modelRepository.GetByIdAsync(id);
            if (model == null)
                return false;

            await _modelRepository.SoftDeleteAsync(id);
            await _logService.LogWarningAsync("Admin", "Deleted", "VehicleModel", id, $"Model {model.Name} deleted");
            return true;
        }
    }
}