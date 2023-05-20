using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Logger;
using HR.LeaveManagement.Application.Features.LeaveType.Commands.CreateLeaveType;
using HR.LeaveManagement.Application.Features.LeaveType.Commands.DeleteLeaveType;
using HR.LeaveManagement.Application.Features.LeaveType.Commands.UpdateLeaveType;
using HR.LeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes;
using HR.LeaveManagement.Application.Features.LeaveType.Queries.GetLeaveTypeDetails;
using MediatR;

namespace HR.LeaveManagement.API.Services
{
    public interface ILeaveTypesService
    {
        Task<List<LeaveTypeDTO>> GetAllLeaveTypesAsync();

        Task<LeaveTypeDetailsDTO> GetLeaveTypeByIdAsync(int id);

        Task<LeaveTypeDTO> CreateLeaveTypeAsync(CreateLeaveTypeCommand command);

        Task<LeaveTypeDTO> UpdateLeaveTypeAsync(int id, UpdateLeaveTypeDTO updateLeaveTypeDTO);

        Task DeleteLeaveTypeAsync(DeleteLeaveTypeCommand command);
    }

    public class LeaveTypesService : ILeaveTypesService
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IAppLogger<LeaveTypesService> _appLogger;

        public LeaveTypesService(IMediator mediator,
                                 IMapper mapper,
                                 IAppLogger<LeaveTypesService> appLogger)
        {
            this._mediator = mediator;
            this._mapper = mapper;
            this._appLogger = appLogger;
        }

        public async Task<List<LeaveTypeDTO>> GetAllLeaveTypesAsync()
        {
            var leaveTypes = await _mediator.Send(new GetLeaveTypesQuery());

            _appLogger.LogInformation("{0} method named {1} was executed", nameof(LeaveTypesService), nameof(GetAllLeaveTypesAsync));
            return leaveTypes;
        }

        public async Task<LeaveTypeDetailsDTO> GetLeaveTypeByIdAsync(int id)
        {
            var leaveType = await _mediator.Send(new GetLeaveTypeDetailsByIdQuery(id));

            _appLogger.LogInformation("{0} method named {1} was executed", nameof(LeaveTypesService), nameof(GetLeaveTypeByIdAsync));
            return leaveType;
        }

        public async Task<LeaveTypeDTO> CreateLeaveTypeAsync(CreateLeaveTypeCommand command)
        {
            var leaveType = await _mediator.Send(command);
            var leaveTypeDto = _mapper.Map<LeaveTypeDTO>(leaveType);

            _appLogger.LogInformation("{0} method named {1} was executed", nameof(LeaveTypesService), nameof(CreateLeaveTypeAsync));
            return leaveTypeDto;
        }

        public async Task<LeaveTypeDTO> UpdateLeaveTypeAsync(int id, UpdateLeaveTypeDTO updateLeaveTypeDTO)
        {
            var command = new UpdateLeaveTypeCommand()
            {
                Id = id,
                Name = updateLeaveTypeDTO.Name,
                DefaultDays = updateLeaveTypeDTO.DefaultDays,
            };

            var leaveType = await _mediator.Send(command);
            var leaveTypeDto = _mapper.Map<LeaveTypeDTO>(leaveType);

            _appLogger.LogInformation("{0} method named {1} was executed", nameof(LeaveTypesService), nameof(UpdateLeaveTypeAsync));
            return leaveTypeDto;
        }

        public async Task DeleteLeaveTypeAsync(DeleteLeaveTypeCommand command)
        {
            await _mediator.Send(command);
            _appLogger.LogInformation("{0} method named {1} was executed", nameof(LeaveTypesService), nameof(DeleteLeaveTypeAsync));
        }
    }
}