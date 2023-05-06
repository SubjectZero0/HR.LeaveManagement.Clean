namespace HR.LeaveManagement.Domain
{
    public class LeaveType : BaseClass
    {
        public string Name { get; set; } = string.Empty;

        public int DefaultDays { get; set; }
    }
}