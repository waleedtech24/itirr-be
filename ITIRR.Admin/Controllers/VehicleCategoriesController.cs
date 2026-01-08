using ITIRR.Core.Entities;
using ITIRR.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ITIRR.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class VehicleCategoriesController : Controller
    {
        private readonly IVehicleCategoryService _categoryService;
        private readonly IVehicleTypeService _vehicleTypeService;

        public VehicleCategoriesController(IVehicleCategoryService categoryService, IVehicleTypeService vehicleTypeService)
        {
            _categoryService = categoryService;
            _vehicleTypeService = vehicleTypeService;
        }

        public async Task<IActionResult> Index(Guid? vehicleTypeId)
        {
            IEnumerable<VehicleCategory> categories;

            if (vehicleTypeId.HasValue)
            {
                categories = await _categoryService.GetCategoriesByTypeAsync(vehicleTypeId.Value);
                var type = await _vehicleTypeService.GetVehicleTypeByIdAsync(vehicleTypeId.Value);
                ViewBag.FilteredType = type?.Name;
            }
            else
            {
                categories = await _categoryService.GetAllCategoriesAsync();
            }

            ViewBag.VehicleTypes = new SelectList(await _vehicleTypeService.GetAllVehicleTypesAsync(), "Id", "Name");
            return View(categories);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.VehicleTypes = new SelectList(await _vehicleTypeService.GetActiveVehicleTypesAsync(), "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VehicleCategory category)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _categoryService.CreateCategoryAsync(category);
                    TempData["Success"] = "Category created!";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            ViewBag.VehicleTypes = new SelectList(await _vehicleTypeService.GetActiveVehicleTypesAsync(), "Id", "Name", category.VehicleTypeId);
            return View(category);
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);
            if (category == null) return NotFound();
            ViewBag.VehicleTypes = new SelectList(await _vehicleTypeService.GetAllVehicleTypesAsync(), "Id", "Name", category.VehicleTypeId);
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, VehicleCategory category)
        {
            if (id != category.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    await _categoryService.UpdateCategoryAsync(category);
                    TempData["Success"] = "Updated!";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            ViewBag.VehicleTypes = new SelectList(await _vehicleTypeService.GetAllVehicleTypesAsync(), "Id", "Name", category.VehicleTypeId);
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var result = await _categoryService.DeleteCategoryAsync(id);
                return Json(new { success = result, message = result ? "Deleted!" : "Not found" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
    }
}