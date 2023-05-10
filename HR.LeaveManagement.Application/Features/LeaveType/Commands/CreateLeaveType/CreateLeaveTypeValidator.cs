using FluentValidation;
using HR.LeaveManagement.Application.Contracts.Persistence;

namespace HR.LeaveManagement.Application.Features.LeaveType.Commands.CreateLeaveType
{
    public class CreateLeaveTypeValidator : AbstractValidator<CreateLeaveTypeCommand>
    {
        private readonly ILeaveTypeRepository _leaveTypeRepository;

        public CreateLeaveTypeValidator(ILeaveTypeRepository leaveTypeRepository)
        {
            _leaveTypeRepository = leaveTypeRepository;

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
                .MustAsync(IsLeaveTypeUniqueAsync)
                .WithMessage("LeaveType already Exists");
        }

        private async Task<bool> IsLeaveTypeUniqueAsync(CreateLeaveTypeCommand command, CancellationToken cancellationToken)
        {
            return await _leaveTypeRepository.IsLeaveTypeUnique(command.Name);
        }
    }
}