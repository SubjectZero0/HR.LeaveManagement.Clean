using FluentValidation;
using HR.LeaveManagement.Application.Exceptions;
using MediatR;

namespace HR.LeaveManagement.Application.Services.Validators
{
    public interface IValidatorService<TCommand> where TCommand : IRequest<bool>
    {
        public Task ValidateCommandAsync(TCommand command, AbstractValidator<TCommand> validator, CancellationToken cancellationToken);
    }

    public class ValidatorService<TCommand> : IValidatorService<TCommand> where TCommand : IRequest<bool>
    {
        public async Task ValidateCommandAsync(TCommand command,
                                               AbstractValidator<TCommand> validator,
                                               CancellationToken cancellationToken)
        {
            var validationResult = await validator.ValidateAsync(command, cancellationToken);

            if (validationResult.Errors.Any())
            {
                throw new BadRequestException("Invalid Leave Type", validationResult);
            }
        }
    }
}