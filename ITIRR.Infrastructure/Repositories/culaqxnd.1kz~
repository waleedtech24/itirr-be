using ITIRR.Core.Entities.Common;
using ITIRR.Core.Interfaces;
using ITIRR.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text.Json;
using System.Threading.Tasks;

namespace ITIRR.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly ApplicationDbContext _context;
        protected readonly DbSet<T> _dbSet;
        protected readonly ILogService _logService;

        public Repository(ApplicationDbContext context, ILogService logService)
        {
            _context = context;
            _dbSet = _context.Set<T>();
            _logService = logService;
        }

        public async Task<T?> GetByIdAsync(Guid id)
        {
            return await _dbSet.FirstOrDefaultAsync(e => e.Id == id && !e.IsDeleted);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.Where(e => !e.IsDeleted).ToListAsync();
        }

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.Where(e => !e.IsDeleted).Where(predicate).ToListAsync();
        }

        public async Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.Where(e => !e.IsDeleted).FirstOrDefaultAsync(predicate);
        }

        public async Task<T> AddAsync(T entity)
        {
            try
            {
                entity.CreatedAt = DateTime.UtcNow;
                entity.UpdatedAt = DateTime.UtcNow;

                await _dbSet.AddAsync(entity);
                await _context.SaveChangesAsync();

                // Log the action
                await _logService.LogInformationAsync(
                    "System",
                    "Created",
                    typeof(T).Name,
                    entity.Id,
                    $"{typeof(T).Name} created successfully"
                );

                return entity;
            }
            catch (Exception ex)
            {
                await _logService.LogErrorAsync(
                    "System",
                    "Create Failed",
                    typeof(T).Name,
                    null,
                    ex.Message,
                    ex.StackTrace
                );
                throw;
            }
        }

        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
            try
            {
                foreach (var entity in entities)
                {
                    entity.CreatedAt = DateTime.UtcNow;
                    entity.UpdatedAt = DateTime.UtcNow;
                }

                await _dbSet.AddRangeAsync(entities);
                await _context.SaveChangesAsync();

                await _logService.LogInformationAsync(
                    "System",
                    "BulkCreated",
                    typeof(T).Name,
                    null,
                    $"{entities.Count()} {typeof(T).Name} records created"
                );
            }
            catch (Exception ex)
            {
                await _logService.LogErrorAsync(
                    "System",
                    "BulkCreate Failed",
                    typeof(T).Name,
                    null,
                    ex.Message,
                    ex.StackTrace
                );
                throw;
            }
        }

        public async Task UpdateAsync(T entity)
        {
            try
            {
                var oldEntity = await _dbSet.AsNoTracking().FirstOrDefaultAsync(e => e.Id == entity.Id);

                entity.UpdatedAt = DateTime.UtcNow;
                _dbSet.Update(entity);
                await _context.SaveChangesAsync();

                // Log with old and new values
                string? oldValue = oldEntity != null ? JsonSerializer.Serialize(oldEntity) : null;
                string newValue = JsonSerializer.Serialize(entity);

                await _logService.LogEntityChangeAsync(
                    "System",
                    "Updated",
                    typeof(T).Name,
                    entity.Id,
                    oldValue,
                    newValue
                );
            }
            catch (Exception ex)
            {
                await _logService.LogErrorAsync(
                    "System",
                    "Update Failed",
                    typeof(T).Name,
                    entity.Id,
                    ex.Message,
                    ex.StackTrace
                );
                throw;
            }
        }

        public async Task UpdateRangeAsync(IEnumerable<T> entities)
        {
            try
            {
                foreach (var entity in entities)
                {
                    entity.UpdatedAt = DateTime.UtcNow;
                }

                _dbSet.UpdateRange(entities);
                await _context.SaveChangesAsync();

                await _logService.LogInformationAsync(
                    "System",
                    "BulkUpdated",
                    typeof(T).Name,
                    null,
                    $"{entities.Count()} {typeof(T).Name} records updated"
                );
            }
            catch (Exception ex)
            {
                await _logService.LogErrorAsync(
                    "System",
                    "BulkUpdate Failed",
                    typeof(T).Name,
                    null,
                    ex.Message,
                    ex.StackTrace
                );
                throw;
            }
        }

        public async Task DeleteAsync(T entity)
        {
            try
            {
                _dbSet.Remove(entity);
                await _context.SaveChangesAsync();

                await _logService.LogWarningAsync(
                    "System",
                    "PermanentlyDeleted",
                    typeof(T).Name,
                    entity.Id,
                    "Record permanently deleted from database"
                );
            }
            catch (Exception ex)
            {
                await _logService.LogErrorAsync(
                    "System",
                    "Delete Failed",
                    typeof(T).Name,
                    entity.Id,
                    ex.Message,
                    ex.StackTrace
                );
                throw;
            }
        }

        public async Task DeleteRangeAsync(IEnumerable<T> entities)
        {
            try
            {
                _dbSet.RemoveRange(entities);
                await _context.SaveChangesAsync();

                await _logService.LogWarningAsync(
                    "System",
                    "BulkDeleted",
                    typeof(T).Name,
                    null,
                    $"{entities.Count()} records permanently deleted"
                );
            }
            catch (Exception ex)
            {
                await _logService.LogErrorAsync(
                    "System",
                    "BulkDelete Failed",
                    typeof(T).Name,
                    null,
                    ex.Message,
                    ex.StackTrace
                );
                throw;
            }
        }

        public async Task SoftDeleteAsync(Guid id)
        {
            try
            {
                var entity = await _dbSet.FindAsync(id);
                if (entity != null)
                {
                    entity.IsDeleted = true;
                    entity.DeletedAt = DateTime.UtcNow;
                    await _context.SaveChangesAsync();

                    await _logService.LogInformationAsync(
                        "System",
                        "SoftDeleted",
                        typeof(T).Name,
                        id,
                        "Record marked as deleted (soft delete)"
                    );
                }
            }
            catch (Exception ex)
            {
                await _logService.LogErrorAsync(
                    "System",
                    "SoftDelete Failed",
                    typeof(T).Name,
                    id,
                    ex.Message,
                    ex.StackTrace
                );
                throw;
            }
        }

        public async Task<int> CountAsync()
        {
            return await _dbSet.Where(e => !e.IsDeleted).CountAsync();
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.Where(e => !e.IsDeleted).CountAsync(predicate);
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await _dbSet.AnyAsync(e => e.Id == id && !e.IsDeleted);
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.Where(e => !e.IsDeleted).AnyAsync(predicate);
        }
    }
}