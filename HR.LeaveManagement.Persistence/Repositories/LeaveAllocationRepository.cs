using HR.LeaveManagement.Application.Contracts.Logger;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Domain;
using HR.LeaveManagement.Persistence.DatabaseContexts;
using Microsoft.EntityFrameworkCore;

namespace HR.LeaveManagement.Persistence.Repositories
{
    public class LeaveAllocationRepository : GenericRepository<LeaveAllocation>, ILeaveAllocationsRepository
    {
        public LeaveAllocationRepository(HRDbContext context,
                                         IAppLogger<GenericRepository<LeaveAllocation>> appLogger) : base(context, appLogger)
        {
        }

        public async Task<bool> AddAllocations(List<LeaveAllocation> allocations)
        {
            _context.LeaveAllocations.AddRange(allocations);
            var result = await _context.SaveChangesAsync();

            if (result > 0)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> AllocationExists(string employeeId, int leaveTypeId, int year)
        {
            var exists = await _context.LeaveAllocations.AnyAsync(q => q.EmployeeId == employeeId
                                                                       && q.LeaveTypeId == leaveTypeId
                                                                       && q.Year == year);
            return exists;
        }

        public async Task<LeaveAllocation?> GetEmployeeAllocation(string employeeId, int leaveTypeId)
        {
            var allocation = await _context.LeaveAllocations.FirstOrDefaultAsync(q => q.EmployeeId == employeeId
                                                                                       && q.LeaveTypeId == leaveTypeId);
            return allocation;
        }

        public async Task<List<LeaveAllocation>> GetLeaveAllocationsWithDetails()
        {
            var allocations = await _context.LeaveAllocations
                .Include(q => q.LeaveType)
                .ToListAsync();

            return allocations;
        }

        public async Task<List<LeaveAllocation>> GetLeaveAllocationsWithDetails(string employeeId)
        {
            var allocations = await _context.LeaveAllocations
                .Where(q => q.EmployeeId == employeeId)
                .Include(q => q.LeaveType)
                .ToListAsync();

            return allocations;
        }

        public async Task<LeaveAllocation> GetLeaveAllocationWithDetails(int id)
        {
            var allocation = await _context.LeaveAllocations
                .Include(q => q.LeaveType)
                .FirstOrDefaultAsync(q => q.Id == id);

            if (allocation is null)
            {
                _appLogger.LogWarning("Leave Allocation with ID: {0} was not found", id);
                throw new NotFoundException("Allocation was not found");
            }

            return allocation;
        }
    }
}