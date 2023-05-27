using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Logger;
using HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.CreateLeaveAllocation;
using HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.UpdateLeaveAllocation;
using HR.LeaveManagement.Application.Features.LeaveAllocation.Queries.GetAllLeaveAllocations;
using HR.LeaveManagement.Application.Features.LeaveAllocation.Queries.GetByIdLeaveAllocation;
using MediatR;

namespace HR.LeaveManagement.API.Services
{
    public interface ILeaveAllocationService
    {
        Task<List<LeaveAllocationDTO>> GetAllLeaveAllocationsAsync();

        Task<LeaveAllocationDTO> GetLeaveAllocationByIdAsync(int id);

        Task<LeaveAllocationDTO> CreateLeaveAllocationAsync(CreateLeaveAllocationDTO createLeaveAllocationDTO);

        Task<LeaveAllocationDTO> UpdateLeaveAllocationAsync(int id, UpdateLeaveAllocationDTO updateLeaveAllocationDTO);

        //Task DeleteLeaveTypeAsync(DeleteLeaveTypeCommand command);
    }

    public class LeaveAllocationService : ILeaveAllocationService
    {
        private readonly IAppLogger<LeaveAllocationService> _appLogger;
        private readonly IMediator _mediatr;
        private readonly IMapper _mapper;

        public LeaveAllocationService(IAppLogger<LeaveAllocationService> appLogger,
                                      IMediator mediatr,
                                      IMapper mapper)
        {
            this._appLogger = appLogger;
            this._mediatr = mediatr;
            this._mapper = mapper;
        }

        public async Task<LeaveAllocationDTO> CreateLeaveAllocationAsync(CreateLeaveAllocationDTO createLeaveAllocationDTO)
        {
            var command = new CreateLeaveAllocationCommand()
            {
                LeaveTypeId = createLeaveAllocationDTO.LeaveTypeId,
                EmployeeId = "11111", //changes after Identity
                NumberOfDays = createLeaveAllocationDTO.NumberOfDays,
                Year = createLeaveAllocationDTO.Year,
            };

            var leaveAllocation = await _mediatr.Send(command);
            var leaveAllocationDTO = _mapper.Map<LeaveAllocationDTO>(leaveAllocation);

            return leaveAllocationDTO;
        }

        public async Task<List<LeaveAllocationDTO>> GetAllLeaveAllocationsAsync()
        {
            var leaveAllocations = await _mediatr.Send(new GetAllLeaveAllocationsQuery());
            var leaveAllocationsDTO = _mapper.Map<List<LeaveAllocationDTO>>(leaveAllocations);

            _appLogger.LogInformation("{0} method named {1} was executed", nameof(LeaveAllocationService), nameof(GetAllLeaveAllocationsAsync));
            return leaveAllocationsDTO;
        }

        public async Task<LeaveAllocationDTO> GetLeaveAllocationByIdAsync(int id)
        {
            var leaveAllocation = await _mediatr.Send(new GetByIdLeaveAllocationQuery(id));
            var leaveAllocationDTO = _mapper.Map<LeaveAllocationDTO>(leaveAllocation);

            _appLogger.LogInformation("{0} method named {1} was executed", nameof(LeaveAllocationService), nameof(GetLeaveAllocationByIdAsync));
            return leaveAllocationDTO;
        }

        public async Task<LeaveAllocationDTO> UpdateLeaveAllocationAsync(int id, UpdateLeaveAllocationDTO updateLeaveAllocationDTO)
        {
            var command = new UpdateLeaveAllocationCommand()
            {
                Id = id,
                EmployeeId = "11111", //changes after Identity,
                LeaveTypeId = updateLeaveAllocationDTO.LeaveTypeId,
                NumberOfDays = updateLeaveAllocationDTO.NumberOfDays,
                Year = updateLeaveAllocationDTO.Year,
            };

            var leaveAllocation = await _mediatr.Send(command);
            var leaveAllocationDTO = _mapper.Map<LeaveAllocationDTO>(leaveAllocation);

            _appLogger.LogInformation("{0} method named {1} was executed", nameof(LeaveAllocationService), nameof(UpdateLeaveAllocationAsync));
            return leaveAllocationDTO;
        }
    }
}