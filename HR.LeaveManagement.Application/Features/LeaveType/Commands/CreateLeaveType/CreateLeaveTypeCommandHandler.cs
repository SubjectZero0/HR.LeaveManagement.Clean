using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveType.Commands.CreateLeaveType
{
    public class CreateLeaveTypeCommandHandler : IRequestHandler<CreateLeaveTypeCommand, bool>
    {
        private readonly IMapper _mapper;
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly ICreateLeaveTypeValidatorService _createLeaveTypeValidatorService;

        public CreateLeaveTypeCommandHandler(IMapper mapper,
                                             ILeaveTypeRepository leaveTypeRepository,
                                             ICreateLeaveTypeValidatorService createLeaveTypeValidatorService)
        {
            this._mapper = mapper;
            this._leaveTypeRepository = leaveTypeRepository;
            this._createLeaveTypeValidatorService = createLeaveTypeValidatorService;
        }

        public async Task<bool> Handle(CreateLeaveTypeCommand request,
                                 CancellationToken cancellationToken)
        {
            // validate data
            var validationResult = await _createLeaveTypeValidatorService.ValidateCreateLeaveTypeAsync(request);

            if (validationResult.Errors.Any())
            {
                throw new BadRequestException("Invalid Leave Type", validationResult);
            }

            //map data
            var leaveTypeDB = _mapper.Map<Domain.LeaveType>(request);
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