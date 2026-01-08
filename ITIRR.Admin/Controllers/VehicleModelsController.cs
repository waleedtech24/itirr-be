using ITIRR.Core.Entities;
using ITIRR.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ITIRR.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class VehicleModelsController : Controller
    {
        private readonly IVehicleModelService _modelService;
        private readonly IVehicleBrandService _brandService;

        public VehicleModelsController(IVehicleModelService modelService, IVehicleBrandService brandService)
        {
            _modelService = modelService;
            _brandService = brandService;
        }

        public async Task<IActionResult> Index(Guid? brandId)
        {
            IEnumerable<VehicleModel> models;

            if (brandId.HasValue)
            {
                models = await _modelService.GetModelsByBrandAsync(brandId.Value);
                var brand = await _brandService.GetBrandByIdAsync(brandId.Value);
                ViewBag.FilteredBrand = brand?.Name;
            }
            else
            {
                models = await _modelService.GetAllModelsAsync();
            }

            ViewBag.Brands = new SelectList(await _brandService.GetAllBrandsAsync(), "Id", "Name");
            return View(models);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Brands = new SelectList(await _brandService.GetActiveBrandsAsync(), "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VehicleModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _modelService.CreateModelAsync(model);
                    TempData["Success"] = "Model created!";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            ViewBag.Brands = new SelectList(await _brandService.GetActiveBrandsAsync(), "Id", "Name", model.VehicleBrandId);
            return View(model);
        }

        public async Task<IActionResult> Edit(Guid id)
        {
            var model = await _modelService.GetModelByIdAsync(id);
            if (model == null) return NotFound();
            ViewBag.Brands = new SelectList(await _brandService.GetAllBrandsAsync(), "Id", "Name", model.VehicleBrandId);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, VehicleModel model)
        {
            if (id != model.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    await _modelService.UpdateModelAsync(model);
                    TempData["Success"] = "Updated!";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            ViewBag.Brands = new SelectList(await _brandService.GetAllBrandsAsync(), "Id", "Name", model.VehicleBrandId);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var result = await _modelService.DeleteModelAsync(id);
                return Json(new { success = result, message = result ? "Deleted!" : "Not found" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
    }
}