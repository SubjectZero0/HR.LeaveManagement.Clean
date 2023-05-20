using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Logger;
using HR.LeaveManagement.Application.Contracts.Persistence;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes
{
    public class GetLeaveTypesQueryHandler : IRequestHandler<GetLeaveTypesQuery, List<LeaveTypeDTO>>
    {
        private readonly IMapper _mapper;
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly IAppLogger<GetLeaveTypesQueryHandler> _appLogger;

        public GetLeaveTypesQueryHandler(IMapper mapper,
                                         ILeaveTypeRepository leaveTypeRepository,
                                         IAppLogger<GetLeaveTypesQueryHandler> appLogger)
        {
            this._mapper = mapper;
            this._leaveTypeRepository = leaveTypeRepository;
            this._appLogger = appLogger;
        }

        public async Task<List<LeaveTypeDTO>> Handle(GetLeaveTypesQuery request, CancellationToken cancellationToken)
        {
            _appLogger.LogInformation("Attempting to execute {0}", nameof(GetLeaveTypesQueryHandler));

            var leaveTypes = await _leaveTypeRepository.GetAllAsync();
            var leaveTypesDTO = _mapper.Map<List<LeaveTypeDTO>>(leaveTypes);
            return leaveTypesDTO;
        }
    }
}