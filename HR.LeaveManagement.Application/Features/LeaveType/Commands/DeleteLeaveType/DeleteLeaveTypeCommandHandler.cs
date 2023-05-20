using HR.LeaveManagement.Application.Contracts.Logger;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveType.Commands.DeleteLeaveType
{
    public class DeleteLeaveTypeCommandHandler : IRequestHandler<DeleteLeaveTypeCommand, Unit>
    {
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly IAppLogger<DeleteLeaveTypeCommandHandler> _appLogger;

        public DeleteLeaveTypeCommandHandler(ILeaveTypeRepository leaveTypeRepository,
                                             IAppLogger<DeleteLeaveTypeCommandHandler> appLogger)
        {
            this._leaveTypeRepository = leaveTypeRepository;
            this._appLogger = appLogger;
        }

        public async Task<Unit> Handle(DeleteLeaveTypeCommand request, CancellationToken cancellationToken)
        {
            _appLogger.LogInformation("Attempting to execute {0}", nameof(DeleteLeaveTypeCommandHandler));
            var leaveTypeDb = await _leaveTypeRepository.GetByIdAsync(request.Id);

            if (leaveTypeDb is null)
            {
                _appLogger.LogWarning("Throwing {0}", nameof(NotFoundException));
                throw new NotFoundException("Could not find leave type");
            }

            var isDeleted = await _leaveTypeRepository.DeleteAsync(leaveTypeDb);

            if (!isDeleted)
            {
                _appLogger.LogCritical("Throwing {0}", nameof(BadTransactionEcxeption));
                throw new BadTransactionEcxeption("Transaction failed");
            }

            return Unit.Value;
        }
    }
}