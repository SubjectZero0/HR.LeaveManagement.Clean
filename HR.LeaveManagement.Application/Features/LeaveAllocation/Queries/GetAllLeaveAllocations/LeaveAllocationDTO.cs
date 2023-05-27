using HR.LeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

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