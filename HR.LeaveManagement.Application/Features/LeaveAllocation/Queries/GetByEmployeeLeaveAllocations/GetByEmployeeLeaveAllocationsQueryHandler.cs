using HR.LeaveManagement.Application.Contracts.Logger;
using HR.LeaveManagement.Application.Contracts.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Queries.GetByEmployeeLeaveAllocations
{
    public class GetByEmployeeLeaveAllocationsQueryHandler : IRequestHandler<GetByEmployeeLeaveAllocationsQuery, List<Domain.LeaveAllocation>>
    {
        private readonly ILeaveAllocationsRepository _leaveAllocationsRepository;
        private readonly IAppLogger<GetByEmployeeLeaveAllocationsQueryHandler> _appLogger;

        public GetByEmployeeLeaveAllocationsQueryHandler(ILeaveAllocationsRepository leaveAllocationsRepository,
                                                         IAppLogger<GetByEmployeeLeaveAllocationsQueryHandler> appLogger)
        {
            this._leaveAllocationsRepository = leaveAllocationsRepository;
            this._appLogger = appLogger;
        }

        public async Task<List<Domain.LeaveAllocation>> Handle(GetByEmployeeLeaveAllocationsQuery request,
                                                   CancellationToken cancellationToken)
        {
            _appLogger.LogInformation("Attempting to execute {0}", nameof(GetByEmployeeLeaveAllocationsQueryHandler));
            var leaveAllocations = await _leaveAllocationsRepository.GetLeaveAllocationsWithDetails(request.employeeId);

            return leaveAllocations;
        }
    }
}