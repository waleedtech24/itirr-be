using ITIRR.Core;
using ITIRR.Core.Entities;
using Microsoft.AspNetCore.Identity;  // ADD THIS
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;  // ADD THIS
using System;
using System.Linq;
using System.Threading.Tasks;  // ADD THIS
namespace ITIRR.Infrastructure.Data
{
    public static class SeedData
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            // ============================================
            // COUNTRIES
            // ============================================
            if (!context.Countries.Any())
            {
                var countries = new[]
                {
                    new Country { Name = "United Kingdom", Code = "GB", PhoneCode = "+44", IsActive = true },
                    new Country { Name = "United States", Code = "US", PhoneCode = "+1", IsActive = true },
                    new Country { Name = "United Arab Emirates", Code = "AE", PhoneCode = "+971", IsActive = true },
                    new Country { Name = "France", Code = "FR", PhoneCode = "+33", IsActive = true },
                    new Country { Name = "Monaco", Code = "MC", PhoneCode = "+377", IsActive = true },
                    new Country { Name = "Switzerland", Code = "CH", PhoneCode = "+41", IsActive = true }
                };
                context.Countries.AddRange(countries);
                context.SaveChanges();
            }

            var ukCountryId = context.Countries.First(c => c.Code == "GB").Id;

            // ============================================
            // UK CITIES - Comprehensive List
            // ============================================
            if (!context.Cities.Any())
            {
                var cities = new[]
                {
                    // England - Major Cities
                    new City { Name = "London", CountryId = ukCountryId, Latitude = 51.5074m, Longitude = -0.1278m, IsActive = true },
                    new City { Name = "Manchester", CountryId = ukCountryId, Latitude = 53.4808m, Longitude = -2.2426m, IsActive = true },
                    new City { Name = "Birmingham", CountryId = ukCountryId, Latitude = 52.4862m, Longitude = -1.8904m, IsActive = true },
                    new City { Name = "Liverpool", CountryId = ukCountryId, Latitude = 53.4084m, Longitude = -2.9916m, IsActive = true },
                    new City { Name = "Leeds", CountryId = ukCountryId, Latitude = 53.8008m, Longitude = -1.5491m, IsActive = true },
                    new City { Name = "Sheffield", CountryId = ukCountryId, Latitude = 53.3811m, Longitude = -1.4701m, IsActive = true },
                    new City { Name = "Bristol", CountryId = ukCountryId, Latitude = 51.4545m, Longitude = -2.5879m, IsActive = true },
                    new City { Name = "Newcastle upon Tyne", CountryId = ukCountryId, Latitude = 54.9783m, Longitude = -1.6178m, IsActive = true },
                    new City { Name = "Nottingham", CountryId = ukCountryId, Latitude = 52.9548m, Longitude = -1.1581m, IsActive = true },
                    new City { Name = "Leicester", CountryId = ukCountryId, Latitude = 52.6369m, Longitude = -1.1398m, IsActive = true },
                    
                    // England - Southern Cities
                    new City { Name = "Southampton", CountryId = ukCountryId, Latitude = 50.9097m, Longitude = -1.4044m, IsActive = true },
                    new City { Name = "Portsmouth", CountryId = ukCountryId, Latitude = 50.8198m, Longitude = -1.0880m, IsActive = true },
                    new City { Name = "Brighton", CountryId = ukCountryId, Latitude = 50.8225m, Longitude = -0.1372m, IsActive = true },
                    new City { Name = "Bournemouth", CountryId = ukCountryId, Latitude = 50.7192m, Longitude = -1.8808m, IsActive = true },
                    new City { Name = "Plymouth", CountryId = ukCountryId, Latitude = 50.3755m, Longitude = -4.1427m, IsActive = true },
                    new City { Name = "Reading", CountryId = ukCountryId, Latitude = 51.4543m, Longitude = -0.9781m, IsActive = true },
                    new City { Name = "Oxford", CountryId = ukCountryId, Latitude = 51.7520m, Longitude = -1.2577m, IsActive = true },
                    new City { Name = "Cambridge", CountryId = ukCountryId, Latitude = 52.2053m, Longitude = 0.1218m, IsActive = true },
                    new City { Name = "Canterbury", CountryId = ukCountryId, Latitude = 51.2802m, Longitude = 1.0789m, IsActive = true },
                    new City { Name = "Bath", CountryId = ukCountryId, Latitude = 51.3811m, Longitude = -2.3590m, IsActive = true },
                    
                    // England - Midlands
                    new City { Name = "Coventry", CountryId = ukCountryId, Latitude = 52.4068m, Longitude = -1.5197m, IsActive = true },
                    new City { Name = "Derby", CountryId = ukCountryId, Latitude = 52.9225m, Longitude = -1.4746m, IsActive = true },
                    new City { Name = "Stoke-on-Trent", CountryId = ukCountryId, Latitude = 53.0027m, Longitude = -2.1794m, IsActive = true },
                    new City { Name = "Wolverhampton", CountryId = ukCountryId, Latitude = 52.5862m, Longitude = -2.1285m, IsActive = true },
                    new City { Name = "Northampton", CountryId = ukCountryId, Latitude = 52.2405m, Longitude = -0.9027m, IsActive = true },
                    
                    // England - Northern Cities
                    new City { Name = "York", CountryId = ukCountryId, Latitude = 53.9600m, Longitude = -1.0873m, IsActive = true },
                    new City { Name = "Bradford", CountryId = ukCountryId, Latitude = 53.7960m, Longitude = -1.7594m, IsActive = true },
                    new City { Name = "Hull", CountryId = ukCountryId, Latitude = 53.7457m, Longitude = -0.3367m, IsActive = true },
                    new City { Name = "Sunderland", CountryId = ukCountryId, Latitude = 54.9069m, Longitude = -1.3838m, IsActive = true },
                    new City { Name = "Middlesbrough", CountryId = ukCountryId, Latitude = 54.5742m, Longitude = -1.2350m, IsActive = true },
                    new City { Name = "Preston", CountryId = ukCountryId, Latitude = 53.7632m, Longitude = -2.7031m, IsActive = true },
                    new City { Name = "Blackpool", CountryId = ukCountryId, Latitude = 53.8175m, Longitude = -3.0357m, IsActive = true },
                    
                    // Scotland
                    new City { Name = "Edinburgh", CountryId = ukCountryId, Latitude = 55.9533m, Longitude = -3.1883m, IsActive = true },
                    new City { Name = "Glasgow", CountryId = ukCountryId, Latitude = 55.8642m, Longitude = -4.2518m, IsActive = true },
                    new City { Name = "Aberdeen", CountryId = ukCountryId, Latitude = 57.1497m, Longitude = -2.0943m, IsActive = true },
                    new City { Name = "Dundee", CountryId = ukCountryId, Latitude = 56.4620m, Longitude = -2.9707m, IsActive = true },
                    new City { Name = "Inverness", CountryId = ukCountryId, Latitude = 57.4778m, Longitude = -4.2247m, IsActive = true },
                    new City { Name = "Stirling", CountryId = ukCountryId, Latitude = 56.1165m, Longitude = -3.9369m, IsActive = true },
                    
                    // Wales
                    new City { Name = "Cardiff", CountryId = ukCountryId, Latitude = 51.4816m, Longitude = -3.1791m, IsActive = true },
                    new City { Name = "Swansea", CountryId = ukCountryId, Latitude = 51.6214m, Longitude = -3.9436m, IsActive = true },
                    new City { Name = "Newport", CountryId = ukCountryId, Latitude = 51.5842m, Longitude = -2.9977m, IsActive = true },
                    new City { Name = "Wrexham", CountryId = ukCountryId, Latitude = 53.0462m, Longitude = -2.9930m, IsActive = true },
                    
                    // Northern Ireland
                    new City { Name = "Belfast", CountryId = ukCountryId, Latitude = 54.5973m, Longitude = -5.9301m, IsActive = true },
                    new City { Name = "Londonderry", CountryId = ukCountryId, Latitude = 54.9966m, Longitude = -7.3086m, IsActive = true },
                    
                    // England - Additional Important Cities
                    new City { Name = "Exeter", CountryId = ukCountryId, Latitude = 50.7184m, Longitude = -3.5339m, IsActive = true },
                    new City { Name = "Norwich", CountryId = ukCountryId, Latitude = 52.6309m, Longitude = 1.2974m, IsActive = true },
                    new City { Name = "Ipswich", CountryId = ukCountryId, Latitude = 52.0594m, Longitude = 1.1556m, IsActive = true },
                    new City { Name = "Peterborough", CountryId = ukCountryId, Latitude = 52.5695m, Longitude = -0.2405m, IsActive = true },
                    new City { Name = "Chelmsford", CountryId = ukCountryId, Latitude = 51.7356m, Longitude = 0.4685m, IsActive = true },
                    new City { Name = "Colchester", CountryId = ukCountryId, Latitude = 51.8959m, Longitude = 0.8919m, IsActive = true },
                    new City { Name = "Gloucester", CountryId = ukCountryId, Latitude = 51.8642m, Longitude = -2.2382m, IsActive = true },
                    new City { Name = "Worcester", CountryId = ukCountryId, Latitude = 52.1936m, Longitude = -2.2200m, IsActive = true },
                    new City { Name = "Chester", CountryId = ukCountryId, Latitude = 53.1906m, Longitude = -2.8908m, IsActive = true },
                    new City { Name = "Carlisle", CountryId = ukCountryId, Latitude = 54.8951m, Longitude = -2.9382m, IsActive = true },
                    new City { Name = "Durham", CountryId = ukCountryId, Latitude = 54.7761m, Longitude = -1.5733m, IsActive = true },
                    new City { Name = "Lancaster", CountryId = ukCountryId, Latitude = 54.0465m, Longitude = -2.8010m, IsActive = true },
                    new City { Name = "Lincoln", CountryId = ukCountryId, Latitude = 53.2307m, Longitude = -0.5406m, IsActive = true },
                    new City { Name = "Winchester", CountryId = ukCountryId, Latitude = 51.0632m, Longitude = -1.3080m, IsActive = true },
                    new City { Name = "Salisbury", CountryId = ukCountryId, Latitude = 51.0693m, Longitude = -1.7944m, IsActive = true },
                    new City { Name = "Cheltenham", CountryId = ukCountryId, Latitude = 51.8994m, Longitude = -2.0783m, IsActive = true },
                    new City { Name = "Harrogate", CountryId = ukCountryId, Latitude = 53.9921m, Longitude = -1.5418m, IsActive = true },
                    new City { Name = "Windsor", CountryId = ukCountryId, Latitude = 51.4816m, Longitude = -0.6107m, IsActive = true },
                    new City { Name = "St Albans", CountryId = ukCountryId, Latitude = 51.7520m, Longitude = -0.3360m, IsActive = true }
                };
                context.Cities.AddRange(cities);
                context.SaveChanges();
            }

