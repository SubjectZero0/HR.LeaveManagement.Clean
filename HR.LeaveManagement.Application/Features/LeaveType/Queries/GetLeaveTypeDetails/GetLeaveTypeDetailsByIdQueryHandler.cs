using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveType.Queries.GetLeaveTypeDetails
{
    public class GetLeaveTypeDetailsByIdQueryHandler
        : IRequestHandler<GetLeaveTypeDetailsByIdQuery, LeaveTypeDetailsDTO>
    {
        private readonly IMapper _mapper;
        private readonly ILeaveTypeRepository _leaveTypeRepository;

        public GetLeaveTypeDetailsByIdQueryHandler(IMapper mapper,
                                                   ILeaveTypeRepository leaveTypeRepository)
        {
            this._mapper = mapper;
            this._leaveTypeRepository = leaveTypeRepository;
        }

        public async Task<LeaveTypeDetailsDTO> Handle(GetLeaveTypeDetailsByIdQuery request,
                                                      CancellationToken cancellationToken)
        {
            var details = await _leaveTypeRepository.GetByIdAsync(request.Id);

            if (details is null)
            {
                throw new NotFoundException("Leave type not found");
            }

            var detailsDTO = _mapper.Map<LeaveTypeDetailsDTO>(details);
            return detailsDTO;
        }
    }
}