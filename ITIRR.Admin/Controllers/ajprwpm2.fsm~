using ITIRR.Core.Entities;
using ITIRR.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ITIRR.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class VehicleTypesController : Controller
    {
        private readonly IVehicleTypeService _vehicleTypeService;

        public VehicleTypesController(IVehicleTypeService vehicleTypeService)
        {
            _vehicleTypeService = vehicleTypeService;
        }

        public async Task<IActionResult> Index()
        {
            var types = await _vehicleTypeService.GetAllVehicleTypesAsync();
            return View(types);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VehicleType vehicleType)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _vehicleTypeService.CreateVehicleTypeAsync(vehicleType);
                    TempData["Success"] = "Vehicle Type created successfully!";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return View(vehicleType);
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var vehicleType = await _vehicleTypeService.GetVehicleTypeByIdAsync(id);
            if (vehicleType == null) return NotFound();
            return View(vehicleType);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, VehicleType vehicleType)
        {
            if (id != vehicleType.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    await _vehicleTypeService.UpdateVehicleTypeAsync(vehicleType);
                    TempData["Success"] = "Vehicle Type updated successfully!";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return View(vehicleType);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var result = await _vehicleTypeService.DeleteVehicleTypeAsync(id);
                return Json(new { success = result, message = result ? "Deleted successfully!" : "Not found" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
    }
}