            // ============================================
            // VEHICLE TYPES
            // ============================================
            if (!context.VehicleTypes.Any())
            {
                var vehicleTypes = new[]
                {
                    new VehicleType { Name = "Car", Code = "CAR", Description = "Luxury and prestige cars", BookingType = "Direct", IsActive = true, DisplayOrder = 1 },
                    new VehicleType { Name = "Private Jet", Code = "JET", Description = "Private jets and aircraft", BookingType = "Inquiry", IsActive = true, DisplayOrder = 2 },
                    new VehicleType { Name = "Yacht", Code = "YACHT", Description = "Luxury yachts and superyachts", BookingType = "Inquiry", IsActive = true, DisplayOrder = 3 },
                    new VehicleType { Name = "Boat", Code = "BOAT", Description = "Speedboats and motorboats", BookingType = "Inquiry", IsActive = true, DisplayOrder = 4 }
                };
                context.VehicleTypes.AddRange(vehicleTypes);
                context.SaveChanges();
            }

            var carTypeId = context.VehicleTypes.First(vt => vt.Code == "CAR").Id;
            var jetTypeId = context.VehicleTypes.First(vt => vt.Code == "JET").Id;
            var yachtTypeId = context.VehicleTypes.First(vt => vt.Code == "YACHT").Id;
            var boatTypeId = context.VehicleTypes.First(vt => vt.Code == "BOAT").Id;

            // ============================================
            // VEHICLE CATEGORIES - Comprehensive
            // ============================================
            if (!context.VehicleCategories.Any())
            {
                var categories = new[]
                {
                    // ===== CAR CATEGORIES (15) =====
                    new VehicleCategory { Name = "Luxury Saloon", Code = "LUXURY_SALOON", Description = "Executive luxury saloons", VehicleTypeId = carTypeId, IsActive = true, DisplayOrder = 1 },
                    new VehicleCategory { Name = "Prestige Saloon", Code = "PRESTIGE_SALOON", Description = "Ultra-luxury prestige saloons", VehicleTypeId = carTypeId, IsActive = true, DisplayOrder = 2 },
                    new VehicleCategory { Name = "Sports Car", Code = "SPORTS_CAR", Description = "High-performance sports cars", VehicleTypeId = carTypeId, IsActive = true, DisplayOrder = 3 },
                    new VehicleCategory { Name = "Supercar", Code = "SUPERCAR", Description = "Exotic supercars", VehicleTypeId = carTypeId, IsActive = true, DisplayOrder = 4 },
                    new VehicleCategory { Name = "Hypercar", Code = "HYPERCAR", Description = "Limited edition hypercars", VehicleTypeId = carTypeId, IsActive = true, DisplayOrder = 5 },
                    new VehicleCategory { Name = "Luxury SUV", Code = "LUXURY_SUV", Description = "Premium luxury SUVs", VehicleTypeId = carTypeId, IsActive = true, DisplayOrder = 6 },
                    new VehicleCategory { Name = "Performance SUV", Code = "PERFORMANCE_SUV", Description = "High-performance SUVs", VehicleTypeId = carTypeId, IsActive = true, DisplayOrder = 7 },
                    new VehicleCategory { Name = "Convertible", Code = "CONVERTIBLE", Description = "Luxury convertibles and cabriolets", VehicleTypeId = carTypeId, IsActive = true, DisplayOrder = 8 },
                    new VehicleCategory { Name = "Coupe", Code = "COUPE", Description = "Luxury coupes", VehicleTypeId = carTypeId, IsActive = true, DisplayOrder = 9 },
                    new VehicleCategory { Name = "GT", Code = "GT", Description = "Grand touring cars", VehicleTypeId = carTypeId, IsActive = true, DisplayOrder = 10 },
                    new VehicleCategory { Name = "Estate", Code = "ESTATE", Description = "Luxury estate cars", VehicleTypeId = carTypeId, IsActive = true, DisplayOrder = 11 },
                    new VehicleCategory { Name = "Executive MPV", Code = "EXEC_MPV", Description = "Executive people carriers", VehicleTypeId = carTypeId, IsActive = true, DisplayOrder = 12 },
                    new VehicleCategory { Name = "Luxury Van", Code = "LUXURY_VAN", Description = "Luxury conversion vans", VehicleTypeId = carTypeId, IsActive = true, DisplayOrder = 13 },
                    new VehicleCategory { Name = "Classic Car", Code = "CLASSIC_CAR", Description = "Classic and vintage luxury cars", VehicleTypeId = carTypeId, IsActive = true, DisplayOrder = 14 },
                    new VehicleCategory { Name = "Electric Luxury", Code = "ELECTRIC_LUXURY", Description = "High-end electric vehicles", VehicleTypeId = carTypeId, IsActive = true, DisplayOrder = 15 },
                    
                    // ===== JET CATEGORIES (10) =====
                    new VehicleCategory { Name = "Very Light Jet", Code = "VLJ", Description = "4-6 passenger very light jets", VehicleTypeId = jetTypeId, IsActive = true, DisplayOrder = 1 },
                    new VehicleCategory { Name = "Light Jet", Code = "LIGHT_JET", Description = "6-8 passenger light jets", VehicleTypeId = jetTypeId, IsActive = true, DisplayOrder = 2 },
                    new VehicleCategory { Name = "Midsize Jet", Code = "MIDSIZE_JET", Description = "8-10 passenger midsize jets", VehicleTypeId = jetTypeId, IsActive = true, DisplayOrder = 3 },
                    new VehicleCategory { Name = "Super Midsize Jet", Code = "SUPER_MIDSIZE", Description = "9-12 passenger super midsize", VehicleTypeId = jetTypeId, IsActive = true, DisplayOrder = 4 },
                    new VehicleCategory { Name = "Heavy Jet", Code = "HEAVY_JET", Description = "10-16 passenger heavy jets", VehicleTypeId = jetTypeId, IsActive = true, DisplayOrder = 5 },
                    new VehicleCategory { Name = "Ultra Long Range", Code = "ULTRA_LONG", Description = "Long-range intercontinental jets", VehicleTypeId = jetTypeId, IsActive = true, DisplayOrder = 6 },
                    new VehicleCategory { Name = "VIP Airliner", Code = "VIP_AIRLINER", Description = "Boeing/Airbus VIP conversions", VehicleTypeId = jetTypeId, IsActive = true, DisplayOrder = 7 },
                    new VehicleCategory { Name = "Turboprop", Code = "TURBOPROP", Description = "Turboprop aircraft", VehicleTypeId = jetTypeId, IsActive = true, DisplayOrder = 8 },
                    new VehicleCategory { Name = "Helicopter", Code = "HELICOPTER", Description = "Luxury helicopters", VehicleTypeId = jetTypeId, IsActive = true, DisplayOrder = 9 },
                    new VehicleCategory { Name = "Business Jet", Code = "BUSINESS_JET", Description = "Corporate business jets", VehicleTypeId = jetTypeId, IsActive = true, DisplayOrder = 10 },
                    
                    // ===== YACHT CATEGORIES (12) =====
                    new VehicleCategory { Name = "Motor Yacht", Code = "MOTOR_YACHT", Description = "Luxury motor yachts", VehicleTypeId = yachtTypeId, IsActive = true, DisplayOrder = 1 },
                    new VehicleCategory { Name = "Sailing Yacht", Code = "SAILING_YACHT", Description = "Luxury sailing yachts", VehicleTypeId = yachtTypeId, IsActive = true, DisplayOrder = 2 },
                    new VehicleCategory { Name = "Superyacht", Code = "SUPERYACHT", Description = "Superyachts 24m-50m", VehicleTypeId = yachtTypeId, IsActive = true, DisplayOrder = 3 },
                    new VehicleCategory { Name = "Megayacht", Code = "MEGAYACHT", Description = "Megayachts over 50m", VehicleTypeId = yachtTypeId, IsActive = true, DisplayOrder = 4 },
                    new VehicleCategory { Name = "Catamaran", Code = "CATAMARAN", Description = "Luxury catamarans", VehicleTypeId = yachtTypeId, IsActive = true, DisplayOrder = 5 },
                    new VehicleCategory { Name = "Explorer Yacht", Code = "EXPLORER", Description = "Long-range explorer yachts", VehicleTypeId = yachtTypeId, IsActive = true, DisplayOrder = 6 },
                    new VehicleCategory { Name = "Sports Yacht", Code = "SPORTS_YACHT", Description = "High-performance sports yachts", VehicleTypeId = yachtTypeId, IsActive = true, DisplayOrder = 7 },
                    new VehicleCategory { Name = "Flybridge", Code = "FLYBRIDGE", Description = "Flybridge motor yachts", VehicleTypeId = yachtTypeId, IsActive = true, DisplayOrder = 8 },
                    new VehicleCategory { Name = "Expedition Yacht", Code = "EXPEDITION", Description = "Ice-class expedition yachts", VehicleTypeId = yachtTypeId, IsActive = true, DisplayOrder = 9 },
                    new VehicleCategory { Name = "Day Charter Yacht", Code = "DAY_CHARTER", Description = "Day charter yachts", VehicleTypeId = yachtTypeId, IsActive = true, DisplayOrder = 10 },
                    new VehicleCategory { Name = "Gulet", Code = "GULET", Description = "Traditional wooden gulets", VehicleTypeId = yachtTypeId, IsActive = true, DisplayOrder = 11 },
                    new VehicleCategory { Name = "Classic Yacht", Code = "CLASSIC_YACHT", Description = "Classic and vintage yachts", VehicleTypeId = yachtTypeId, IsActive = true, DisplayOrder = 12 },
                    
                    // ===== BOAT CATEGORIES (10) =====
                    new VehicleCategory { Name = "Speedboat", Code = "SPEEDBOAT", Description = "High-speed performance boats", VehicleTypeId = boatTypeId, IsActive = true, DisplayOrder = 1 },
                    new VehicleCategory { Name = "RIB", Code = "RIB", Description = "Rigid inflatable boats", VehicleTypeId = boatTypeId, IsActive = true, DisplayOrder = 2 },
                    new VehicleCategory { Name = "Day Boat", Code = "DAYBOAT", Description = "Day cruising boats", VehicleTypeId = boatTypeId, IsActive = true, DisplayOrder = 3 },
                    new VehicleCategory { Name = "Sports Boat", Code = "SPORTS_BOAT", Description = "Sports and wakeboard boats", VehicleTypeId = boatTypeId, IsActive = true, DisplayOrder = 4 },
                    new VehicleCategory { Name = "Fishing Boat", Code = "FISHING_BOAT", Description = "Sport fishing boats", VehicleTypeId = boatTypeId, IsActive = true, DisplayOrder = 5 },
                    new VehicleCategory { Name = "Bowrider", Code = "BOWRIDER", Description = "Bowrider leisure boats", VehicleTypeId = boatTypeId, IsActive = true, DisplayOrder = 6 },
                    new VehicleCategory { Name = "Cabin Cruiser", Code = "CABIN_CRUISER", Description = "Small cabin cruisers", VehicleTypeId = boatTypeId, IsActive = true, DisplayOrder = 7 },
                    new VehicleCategory { Name = "Pontoon", Code = "PONTOON", Description = "Pontoon leisure boats", VehicleTypeId = boatTypeId, IsActive = true, DisplayOrder = 8 },
                    new VehicleCategory { Name = "Canal Boat", Code = "CANAL_BOAT", Description = "Narrowboats and canal boats", VehicleTypeId = boatTypeId, IsActive = true, DisplayOrder = 9 },
                    new VehicleCategory { Name = "Tender", Code = "TENDER", Description = "Yacht tenders", VehicleTypeId = boatTypeId, IsActive = true, DisplayOrder = 10 }
                };
                context.VehicleCategories.AddRange(categories);
                context.SaveChanges();
            }

