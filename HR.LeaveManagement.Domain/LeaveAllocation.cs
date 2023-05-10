using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HR.LeaveManagement.Domain
{
    public class LeaveAllocation : BaseClass

    {
        [ForeignKey("LeaveTypeId")]
        public LeaveType? LeaveType { get; set; }

        public int LeaveTypeId { get; set; }

        [Range(0, 100)]
        public int NumberOfDays { get; set; }

        [Range(2000, 2050)]
        public int Year { get; set; }
    }
}