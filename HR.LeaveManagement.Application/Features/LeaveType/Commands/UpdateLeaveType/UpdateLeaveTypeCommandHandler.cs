using AutoMapper;
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

        public UpdateLeaveTypeCommandHandler(IMapper mapper,
                                             ILeaveTypeRepository leaveTypeRepository,
                                             IUpdateLeaveTypeValidatorService validatorService)
        {
            this._mapper = mapper;
            this._leaveTypeRepository = leaveTypeRepository;
            this._validatorService = validatorService;
        }

        public async Task<Domain.LeaveType> Handle(UpdateLeaveTypeCommand command, CancellationToken cancellationToken)
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

            if (!isUpdated)
            {
                throw new BadTransactionEcxeption("Transaction failed");
            }

            return leaveTypeDB;
        }
    }
}