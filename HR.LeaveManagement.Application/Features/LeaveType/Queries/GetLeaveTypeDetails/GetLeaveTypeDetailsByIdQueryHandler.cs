using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Logger;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveType.Queries.GetLeaveTypeDetails
{
    public class GetLeaveTypeDetailsByIdQueryHandler
        : IRequestHandler<GetLeaveTypeDetailsByIdQuery, LeaveTypeDetailsDTO>
    {
        private readonly IMapper _mapper;
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly IAppLogger<GetLeaveTypeDetailsByIdQueryHandler> _appLogger;

        public GetLeaveTypeDetailsByIdQueryHandler(IMapper mapper,
                                                   ILeaveTypeRepository leaveTypeRepository,
                                                   IAppLogger<GetLeaveTypeDetailsByIdQueryHandler> appLogger)
        {
            this._mapper = mapper;
            this._leaveTypeRepository = leaveTypeRepository;
            this._appLogger = appLogger;
        }

        public async Task<LeaveTypeDetailsDTO> Handle(GetLeaveTypeDetailsByIdQuery request,
                                                      CancellationToken cancellationToken)
        {
            _appLogger.LogInformation("Attempting to execute {0}", nameof(GetLeaveTypeDetailsByIdQueryHandler));
            var details = await _leaveTypeRepository.GetByIdAsync(request.Id);

            if (details is null)
            {
                _appLogger.LogWarning("Throwing {0}", nameof(NotFoundException));
                throw new NotFoundException("Leave type not found");
            }

            var detailsDTO = _mapper.Map<LeaveTypeDetailsDTO>(details);
            return detailsDTO;
        }
    }
}