using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Logger;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.UpdateLeaveAllocation
{
    public class UpdateLeaveAllocationCommandHandler : IRequestHandler<UpdateLeaveAllocationCommand, Domain.LeaveAllocation>
    {
        private readonly IMapper _mapper;
        private readonly IAppLogger<UpdateLeaveAllocationCommandHandler> _appLogger;
        private readonly ILeaveAllocationsRepository _leaveAllocationsRepository;

        public UpdateLeaveAllocationCommandHandler(IMapper mapper,
                                                   IAppLogger<UpdateLeaveAllocationCommandHandler> appLogger,
                                                   ILeaveAllocationsRepository leaveAllocationsRepository)
        {
            this._mapper = mapper;
            this._appLogger = appLogger;
            this._leaveAllocationsRepository = leaveAllocationsRepository;
        }

        public async Task<Domain.LeaveAllocation> Handle(UpdateLeaveAllocationCommand command, CancellationToken cancellationToken)
        {
            _appLogger.LogInformation("Attempting to execute {0}", nameof(UpdateLeaveAllocationCommandHandler));

            var leaveAllocationDB = await _leaveAllocationsRepository.GetByIdAsync(command.Id);

            if (leaveAllocationDB == null)
            {
                throw new NotFoundException($"Leave Allocation with ID: {command.Id} not found");
            }
        }
    }
}