            // ============================================
            // VEHICLE BRANDS - UK Market Focus
            // ============================================
            if (!context.VehicleBrands.Any())
            {
                var brands = new[]
                {
                    // ===== CAR BRANDS (30+) =====
                    // British Luxury
                    new VehicleBrand { Name = "Rolls-Royce", Code = "ROLLS_ROYCE", VehicleTypeId = carTypeId, IsActive = true },
                    new VehicleBrand { Name = "Bentley", Code = "BENTLEY", VehicleTypeId = carTypeId, IsActive = true },
                    new VehicleBrand { Name = "Aston Martin", Code = "ASTON_MARTIN", VehicleTypeId = carTypeId, IsActive = true },
                    new VehicleBrand { Name = "Jaguar", Code = "JAGUAR", VehicleTypeId = carTypeId, IsActive = true },
                    new VehicleBrand { Name = "Land Rover", Code = "LAND_ROVER", VehicleTypeId = carTypeId, IsActive = true },
                    new VehicleBrand { Name = "Range Rover", Code = "RANGE_ROVER", VehicleTypeId = carTypeId, IsActive = true },
                    new VehicleBrand { Name = "McLaren", Code = "MCLAREN", VehicleTypeId = carTypeId, IsActive = true },
                    new VehicleBrand { Name = "Lotus", Code = "LOTUS", VehicleTypeId = carTypeId, IsActive = true },
                    new VehicleBrand { Name = "Morgan", Code = "MORGAN", VehicleTypeId = carTypeId, IsActive = true },
                    
                    // German Luxury
                    new VehicleBrand { Name = "Mercedes-Benz", Code = "MERCEDES", VehicleTypeId = carTypeId, IsActive = true },
                    new VehicleBrand { Name = "BMW", Code = "BMW", VehicleTypeId = carTypeId, IsActive = true },
                    new VehicleBrand { Name = "Audi", Code = "AUDI", VehicleTypeId = carTypeId, IsActive = true },
                    new VehicleBrand { Name = "Porsche", Code = "PORSCHE", VehicleTypeId = carTypeId, IsActive = true },
                    new VehicleBrand { Name = "Maybach", Code = "MAYBACH", VehicleTypeId = carTypeId, IsActive = true },
                    
                    // Italian Exotic
                    new VehicleBrand { Name = "Ferrari", Code = "FERRARI", VehicleTypeId = carTypeId, IsActive = true },
                    new VehicleBrand { Name = "Lamborghini", Code = "LAMBORGHINI", VehicleTypeId = carTypeId, IsActive = true },
                    new VehicleBrand { Name = "Maserati", Code = "MASERATI", VehicleTypeId = carTypeId, IsActive = true },
                    new VehicleBrand { Name = "Bugatti", Code = "BUGATTI", VehicleTypeId = carTypeId, IsActive = true },
                    new VehicleBrand { Name = "Pagani", Code = "PAGANI", VehicleTypeId = carTypeId, IsActive = true },
                    new VehicleBrand { Name = "Alfa Romeo", Code = "ALFA_ROMEO", VehicleTypeId = carTypeId, IsActive = true },
                    
                    // Other Premium
                    new VehicleBrand { Name = "Lexus", Code = "LEXUS", VehicleTypeId = carTypeId, IsActive = true },
                    new VehicleBrand { Name = "Tesla", Code = "TESLA", VehicleTypeId = carTypeId, IsActive = true },
                    new VehicleBrand { Name = "Lucid", Code = "LUCID", VehicleTypeId = carTypeId, IsActive = true },
                    new VehicleBrand { Name = "Koenigsegg", Code = "KOENIGSEGG", VehicleTypeId = carTypeId, IsActive = true },
                    new VehicleBrand { Name = "Volvo", Code = "VOLVO", VehicleTypeId = carTypeId, IsActive = true },
                    new VehicleBrand { Name = "Cadillac", Code = "CADILLAC", VehicleTypeId = carTypeId, IsActive = true },
                    new VehicleBrand { Name = "Genesis", Code = "GENESIS", VehicleTypeId = carTypeId, IsActive = true },
                    
                    // ===== JET BRANDS (15) =====
                    new VehicleBrand { Name = "Gulfstream", Code = "GULFSTREAM", VehicleTypeId = jetTypeId, IsActive = true },
                    new VehicleBrand { Name = "Bombardier", Code = "BOMBARDIER", VehicleTypeId = jetTypeId, IsActive = true },
                    new VehicleBrand { Name = "Cessna", Code = "CESSNA", VehicleTypeId = jetTypeId, IsActive = true },
                    new VehicleBrand { Name = "Embraer", Code = "EMBRAER", VehicleTypeId = jetTypeId, IsActive = true },
                    new VehicleBrand { Name = "Dassault Falcon", Code = "DASSAULT", VehicleTypeId = jetTypeId, IsActive = true },
                    new VehicleBrand { Name = "Pilatus", Code = "PILATUS", VehicleTypeId = jetTypeId, IsActive = true },
                    new VehicleBrand { Name = "Beechcraft", Code = "BEECHCRAFT", VehicleTypeId = jetTypeId, IsActive = true },
                    new VehicleBrand { Name = "Learjet", Code = "LEARJET", VehicleTypeId = jetTypeId, IsActive = true },
                    new VehicleBrand { Name = "Boeing Business Jets", Code = "BBJ", VehicleTypeId = jetTypeId, IsActive = true },
                    new VehicleBrand { Name = "Airbus Corporate Jets", Code = "ACJ", VehicleTypeId = jetTypeId, IsActive = true },
                    new VehicleBrand { Name = "Cirrus", Code = "CIRRUS", VehicleTypeId = jetTypeId, IsActive = true },
                    new VehicleBrand { Name = "HondaJet", Code = "HONDAJET", VehicleTypeId = jetTypeId, IsActive = true },
                    new VehicleBrand { Name = "Agusta", Code = "AGUSTA", VehicleTypeId = jetTypeId, IsActive = true },
                    new VehicleBrand { Name = "Bell", Code = "BELL", VehicleTypeId = jetTypeId, IsActive = true },
                    new VehicleBrand { Name = "Airbus Helicopters", Code = "AIRBUS_HELI", VehicleTypeId = jetTypeId, IsActive = true },
                    
                    // ===== YACHT BRANDS (20+) =====
                    // British
                    new VehicleBrand { Name = "Sunseeker", Code = "SUNSEEKER", VehicleTypeId = yachtTypeId, IsActive = true },
                    new VehicleBrand { Name = "Princess", Code = "PRINCESS", VehicleTypeId = yachtTypeId, IsActive = true },
                    new VehicleBrand { Name = "Fairline", Code = "FAIRLINE", VehicleTypeId = yachtTypeId, IsActive = true },
                    
                    // Italian
                    new VehicleBrand { Name = "Azimut", Code = "AZIMUT", VehicleTypeId = yachtTypeId, IsActive = true },
                    new VehicleBrand { Name = "Ferretti", Code = "FERRETTI", VehicleTypeId = yachtTypeId, IsActive = true },
                    new VehicleBrand { Name = "Benetti", Code = "BENETTI", VehicleTypeId = yachtTypeId, IsActive = true },
                    new VehicleBrand { Name = "Riva", Code = "RIVA", VehicleTypeId = yachtTypeId, IsActive = true },
                    new VehicleBrand { Name = "Pershing", Code = "PERSHING", VehicleTypeId = yachtTypeId, IsActive = true },
                    new VehicleBrand { Name = "Sanlorenzo", Code = "SANLORENZO", VehicleTypeId = yachtTypeId, IsActive = true },
                    new VehicleBrand { Name = "Cantieri di Pisa", Code = "CANTIERI", VehicleTypeId = yachtTypeId, IsActive = true },
                    
                    // Dutch
                    new VehicleBrand { Name = "Feadship", Code = "FEADSHIP", VehicleTypeId = yachtTypeId, IsActive = true },
                    new VehicleBrand { Name = "Oceanco", Code = "OCEANCO", VehicleTypeId = yachtTypeId, IsActive = true },
                    new VehicleBrand { Name = "Amels", Code = "AMELS", VehicleTypeId = yachtTypeId, IsActive = true },
                    
                    // German
                    new VehicleBrand { Name = "Lürssen", Code = "LURSSEN", VehicleTypeId = yachtTypeId, IsActive = true },
                    new VehicleBrand { Name = "Abeking & Rasmussen", Code = "ABEKING", VehicleTypeId = yachtTypeId, IsActive = true },
                    
                    // American
                    new VehicleBrand { Name = "Viking", Code = "VIKING", VehicleTypeId = yachtTypeId, IsActive = true },
                    new VehicleBrand { Name = "Hatteras", Code = "HATTERAS", VehicleTypeId = yachtTypeId, IsActive = true },
                    new VehicleBrand { Name = "Westport", Code = "WESTPORT", VehicleTypeId = yachtTypeId, IsActive = true },
                    
                    // Turkish
                    new VehicleBrand { Name = "Numarine", Code = "NUMARINE", VehicleTypeId = yachtTypeId, IsActive = true },
                    
                    // Other
                    new VehicleBrand { Name = "Bavaria", Code = "BAVARIA", VehicleTypeId = yachtTypeId, IsActive = true },
                    new VehicleBrand { Name = "Beneteau", Code = "BENETEAU", VehicleTypeId = yachtTypeId, IsActive = true },
                    new VehicleBrand { Name = "Jeanneau", Code = "JEANNEAU", VehicleTypeId = yachtTypeId, IsActive = true },
                    
                    // ===== BOAT BRANDS (15) =====
                    new VehicleBrand { Name = "Ribeye", Code = "RIBEYE", VehicleTypeId = boatTypeId, IsActive = true },
                    new VehicleBrand { Name = "Williams", Code = "WILLIAMS", VehicleTypeId = boatTypeId, IsActive = true },
                    new VehicleBrand { Name = "Zodiac", Code = "ZODIAC", VehicleTypeId = boatTypeId, IsActive = true },
                    new VehicleBrand { Name = "Sea Ray", Code = "SEA_RAY", VehicleTypeId = boatTypeId, IsActive = true },
                    new VehicleBrand { Name = "Boston Whaler", Code = "BOSTON_WHALER", VehicleTypeId = boatTypeId, IsActive = true },
                    new VehicleBrand { Name = "Bayliner", Code = "BAYLINER", VehicleTypeId = boatTypeId, IsActive = true },
                    new VehicleBrand { Name = "Brig", Code = "BRIG", VehicleTypeId = boatTypeId, IsActive = true },
                    new VehicleBrand { Name = "Cobra", Code = "COBRA", VehicleTypeId = boatTypeId, IsActive = true },
                    new VehicleBrand { Name = "Avon", Code = "AVON", VehicleTypeId = boatTypeId, IsActive = true },
                    new VehicleBrand { Name = "XS Ribs", Code = "XS_RIBS", VehicleTypeId = boatTypeId, IsActive = true },
                    new VehicleBrand { Name = "Ballistic", Code = "BALLISTIC", VehicleTypeId = boatTypeId, IsActive = true },
                    new VehicleBrand { Name = "Ribeye", Code = "RIBEYE_BOATS", VehicleTypeId = boatTypeId, IsActive = true },
                    new VehicleBrand { Name = "Mastercraft", Code = "MASTERCRAFT", VehicleTypeId = boatTypeId, IsActive = true },
                    new VehicleBrand { Name = "Nautique", Code = "NAUTIQUE", VehicleTypeId = boatTypeId, IsActive = true },
                    new VehicleBrand { Name = "Malibu", Code = "MALIBU", VehicleTypeId = boatTypeId, IsActive = true }
                };
                context.VehicleBrands.AddRange(brands);
                context.SaveChanges();
            }

