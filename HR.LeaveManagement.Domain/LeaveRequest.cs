using System.ComponentModel.DataAnnotations.Schema;

namespace HR.LeaveManagement.Domain
{
    public class LeaveRequest : BaseClass
    {
        public DateTime DateStarted { get; set; }

        public DateTime DateEnded { get; set; }

        public DateTime DateRequested { get; set; }

        [ForeignKey("LeaveTypeId")]
        public LeaveType? LeaveType { get; set; }

        public int LeaveTypeId { get; set; }

        public bool? IsApproved { get; set; }

        public bool IsCancelled { get; set; }

        public string? Comment { get; set; }

        public string RequestingEmployeeId { get; set; } = string.Empty;
    }
}