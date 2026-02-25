namespace ITIRR.Services.DTOs.VehicleType
{
    public class VehicleTypeDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string BookingType { get; set; } = string.Empty;
        public string? IconUrl { get; set; }
        public int? DisplayOrder { get; set; }
    }
}