            // ============================================
            // VEHICLE FEATURES - Comprehensive
            // ============================================
            if (!context.VehicleFeatures.Any())
            {
                var features = new[]
                {
                    // ===== CAR FEATURES (40+) =====
                    // Safety & Driver Assistance
                    new VehicleFeature { Name = "Adaptive Cruise Control", Code = "ACC", Description = "Adaptive cruise control with traffic assist", VehicleTypeId = carTypeId, IsActive = true },
                    new VehicleFeature { Name = "Lane Keep Assist", Code = "LKA", Description = "Lane keeping and departure warning", VehicleTypeId = carTypeId, IsActive = true },
                    new VehicleFeature { Name = "Blind Spot Monitor", Code = "BSM", Description = "Blind spot monitoring system", VehicleTypeId = carTypeId, IsActive = true },
                    new VehicleFeature { Name = "360° Camera", Code = "360CAM", Description = "Surround view camera system", VehicleTypeId = carTypeId, IsActive = true },
                    new VehicleFeature { Name = "Reversing Camera", Code = "REV_CAMERA", Description = "Rear-view parking camera", VehicleTypeId = carTypeId, IsActive = true },
                    new VehicleFeature { Name = "Front & Rear Sensors", Code = "PARK_SENSOR", Description = "Parking sensors all round", VehicleTypeId = carTypeId, IsActive = true },
                    new VehicleFeature { Name = "Night Vision", Code = "NIGHT_VISION", Description = "Night vision assistance", VehicleTypeId = carTypeId, IsActive = true },
                    new VehicleFeature { Name = "Head-Up Display", Code = "HUD", Description = "Head-up display", VehicleTypeId = carTypeId, IsActive = true },
                    
                    // Interior Comfort
                    new VehicleFeature { Name = "Leather Interior", Code = "LEATHER", Description = "Full leather upholstery", VehicleTypeId = carTypeId, IsActive = true },
                    new VehicleFeature { Name = "Heated Front Seats", Code = "HEATED_FRONT", Description = "Heated front seats", VehicleTypeId = carTypeId, IsActive = true },
                    new VehicleFeature { Name = "Heated Rear Seats", Code = "HEATED_REAR", Description = "Heated rear seats", VehicleTypeId = carTypeId, IsActive = true },
                    new VehicleFeature { Name = "Ventilated Seats", Code = "VENT_SEATS", Description = "Cooled and ventilated seats", VehicleTypeId = carTypeId, IsActive = true },
                    new VehicleFeature { Name = "Massage Seats", Code = "MASSAGE", Description = "Multi-contour massage seats", VehicleTypeId = carTypeId, IsActive = true },
                    new VehicleFeature { Name = "Memory Seats", Code = "MEMORY_SEATS", Description = "Driver and passenger memory seats", VehicleTypeId = carTypeId, IsActive = true },
                    new VehicleFeature { Name = "Electric Seats", Code = "ELECTRIC_SEATS", Description = "Fully electric adjustable seats", VehicleTypeId = carTypeId, IsActive = true },
                    new VehicleFeature { Name = "Heated Steering Wheel", Code = "HEATED_WHEEL", Description = "Heated steering wheel", VehicleTypeId = carTypeId, IsActive = true },
                    new VehicleFeature { Name = "4-Zone Climate", Code = "4ZONE_CLIMATE", Description = "Four-zone climate control", VehicleTypeId = carTypeId, IsActive = true },
                    new VehicleFeature { Name = "Air Suspension", Code = "AIR_SUSP", Description = "Adaptive air suspension", VehicleTypeId = carTypeId, IsActive = true },
                    
                    // Entertainment & Technology
                    new VehicleFeature { Name = "Sat Nav", Code = "SATNAV", Description = "Satellite navigation system", VehicleTypeId = carTypeId, IsActive = true },
                    new VehicleFeature { Name = "Apple CarPlay", Code = "CARPLAY", Description = "Apple CarPlay connectivity", VehicleTypeId = carTypeId, IsActive = true },
                    new VehicleFeature { Name = "Android Auto", Code = "ANDROID_AUTO", Description = "Android Auto connectivity", VehicleTypeId = carTypeId, IsActive = true },
                    new VehicleFeature { Name = "Bluetooth", Code = "BLUETOOTH", Description = "Bluetooth phone connectivity", VehicleTypeId = carTypeId, IsActive = true },
                    new VehicleFeature { Name = "WiFi Hotspot", Code = "WIFI", Description = "In-car WiFi hotspot", VehicleTypeId = carTypeId, IsActive = true },
                    new VehicleFeature { Name = "Premium Sound System", Code = "PREMIUM_AUDIO", Description = "High-end audio system", VehicleTypeId = carTypeId, IsActive = true },
                    new VehicleFeature { Name = "Rear Entertainment", Code = "REAR_ENT", Description = "Rear seat entertainment screens", VehicleTypeId = carTypeId, IsActive = true },
                    new VehicleFeature { Name = "Wireless Charging", Code = "WIRELESS_CHARGE", Description = "Wireless phone charging", VehicleTypeId = carTypeId, IsActive = true },
                    
                    // Exterior Features
                    new VehicleFeature { Name = "Panoramic Roof", Code = "PANOROOF", Description = "Panoramic glass roof", VehicleTypeId = carTypeId, IsActive = true },
                    new VehicleFeature { Name = "Sunroof", Code = "SUNROOF", Description = "Electric sunroof", VehicleTypeId = carTypeId, IsActive = true },
                    new VehicleFeature { Name = "LED Headlights", Code = "LED_LIGHTS", Description = "LED headlight system", VehicleTypeId = carTypeId, IsActive = true },
                    new VehicleFeature { Name = "Matrix LED", Code = "MATRIX_LED", Description = "Adaptive matrix LED lights", VehicleTypeId = carTypeId, IsActive = true },
                    new VehicleFeature { Name = "Laser Lights", Code = "LASER_LIGHTS", Description = "Laser headlight technology", VehicleTypeId = carTypeId, IsActive = true },
                    new VehicleFeature { Name = "Privacy Glass", Code = "PRIVACY_GLASS", Description = "Rear privacy glass", VehicleTypeId = carTypeId, IsActive = true },
                    new VehicleFeature { Name = "Electric Boot", Code = "ELEC_BOOT", Description = "Hands-free electric tailgate", VehicleTypeId = carTypeId, IsActive = true },
                    new VehicleFeature { Name = "Tow Bar", Code = "TOW_BAR", Description = "Electric deployable tow bar", VehicleTypeId = carTypeId, IsActive = true },
                    
                    // Performance Features
                    new VehicleFeature { Name = "4-Wheel Drive", Code = "4WD", Description = "Four-wheel drive system", VehicleTypeId = carTypeId, IsActive = true },
                    new VehicleFeature { Name = "Sport Exhaust", Code = "SPORT_EXHAUST", Description = "Sport exhaust system", VehicleTypeId = carTypeId, IsActive = true },
                    new VehicleFeature { Name = "Launch Control", Code = "LAUNCH_CTRL", Description = "Electronic launch control", VehicleTypeId = carTypeId, IsActive = true },
                    new VehicleFeature { Name = "Sport Mode", Code = "SPORT_MODE", Description = "Selectable drive modes", VehicleTypeId = carTypeId, IsActive = true },
                    new VehicleFeature { Name = "Active Exhaust", Code = "ACTIVE_EXHAUST", Description = "Valve-controlled active exhaust", VehicleTypeId = carTypeId, IsActive = true },
                    
                    // Convenience
                    new VehicleFeature { Name = "Keyless Entry", Code = "KEYLESS_ENTRY", Description = "Keyless entry and start", VehicleTypeId = carTypeId, IsActive = true },
                    new VehicleFeature { Name = "Electric Doors", Code = "ELEC_DOORS", Description = "Power closing doors", VehicleTypeId = carTypeId, IsActive = true },
                    new VehicleFeature { Name = "Ambient Lighting", Code = "AMBIENT_LIGHT", Description = "Multi-colour ambient lighting", VehicleTypeId = carTypeId, IsActive = true },
                    new VehicleFeature { Name = "Fridge", Code = "FRIDGE", Description = "Built-in refrigerator", VehicleTypeId = carTypeId, IsActive = true },
                    new VehicleFeature { Name = "Champagne Flutes", Code = "CHAMPAGNE", Description = "Champagne flutes and holder", VehicleTypeId = carTypeId, IsActive = true },
                    
                    // ===== JET FEATURES (25+) =====
                    // Cabin Amenities
                    new VehicleFeature { Name = "WiFi", Code = "JET_WIFI", Description = "High-speed in-flight WiFi", VehicleTypeId = jetTypeId, IsActive = true },
                    new VehicleFeature { Name = "Satcom", Code = "SATCOM", Description = "Satellite communications", VehicleTypeId = jetTypeId, IsActive = true },
                    new VehicleFeature { Name = "Entertainment System", Code = "JET_ENT", Description = "Premium cabin entertainment", VehicleTypeId = jetTypeId, IsActive = true },
                    new VehicleFeature { Name = "Conference Table", Code = "CONF_TABLE", Description = "Executive conference table", VehicleTypeId = jetTypeId, IsActive = true },
                    new VehicleFeature { Name = "Divan", Code = "DIVAN", Description = "Convertible divan seating", VehicleTypeId = jetTypeId, IsActive = true },
                    new VehicleFeature { Name = "Club Seating", Code = "CLUB_SEATS", Description = "Four-place club seating", VehicleTypeId = jetTypeId, IsActive = true },
                    new VehicleFeature { Name = "Fully-Berthing", Code = "BERTHING", Description = "Fully-berthing seats", VehicleTypeId = jetTypeId, IsActive = true },
                    
                    // Sleeping & Private Areas
                    new VehicleFeature { Name = "Master Bedroom", Code = "MASTER_BED", Description = "Private master stateroom", VehicleTypeId = jetTypeId, IsActive = true },
                    new VehicleFeature { Name = "Double Bed", Code = "DOUBLE_BED", Description = "Full-size double bed", VehicleTypeId = jetTypeId, IsActive = true },
                    new VehicleFeature { Name = "Guest Bedroom", Code = "GUEST_BED", Description = "Separate guest bedroom", VehicleTypeId = jetTypeId, IsActive = true },
                    new VehicleFeature { Name = "Stand-Up Shower", Code = "SHOWER", Description = "Stand-up shower facility", VehicleTypeId = jetTypeId, IsActive = true },
                    new VehicleFeature { Name = "Private Lavatory", Code = "PRIVATE_LAV", Description = "Private lavatory with sink", VehicleTypeId = jetTypeId, IsActive = true },
                    new VehicleFeature { Name = "Forward Lavatory", Code = "FWD_LAV", Description = "Forward cabin lavatory", VehicleTypeId = jetTypeId, IsActive = true },
                    new VehicleFeature { Name = "Aft Lavatory", Code = "AFT_LAV", Description = "Aft cabin lavatory", VehicleTypeId = jetTypeId, IsActive = true },
                    
                    // Galley & Dining
                    new VehicleFeature { Name = "Full Galley", Code = "FULL_GALLEY", Description = "Fully-equipped galley", VehicleTypeId = jetTypeId, IsActive = true },
                    new VehicleFeature { Name = "Oven", Code = "OVEN", Description = "Convection oven", VehicleTypeId = jetTypeId, IsActive = true },
                    new VehicleFeature { Name = "Microwave", Code = "MICROWAVE", Description = "Built-in microwave", VehicleTypeId = jetTypeId, IsActive = true },
                    new VehicleFeature { Name = "Coffee Maker", Code = "COFFEE", Description = "Espresso coffee maker", VehicleTypeId = jetTypeId, IsActive = true },
                    new VehicleFeature { Name = "Ice Maker", Code = "ICE_MAKER", Description = "Automatic ice maker", VehicleTypeId = jetTypeId, IsActive = true },
                    
                    // Technology & Performance
                    new VehicleFeature { Name = "Synthetic Vision", Code = "SYNTH_VISION", Description = "Synthetic vision system", VehicleTypeId = jetTypeId, IsActive = true },
                    new VehicleFeature { Name = "HUD", Code = "JET_HUD", Description = "Head-up display system", VehicleTypeId = jetTypeId, IsActive = true },
                    new VehicleFeature { Name = "Enhanced Vision", Code = "EVS", Description = "Enhanced vision system", VehicleTypeId = jetTypeId, IsActive = true },
                    new VehicleFeature { Name = "Winglets", Code = "WINGLETS", Description = "Fuel-saving winglets", VehicleTypeId = jetTypeId, IsActive = true },
                    new VehicleFeature { Name = "APU", Code = "APU", Description = "Auxiliary power unit", VehicleTypeId = jetTypeId, IsActive = true },
                    new VehicleFeature { Name = "Air Stair", Code = "AIR_STAIR", Description = "Built-in air stair", VehicleTypeId = jetTypeId, IsActive = true },
                    
                    // ===== YACHT FEATURES (35+) =====
                    // Deck & Exterior
                    new VehicleFeature { Name = "Jacuzzi", Code = "JACUZZI", Description = "On-deck jacuzzi spa", VehicleTypeId = yachtTypeId, IsActive = true },
                    new VehicleFeature { Name = "Swim Platform", Code = "SWIM_PLATFORM", Description = "Hydraulic swim platform", VehicleTypeId = yachtTypeId, IsActive = true },
                    new VehicleFeature { Name = "Beach Club", Code = "BEACH_CLUB", Description = "Fold-down beach club", VehicleTypeId = yachtTypeId, IsActive = true },
                    new VehicleFeature { Name = "Sun Pads", Code = "SUN_PADS", Description = "Foredeck sun loungers", VehicleTypeId = yachtTypeId, IsActive = true },
                    new VehicleFeature { Name = "Flybridge", Code = "FLYBRIDGE", Description = "Upper flybridge deck", VehicleTypeId = yachtTypeId, IsActive = true },
                    new VehicleFeature { Name = "Aft Deck Dining", Code = "AFT_DINING", Description = "Aft deck dining area", VehicleTypeId = yachtTypeId, IsActive = true },
                    new VehicleFeature { Name = "BBQ Grill", Code = "BBQ", Description = "Built-in BBQ grill", VehicleTypeId = yachtTypeId, IsActive = true },
                    new VehicleFeature { Name = "Wet Bar", Code = "WET_BAR", Description = "Exterior wet bar", VehicleTypeId = yachtTypeId, IsActive = true },
                    new VehicleFeature { Name = "Passerelle", Code = "PASSERELLE", Description = "Hydraulic passerelle gangway", VehicleTypeId = yachtTypeId, IsActive = true },
                    
                    // Water Toys & Tenders
                    new VehicleFeature { Name = "Jet Ski", Code = "JET_SKI", Description = "Jet ski included", VehicleTypeId = yachtTypeId, IsActive = true },
                    new VehicleFeature { Name = "Tender", Code = "TENDER", Description = "Tender boat included", VehicleTypeId = yachtTypeId, IsActive = true },
                    new VehicleFeature { Name = "SeaBobs", Code = "SEABOBS", Description = "Underwater SeaBobs", VehicleTypeId = yachtTypeId, IsActive = true },
                    new VehicleFeature { Name = "Paddleboards", Code = "SUP", Description = "Stand-up paddleboards", VehicleTypeId = yachtTypeId, IsActive = true },
                    new VehicleFeature { Name = "Kayaks", Code = "KAYAKS", Description = "Kayaks included", VehicleTypeId = yachtTypeId, IsActive = true },
                    new VehicleFeature { Name = "Diving Equipment", Code = "DIVE_GEAR", Description = "Full diving equipment", VehicleTypeId = yachtTypeId, IsActive = true },
                    new VehicleFeature { Name = "Snorkeling Gear", Code = "SNORKEL", Description = "Snorkeling equipment", VehicleTypeId = yachtTypeId, IsActive = true },
                    new VehicleFeature { Name = "Fishing Equipment", Code = "FISHING", Description = "Sport fishing tackle", VehicleTypeId = yachtTypeId, IsActive = true },
                    new VehicleFeature { Name = "Inflatable Toys", Code = "INFLATABLES", Description = "Water inflatable toys", VehicleTypeId = yachtTypeId, IsActive = true },
                    
                    // Interior Amenities
                    new VehicleFeature { Name = "Cinema Room", Code = "CINEMA", Description = "Dedicated cinema room", VehicleTypeId = yachtTypeId, IsActive = true },
                    new VehicleFeature { Name = "Gym", Code = "GYM", Description = "Onboard gymnasium", VehicleTypeId = yachtTypeId, IsActive = true },
                    new VehicleFeature { Name = "Spa", Code = "SPA", Description = "Beauty spa and massage", VehicleTypeId = yachtTypeId, IsActive = true },
                    new VehicleFeature { Name = "Sauna", Code = "SAUNA", Description = "Traditional sauna", VehicleTypeId = yachtTypeId, IsActive = true },
                    new VehicleFeature { Name = "Steam Room", Code = "STEAM", Description = "Steam shower room", VehicleTypeId = yachtTypeId, IsActive = true },
                    new VehicleFeature { Name = "Wine Cellar", Code = "WINE_CELLAR", Description = "Climate-controlled wine cellar", VehicleTypeId = yachtTypeId, IsActive = true },
                    new VehicleFeature { Name = "Piano", Code = "PIANO", Description = "Grand or baby grand piano", VehicleTypeId = yachtTypeId, IsActive = true },
                    
                    // Technology & Systems
                    new VehicleFeature { Name = "Satellite WiFi", Code = "SAT_WIFI", Description = "High-speed satellite WiFi", VehicleTypeId = yachtTypeId, IsActive = true },
                    new VehicleFeature { Name = "Stabilisers", Code = "STABILISERS", Description = "Zero-speed stabilizers", VehicleTypeId = yachtTypeId, IsActive = true },
                    new VehicleFeature { Name = "Bow Thruster", Code = "BOW_THRUSTER", Description = "Bow thruster for maneuvering", VehicleTypeId = yachtTypeId, IsActive = true },
                    new VehicleFeature { Name = "Stern Thruster", Code = "STERN_THRUSTER", Description = "Stern thruster", VehicleTypeId = yachtTypeId, IsActive = true },
                    new VehicleFeature { Name = "Watermaker", Code = "WATERMAKER", Description = "Reverse osmosis watermaker", VehicleTypeId = yachtTypeId, IsActive = true },
                    new VehicleFeature { Name = "Generator", Code = "GENERATOR", Description = "High-capacity generators", VehicleTypeId = yachtTypeId, IsActive = true },
                    new VehicleFeature { Name = "Air Conditioning", Code = "AIRCON", Description = "Full air conditioning", VehicleTypeId = yachtTypeId, IsActive = true },
                    new VehicleFeature { Name = "Heating", Code = "HEATING", Description = "Central heating system", VehicleTypeId = yachtTypeId, IsActive = true },
                    new VehicleFeature { Name = "Underwater Lights", Code = "UNDERWATER_LIGHTS", Description = "LED underwater lighting", VehicleTypeId = yachtTypeId, IsActive = true },
                    
                    // ===== BOAT FEATURES (20+) =====
                    new VehicleFeature { Name = "GPS Chartplotter", Code = "GPS_CHART", Description = "GPS chartplotter system", VehicleTypeId = boatTypeId, IsActive = true },
                    new VehicleFeature { Name = "VHF Radio", Code = "VHF", Description = "Marine VHF radio", VehicleTypeId = boatTypeId, IsActive = true },
                    new VehicleFeature { Name = "Fish Finder", Code = "FISH_FINDER", Description = "Sonar fish finder", VehicleTypeId = boatTypeId, IsActive = true },
                    new VehicleFeature { Name = "Radar", Code = "RADAR", Description = "Marine radar system", VehicleTypeId = boatTypeId, IsActive = true },
                    new VehicleFeature { Name = "AIS", Code = "AIS", Description = "AIS transponder", VehicleTypeId = boatTypeId, IsActive = true },
                    new VehicleFeature { Name = "Autopilot", Code = "AUTOPILOT", Description = "Marine autopilot", VehicleTypeId = boatTypeId, IsActive = true },
                    new VehicleFeature { Name = "Sound System", Code = "SOUND", Description = "Marine sound system", VehicleTypeId = boatTypeId, IsActive = true },
                    new VehicleFeature { Name = "Bluetooth Audio", Code = "BT_AUDIO", Description = "Bluetooth audio streaming", VehicleTypeId = boatTypeId, IsActive = true },
                    new VehicleFeature { Name = "Bimini Top", Code = "BIMINI", Description = "Sunshade bimini top", VehicleTypeId = boatTypeId, IsActive = true },
                    new VehicleFeature { Name = "Sprayhood", Code = "SPRAYHOOD", Description = "Sprayhood canopy", VehicleTypeId = boatTypeId, IsActive = true },
                    new VehicleFeature { Name = "Electric Anchor", Code = "ELEC_ANCHOR", Description = "Electric anchor windlass", VehicleTypeId = boatTypeId, IsActive = true },
                    new VehicleFeature { Name = "Bow Thruster", Code = "BOAT_BOW", Description = "Bow thruster", VehicleTypeId = boatTypeId, IsActive = true },
                    new VehicleFeature { Name = "Trim Tabs", Code = "TRIM_TABS", Description = "Hydraulic trim tabs", VehicleTypeId = boatTypeId, IsActive = true },
                    new VehicleFeature { Name = "Live Well", Code = "LIVE_WELL", Description = "Aerated live well", VehicleTypeId = boatTypeId, IsActive = true },
                    new VehicleFeature { Name = "Rod Holders", Code = "ROD_HOLDERS", Description = "Fishing rod holders", VehicleTypeId = boatTypeId, IsActive = true },
                    new VehicleFeature { Name = "Cockpit Table", Code = "COCKPIT_TABLE", Description = "Removable cockpit table", VehicleTypeId = boatTypeId, IsActive = true },
                    new VehicleFeature { Name = "Sun Cushions", Code = "SUN_CUSHIONS", Description = "Forward sun cushions", VehicleTypeId = boatTypeId, IsActive = true },
                    new VehicleFeature { Name = "Swim Ladder", Code = "SWIM_LADDER", Description = "Boarding swim ladder", VehicleTypeId = boatTypeId, IsActive = true },
                    new VehicleFeature { Name = "Shore Power", Code = "SHORE_POWER", Description = "Shore power connection", VehicleTypeId = boatTypeId, IsActive = true },
                    new VehicleFeature { Name = "Battery Charger", Code = "BATT_CHARGER", Description = "Battery charging system", VehicleTypeId = boatTypeId, IsActive = true }
                };
                context.VehicleFeatures.AddRange(features);
                context.SaveChanges();
            }

