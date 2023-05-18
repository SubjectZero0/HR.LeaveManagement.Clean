using FluentValidation;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Domain;
using MediatR;

namespace HR.LeaveManagement.Application.Services.Validators
{
    public interface IGenericValidatorService<TCommand, T> where TCommand : IRequest<T> where T : BaseClass
    {
        public Task ValidateCommandAsync(TCommand command, AbstractValidator<TCommand> validator, CancellationToken cancellationToken);
    }

    public class GenericValidatorService<TCommand, T> : IGenericValidatorService<TCommand, T> where TCommand : IRequest<T> where T : BaseClass
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