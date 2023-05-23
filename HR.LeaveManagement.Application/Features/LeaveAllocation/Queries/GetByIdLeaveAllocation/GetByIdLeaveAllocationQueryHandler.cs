using HR.LeaveManagement.Application.Contracts.Logger;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Features.LeaveAllocation.Queries.GetByEmployeeLeaveAllocations;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Queries.GetByIdLeaveAllocation
{
    public class GetByIdLeaveAllocationQueryHandler : IRequestHandler<GetByIdLeaveAllocationQuery, Domain.LeaveAllocation>
    {
        private readonly ILeaveAllocationsRepository _leaveAllocationsRepository;
        private readonly IAppLogger<GetByIdLeaveAllocationQuery> _appLogger;

        public GetByIdLeaveAllocationQueryHandler(ILeaveAllocationsRepository leaveAllocationsRepository,
                                                  IAppLogger<GetByIdLeaveAllocationQuery> appLogger)
        {
            this._leaveAllocationsRepository = leaveAllocationsRepository;
            this._appLogger = appLogger;
        }

        public async Task<Domain.LeaveAllocation> Handle(GetByIdLeaveAllocationQuery request, CancellationToken cancellationToken)
        {
            _appLogger.LogInformation("Attempting to execute {0}", nameof(GetByIdLeaveAllocationQueryHandler));
            var leaveAllocation = await _leaveAllocationsRepository.GetLeaveAllocationWithDetails(request.Id);

            if (leaveAllocation is null)
            {
                _appLogger.LogWarning("Throwing a NotFoundException for leave allocation with ID: {0}", request.Id);
                throw new NotFoundException($"Allocation with ID: {request.Id} not found");
            }

            return leaveAllocation;
        }
    }
}