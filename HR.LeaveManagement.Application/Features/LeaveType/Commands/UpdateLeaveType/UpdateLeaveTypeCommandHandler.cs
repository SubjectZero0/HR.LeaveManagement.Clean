using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Logger;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Services.Validators;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveType.Commands.UpdateLeaveType
{
    public class UpdateLeaveTypeCommandHandler : IRequestHandler<UpdateLeaveTypeCommand, Domain.LeaveType>
    {
        private readonly IMapper _mapper;
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly IUpdateLeaveTypeValidatorService _validatorService;
        private readonly IAppLogger<UpdateLeaveTypeCommandHandler> _appLogger;

        public UpdateLeaveTypeCommandHandler(IMapper mapper,
                                             ILeaveTypeRepository leaveTypeRepository,
                                             IUpdateLeaveTypeValidatorService validatorService,
                                             IAppLogger<UpdateLeaveTypeCommandHandler> appLogger)
        {
            this._mapper = mapper;
            this._leaveTypeRepository = leaveTypeRepository;
            this._validatorService = validatorService;
            this._appLogger = appLogger;
        }

        public async Task<Domain.LeaveType> Handle(UpdateLeaveTypeCommand command, CancellationToken cancellationToken)
        {
            _appLogger.LogInformation("Attempting to execute {0}", nameof(UpdateLeaveTypeCommandHandler));
            var leaveTypeDB = await _leaveTypeRepository.GetByIdAsync(command.Id);

            if (leaveTypeDB is null)
            {
                _appLogger.LogWarning("Throwing {0}", nameof(NotFoundException));
                throw new NotFoundException("Could not find leave type");
            }

            _appLogger.LogInformation("Attempting to validate input: {0}, {1}, {2}", command.Id, command.Name, command.DefaultDays);
            var validator = new UpdateLeaveTypeValidator(_leaveTypeRepository);
            await _validatorService.ValidateCommandAsync(command, validator, cancellationToken);

            _mapper.Map(command, leaveTypeDB);
            leaveTypeDB.DateModified = DateTime.Now;

            var isUpdated = await _leaveTypeRepository.UpdateAsync(leaveTypeDB);

            if (!isUpdated)
            {
                _appLogger.LogCritical("Throwing {0}", nameof(BadTransactionEcxeption));
                throw new BadTransactionEcxeption("Transaction failed");
            }

            return leaveTypeDB;
        }
    }
}