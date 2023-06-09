﻿using AutoMapper;
using HR.LeaveManagement.API.Services;
using HR.LeaveManagement.Application.Contracts.Logger;
using HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.CreateLeaveAllocation;
using HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.UpdateLeaveAllocation;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HR.LeaveManagement.API.Controllers
{
    [Route("api/leave-allocations")]
    [ApiController]
    public class LeaveAllocationsController : ControllerBase
    {
        private readonly ILeaveAllocationService _leaveAllocationService;
        private readonly IMapper _mapper;
        private readonly IAppLogger<LeaveAllocationsController> _appLogger;

        public LeaveAllocationsController(ILeaveAllocationService leaveAllocationService,
                                          IMapper mapper,
                                          IAppLogger<LeaveAllocationsController> appLogger)
        {
            this._leaveAllocationService = leaveAllocationService;
            this._mapper = mapper;
            this._appLogger = appLogger;
        }

        // GET: api/<LeaveAllocationsController>
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            _appLogger.LogInformation("Attempting to get all Leave Allocations");
            var leaveAllocationsDTO = await _leaveAllocationService.GetAllLeaveAllocationsAsync();

            _appLogger.LogInformation("Returning OK for all Leave Allocations");
            return Ok(leaveAllocationsDTO);
        }

        // GET api/<LeaveAllocationsController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            _appLogger.LogInformation("Attempting to get Allocation Type with ID: {0}", id);
            var leaveAllocationsDTO = await _leaveAllocationService.GetLeaveAllocationByIdAsync(id);

            _appLogger.LogInformation("Returning OK for Leave Allocation with ID: {0}", id);
            return Ok(leaveAllocationsDTO);
        }

        // POST api/<LeaveAllocationsController>
        [HttpPost("new")]
        public async Task<IActionResult> Create([FromBody] CreateLeaveAllocationDTO createLeaveAllocationDTO)
        {
            _appLogger.LogInformation("Attempting to create Leave Allocation");
            var leaveTypeDTO = await _leaveAllocationService.CreateLeaveAllocationAsync(createLeaveAllocationDTO);

            _appLogger.LogInformation("Returning Created for Leave Allocation");
            return Created(nameof(GetById), leaveTypeDTO);
        }

        //PUT api/<LeaveAllocationsController>/5
        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateLeaveAllocationDTO updateLeaveAllocationDTO)
        {
            _appLogger.LogInformation("Attempting to update Allocation with ID: {0}", id);
            var leaveAllocationDTO = await _leaveAllocationService.UpdateLeaveAllocationAsync(id, updateLeaveAllocationDTO);

            _appLogger.LogInformation("Returning OK for updated Leave Allocation with ID: {0}", id);
            return Ok(leaveAllocationDTO);
        }

        // DELETE api/<LeaveAllocationsController>/5
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            _appLogger.LogInformation("Attempting to delete Leave Allocation with ID: {0}", id);
            await _leaveAllocationService.DeleteLeaveAllocationAsync(id);

            _appLogger.LogInformation("Returning 204 for Leave Allocation with ID: {0}", id);
            return NoContent();
        }
    }
}