using System;
using System.Threading.Tasks;

namespace ITIRR.Core.Interfaces
{
    public interface ILogService
    {
        Task LogInformationAsync(string userId, string action, string entityType, Guid? entityId, string? notes = null);
        Task LogErrorAsync(string userId, string action, string entityType, Guid? entityId, string errorMessage, string? stackTrace = null);
        Task LogWarningAsync(string userId, string action, string entityType, Guid? entityId, string? notes = null);
        Task LogEntityChangeAsync(string userId, string action, string entityType, Guid entityId, string? oldValue, string? newValue, string? notes = null);
    }
}