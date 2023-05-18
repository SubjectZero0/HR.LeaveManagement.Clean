using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveType.Queries.GetLeaveTypeDetails
{
    public record GetLeaveTypeDetailsByIdQuery(int Id) : IRequest<LeaveTypeDetailsDTO>
    {
    }
}