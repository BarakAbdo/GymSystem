using GymSystem.Application.Dtos.TrainerDtos;
using GymSystem.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace GymSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainersController : ControllerBase
    {
        private readonly ITrainerService _trainerService;

        public TrainersController(ITrainerService trainerService)
        {
            _trainerService = trainerService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var trainers = _trainerService.GetAll();
            return Ok(trainers);
        }

        [HttpGet("{uid}")]
        public IActionResult GetByUid(string uid)
        {
            var trainer = _trainerService.GetByUid(uid);
            if (trainer == null)
            {
                return NotFound(new { message = "Trainer not found" });
            }

            return Ok(trainer);
        }

        [HttpPost]
        public IActionResult Add([FromBody] CreateTrainerDto dto)
        {
            if (dto == null || !ModelState.IsValid)
            {
                return BadRequest(new { message = "Invalid trainer data" });
            }

            _trainerService.Add(dto);
            return Ok(new { message = "Trainer added successfully" });
        }

        [HttpPut("{uid}")]
        public IActionResult Update(string uid, [FromBody] UpdateTrainerDto dto)
        {
            if (dto == null || !ModelState.IsValid)
            {
                return BadRequest(new { message = "Invalid trainer data" });
            }

            var existing = _trainerService.GetByUid(uid);
            if (existing == null)
            {
                return NotFound(new { message = "Trainer not found" });
            }

            _trainerService.Update(uid, dto);
            return Ok(new { message = "Trainer updated successfully" });
        }

        [HttpDelete("{uid}")]
        public IActionResult Delete(string uid)
        {
            var existing = _trainerService.GetByUid(uid);
            if (existing == null)
            {
                return NotFound(new { message = "Trainer not found" });
            }

            _trainerService.Delete(uid);
            return Ok(new { message = "Trainer deleted successfully" });
        }
    }
}