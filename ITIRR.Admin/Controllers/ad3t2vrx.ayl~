using ITIRR.Core.Entities;
using ITIRR.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ITIRR.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CitiesController : Controller
    {
        private readonly ICityService _cityService;
        private readonly ICountryService _countryService;

        public CitiesController(ICityService cityService, ICountryService countryService)
        {
            _cityService = cityService;
            _countryService = countryService;
        }

        // GET: Cities
        public async Task<IActionResult> Index(Guid? countryId)
        {
            IEnumerable<City> cities;

            if (countryId.HasValue)
            {
                cities = await _cityService.GetCitiesByCountryAsync(countryId.Value);
                var country = await _countryService.GetCountryByIdAsync(countryId.Value);
                ViewBag.FilteredCountry = country?.Name;
            }
            else
            {
                cities = await _cityService.GetAllCitiesAsync();
            }

            ViewBag.Countries = new SelectList(await _countryService.GetAllCountriesAsync(), "Id", "Name");
            return View(cities);
        }

        // GET: Cities/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.Countries = new SelectList(await _countryService.GetActiveCountriesAsync(), "Id", "Name");
            return View();
        }

        // POST: Cities/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(City city)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _cityService.CreateCityAsync(city);
                    TempData["Success"] = "City created successfully!";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }

            ViewBag.Countries = new SelectList(await _countryService.GetActiveCountriesAsync(), "Id", "Name", city.CountryId);
            return View(city);
        }

        // GET: Cities/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            var city = await _cityService.GetCityByIdAsync(id);
            if (city == null)
            {
                return NotFound();
            }

            ViewBag.Countries = new SelectList(await _countryService.GetAllCountriesAsync(), "Id", "Name", city.CountryId);
            return View(city);
        }

        // POST: Cities/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, City city)
        {
            if (id != city.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _cityService.UpdateCityAsync(city);
                    TempData["Success"] = "City updated successfully!";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }

            ViewBag.Countries = new SelectList(await _countryService.GetAllCountriesAsync(), "Id", "Name", city.CountryId);
            return View(city);
        }

        // POST: Cities/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var result = await _cityService.DeleteCityAsync(id);
                if (result)
                {
                    return Json(new { success = true, message = "City deleted successfully!" });
                }
                return Json(new { success = false, message = "City not found" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
    }
}