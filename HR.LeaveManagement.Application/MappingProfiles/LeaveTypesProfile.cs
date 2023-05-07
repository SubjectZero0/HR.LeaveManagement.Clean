using AutoMapper;
using HR.LeaveManagement.Application.Features.LeaveType.Commands.CreateLeaveType;
using HR.LeaveManagement.Application.Features.LeaveType.Commands.UpdateLeaveType;
using HR.LeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes;
using HR.LeaveManagement.Application.Features.LeaveType.Queries.GetLeaveTypeDetails;
using HR.LeaveManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.MappingProfiles
{
    public class LeaveTypesProfile : Profile
    {
        public LeaveTypesProfile()
        {
            CreateMap<LeaveType, LeaveTypeDTO>().ReverseMap();

            CreateMap<LeaveType, LeaveTypeDetailsDTO>().ReverseMap();

            CreateMap<LeaveType, CreateLeaveTypeCommand>().ReverseMap();

            CreateMap<UpdateLeaveTypeCommand, LeaveType>()
                .ForMember(dest => dest.Id, options => options.Ignore());
        }
    }
}