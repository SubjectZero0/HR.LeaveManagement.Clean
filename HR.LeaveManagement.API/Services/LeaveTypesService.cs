using HR.LeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes;
using HR.LeaveManagement.Application.Features.LeaveType.Queries.GetLeaveTypeDetails;
using MediatR;

namespace HR.LeaveManagement.API.Services
{
    public interface ILeaveTypesService
    {
        Task<List<LeaveTypeDTO>> GetAllLeaveTypesAsync();

        Task<LeaveTypeDetailsDTO> GetLeaveTypeByIdAsync(int id);
    }

    public class LeaveTypesService : ILeaveTypesService
    {
        private readonly IMediator _mediator;

        public LeaveTypesService(IMediator mediator)
        {
            this._mediator = mediator;
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
    }
}