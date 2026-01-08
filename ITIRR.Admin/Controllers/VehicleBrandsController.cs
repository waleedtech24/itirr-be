using ITIRR.Core.Entities;
using ITIRR.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ITIRR.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class VehicleBrandsController : Controller
    {
        private readonly IVehicleBrandService _brandService;
        private readonly IVehicleTypeService _vehicleTypeService;

        public VehicleBrandsController(IVehicleBrandService brandService, IVehicleTypeService vehicleTypeService)
        {
            _brandService = brandService;
            _vehicleTypeService = vehicleTypeService;
        }

        public async Task<IActionResult> Index(Guid? vehicleTypeId)
        {
            IEnumerable<VehicleBrand> brands;

            if (vehicleTypeId.HasValue)
            {
                brands = await _brandService.GetBrandsByTypeAsync(vehicleTypeId.Value);
            }
            else
            {
                brands = await _brandService.GetAllBrandsAsync();
            }

            ViewBag.VehicleTypes = new SelectList(await _vehicleTypeService.GetAllVehicleTypesAsync(), "Id", "Name");
            return View(brands);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.VehicleTypes = new SelectList(await _vehicleTypeService.GetActiveVehicleTypesAsync(), "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VehicleBrand brand)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _brandService.CreateBrandAsync(brand);
                    TempData["Success"] = "Brand created!";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            ViewBag.VehicleTypes = new SelectList(await _vehicleTypeService.GetActiveVehicleTypesAsync(), "Id", "Name", brand.VehicleTypeId);
            return View(brand);
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var brand = await _brandService.GetBrandByIdAsync(id);
            if (brand == null) return NotFound();
            ViewBag.VehicleTypes = new SelectList(await _vehicleTypeService.GetAllVehicleTypesAsync(), "Id", "Name", brand.VehicleTypeId);
            return View(brand);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, VehicleBrand brand)
        {
            if (id != brand.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    await _brandService.UpdateBrandAsync(brand);
                    TempData["Success"] = "Updated!";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            ViewBag.VehicleTypes = new SelectList(await _vehicleTypeService.GetAllVehicleTypesAsync(), "Id", "Name", brand.VehicleTypeId);
            return View(brand);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var result = await _brandService.DeleteBrandAsync(id);
                return Json(new { success = result, message = result ? "Deleted!" : "Not found" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
    }
}