using HR.LeaveManagement.Application.Features.LeaveType.Commands.UpdateLeaveType;
using HR.LeaveManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Services.Validators
{
    public interface IUpdateLeaveTypeValidatorService : IGenericValidatorService<UpdateLeaveTypeCommand, LeaveType>
    {
    }

    public class UpdateLeaveTypeValidatorService : GenericValidatorService<UpdateLeaveTypeCommand, LeaveType>, IUpdateLeaveTypeValidatorService
    {
    }
}