using HR.LeaveManagement.Domain;

namespace HR.LeaveManagement.Application.Contracts.Persistence
{
    public interface ILeaveAllocationsRepository : IGenericRepository<LeaveAllocation>
    {
        public Task<LeaveAllocation> GetLeaveAllocationWithDetails(int id);

        public Task<List<LeaveAllocation>> GetLeaveAllocationsWithDetails();

        public Task<List<LeaveAllocation>> GetLeaveAllocationsWithDetails(string employeeId);

        public Task<bool> AllocationExists(string employeeId, int leaveTypeId, int year);

        public Task<bool> AddAllocations(List<LeaveAllocation> allocations);

        public Task<LeaveAllocation?> GetEmployeeAllocation(string employeeId, int leaveTypeId);
    }
}