using HR.LeaveManagement.Application.Contracts.Logger;
using HR.LeaveManagement.Application.Contracts.Persistence;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Queries.GetAllLeaveAllocations
{
    public class GetAllLeaveAllocationsQueryHandler : IRequestHandler<GetAllLeaveAllocationsQuery, List<Domain.LeaveAllocation>>
    {
        private readonly ILeaveAllocationsRepository _leaveAllocationsRepository;
        private readonly IAppLogger<GetAllLeaveAllocationsQueryHandler> _appLogger;

        public GetAllLeaveAllocationsQueryHandler(ILeaveAllocationsRepository leaveAllocationsRepository,
                                                  IAppLogger<GetAllLeaveAllocationsQueryHandler> appLogger)
        {
            this._leaveAllocationsRepository = leaveAllocationsRepository;
            this._appLogger = appLogger;
        }

        public async Task<List<Domain.LeaveAllocation>> Handle(GetAllLeaveAllocationsQuery request,
                                                               CancellationToken cancellationToken)
        {
            _appLogger.LogInformation("Attempting to execute {0}", nameof(GetAllLeaveAllocationsQueryHandler));

            var leaveAllocation = await _leaveAllocationsRepository.GetLeaveAllocationsWithDetails();

            return leaveAllocation;
        }
    }
}