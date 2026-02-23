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

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<ApiResponse<List<VehicleTypeDto>>>> GetAllVehicleTypes()
        {
            try
            {
                var vehicleTypes = await _vehicleTypeService.GetActiveVehicleTypesAsync();
                var vehicleTypeDtos = vehicleTypes.ToDtoList();


                return Ok(ApiResponse<List<VehicleTypeDto>>.SuccessResponse(
                    vehicleTypeDtos,
                    "Vehicle type retrieved successfully"
                ));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<List<VehicleTypeDto>>.ErrorResponse(
                    "An error occurred while retrieving vehicle types",
                    new List<string> { ex.Message }
                ));
            }
        }
    }
}