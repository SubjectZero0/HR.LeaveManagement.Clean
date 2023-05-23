using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Queries.GetEmployeeAllocation
{
    public record GetEmployeeAllocationQuery(string employeeId, int leaveTypeId) : IRequest<Domain.LeaveAllocation>
    {
    }
}