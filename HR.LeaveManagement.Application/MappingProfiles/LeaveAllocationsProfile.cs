using AutoMapper;
using HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.CreateLeaveAllocation;
using HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.UpdateLeaveAllocation;
using HR.LeaveManagement.Application.Features.LeaveAllocation.Queries.GetAllLeaveAllocations;
using HR.LeaveManagement.Domain;
using Microsoft.Extensions.Options;

namespace HR.LeaveManagement.Application.MappingProfiles
{
    public class LeaveAllocationsProfile : Profile
    {
        public LeaveAllocationsProfile()
        {
            CreateMap<LeaveAllocation, LeaveAllocationDTO>().ReverseMap();

            CreateMap<LeaveAllocation, CreateLeaveAllocationDTO>().ReverseMap();

            CreateMap<LeaveAllocation, CreateLeaveAllocationCommand>().ReverseMap();

            CreateMap<LeaveAllocationDTO, CreateLeaveAllocationDTO>().ReverseMap();

            CreateMap<LeaveAllocationDTO, CreateLeaveAllocationCommand>().ReverseMap();

            CreateMap<UpdateLeaveAllocationCommand, LeaveAllocation>()
                .ForMember(dest => dest.Id, options => options.Ignore());
        }
    }
}