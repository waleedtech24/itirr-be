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

        // GET api/v1/countries
        [HttpGet]
        public async Task<IActionResult> GetCountries()
        {
            try
            {
                var countries = await _countryService.GetActiveCountriesAsync();

                if (countries == null || !countries.Any())
                    return Ok(ApiResponse<object>
                        .NotFound("No countries found."));

                return Ok(ApiResponse<object>
                    .Success(countries));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<object>
                    .ServerError(ex.Message));
            }
        }

        // GET api/v1/countries/{countryId}/cities
        [HttpGet("{countryId}/cities")]
        public async Task<IActionResult> GetCities(Guid countryId)
        {
            try
            {
                if (countryId == Guid.Empty)
                    return BadRequest(ApiResponse<object>
                        .ValidationFailed("Country ID is required."));

                var cities = await _cityService.GetCitiesByCountryAsync(countryId);

                if (cities == null || !cities.Any())
                    return Ok(ApiResponse<object>
                        .NotFound("No cities found for this country."));

                return Ok(ApiResponse<object>
                    .Success(cities));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<object>
                    .ServerError(ex.Message));
            }
        }
    }
}