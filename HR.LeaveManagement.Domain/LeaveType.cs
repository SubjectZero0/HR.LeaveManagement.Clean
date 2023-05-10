using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace HR.LeaveManagement.Domain
{
    public class LeaveType : BaseClass
    {
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [Range(0, 100)]
        public int DefaultDays { get; set; }
    }
}