            // ============================================
            // DOCUMENT TYPES - UK Specific
            // ============================================
            if (!context.DocumentTypes.Any())
            {
                var documentTypes = new[]
                {
                    // Vehicle Documents - Cars
                    new DocumentType { Name = "V5C Registration", Code = "V5C", Description = "Vehicle registration certificate (logbook)", EntityType = "Vehicle", IsRequired = true, RequiresExpiry = false, IsActive = true },
                    new DocumentType { Name = "MOT Certificate", Code = "MOT", Description = "Ministry of Transport test certificate", EntityType = "Vehicle", IsRequired = true, RequiresExpiry = true, IsActive = true },
                    new DocumentType { Name = "Motor Insurance", Code = "CAR_INS", Description = "Comprehensive motor insurance policy", EntityType = "Vehicle", IsRequired = true, RequiresExpiry = true, IsActive = true },
                    new DocumentType { Name = "Road Tax", Code = "ROAD_TAX", Description = "Vehicle road tax (VED)", EntityType = "Vehicle", IsRequired = true, RequiresExpiry = true, IsActive = true },
                    new DocumentType { Name = "Service History", Code = "SERVICE_HIST", Description = "Full service history", EntityType = "Vehicle", IsRequired = false, RequiresExpiry = false, IsActive = true },
                    
                    // Vehicle Documents - Aircraft
                    new DocumentType { Name = "Certificate of Airworthiness", Code = "COA", Description = "CAA certificate of airworthiness", EntityType = "Vehicle", IsRequired = true, RequiresExpiry = true, IsActive = true },
                    new DocumentType { Name = "Aircraft Registration", Code = "AIRCRAFT_REG", Description = "G-registration certificate", EntityType = "Vehicle", IsRequired = true, RequiresExpiry = false, IsActive = true },
                    new DocumentType { Name = "Aircraft Insurance", Code = "AIRCRAFT_INS", Description = "Hull and liability insurance", EntityType = "Vehicle", IsRequired = true, RequiresExpiry = true, IsActive = true },
                    new DocumentType { Name = "Noise Certificate", Code = "NOISE_CERT", Description = "Aircraft noise certificate", EntityType = "Vehicle", IsRequired = false, RequiresExpiry = true, IsActive = true },
                    new DocumentType { Name = "Maintenance Log", Code = "MAINT_LOG", Description = "Aircraft maintenance logbook", EntityType = "Vehicle", IsRequired = true, RequiresExpiry = false, IsActive = true },
                    
                    // Vehicle Documents - Marine
                    new DocumentType { Name = "Small Ships Register", Code = "SSR", Description = "Part I or Part III registration", EntityType = "Vehicle", IsRequired = true, RequiresExpiry = false, IsActive = true },
                    new DocumentType { Name = "Marine Insurance", Code = "MARINE_INS", Description = "Yacht/boat insurance policy", EntityType = "Vehicle", IsRequired = true, RequiresExpiry = true, IsActive = true },
                    new DocumentType { Name = "Boat Safety Certificate", Code = "BSS", Description = "Boat Safety Scheme certificate", EntityType = "Vehicle", IsRequired = false, RequiresExpiry = true, IsActive = true },
                    new DocumentType { Name = "Survey Report", Code = "SURVEY", Description = "Marine survey report", EntityType = "Vehicle", IsRequired = false, RequiresExpiry = false, IsActive = true },
                    new DocumentType { Name = "Radio Licence", Code = "RADIO_LIC", Description = "Ship radio licence", EntityType = "Vehicle", IsRequired = false, RequiresExpiry = true, IsActive = true },
                    
                    // User Documents
                    new DocumentType { Name = "UK Driving Licence", Code = "UK_DRIVING", Description = "Full UK driving licence", EntityType = "User", IsRequired = true, RequiresExpiry = true, IsActive = true },
                    new DocumentType { Name = "International Driving Permit", Code = "IDP", Description = "International driving permit", EntityType = "User", IsRequired = false, RequiresExpiry = true, IsActive = true },
                    new DocumentType { Name = "Passport", Code = "PASSPORT", Description = "Valid passport", EntityType = "User", IsRequired = true, RequiresExpiry = true, IsActive = true },
                    new DocumentType { Name = "Proof of Address", Code = "ADDRESS_PROOF", Description = "Council tax or utility bill", EntityType = "User", IsRequired = false, RequiresExpiry = false, IsActive = true },
                    new DocumentType { Name = "National Insurance", Code = "NI_NUMBER", Description = "National Insurance number", EntityType = "User", IsRequired = false, RequiresExpiry = false, IsActive = true },
                    
                    // Professional Licences
                    new DocumentType { Name = "PPL", Code = "PPL", Description = "Private pilot licence", EntityType = "User", IsRequired = false, RequiresExpiry = true, IsActive = true },
                    new DocumentType { Name = "ATPL", Code = "ATPL", Description = "Airline transport pilot licence", EntityType = "User", IsRequired = false, RequiresExpiry = true, IsActive = true },
                    new DocumentType { Name = "RYA Day Skipper", Code = "RYA_DS", Description = "RYA Day Skipper certificate", EntityType = "User", IsRequired = false, RequiresExpiry = false, IsActive = true },
                    new DocumentType { Name = "RYA Yachtmaster", Code = "RYA_YM", Description = "RYA Yachtmaster qualification", EntityType = "User", IsRequired = false, RequiresExpiry = false, IsActive = true },
                    new DocumentType { Name = "SRC Certificate", Code = "SRC", Description = "Short range radio certificate", EntityType = "User", IsRequired = false, RequiresExpiry = false, IsActive = true }
                };
                context.DocumentTypes.AddRange(documentTypes);
                context.SaveChanges();
            }

