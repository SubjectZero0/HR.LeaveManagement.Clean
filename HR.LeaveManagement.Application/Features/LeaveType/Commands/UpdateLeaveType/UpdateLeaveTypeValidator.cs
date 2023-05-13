using FluentValidation;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Features.LeaveType.Commands.CreateLeaveType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveType.Commands.UpdateLeaveType
{
    public class UpdateLeaveTypeValidator : AbstractValidator<UpdateLeaveTypeCommand>
    {
        private readonly ILeaveTypeRepository _leaveTypeRepository;

        public UpdateLeaveTypeValidator(ILeaveTypeRepository leaveTypeRepository)
        {
            this._leaveTypeRepository = leaveTypeRepository;

            RuleFor(p => p.Name)
                 .NotEmpty()
                 .WithMessage("Property must not be empty");

            RuleFor(p => p.Name)
               .NotNull()
               .WithMessage("Property must not be empty");

            RuleFor(p => p.Name)
               .MinimumLength(3)
               .WithMessage("Property must be more than 3 characters");

            RuleFor(p => p.Name)
               .MaximumLength(50)
               .WithMessage("Property must not exceed 50 characters");

            RuleFor(p => p.Name)
               .Matches("^[^0-9]+$")
               .WithMessage("Property must not contain any numbers");

            RuleFor(p => p.DefaultDays)
                .NotNull()
                .WithMessage("Property cannot be empty");

            RuleFor(p => p.DefaultDays)
               .LessThan(100)
               .WithMessage("Property cannot exceed 100");

            RuleFor(p => p.DefaultDays)
               .GreaterThan(1)
               .WithMessage("Property cannot be less than 1");

            RuleFor(p => p)
                .MustAsync(ExistsAsync)
                .WithMessage("LeaveType does not exist");

            RuleFor(p => p)
                .MustAsync(IsLeaveTypeUniqueAsync)
                .WithMessage("LeaveType with that name already Exists");
        }

        private async Task<bool> ExistsAsync(UpdateLeaveTypeCommand command, CancellationToken cancellationToken)
        {
            return await _leaveTypeRepository.ExistsInDbAsync(command.Id);
        }

        private async Task<bool> IsLeaveTypeUniqueAsync(UpdateLeaveTypeCommand command, CancellationToken cancellationToken)
        {
            return await _leaveTypeRepository.IsLeaveTypeUnique(command.Name);
        }
    }
}