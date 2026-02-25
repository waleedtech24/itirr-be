using ITIRR.Core.Entities;
using ITIRR.Services.DTOs.VehicleType;

namespace ITIRR.Services.Extensions
{
    public static class VehicleTypeExtensions
    {
        public static VehicleTypeDto ToDto(this VehicleType vehicleType)
        {
            return new VehicleTypeDto
            {
                Id = vehicleType.Id,
                Name = vehicleType.Name,
                Code = vehicleType.Code ?? string.Empty,
                Description = vehicleType.Description ?? string.Empty,
                BookingType = vehicleType.BookingType,
                IconUrl = vehicleType.IconUrl,
                DisplayOrder = vehicleType.DisplayOrder ?? 0
            };
        }

        public static List<VehicleTypeDto> ToDtoList(this IEnumerable<VehicleType> vehicleTypes)
        {
            return vehicleTypes.Select(vt => vt.ToDto())
                              .OrderBy(vt => vt.DisplayOrder)
                              .ToList();
        }
    }
}