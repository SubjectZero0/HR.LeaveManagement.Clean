using FluentValidation.Results;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;

namespace HR.LeaveManagement.Application.Features.LeaveType.Commands.UpdateLeaveType
{
    public interface IUpdateLeaveTypeValidatorService
    {
        public Task<ValidationResult> ValidateLeaveTypeUpdateAsync(UpdateLeaveTypeCommand command);
    }

    public class UpdateLeaveTypeValidatorService : IUpdateLeaveTypeValidatorService
    {
        private readonly ILeaveTypeRepository _leaveTypeRepository;

        public UpdateLeaveTypeValidatorService(ILeaveTypeRepository leaveTypeRepository)
        {
            this._leaveTypeRepository = leaveTypeRepository;
        }

        public async Task<ValidationResult> ValidateLeaveTypeUpdateAsync(UpdateLeaveTypeCommand command)
        {
            var validator = new UpdateLeaveTypeValidator(_leaveTypeRepository);
            var validationResult = await validator.ValidateAsync(command);

            return validationResult;
        }
    }
}