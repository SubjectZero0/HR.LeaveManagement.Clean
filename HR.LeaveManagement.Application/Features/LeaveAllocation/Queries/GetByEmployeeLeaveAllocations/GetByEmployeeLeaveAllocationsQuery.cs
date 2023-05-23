using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Queries.GetByEmployeeLeaveAllocations
{
    public record GetByEmployeeLeaveAllocationsQuery(string employeeId) : IRequest<List<Domain.LeaveAllocation>>
    {
    }
}