using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Features.LeaveType.Commands.UpdateLeaveType
{
    public class UpdateLeaveTypeCommandHandler : IRequestHandler<UpdateLeaveTypeCommand, bool>
    {
        private readonly IMapper _mapper;
        private readonly ILeaveTypeRepository _leaveTypeRepository;

        public UpdateLeaveTypeCommandHandler(IMapper mapper,
                                             ILeaveTypeRepository leaveTypeRepository)
        {
            this._mapper = mapper;
            this._leaveTypeRepository = leaveTypeRepository;
        }

        public async Task<bool> Handle(UpdateLeaveTypeCommand request, CancellationToken cancellationToken)
        {
            var leaveTypeDB = await _leaveTypeRepository.GetByIdAsync(request.Id);

            if (leaveTypeDB is not null)
            {
                _mapper.Map(request, leaveTypeDB);
                leaveTypeDB.DateModified = DateTime.Now;

                var isUpdated = await _leaveTypeRepository.UpdateAsync(leaveTypeDB);

                if (isUpdated)
                {
                    return true;
                }
            }

            return false;
        }
    }
}