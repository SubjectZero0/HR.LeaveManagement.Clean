using AutoMapper;
using HR.LeaveManagement.Application.Features.LeaveAllocation.Queries.GetAllLeaveAllocations;
using HR.LeaveManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.MappingProfiles
{
    public class LeaveAllocationsProfile : Profile
    {
        public LeaveAllocationsProfile()
        {
            CreateMap<LeaveAllocation, LeaveAllocationDTO>().ReverseMap();
        }
    }
}