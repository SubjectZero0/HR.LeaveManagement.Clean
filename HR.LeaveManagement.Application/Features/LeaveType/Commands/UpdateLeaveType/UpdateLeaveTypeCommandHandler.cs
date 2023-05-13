using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveType.Commands.UpdateLeaveType
{
    public class UpdateLeaveTypeCommandHandler : IRequestHandler<UpdateLeaveTypeCommand, bool>
    {
        private readonly IMapper _mapper;
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly IUpdateLeaveTypeValidatorService _updateLeaveTypeValidatorService;

        public UpdateLeaveTypeCommandHandler(IMapper mapper,
                                             ILeaveTypeRepository leaveTypeRepository,
                                             IUpdateLeaveTypeValidatorService updateLeaveTypeValidatorService)
        {
            this._mapper = mapper;
            this._leaveTypeRepository = leaveTypeRepository;
            this._updateLeaveTypeValidatorService = updateLeaveTypeValidatorService;
        }

        public async Task<bool> Handle(UpdateLeaveTypeCommand request, CancellationToken cancellationToken)
        {
            var leaveTypeDB = await _leaveTypeRepository.GetByIdAsync(request.Id);

            if (leaveTypeDB is null)
            {
                throw new NotFoundException("Could not find leave type");
            }

            var validationResult = await _updateLeaveTypeValidatorService.ValidateLeaveTypeUpdateAsync(request);

            if (validationResult.Errors.Any())
            {
                throw new BadRequestException("Invalid Leave Type", validationResult);
            }

            _mapper.Map(request, leaveTypeDB);
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