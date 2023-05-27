using HR.LeaveManagement.Application.Contracts.Logger;
using HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.CreateLeaveAllocation;
using HR.LeaveManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Services.Validators
{
    public interface ICreateLeaveAllocationValidatorService : IGenericValidatorService<CreateLeaveAllocationCommand, LeaveAllocation>
    {
    }

    public class CreateLeaveAllocationValidatorService : GenericValidatorService<CreateLeaveAllocationCommand, LeaveAllocation>, ICreateLeaveAllocationValidatorService
    {
        public CreateLeaveAllocationValidatorService(IAppLogger<GenericValidatorService<CreateLeaveAllocationCommand, LeaveAllocation>> appLogger) : base(appLogger)
        {
        }
    }
}