using JobApplication.Application.DTOs;
using JobApplication.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JobApplication.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobsController : ControllerBase
    {
        private readonly JobService _JobService;

        public JobsController(JobService jobService)
        {
            _JobService = jobService;
        }

        [HttpPost]
        [Authorize(Roles = "Recruiter")]
        public async Task<IActionResult> Create(CreateJobDto createJobDto)
        {
            var id = await _JobService.CreateAsync(createJobDto);
            return Ok(new
            {
                id = id 
            }); 
        }

        [HttpPut("{id}/close")]
        [Authorize(Roles = "Recruiter")]
        public async Task<IActionResult> Close(int id)
        {
            try
            {
                await _JobService.CloseAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new { error = "Job not found." });
            }
            catch (UnauthorizedAccessException ex)
            {
                return StatusCode(403, new { error = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
