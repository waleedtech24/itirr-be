using ITIRR.Core.Entities;
using ITIRR.Core.Interfaces;
using ITIRR.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace ITIRR.Services.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public DashboardService(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<DashboardStats> GetDashboardStatsAsync()
        {
            var stats = new DashboardStats
            {
                TotalVehicles = await _context.Vehicles.CountAsync(v => !v.IsDeleted),
                ActiveVehicles = await _context.Vehicles.CountAsync(v => v.IsActive && !v.IsDeleted),
                UnverifiedVehicles = await _context.Vehicles.CountAsync(v => !v.IsVerified && v.IsActive && !v.IsDeleted),
                TotalUsers = await _userManager.Users.CountAsync(),
                TotalCountries = await _context.Countries.CountAsync(c => !c.IsDeleted),
                TotalCities = await _context.Cities.CountAsync(c => !c.IsDeleted),
                TotalBrands = await _context.VehicleBrands.CountAsync(b => !b.IsDeleted),
                TotalCategories = await _context.VehicleCategories.CountAsync(c => !c.IsDeleted)
            };

            // Get role counts
            var hosts = await _userManager.GetUsersInRoleAsync("Host");
            var renters = await _userManager.GetUsersInRoleAsync("Renter");

            stats.TotalHosts = hosts.Count;
            stats.TotalRenters = renters.Count;

            return stats;
        }
    }
}