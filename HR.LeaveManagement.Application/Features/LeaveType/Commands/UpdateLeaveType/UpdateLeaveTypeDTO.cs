﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveType.Commands.UpdateLeaveType
{
    public class UpdateLeaveTypeDTO
    {
        public string Name { get; set; } = string.Empty;
        public int DefaultDays { get; set; }
    }
}