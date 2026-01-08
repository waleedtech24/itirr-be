using ITIRR.Core.Entities;
using ITIRR.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ITIRR.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class VehicleFeaturesController : Controller
    {
        private readonly IRepository<VehicleFeature> _featureRepository;
        private readonly IVehicleTypeRepository _vehicleTypeRepository;
        private readonly ILogService _logService;

        public VehicleFeaturesController(
            IRepository<VehicleFeature> featureRepository,
            IVehicleTypeRepository vehicleTypeRepository,
            ILogService logService)
        {
            _featureRepository = featureRepository;
            _vehicleTypeRepository = vehicleTypeRepository;
            _logService = logService;
        }

        // GET: VehicleFeatures
        public async Task<IActionResult> Index(Guid? vehicleTypeId, string searchTerm)
        {
            try
            {
                var features = await _featureRepository.GetAllAsync();

                if (vehicleTypeId.HasValue && vehicleTypeId.Value != Guid.Empty)
                {
                    features = features.Where(f => f.VehicleTypeId == vehicleTypeId.Value).ToList();
                }

                if (!string.IsNullOrWhiteSpace(searchTerm))
                {
                    features = features.Where(f =>
                        f.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                        (f.Description != null && f.Description.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                    ).ToList();
                }

                var vehicleTypes = await _vehicleTypeRepository.GetAllAsync();
                ViewBag.VehicleTypes = new SelectList(vehicleTypes, "Id", "Name", vehicleTypeId);
                ViewBag.SearchTerm = searchTerm;
                ViewBag.SelectedVehicleTypeId = vehicleTypeId;

                return View(features.OrderBy(f => f.VehicleType.Name).ThenBy(f => f.Name));
            }
            catch (Exception ex)
            {
                await _logService.LogErrorAsync(
                    userId: User.Identity?.Name ?? "System",
                    action: "INDEX_ERROR",
                    entityType: "VehicleFeature",
                    entityId: null,
                    errorMessage: ex.Message,
                    stackTrace: ex.StackTrace
                );

                TempData["Error"] = "An error occurred while loading vehicle features.";
                return View(new List<VehicleFeature>());
            }
        }

        // GET: VehicleFeatures/Create
        public async Task<IActionResult> Create()
        {
            try
            {
                await LoadVehicleTypes();
                return View();
            }
            catch (Exception ex)
            {
                await _logService.LogErrorAsync(
                    userId: User.Identity?.Name ?? "System",
                    action: "CREATE_PAGE_ERROR",
                    entityType: "VehicleFeature",
                    entityId: null,
                    errorMessage: ex.Message
                );

                TempData["Error"] = "An error occurred while loading the create page.";
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: VehicleFeatures/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VehicleFeature feature)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var existingFeatures = await _featureRepository.GetAllAsync();
                    var duplicate = existingFeatures.FirstOrDefault(f =>
                        f.Name.Equals(feature.Name, StringComparison.OrdinalIgnoreCase) &&
                        f.VehicleTypeId == feature.VehicleTypeId
                    );

                    if (duplicate != null)
                    {
                        ModelState.AddModelError("Name", "A feature with this name already exists for the selected vehicle type.");
                        await LoadVehicleTypes(feature.VehicleTypeId);
                        return View(feature);
                    }

                    feature.Id = Guid.NewGuid();
                    feature.CreatedAt = DateTime.UtcNow;
                    feature.UpdatedAt = DateTime.UtcNow;

                    await _featureRepository.AddAsync(feature);

                    await _logService.LogInformationAsync(
                        userId: User.Identity?.Name ?? "System",
                        action: "CREATE",
                        entityType: "VehicleFeature",
                        entityId: feature.Id,
                        notes: $"Created feature: {feature.Name}"
                    );

                    TempData["Success"] = $"Vehicle feature '{feature.Name}' has been created successfully.";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    await _logService.LogErrorAsync(
                        userId: User.Identity?.Name ?? "System",
                        action: "CREATE_ERROR",
                        entityType: "VehicleFeature",
                        entityId: null,
                        errorMessage: ex.Message,
                        stackTrace: ex.StackTrace
                    );

                    ModelState.AddModelError("", "An error occurred while creating the feature.");
                }
            }

            await LoadVehicleTypes(feature.VehicleTypeId);
            return View(feature);
        }

        // GET: VehicleFeatures/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            try
            {
                var feature = await _featureRepository.GetByIdAsync(id);
                if (feature == null)
                {
                    TempData["Error"] = "Vehicle feature not found.";
                    return RedirectToAction(nameof(Index));
                }

                await LoadVehicleTypes(feature.VehicleTypeId);
                return View(feature);
            }
            catch (Exception ex)
            {
                await _logService.LogErrorAsync(
                    userId: User.Identity?.Name ?? "System",
                    action: "EDIT_PAGE_ERROR",
                    entityType: "VehicleFeature",
                    entityId: id,
                    errorMessage: ex.Message
                );

                TempData["Error"] = "An error occurred while loading the edit page.";
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: VehicleFeatures/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, VehicleFeature feature)
        {
            if (id != feature.Id)
            {
                TempData["Error"] = "Invalid feature ID.";
                return RedirectToAction(nameof(Index));
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var existingFeature = await _featureRepository.GetByIdAsync(id);
                    if (existingFeature == null)
                    {
                        TempData["Error"] = "Vehicle feature not found.";
                        return RedirectToAction(nameof(Index));
                    }

                    var allFeatures = await _featureRepository.GetAllAsync();
                    var duplicate = allFeatures.FirstOrDefault(f =>
                        f.Id != id &&
                        f.Name.Equals(feature.Name, StringComparison.OrdinalIgnoreCase) &&
                        f.VehicleTypeId == feature.VehicleTypeId
                    );

                    if (duplicate != null)
                    {
                        ModelState.AddModelError("Name", "A feature with this name already exists for the selected vehicle type.");
                        await LoadVehicleTypes(feature.VehicleTypeId);
                        return View(feature);
                    }

                    string oldValue = $"Name: {existingFeature.Name}, Active: {existingFeature.IsActive}";

                    existingFeature.Name = feature.Name;
                    existingFeature.Code = feature.Code;
                    existingFeature.Description = feature.Description;
                    existingFeature.IconUrl = feature.IconUrl;
                    existingFeature.VehicleTypeId = feature.VehicleTypeId;
                    existingFeature.IsActive = feature.IsActive;
                    existingFeature.UpdatedAt = DateTime.UtcNow;

                    await _featureRepository.UpdateAsync(existingFeature);

                    await _logService.LogEntityChangeAsync(
                        userId: User.Identity?.Name ?? "System",
                        action: "UPDATE",
                        entityType: "VehicleFeature",
                        entityId: feature.Id,
                        oldValue: oldValue,
                        newValue: $"Name: {feature.Name}, Active: {feature.IsActive}"
                    );

                    TempData["Success"] = $"Vehicle feature '{feature.Name}' has been updated successfully.";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    await _logService.LogErrorAsync(
                        userId: User.Identity?.Name ?? "System",
                        action: "UPDATE_ERROR",
                        entityType: "VehicleFeature",
                        entityId: id,
                        errorMessage: ex.Message,
                        stackTrace: ex.StackTrace
                    );

                    ModelState.AddModelError("", "An error occurred while updating the feature.");
                }
            }

            await LoadVehicleTypes(feature.VehicleTypeId);
            return View(feature);
        }

        // POST: VehicleFeatures/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var feature = await _featureRepository.GetByIdAsync(id);
                if (feature == null)
                {
                    return Json(new { success = false, message = "Feature not found" });
                }

                string featureName = feature.Name;

                await _featureRepository.DeleteAsync(feature);

                await _logService.LogInformationAsync(
                    userId: User.Identity?.Name ?? "System",
                    action: "DELETE",
                    entityType: "VehicleFeature",
                    entityId: id,
                    notes: $"Deleted feature: {featureName}"
                );

                return Json(new { success = true, message = "Deleted!" });
            }
            catch (Exception ex)
            {
                await _logService.LogErrorAsync(
                    userId: User.Identity?.Name ?? "System",
                    action: "DELETE_ERROR",
                    entityType: "VehicleFeature",
                    entityId: id,
                    errorMessage: ex.Message,
                    stackTrace: ex.StackTrace
                );

                return Json(new { success = false, message = ex.Message });
            }
        }

        // AJAX: Toggle Active Status
        [HttpPost]
        public async Task<IActionResult> ToggleActive(Guid id)
        {
            try
            {
                var feature = await _featureRepository.GetByIdAsync(id);
                if (feature == null)
                {
                    return Json(new { success = false, message = "Vehicle feature not found." });
                }

                feature.IsActive = !feature.IsActive;
                feature.UpdatedAt = DateTime.UtcNow;

                await _featureRepository.UpdateAsync(feature);

                await _logService.LogInformationAsync(
                    userId: User.Identity?.Name ?? "System",
                    action: "TOGGLE_ACTIVE",
                    entityType: "VehicleFeature",
                    entityId: id,
                    notes: $"Toggled {feature.Name} to {(feature.IsActive ? "Active" : "Inactive")}"
                );

                return Json(new
                {
                    success = true,
                    isActive = feature.IsActive,
                    message = $"Feature '{feature.Name}' is now {(feature.IsActive ? "active" : "inactive")}."
                });
            }
            catch (Exception ex)
            {
                await _logService.LogErrorAsync(
                    userId: User.Identity?.Name ?? "System",
                    action: "TOGGLE_ACTIVE_ERROR",
                    entityType: "VehicleFeature",
                    entityId: id,
                    errorMessage: ex.Message
                );

                return Json(new { success = false, message = "An error occurred. Please try again." });
            }
        }

        private async Task LoadVehicleTypes(Guid? selectedId = null)
        {
            var vehicleTypes = await _vehicleTypeRepository.GetAllAsync();
            ViewBag.VehicleTypes = new SelectList(vehicleTypes, "Id", "Name", selectedId);
        }
    }
}