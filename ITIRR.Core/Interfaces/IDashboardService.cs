using System.Threading.Tasks;

namespace ITIRR.Core.Interfaces
{
    public interface IDashboardService
    {
        Task<DashboardStats> GetDashboardStatsAsync();
    }

    public class DashboardStats
    {
        public int TotalVehicles { get; set; }
        public int ActiveVehicles { get; set; }
        public int UnverifiedVehicles { get; set; }
        public int TotalUsers { get; set; }
        public int TotalHosts { get; set; }
        public int TotalRenters { get; set; }
        public int TotalCountries { get; set; }
        public int TotalCities { get; set; }
        public int TotalBrands { get; set; }
        public int TotalCategories { get; set; }
    }
}