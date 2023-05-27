using HR.LeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.CreateLeaveAllocation
{
    public class CreateLeaveAllocationDTO
    {
        public int LeaveTypeId { get; set; }

        public int NumberOfDays { get; set; }

        public int Year { get; set; } = (int)DateTime.Now.Year;
    }
}