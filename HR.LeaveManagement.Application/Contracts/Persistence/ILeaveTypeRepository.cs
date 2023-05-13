using HR.LeaveManagement.Domain;

namespace HR.LeaveManagement.Application.Contracts.Persistence
{
    public interface ILeaveTypeRepository : IGenericRepository<LeaveType>
    {
        public Task<bool> IsLeaveTypeUnique(string name);

        public Task<bool> ExistsInDbAsync(int id);
    }
}