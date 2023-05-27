using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Logger;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Services.Validators;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.UpdateLeaveAllocation
{
    public class UpdateLeaveAllocationCommandHandler : IRequestHandler<UpdateLeaveAllocationCommand, Domain.LeaveAllocation>
    {
        private readonly IMapper _mapper;
        private readonly IAppLogger<UpdateLeaveAllocationCommandHandler> _appLogger;
        private readonly ILeaveAllocationsRepository _leaveAllocationsRepository;
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly IUpdateLeaveAllocationValidatorService _validatorService;

        public UpdateLeaveAllocationCommandHandler(IMapper mapper,
                                                   IAppLogger<UpdateLeaveAllocationCommandHandler> appLogger,
                                                   ILeaveAllocationsRepository leaveAllocationsRepository,
                                                   ILeaveTypeRepository leaveTypeRepository,
                                                   IUpdateLeaveAllocationValidatorService _validatorService)
        {
            this._mapper = mapper;
            this._appLogger = appLogger;
            this._leaveAllocationsRepository = leaveAllocationsRepository;
            this._leaveTypeRepository = leaveTypeRepository;
            this._validatorService = _validatorService;
        }

        public async Task<Domain.LeaveAllocation> Handle(UpdateLeaveAllocationCommand command, CancellationToken cancellationToken)
        {
            _appLogger.LogInformation("Attempting to execute {0}", nameof(UpdateLeaveAllocationCommandHandler));

            // validate input
            _appLogger.LogInformation("Attempting to validate command {0}", nameof(UpdateLeaveAllocationCommand));
            var validator = new UpdateLeaveAllocationValidator(_leaveAllocationsRepository, _leaveTypeRepository);
            await _validatorService.ValidateCommandAsync(command, validator, cancellationToken);

            // get from db
            var leaveAllocationDB = await _leaveAllocationsRepository.GetByIdAsync(command.Id);

            if (leaveAllocationDB is null)
            {
                _appLogger.LogWarning("Throwing NotFoundException");
                throw new NotFoundException($"Leave Allocation with ID: {command.Id} not found");
            }

            // map data
            _mapper.Map(command, leaveAllocationDB);

            // execute update
            var isUpdated = await _leaveAllocationsRepository.UpdateAsync(leaveAllocationDB);

            if (!isUpdated)
            {
                _appLogger.LogCritical("Throwing BadTransactionException");
                throw new BadTransactionEcxeption("Leave Allocation Update failed");
            }

            // get the leave allocation with details
            var leaveAllocationDBWithDetails = await _leaveAllocationsRepository.GetLeaveAllocationWithDetails(command.Id);

            return leaveAllocationDBWithDetails;
        }
    }
}