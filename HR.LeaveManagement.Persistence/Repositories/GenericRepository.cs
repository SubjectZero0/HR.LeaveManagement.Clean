using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Persistence.DatabaseContexts;
using Microsoft.EntityFrameworkCore;

namespace HR.LeaveManagement.Persistence.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly HRDbContext _context;

        public GenericRepository(HRDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddAsync(T entity)
        {
            var addedEntity = await _context.Set<T>().AddAsync(entity);

            if (addedEntity is not null)
            {
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<bool> DeleteAsync(T entity)
        {
            var removedEntity = _context.Set<T>().Remove(entity);
            if (removedEntity is not null)
            {
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<T?> GetByIdAsync(int? id)
        {
            var entity = await _context.Set<T>().FindAsync(id);

            if (entity is not null)
            {
                return entity;
            }
            return null;
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<bool> UpdateAsync(T entity)
        {
            var updatedEntity = _context.Set<T>().Update(entity);
            if (updatedEntity is not null)
            {
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}