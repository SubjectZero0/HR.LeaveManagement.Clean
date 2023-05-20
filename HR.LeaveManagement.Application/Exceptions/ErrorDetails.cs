using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Exceptions
{
    public class ErrorDetails
    {
        public string Message { get; set; }
        public List<string> Details { get; set; } = new List<string>();
        public string Type { get; set; }
    }
}