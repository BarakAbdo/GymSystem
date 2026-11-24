using GymSystem.Application.Dtos.PaymentDtos;
using GymSystem.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace GymSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentsController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var payments = _paymentService.GetAll();
            return Ok(payments);
        }

        [HttpGet("{uid}")]
        public IActionResult GetByUid(string uid)
        {
            var payment = _paymentService.GetByUid(uid);
            if (payment == null)
            {
                return NotFound(new { message = "Payment not found" });
            }

            return Ok(payment);
        }

        [HttpPost]
        public IActionResult Add([FromBody] CreatePaymentDto dto)
        {
            if (dto == null || !ModelState.IsValid)
            {
                return BadRequest(new { message = "Invalid payment data" });
            }

            _paymentService.Add(dto);
            return Ok(new { message = "Payment added successfully" });
        }

        [HttpPut("{uid}")]
        public IActionResult Update(string uid, [FromBody] UpdatePaymentDto dto)
        {
            if (dto == null || !ModelState.IsValid)
            {
                return BadRequest(new { message = "Invalid payment data" });
            }

            var existing = _paymentService.GetByUid(uid);
            if (existing == null)
            {
                return NotFound(new { message = "Payment not found" });
            }

            _paymentService.Update(uid, dto);
            return Ok(new { message = "Payment updated successfully" });
        }

        [HttpDelete("{uid}")]
        public IActionResult Delete(string uid)
        {
            var existing = _paymentService.GetByUid(uid);
            if (existing == null)
            {
                return NotFound(new { message = "Payment not found" });
            }

            _paymentService.Delete(uid);
            return Ok(new { message = "Payment deleted successfully" });
        }
    }
}