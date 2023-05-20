using HR.LeaveManagement.Application.Contracts.Logger;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Persistence.DatabaseContexts;
using Microsoft.EntityFrameworkCore;

namespace HR.LeaveManagement.Persistence.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly HRDbContext _context;
        protected readonly IAppLogger<GenericRepository<T>> _appLogger;

        public GenericRepository(HRDbContext context, IAppLogger<GenericRepository<T>> appLogger)
        {
            _context = context;
            this._appLogger = appLogger;
        }

        public async Task<bool> AddAsync(T entity)
        {
            var addedEntity = await _context.Set<T>().AddAsync(entity);

            if (addedEntity is not null)
            {
                await _context.SaveChangesAsync();

                _appLogger.LogInformation("{0} was executed successfully", nameof(AddAsync));
                return true;
            }

            _appLogger.LogCritical("{0} failed to execute", nameof(AddAsync));
            return false;
        }

        public async Task<bool> DeleteAsync(T entity)
        {
            var removedEntity = _context.Set<T>().Remove(entity);

            if (removedEntity is not null)
            {
                await _context.SaveChangesAsync();

                _appLogger.LogInformation("{0} was executed successfully", nameof(DeleteAsync));
                return true;
            }

            _appLogger.LogCritical("{0} failed to execute", nameof(DeleteAsync));
            return false;
        }

        public async Task<T?> GetByIdAsync(int? id)
        {
            var entity = await _context.Set<T>().FindAsync(id);

            if (entity is not null)
            {
                _appLogger.LogInformation("{0} was executed successfully and found an instance", nameof(GetByIdAsync));
                return entity;
            }

            _appLogger.LogWarning("{0} was executed successfully and did not find an instance", nameof(GetByIdAsync));
            return null;
        }

        public async Task<List<T>> GetAllAsync()
        {
            _appLogger.LogInformation("{0} was executed successfully", nameof(GetAllAsync));
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<bool> UpdateAsync(T entity)
        {
            var updatedEntity = _context.Set<T>().Update(entity);
            if (updatedEntity is not null)
            {
                await _context.SaveChangesAsync();

                _appLogger.LogInformation("{0} was executed successfully", nameof(UpdateAsync));
                return true;
            }

            _appLogger.LogCritical("{0} failed to execute", nameof(UpdateAsync));
            return false;
        }
    }
}