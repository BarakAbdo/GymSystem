using GymSystem.Application.Dtos.PlanDtos;
using GymSystem.Application.Services;
using GymSystem.Application.Services.PlanService;
using Microsoft.AspNetCore.Mvc;

namespace GymSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlansController : ControllerBase
    {
        private readonly IPlanService _planService;

        public PlansController(IPlanService planService)
        {
            _planService = planService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var plans = _planService.GetAll();
            return Ok(plans);
        }

        [HttpGet("{uid}")]
        public IActionResult GetByUid(string uid)
        {
            var plan = _planService.GetByUid(uid);
            if (plan == null)
            {
                return NotFound(new { message = "Plan not found" });
            }

            return Ok(plan);
        }

        [HttpPost]
        public IActionResult Add([FromBody] CreatePlanDto dto)
        {
            if (dto == null || !ModelState.IsValid)
            {
                return BadRequest(new { message = "Invalid plan data" });
            }

            _planService.Add(dto);
            return Ok(new { message = "Plan added successfully" });
        }

        [HttpPut("{uid}")]
        public IActionResult Update(string uid, [FromBody] UpdatePlanDto dto)
        {
            if (dto == null || !ModelState.IsValid)
            {
                return BadRequest(new { message = "Invalid plan data" });
            }

            var existing = _planService.GetByUid(uid);
            if (existing == null)
            {
                return NotFound(new { message = "Plan not found" });
            }

            _planService.Update(uid, dto);
            return Ok(new { message = "Plan updated successfully" });
        }

        [HttpDelete("{uid}")]
        public IActionResult Delete(string uid)
        {
            var existing = _planService.GetByUid(uid);
            if (existing == null)
            {
                return NotFound(new { message = "Plan not found" });
            }

            _planService.Delete(uid);
            return Ok(new { message = "Plan deleted successfully" });
        }
    }
}