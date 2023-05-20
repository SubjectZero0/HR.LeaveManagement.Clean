using HR.LeaveManagement.API.Services;
using HR.LeaveManagement.Application.Contracts.Logger;
using HR.LeaveManagement.Application.Features.LeaveType.Commands.CreateLeaveType;
using HR.LeaveManagement.Application.Features.LeaveType.Commands.DeleteLeaveType;
using HR.LeaveManagement.Application.Features.LeaveType.Commands.UpdateLeaveType;
using HR.LeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes;
using Microsoft.AspNetCore.Mvc;

namespace HR.LeaveManagement.API.Controllers
{
    [Route("api/leavetypes")]
    [ApiController]
    public class LeaveTypesController : ControllerBase
    {
        private readonly ILeaveTypesService _leaveTypesService;
        private readonly IAppLogger<LeaveTypesController> _appLogger;

        public LeaveTypesController(ILeaveTypesService leaveTypesService, IAppLogger<LeaveTypesController> appLogger)
        {
            this._leaveTypesService = leaveTypesService;
            this._appLogger = appLogger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            _appLogger.LogInformation("Attempting to get all Leave Types");
            var leaveTypesDto = await _leaveTypesService.GetAllLeaveTypesAsync();

            _appLogger.LogInformation("Returning OK for all Leave Types");
            return Ok(leaveTypesDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            _appLogger.LogInformation("Attempting to get Leave Type with ID: {0}", id);
            var leaveTypeDto = await _leaveTypesService.GetLeaveTypeByIdAsync(id);

            _appLogger.LogInformation("Returning OK for Leave Type with ID: {0}", id);
            return Ok(leaveTypeDto);
        }

        [HttpPost("new")]
        public async Task<IActionResult> Create(CreateLeaveTypeCommand command)
        {
            _appLogger.LogInformation("Attempting to create Leave Type");
            var leaveTypeDTO = await _leaveTypesService.CreateLeaveTypeAsync(command);

            _appLogger.LogInformation("Returning Created for Leave Type with ID: {0}", leaveTypeDTO.Id);
            return Created(nameof(GetById), leaveTypeDTO);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateLeaveTypeDTO updateLeaveTypeDTO)
        {
            _appLogger.LogInformation("Attempting to update Leave Type with ID: {0}", id);
            var leaveTypeDTO = await _leaveTypesService.UpdateLeaveTypeAsync(id, updateLeaveTypeDTO);

            _appLogger.LogInformation("Returning OK for Leave Type with ID: {0}", id);
            return Ok(leaveTypeDTO);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(DeleteLeaveTypeCommand command)
        {
            _appLogger.LogInformation("Attempting to delete Leave Type with ID: {0}", command.Id);
            await _leaveTypesService.DeleteLeaveTypeAsync(command);

            _appLogger.LogInformation("Returning 204 for Leave Type with ID: {0}", command.Id);
            return NoContent();
        }
    }
}