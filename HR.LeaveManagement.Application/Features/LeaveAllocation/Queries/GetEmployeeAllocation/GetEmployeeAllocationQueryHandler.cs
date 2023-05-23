using HR.LeaveManagement.Application.Contracts.Logger;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Queries.GetEmployeeAllocation
{
    public class GetEmployeeAllocationQueryHandler : IRequestHandler<GetEmployeeAllocationQuery, Domain.LeaveAllocation>
    {
        private readonly ILeaveAllocationsRepository _leaveAllocationsRepository;
        private readonly IAppLogger<GetEmployeeAllocationQueryHandler> _appLogger;

        public GetEmployeeAllocationQueryHandler(ILeaveAllocationsRepository leaveAllocationsRepository,
                                                 IAppLogger<GetEmployeeAllocationQueryHandler> appLogger)
        {
            this._leaveAllocationsRepository = leaveAllocationsRepository;
            this._appLogger = appLogger;
        }

        public async Task<Domain.LeaveAllocation> Handle(GetEmployeeAllocationQuery request,
                                                         CancellationToken cancellationToken)
        {
            var leaveAllocation = await _leaveAllocationsRepository.GetEmployeeAllocation(request.employeeId, request.leaveTypeId);

            if (leaveAllocation is null)
            {
                _appLogger.LogWarning("Throwing a NotFoundException for leave allocation with EmployeeId: {0} and leaveTypeId: {1}", request.employeeId, request.leaveTypeId);
                throw new NotFoundException($"Throwing NotFoundException for leave allocation with EmployeeId: {request.employeeId} and LeaveTypeId: {request.leaveTypeId}");
            }
            return leaveAllocation;
        }
    }
}