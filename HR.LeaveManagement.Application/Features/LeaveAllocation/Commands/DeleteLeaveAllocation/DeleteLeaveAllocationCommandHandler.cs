using HR.LeaveManagement.Application.Contracts.Logger;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Features.LeaveType.Commands.DeleteLeaveType;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.DeleteLeaveAllocation
{
    public class DeleteLeaveAllocationCommandHandler : IRequestHandler<DeleteLeaveAllocationCommand, Unit>
    {
        private readonly IAppLogger<DeleteLeaveAllocationCommandHandler> _appLogger;
        private readonly ILeaveAllocationsRepository _leaveAllocationsRepository;

        public DeleteLeaveAllocationCommandHandler(IAppLogger<DeleteLeaveAllocationCommandHandler> appLogger,
                                                   ILeaveAllocationsRepository leaveAllocationsRepository)
        {
            this._appLogger = appLogger;
            this._leaveAllocationsRepository = leaveAllocationsRepository;
        }

        public async Task<Unit> Handle(DeleteLeaveAllocationCommand request, CancellationToken cancellationToken)
        {
            _appLogger.LogInformation("Attempting to execute {0}", nameof(DeleteLeaveAllocationCommandHandler));
            var leaveAllocation = await _leaveAllocationsRepository.GetByIdAsync(request.Id);

            if (leaveAllocation is null)
            {
                _appLogger.LogWarning("Throwing {0}", nameof(NotFoundException));
                throw new NotFoundException($"Leave Allocation with ID: {request.Id} was not found");
            }

            var isDeleted = await _leaveAllocationsRepository.DeleteAsync(leaveAllocation);

            if (!isDeleted)
            {
                _appLogger.LogCritical("Throwing {0}", nameof(BadTransactionEcxeption));
                throw new BadTransactionEcxeption($"Leave Allocation with ID: {request.Id} failed to delete");
            }

            return Unit.Value;
        }
    }
}