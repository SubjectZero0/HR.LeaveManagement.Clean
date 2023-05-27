using HR.LeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.CreateLeaveAllocation
{
    public class CreateLeaveAllocationCommand : IRequest<Domain.LeaveAllocation>
    {
        public int LeaveTypeId { get; set; }

        public int NumberOfDays { get; set; }

        public int Year { get; set; }

        public string EmployeeId { get; set; }
    }
}