            // ============================================
            // SYSTEM SETTINGS - UK Configuration
            // ============================================
            if (!context.SystemSettings.Any())
            {
                var systemSettings = new[]
                {
                    // Payment Settings
                    new SystemSetting { Key = "PLATFORM_FEE_PERCENTAGE", Value = "15", DataType = "Decimal", Category = "Payment", Description = "Platform commission percentage", IsEditable = true, IsActive = true },
                    new SystemSetting { Key = "VAT_RATE", Value = "20", DataType = "Decimal", Category = "Payment", Description = "UK VAT rate percentage", IsEditable = true, IsActive = true },
                    new SystemSetting { Key = "SECURITY_DEPOSIT_PERCENTAGE", Value = "20", DataType = "Decimal", Category = "Payment", Description = "Security deposit as % of booking", IsEditable = true, IsActive = true },
                    new SystemSetting { Key = "DEFAULT_CURRENCY", Value = "GBP", DataType = "String", Category = "Payment", Description = "Default currency - British Pound", IsEditable = true, IsActive = true },
                    
                    // Booking Settings
                    new SystemSetting { Key = "MIN_BOOKING_HOURS_CAR", Value = "4", DataType = "Integer", Category = "Booking", Description = "Minimum car rental hours", IsEditable = true, IsActive = true },
                    new SystemSetting { Key = "MIN_BOOKING_HOURS_JET", Value = "1", DataType = "Integer", Category = "Booking", Description = "Minimum jet charter hours", IsEditable = true, IsActive = true },
                    new SystemSetting { Key = "MIN_BOOKING_DAYS_YACHT", Value = "3", DataType = "Integer", Category = "Booking", Description = "Minimum yacht charter days", IsEditable = true, IsActive = true },
                    new SystemSetting { Key = "CANCELLATION_HOURS", Value = "48", DataType = "Integer", Category = "Booking", Description = "Free cancellation period (hours)", IsEditable = true, IsActive = true },
                    new SystemSetting { Key = "BOOKING_ADVANCE_DAYS", Value = "1", DataType = "Integer", Category = "Booking", Description = "Minimum advance booking (days)", IsEditable = true, IsActive = true },
                    
                    // Stripe Integration
                    new SystemSetting { Key = "STRIPE_PUBLIC_KEY", Value = "", DataType = "String", Category = "Payment", Description = "Stripe publishable key", IsEditable = true, IsActive = true },
                    new SystemSetting { Key = "STRIPE_SECRET_KEY", Value = "", DataType = "String", Category = "Payment", Description = "Stripe secret key", IsEditable = false, IsActive = true },
                    new SystemSetting { Key = "STRIPE_WEBHOOK_SECRET", Value = "", DataType = "String", Category = "Payment", Description = "Stripe webhook signing secret", IsEditable = false, IsActive = true },
                    
                    // Email Configuration
                    new SystemSetting { Key = "EMAIL_FROM", Value = "noreply@itirr.co.uk", DataType = "String", Category = "Email", Description = "System sender email", IsEditable = true, IsActive = true },
                    new SystemSetting { Key = "EMAIL_FROM_NAME", Value = "ITIRR Luxury Rentals", DataType = "String", Category = "Email", Description = "Sender display name", IsEditable = true, IsActive = true },
                    new SystemSetting { Key = "SUPPORT_EMAIL", Value = "support@itirr.co.uk", DataType = "String", Category = "Email", Description = "Customer support email", IsEditable = true, IsActive = true },
                    new SystemSetting { Key = "ADMIN_EMAIL", Value = "admin@itirr.co.uk", DataType = "String", Category = "Email", Description = "Admin notifications email", IsEditable = true, IsActive = true },
                    
                    // SMS Configuration
                    new SystemSetting { Key = "SMS_ENABLED", Value = "false", DataType = "Boolean", Category = "SMS", Description = "Enable SMS notifications", IsEditable = true, IsActive = true },
                    new SystemSetting { Key = "SMS_PROVIDER", Value = "Twilio", DataType = "String", Category = "SMS", Description = "SMS provider service", IsEditable = true, IsActive = true },
                    
                    // System Behaviour
                    new SystemSetting { Key = "ADMIN_APPROVAL_REQUIRED", Value = "true", DataType = "Boolean", Category = "System", Description = "Require admin approval for hosts", IsEditable = true, IsActive = true },
                    new SystemSetting { Key = "AUTO_APPROVE_VERIFIED_HOSTS", Value = "false", DataType = "Boolean", Category = "System", Description = "Auto-approve verified hosts", IsEditable = true, IsActive = true },
                    new SystemSetting { Key = "VEHICLE_APPROVAL_REQUIRED", Value = "true", DataType = "Boolean", Category = "System", Description = "Require admin vehicle approval", IsEditable = true, IsActive = true },
                    new SystemSetting { Key = "DOCUMENT_EXPIRY_WARNING_DAYS", Value = "30", DataType = "Integer", Category = "System", Description = "Document expiry warning (days)", IsEditable = true, IsActive = true },
                    
                    // UK Specific
                    new SystemSetting { Key = "REQUIRE_MOT", Value = "true", DataType = "Boolean", Category = "Compliance", Description = "Require valid MOT certificate", IsEditable = true, IsActive = true },
                    new SystemSetting { Key = "REQUIRE_ROAD_TAX", Value = "true", DataType = "Boolean", Category = "Compliance", Description = "Require valid road tax", IsEditable = true, IsActive = true },
                    new SystemSetting { Key = "MIN_AGE_LUXURY_CAR", Value = "25", DataType = "Integer", Category = "Compliance", Description = "Minimum age for luxury cars", IsEditable = true, IsActive = true },
                    new SystemSetting { Key = "MIN_AGE_SUPERCAR", Value = "30", DataType = "Integer", Category = "Compliance", Description = "Minimum age for supercars", IsEditable = true, IsActive = true }
                };
                context.SystemSettings.AddRange(systemSettings);
                context.SaveChanges();
            }
        }


        // ============================================
        // CREATE DEFAULT ADMIN USER
        // ============================================
        public static async Task CreateAdminUser(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            // Create roles if they don't exist
            string[] roles = { "Admin", "Host", "Renter" };
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            // Create default admin user
            var adminEmail = "admin@itirr.co.uk";
            var adminUser = await userManager.FindByEmailAsync(adminEmail);

            if (adminUser == null)
            {
                adminUser = new ApplicationUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true,
                    FirstName = "Admin",
                    LastName = "ITIRR",
                    UserType = "Admin",
                    AccountType = "Individual",
                    IsVerified = true,
                    PhoneNumberConfirmed = true,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                var result = await userManager.CreateAsync(adminUser, "Admin@123");

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                    Console.WriteLine("✅ Admin user created successfully!");
                    Console.WriteLine($"📧 Email: {adminEmail}");
                    Console.WriteLine($"🔑 Password: Admin@123");
                }
                else
                {
                    Console.WriteLine("❌ Failed to create admin user:");
                    foreach (var error in result.Errors)
                    {
                        Console.WriteLine($"  - {error.Description}");
                    }
                }
            }
            else
            {
                Console.WriteLine("ℹ️  Admin user already exists");
            }
        }


    }
}