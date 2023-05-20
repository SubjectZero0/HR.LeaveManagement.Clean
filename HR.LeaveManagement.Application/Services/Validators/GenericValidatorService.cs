using FluentValidation;
using HR.LeaveManagement.Application.Contracts.Logger;
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
        protected readonly IAppLogger<GenericValidatorService<TCommand, T>> _appLogger;

        public GenericValidatorService(IAppLogger<GenericValidatorService<TCommand, T>> appLogger)
        {
            this._appLogger = appLogger;
        }

        public async Task ValidateCommandAsync(TCommand command,
                                               AbstractValidator<TCommand> validator,
                                               CancellationToken cancellationToken)
        {
            
            var validationResult = await validator.ValidateAsync(command, cancellationToken);

            if (validationResult.Errors.Any())
            {
                _appLogger.LogCritical("Throwing BadRequestException with validation errors: {0}", validationResult.Errors);
                throw new BadRequestException("Invalid Input", validationResult);
            }
        }
    }
}