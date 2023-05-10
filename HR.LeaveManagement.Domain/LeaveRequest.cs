using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HR.LeaveManagement.Domain
{
    public class LeaveRequest : BaseClass, IValidatableObject
    {
        [DataType(DataType.Date)]
        public DateTime DateStarted { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateEnded { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateRequested { get; set; }

        [ForeignKey("LeaveTypeId")]
        public LeaveType? LeaveType { get; set; }

        public int LeaveTypeId { get; set; }

        public bool IsApproved { get; set; } = false;

        public bool IsCancelled { get; set; } = false;

        [MaxLength(300)]
        public string? Comment { get; set; }

        public string RequestingEmployeeId { get; set; } = string.Empty;

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> results = new();

            if (DateStarted >= DateEnded)
            {
                results.Add(new ValidationResult("DateStarted must be earlier than DateEnded", new[] { "DateStarted", "DateEnded" }));
            }

            return results;
        }
    }
}