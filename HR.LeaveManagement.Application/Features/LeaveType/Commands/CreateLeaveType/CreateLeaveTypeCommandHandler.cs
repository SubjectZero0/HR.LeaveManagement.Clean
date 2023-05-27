using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Logger;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Features.LeaveType.Commands.UpdateLeaveType;
using HR.LeaveManagement.Application.Services.Validators;
using HR.LeaveManagement.Domain;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveType.Commands.CreateLeaveType
{
    public class CreateLeaveTypeCommandHandler : IRequestHandler<CreateLeaveTypeCommand, Domain.LeaveType>
    {
        private readonly IMapper _mapper;
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly ICreateLeaveTypeValidatorService _validatorService;
        private readonly IAppLogger<CreateLeaveTypeCommandHandler> _appLogger;

        public CreateLeaveTypeCommandHandler(IMapper mapper,
                                             ILeaveTypeRepository leaveTypeRepository,
                                             ICreateLeaveTypeValidatorService validatorService,
                                             IAppLogger<CreateLeaveTypeCommandHandler> appLogger)
        {
            this._mapper = mapper;
            this._leaveTypeRepository = leaveTypeRepository;
            this._validatorService = validatorService;
            this._appLogger = appLogger;
        }

        public async Task<Domain.LeaveType> Handle(CreateLeaveTypeCommand command,
                                                   CancellationToken cancellationToken)
        {
            _appLogger.LogInformation("Attempting to execute {0}", nameof(CreateLeaveTypeCommandHandler));

            // validate data
            _appLogger.LogInformation("Attempting to validate input: {0}, {1}", command.Name, command.DefaultDays);
            var validator = new CreateLeaveTypeValidator(_leaveTypeRepository);
            await _validatorService.ValidateCommandAsync(command, validator, cancellationToken);

            //map data
            var leaveTypeDB = _mapper.Map<Domain.LeaveType>(command);

            //create entity
            var isCreated = await _leaveTypeRepository.AddAsync(leaveTypeDB);

            if (!isCreated)
            {
                _appLogger.LogCritical("Throwing {0}", nameof(BadTransactionEcxeption));
                throw new BadTransactionEcxeption("Record was lost");
            }
            return leaveTypeDB;
        }
    }
}