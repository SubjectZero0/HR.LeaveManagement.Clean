using System.ComponentModel.DataAnnotations.Schema;

namespace HR.LeaveManagement.Domain
{
    public class LeaveAllocation : BaseClass

    {
        [ForeignKey("LeaveTypeId")]
        public LeaveType? LeaveType { get; set; }

        public int LeaveTypeId { get; set; }

        public int NumberOfDays { get; set; }

        public int Year { get; set; }
    }
}