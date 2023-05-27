using AutoMapper;
using HR.LeaveManagement.API.Services;
using HR.LeaveManagement.Application.Contracts.Logger;
using HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.CreateLeaveAllocation;
using Microsoft.AspNetCore.Mvc;

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

        // PUT api/<LeaveAllocationsController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<LeaveAllocationsController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}