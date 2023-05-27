using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Logger;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Services.Validators;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.CreateLeaveAllocation
{
    public class CreateLeaveAllocationCommandHandler : IRequestHandler<CreateLeaveAllocationCommand, Domain.LeaveAllocation>
    {
        private readonly IMapper _mapper;
        private readonly IAppLogger<CreateLeaveAllocationCommandHandler> _appLogger;
        private readonly ICreateLeaveAllocationValidatorService _validatorService;
        private readonly ILeaveAllocationsRepository _leaveAllocationsRepository;
        private readonly ILeaveTypeRepository _leaveTypeRepository;

        public CreateLeaveAllocationCommandHandler(IMapper mapper,
                                                   IAppLogger<CreateLeaveAllocationCommandHandler> appLogger,
                                                   ICreateLeaveAllocationValidatorService validatorService,
                                                   ILeaveAllocationsRepository leaveAllocationsRepository,
                                                   ILeaveTypeRepository leaveTypeRepository)
        {
            this._mapper = mapper;
            this._appLogger = appLogger;
            this._validatorService = validatorService;
            this._leaveAllocationsRepository = leaveAllocationsRepository;
            this._leaveTypeRepository = leaveTypeRepository;
        }

        public async Task<Domain.LeaveAllocation> Handle(CreateLeaveAllocationCommand command, CancellationToken cancellationToken)
        {
            _appLogger.LogInformation("Attempting to execute {0}", nameof(CreateLeaveAllocationCommandHandler));

            // validate input
            _appLogger.LogInformation("Attempting to validate input: {0}, {1}, {3}, {4}", command.LeaveTypeId, command.NumberOfDays, command.Year, command.EmployeeId);
            var validator = new CreateLeaveAllocationValidator(_leaveAllocationsRepository, _leaveTypeRepository);
            await _validatorService.ValidateCommandAsync(command, validator, cancellationToken);

            // map data
            var leaveAllocation = _mapper.Map<Domain.LeaveAllocation>(command);

            // create LeaveAllocation and add to db
            var isCreated = await _leaveAllocationsRepository.AddAsync(leaveAllocation);

            if (!isCreated)
            {
                _appLogger.LogCritical("Throwing {0}", nameof(BadTransactionEcxeption));
                throw new BadTransactionEcxeption("Record was lost");
            }

            leaveAllocation = await _leaveAllocationsRepository.GetLeaveAllocationWithDetails(leaveAllocation.Id);

            return leaveAllocation;
        }
    }
}