using FluentValidation;
using HR.LeaveManagement.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.UpdateLeaveAllocation
{
    public class UpdateLeaveAllocationValidator : AbstractValidator<UpdateLeaveAllocationCommand>
    {
        private readonly ILeaveAllocationsRepository _leaveAllocationsRepository;
        private readonly ILeaveTypeRepository _leaveTypeRepository;

        public UpdateLeaveAllocationValidator(ILeaveAllocationsRepository leaveAllocationsRepository,
                                              ILeaveTypeRepository leaveTypeRepository)
        {
            this._leaveAllocationsRepository = leaveAllocationsRepository;
            this._leaveTypeRepository = leaveTypeRepository;

            RuleFor(p => p)
                .MustAsync(LeaveTypeExistsInDb)
                .WithMessage("LeaveType must exist");

            RuleFor(p => p)
                .MustAsync(LeaveAllocationExistsInDb)
                .WithMessage("Leave Allocation does not exist");

            RuleFor(p => p.LeaveTypeId)
                .NotEmpty()
                .WithMessage("Leave Type must not be empty")
                .NotNull()
                .WithMessage("Leave Type must not be null")
                .GreaterThan(0)
                .WithMessage("Choose a valid LeaveType");

            RuleFor(p => p.Year)
                .NotEmpty()
                .WithMessage("Year cannot be empty")
                .NotNull()
                .WithMessage("Year cannot be null")
                .GreaterThan((int)DateTime.Now.Year - 1)
                .WithMessage("Year must be at least the current year")
                .LessThan((int)DateTime.Now.Year + 5)
                .WithMessage("Year cannot exceed the current year by more than 5 years");

            RuleFor(p => p.NumberOfDays)
                .NotEmpty()
                .WithMessage("NumberOfDays cannot be empty")
                .NotNull()
                .WithMessage("NumberOfDays cannot be null")
                .GreaterThan(0)
                .WithMessage("NumberOfDays must be at least 1")
                .LessThan(100)
                .WithMessage("NumberOfDays cannot exceed 100");

            RuleFor(p => p.EmployeeId)
                .NotEmpty()
                .WithMessage("EmployeeId cannot be empty")
                .NotNull()
                .WithMessage("EmployeeId cannot be null");
        }

        private async Task<bool> LeaveAllocationExistsInDb(UpdateLeaveAllocationCommand command, CancellationToken CancellationToken)
        {
            var exists = await _leaveAllocationsRepository.AllocationExists(command.EmployeeId, command.LeaveTypeId, command.Year);

            return exists;
        }

        private async Task<bool> LeaveTypeExistsInDb(UpdateLeaveAllocationCommand command, CancellationToken cancellationToken)
        {
            var exists = await _leaveTypeRepository.ExistsInDbAsync(command.LeaveTypeId);
            return exists;
        }
    }
}