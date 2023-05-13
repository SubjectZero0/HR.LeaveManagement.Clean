using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Services.Validators;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveType.Commands.UpdateLeaveType
{
    public class UpdateLeaveTypeCommandHandler : IRequestHandler<UpdateLeaveTypeCommand, bool>
    {
        private readonly IMapper _mapper;
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly IValidatorService<UpdateLeaveTypeCommand> _validatorService;

        public UpdateLeaveTypeCommandHandler(IMapper mapper,
                                             ILeaveTypeRepository leaveTypeRepository,
                                             IValidatorService<UpdateLeaveTypeCommand> validatorService)
        {
            this._mapper = mapper;
            this._leaveTypeRepository = leaveTypeRepository;
            this._validatorService = validatorService;
        }

        public async Task<bool> Handle(UpdateLeaveTypeCommand command, CancellationToken cancellationToken)
        {
            var leaveTypeDB = await _leaveTypeRepository.GetByIdAsync(command.Id);

            if (leaveTypeDB is null)
            {
                throw new NotFoundException("Could not find leave type");
            }

            var validator = new UpdateLeaveTypeValidator(_leaveTypeRepository);
            await _validatorService.ValidateCommandAsync(command, validator, cancellationToken);

            _mapper.Map(command, leaveTypeDB);
            leaveTypeDB.DateModified = DateTime.Now;

            var isUpdated = await _leaveTypeRepository.UpdateAsync(leaveTypeDB);

            if (isUpdated)
            {
                return true;
            }

            return false;
        }
    }
}