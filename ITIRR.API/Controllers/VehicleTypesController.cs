using ITIRR.Core.Interfaces;
using ITIRR.Services.DTOs.Common;
using ITIRR.Services.DTOs.VehicleType;
using ITIRR.Services.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ITIRR.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class VehicleTypesController : ControllerBase
    {
        private readonly IVehicleTypeService _vehicleTypeService;

        public VehicleTypesController(IVehicleTypeService vehicleTypeService)
        {
            _vehicleTypeService = vehicleTypeService;
        }

        // GET api/v1/vehicletypes
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<ApiResponse<List<VehicleTypeDto>>>> GetAllVehicleTypes()
        {
            try
            {
                var vehicleTypes = await _vehicleTypeService.GetActiveVehicleTypesAsync();

                if (vehicleTypes == null || !vehicleTypes.Any())
                    return Ok(ApiResponse<List<VehicleTypeDto>>
                        .NotFound("No vehicle types found."));

                var vehicleTypeDtos = vehicleTypes.ToDtoList();

                return Ok(ApiResponse<List<VehicleTypeDto>>
                    .Success(vehicleTypeDtos));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<List<VehicleTypeDto>>
                    .ServerError(ex.Message));
            }
        }
    }
}