using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveType.Commands.CreateLeaveType
{
    public class CreateLeaveTypeCommandHandler : IRequestHandler<CreateLeaveTypeCommand, bool>
    {
        private readonly IMapper _mapper;
        private readonly ILeaveTypeRepository _leaveTypeRepository;

        public CreateLeaveTypeCommandHandler(IMapper mapper,
                                             ILeaveTypeRepository leaveTypeRepository)
        {
            this._mapper = mapper;
            this._leaveTypeRepository = leaveTypeRepository;
        }

        public async Task<bool> Handle(CreateLeaveTypeCommand request,
                                 CancellationToken cancellationToken)
        {
            // validate data

            //map data
            var leaveTypeDB = _mapper.Map<Domain.LeaveType>(request);
            leaveTypeDB.DateCreated = DateTime.UtcNow;

            //create entity
            var isCreated = await _leaveTypeRepository.AddAsync(leaveTypeDB);

            if (isCreated)
            {
                return true;
            }
            return false;
        }
    }
}