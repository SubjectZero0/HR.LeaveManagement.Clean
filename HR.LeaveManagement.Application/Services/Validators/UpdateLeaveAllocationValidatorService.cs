using HR.LeaveManagement.Application.Contracts.Logger;
using HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.UpdateLeaveAllocation;
using HR.LeaveManagement.Domain;

namespace HR.LeaveManagement.Application.Services.Validators
{
    public interface IUpdateLeaveAllocationValidatorService : IGenericValidatorService<UpdateLeaveAllocationCommand, Domain.LeaveAllocation>
    {
    }

    public class UpdateLeaveAllocationValidatorService : GenericValidatorService<UpdateLeaveAllocationCommand, Domain.LeaveAllocation>, IUpdateLeaveAllocationValidatorService
    {
        public UpdateLeaveAllocationValidatorService(IAppLogger<GenericValidatorService<UpdateLeaveAllocationCommand, LeaveAllocation>> appLogger) : base(appLogger)
        {
        }
    }
}