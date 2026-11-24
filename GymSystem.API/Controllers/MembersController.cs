using GymSystem.Application.Dtos.MemberDtos;
using GymSystem.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace GymSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembersController : ControllerBase
    {
        private readonly IMemberService _memberService;

        public MembersController(IMemberService memberService)
        {
            _memberService = memberService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var members = _memberService.GetAll();
            return Ok(members);
        }

        [HttpGet("{uid}")]
        public IActionResult GetByUid(string uid)
        {
            var member = _memberService.GetByUid(uid);
            if (member == null)
            {
                return NotFound(new { message = "Member not found" });
            }

            return Ok(member);
        }

        [HttpPost]
        public IActionResult Add([FromBody] CreateMemberDto dto)
        {
            if (dto == null || !ModelState.IsValid)
            {
                return BadRequest(new { message = "Invalid member data" });
            }

            _memberService.Add(dto);
            return Ok(new { message = "Member added successfully" });
        }

        [HttpPut("{uid}")]
        public IActionResult Update(string uid, [FromBody] UpdateMemberDto dto)
        {
            if (dto == null || !ModelState.IsValid)
            {
                return BadRequest(new { message = "Invalid member data" });
            }

            var existing = _memberService.GetByUid(uid);
            if (existing == null)
            {
                return NotFound(new { message = "Member not found" });
            }

            _memberService.Update(uid, dto);
            return Ok(new { message = "Member updated successfully" });
        }

        [HttpDelete("{uid}")]
        public IActionResult Delete(string uid)
        {
            var existing = _memberService.GetByUid(uid);
            if (existing == null)
            {
                return NotFound(new { message = "Member not found" });
            }

            _memberService.Delete(uid);
            return Ok(new { message = "Member deleted successfully" });
        }
    }
}