using ITIRR.Core.Interfaces;
using ITIRR.Services.DTOs.Common;
using Microsoft.AspNetCore.Mvc;

namespace ITIRR.API.Controllers
{
    [Route("api/v1/countries")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly ICountryService _countryService;
        private readonly ICityService _cityService;

        public CountriesController(ICountryService countryService, ICityService cityService)
        {
            _countryService = countryService;
            _cityService = cityService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCountries()
        {
            try
            {
                var countries = await _countryService.GetActiveCountriesAsync();
                return Ok(ApiResponse<object>.SuccessResponse(countries, "Countries retrieved"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<object>.ErrorResponse(ex.Message,
                    new List<string> { ex.Message }));
            }
        }

        [HttpGet("{countryId}/cities")]
        public async Task<IActionResult> GetCities(Guid countryId)
        {
            try
            {
                var cities = await _cityService.GetCitiesByCountryAsync(countryId);
                return Ok(ApiResponse<object>.SuccessResponse(cities, "Cities retrieved"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<object>.ErrorResponse(ex.Message,
                    new List<string> { ex.Message }));
            }
        }
    }
}