using ITIRR.Core.Entities;
using ITIRR.Core.Interfaces;
using ITIRR.Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace ITIRR.Services.Services
{
    public class LogService : ILogService
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LogService(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task LogInformationAsync(string userId, string action, string entityType, Guid? entityId, string? notes = null)
        {
            await CreateLogAsync(userId, action, entityType, entityId, null, null, notes);
        }

        public async Task LogErrorAsync(string userId, string action, string entityType, Guid? entityId, string errorMessage, string? stackTrace = null)
        {
            var notes = $"Error: {errorMessage}";
            if (!string.IsNullOrEmpty(stackTrace))
            {
                notes += $"\nStack Trace: {stackTrace}";
            }
            await CreateLogAsync(userId, action, entityType, entityId, null, null, notes);
        }

        public async Task LogWarningAsync(string userId, string action, string entityType, Guid? entityId, string? notes = null)
        {
            await CreateLogAsync(userId, action, entityType, entityId, null, null, notes);
        }

        public async Task LogEntityChangeAsync(string userId, string action, string entityType, Guid entityId, string? oldValue, string? newValue, string? notes = null)
        {
            await CreateLogAsync(userId, action, entityType, entityId, oldValue, newValue, notes);
        }

        private async Task CreateLogAsync(string userId, string action, string entityType, Guid? entityId, string? oldValue, string? newValue, string? notes)
        {
            try
            {
                var httpContext = _httpContextAccessor.HttpContext;
                var ipAddress = httpContext?.Connection?.RemoteIpAddress?.ToString() ?? "Unknown";
                var userAgent = httpContext?.Request?.Headers["User-Agent"].ToString();

                var log = new AuditLog
                {
                    UserId = userId,
                    Action = action,
                    EntityType = entityType,
                    EntityId = entityId,
                    OldValue = oldValue,
                    NewValue = newValue,
                    IpAddress = ipAddress,
                    UserAgent = userAgent,
                    Notes = notes
                };

                await _context.AuditLogs.AddAsync(log);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                // Silent fail - logging should never break the application
            }
        }
    }
}