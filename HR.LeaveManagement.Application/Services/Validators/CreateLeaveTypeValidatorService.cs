﻿using HR.LeaveManagement.Application.Contracts.Logger;
using HR.LeaveManagement.Application.Features.LeaveType.Commands.CreateLeaveType;
using HR.LeaveManagement.Domain;

namespace HR.LeaveManagement.Application.Services.Validators
{
    public interface ICreateLeaveTypeValidatorService : IGenericValidatorService<CreateLeaveTypeCommand, LeaveType>
    {
    }

    public class CreateLeaveTypeValidatorService : GenericValidatorService<CreateLeaveTypeCommand, LeaveType>, ICreateLeaveTypeValidatorService
    {
        public CreateLeaveTypeValidatorService(IAppLogger<GenericValidatorService<CreateLeaveTypeCommand, LeaveType>> appLogger) : base(appLogger)
        {
        }
    }
}