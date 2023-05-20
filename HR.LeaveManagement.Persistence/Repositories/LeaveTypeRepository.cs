using HR.LeaveManagement.Application.Contracts.Logger;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Domain;
using HR.LeaveManagement.Persistence.DatabaseContexts;
using Microsoft.EntityFrameworkCore;

namespace HR.LeaveManagement.Persistence.Repositories
{
    public class LeaveTypeRepository : GenericRepository<LeaveType>, ILeaveTypeRepository
    {
        public LeaveTypeRepository(HRDbContext context,
                                   IAppLogger<GenericRepository<LeaveType>> appLogger) : base(context, appLogger)
        {
        }

        public async Task<bool> ExistsInDbAsync(int id)
        {
            var exists = await _context.LeaveTypes.AnyAsync(t => t.Id == id);

            if (exists)
            {
                _appLogger.LogInformation("{0} was executed and leave type with ID: {1} exists in Db", nameof(ExistsInDbAsync), id);
            }
            else
            {
                _appLogger.LogWarning("{0} was executed and leave type with ID: {1} does not exist in Db", nameof(ExistsInDbAsync), id);
            }

            return exists;
        }

        public async Task<bool> IsLeaveTypeUnique(string name)
        {
            var entitiesList = await _context.LeaveTypes
                .Where(t => t.Name == name)
                .ToListAsync();

            if (entitiesList.Count > 1)
            {
                _appLogger.LogWarning("{0} was executed and leave type with name: {1} is not unique", nameof(IsLeaveTypeUnique), name);
                return false;
            }

            _appLogger.LogInformation("{0} was executed and leave type with name: {1} is unique", nameof(IsLeaveTypeUnique), name);
            return true;
        }
    }
}