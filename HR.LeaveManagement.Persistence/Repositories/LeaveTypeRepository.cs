using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Domain;
using HR.LeaveManagement.Persistence.DatabaseContexts;
using Microsoft.EntityFrameworkCore;

namespace HR.LeaveManagement.Persistence.Repositories
{
    public class LeaveTypeRepository : GenericRepository<LeaveType>, ILeaveTypeRepository
    {
        public LeaveTypeRepository(HRDbContext context) : base(context)
        {
        }

        public async Task<bool> ExistsInDbAsync(int id)
        {
            var exists = await _context.LeaveTypes.AnyAsync(t => t.Id == id);
            return exists;
        }

        public async Task<bool> IsLeaveTypeUnique(string name)
        {
            var entitiesList = await _context.LeaveTypes
                .Where(t => t.Name == name)
                .ToListAsync();

            if (entitiesList.Count > 1)
            {
                return false;
            }

            return true;
        }
    }
}