using HR.LeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Queries.GetAllLeaveAllocations
{
    public class LeaveAllocationDTO
    {
        public int Id { get; set; }

        public LeaveTypeDTO? LeaveType { get; set; }

        public int NumberOfDays { get; set; }

        public int Year { get; set; }
    }
}