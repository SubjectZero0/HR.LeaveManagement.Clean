using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Services.Validators;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveType.Commands.CreateLeaveType
{
    public class CreateLeaveTypeCommandHandler : IRequestHandler<CreateLeaveTypeCommand, bool>
    {
        private readonly IMapper _mapper;
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly IValidatorService<CreateLeaveTypeCommand> _validatorService;

        public CreateLeaveTypeCommandHandler(IMapper mapper,
                                             ILeaveTypeRepository leaveTypeRepository,
                                             IValidatorService<CreateLeaveTypeCommand> validatorService)
        {
            this._mapper = mapper;
            this._leaveTypeRepository = leaveTypeRepository;
            this._validatorService = validatorService;
        }

        public async Task<bool> Handle(CreateLeaveTypeCommand command,
                                 CancellationToken cancellationToken)
        {
            // validate data
            var validator = new CreateLeaveTypeValidator(_leaveTypeRepository);
            await _validatorService.ValidateCommandAsync(command, validator, cancellationToken);

            //map data
            var leaveTypeDB = _mapper.Map<Domain.LeaveType>(command);
            leaveTypeDB.DateCreated = DateTime.UtcNow;

            //create entity
            var isCreated = await _leaveTypeRepository.AddAsync(leaveTypeDB);

            if (isCreated)
            {
                return true;
            }

            return false;
        }
    }
}