using HR.LeaveManagement.API.Services;
using HR.LeaveManagement.Application.Features.LeaveType.Commands.CreateLeaveType;
using HR.LeaveManagement.Application.Features.LeaveType.Commands.DeleteLeaveType;
using HR.LeaveManagement.Application.Features.LeaveType.Commands.UpdateLeaveType;
using Microsoft.AspNetCore.Mvc;

namespace HR.LeaveManagement.API.Controllers
{
    [Route("api/leavetypes")]
    [ApiController]
    public class LeaveTypesController : ControllerBase
    {
        private readonly ILeaveTypesService _leaveTypesService;

        public LeaveTypesController(ILeaveTypesService leaveTypesService)
        {
            this._leaveTypesService = leaveTypesService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var leaveTypesDto = await _leaveTypesService.GetAllLeaveTypesAsync();

            return Ok(leaveTypesDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var leaveTypeDto = await _leaveTypesService.GetLeaveTypeByIdAsync(id);

            return Ok(leaveTypeDto);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateLeaveTypeCommand command)
        {
            var leaveTypeDTO = await _leaveTypesService.CreateLeaveTypeAsync(command);

            return Created(nameof(GetById), leaveTypeDTO);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateLeaveTypeCommand command)
        {
            var leaveType = await _leaveTypesService.UpdateLeaveTypeAsync(command);

            return Ok(leaveType);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(DeleteLeaveTypeCommand command)
        {
            await _leaveTypesService.DeleteLeaveTypeAsync(command);

            return NoContent();
        }
    }
}