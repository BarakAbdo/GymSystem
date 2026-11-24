using GymSystem.Application.Dtos.SubscriptionsDtos;
using GymSystem.Application.Services;
using GymSystem.Application.Services.SubscriptionService;
using Microsoft.AspNetCore.Mvc;

namespace GymSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriptionsController : ControllerBase
    {
        private readonly ISubscriptionService _subscriptionService;

        public SubscriptionsController(ISubscriptionService subscriptionService)
        {
            _subscriptionService = subscriptionService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var subscriptions = _subscriptionService.GetAll();
            return Ok(subscriptions);
        }

        [HttpGet("{uid}")]
        public IActionResult GetByUid(string uid)
        {
            var subscription = _subscriptionService.GetByUid(uid);
            if (subscription == null)
            {
                return NotFound(new { message = "Subscription not found" });
            }

            return Ok(subscription);
        }

        [HttpPost]
        public IActionResult Add([FromBody] CreateSubscriptionDto dto)
        {
            if (dto == null || !ModelState.IsValid)
            {
                return BadRequest(new { message = "Invalid subscription data" });
            }

            _subscriptionService.Add(dto);
            return Ok(new { message = "Subscription added successfully" });
        }

        [HttpPut("{uid}")]
        public IActionResult Update(string uid, [FromBody] UpdateSubscriptionDto dto)
        {
            if (dto == null || !ModelState.IsValid)
            {
                return BadRequest(new { message = "Invalid subscription data" });
            }

            var existing = _subscriptionService.GetByUid(uid);
            if (existing == null)
            {
                return NotFound(new { message = "Subscription not found" });
            }

            _subscriptionService.Update(uid, dto);
            return Ok(new { message = "Subscription updated successfully" });
        }

        [HttpDelete("{uid}")]
        public IActionResult Delete(string uid)
        {
            var existing = _subscriptionService.GetByUid(uid);
            if (existing == null)
            {
                return NotFound(new { message = "Subscription not found" });
            }

            _subscriptionService.Delete(uid);
            return Ok(new { message = "Subscription deleted successfully" });
        }
    }
}