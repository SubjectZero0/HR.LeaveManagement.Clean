using HR.LeaveManagement.Domain;
using System.Security.Claims;

namespace HR.LeaveManagement.Application.Contracts.Persistence
{
    public interface ILeaveRequestRepository : IGenericRepository<LeaveRequest>
    {
    }
}