using FluentValidation.Results;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveType.Commands.CreateLeaveType
{
    public interface ICreateLeaveTypeValidatorService
    {
        public Task<ValidationResult> ValidateCreateLeaveTypeAsync(CreateLeaveTypeCommand command);
    }

    public class CreateLeaveTypeValidatorService : ICreateLeaveTypeValidatorService
    {
        private readonly ILeaveTypeRepository _leaveTypeRepository;

        public CreateLeaveTypeValidatorService(ILeaveTypeRepository leaveTypeRepository)
        {
            this._leaveTypeRepository = leaveTypeRepository;
        }

        public async Task<ValidationResult> ValidateCreateLeaveTypeAsync(CreateLeaveTypeCommand command)
        {
            var validatator = new CreateLeaveTypeValidator(_leaveTypeRepository);
            var validationResult = await validatator.ValidateAsync(command);

            return validationResult;
        }
    }
}