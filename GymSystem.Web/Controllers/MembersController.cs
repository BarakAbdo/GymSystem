using GymSystem.Application.Dtos.MemberDtos;
using GymSystem.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace GymSystem.Web.Controllers
{
    public class MembersController : Controller
    {
        private readonly IMemberService _memberService;

        public MembersController(IMemberService memberService)
        {
            _memberService = memberService;
        }

        // GET: Members
        public IActionResult Index()
        {
            var members = _memberService.GetAll();
            return View(members);
        }

        // GET: Members/Details/uid
        public IActionResult Details(string uid)
        {
            var member = _memberService.GetByUid(uid);
            if (member == null) return NotFound();
            return View(member);
        }

        // GET: Members/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Members/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateMemberDto dto)
        {
            if (!ModelState.IsValid) return View(dto);

            _memberService.Add(dto);
            return RedirectToAction(nameof(Index));
        }

        // GET: Members/Edit/uid
        public IActionResult Edit(string uid)
        {
            var member = _memberService.GetByUid(uid);
            if (member == null) return NotFound();

            var updateDto = new UpdateMemberDto
            {
                Name = member.Name,
                Email = member.Email,
                Phone = member.Phone
            };

            return View(updateDto);
        }

        // POST: Members/Edit/uid
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(string uid, UpdateMemberDto dto)
        {
            if (!ModelState.IsValid) return View(dto);

            _memberService.Update(uid, dto);
            return RedirectToAction(nameof(Index));
        }

        // GET: Members/Delete/uid
        public IActionResult Delete(string uid)
        {
            var member = _memberService.GetByUid(uid);
            if (member == null) return NotFound();
            return View(member);
        }

        // POST: Members/Delete/uid
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(string uid)
        {
            _memberService.Delete(uid);
            return RedirectToAction(nameof(Index));
        }
    }
}