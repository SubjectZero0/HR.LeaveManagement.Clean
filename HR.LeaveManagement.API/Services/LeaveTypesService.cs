using AutoMapper;
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

        Task<LeaveTypeDTO> UpdateLeaveTypeAsync(UpdateLeaveTypeCommand command);

        Task DeleteLeaveTypeAsync(DeleteLeaveTypeCommand command);
    }

    public class LeaveTypesService : ILeaveTypesService
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public LeaveTypesService(IMediator mediator, IMapper mapper)
        {
            this._mediator = mediator;
            this._mapper = mapper;
        }

        public async Task<List<LeaveTypeDTO>> GetAllLeaveTypesAsync()
        {
            var leaveTypes = await _mediator.Send(new GetLeaveTypesQuery());
            return leaveTypes;
        }

        public async Task<LeaveTypeDetailsDTO> GetLeaveTypeByIdAsync(int id)
        {
            var leaveType = await _mediator.Send(new GetLeaveTypeDetailsByIdQuery(id));
            return leaveType;
        }

        public async Task<LeaveTypeDTO> CreateLeaveTypeAsync(CreateLeaveTypeCommand command)
        {
            var leaveType = await _mediator.Send(command);
            var leaveTypeDto = _mapper.Map<LeaveTypeDTO>(leaveType);

            return leaveTypeDto;
        }

        public async Task<LeaveTypeDTO> UpdateLeaveTypeAsync(UpdateLeaveTypeCommand command)
        {
            var leaveType = await _mediator.Send(command);
            var leaveTypeDto = _mapper.Map<LeaveTypeDTO>(leaveType);

            return leaveTypeDto;
        }

        public async Task DeleteLeaveTypeAsync(DeleteLeaveTypeCommand command)
        {
            await _mediator.Send(command);
        }
    }
}