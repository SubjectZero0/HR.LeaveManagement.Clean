using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Exceptions
{
    public class BadTransactionEcxeption : Exception
    {
        public BadTransactionEcxeption(string message) : base(message)
        {
        }
    }
}