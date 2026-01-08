using ITIRR.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ITIRR.Admin.Controllers
{
    public class TestController : Controller
    {
        private readonly ICountryService _countryService;
        private readonly ICityService _cityService;
        private readonly IVehicleTypeService _vehicleTypeService;

        public TestController(
            ICountryService countryService,
            ICityService cityService,
            IVehicleTypeService vehicleTypeService)
        {
            _countryService = countryService;
            _cityService = cityService;
            _vehicleTypeService = vehicleTypeService;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var countries = await _countryService.GetAllCountriesAsync();
                var cities = await _cityService.GetAllCitiesAsync();
                var types = await _vehicleTypeService.GetAllVehicleTypesAsync();

                ViewBag.CountriesCount = countries.Count();
                ViewBag.CitiesCount = cities.Count();
                ViewBag.TypesCount = types.Count();
                ViewBag.Status = "✅ All services working!";
            }
            catch (Exception ex)
            {
                ViewBag.Status = $"❌ Error: {ex.Message}";
                ViewBag.CountriesCount = 0;
                ViewBag.CitiesCount = 0;
                ViewBag.TypesCount = 0;
            }

            return View();
        }
    }
}