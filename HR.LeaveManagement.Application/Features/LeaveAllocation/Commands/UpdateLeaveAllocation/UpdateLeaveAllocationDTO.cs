namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.UpdateLeaveAllocation
{
    public class UpdateLeaveAllocationDTO
    {
        public int LeaveTypeId { get; set; }

        public int NumberOfDays { get; set; }

        public int Year { get; set; }
    }
}