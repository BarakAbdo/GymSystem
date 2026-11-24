using GymSystem.Application.Dtos.AttendanceDtos;
using GymSystem.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace GymSystem.Web.Controllers
{
    public class AttendancesController : Controller
    {
        private readonly IAttendanceService _attendanceService;
        private readonly IMemberService _memberService; // لجلب قائمة الأعضاء في القائمة المنسدلة

        public AttendancesController(IAttendanceService attendanceService, IMemberService memberService)
        {
            _attendanceService = attendanceService;
            _memberService = memberService;
        }

        // GET: Attendances
        public IActionResult Index()
        {
            var attendances = _attendanceService.GetAll();
            ViewBag.Members = _memberService.GetAll(); // لإرسال الأعضاء لـ Dropdown الإضافة والتعديل
            return View(attendances);
        }

        // POST: Attendances/Create
        [HttpPost]
        public IActionResult Create(CreateAttendanceDto dto)
        {
            if (ModelState.IsValid)
            {
                _attendanceService.Add(dto);
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }

        // POST: Attendances/Edit
        [HttpPost]
        public IActionResult Edit(string uid, UpdateAttendanceDto dto)
        {
            if (ModelState.IsValid)
            {
                _attendanceService.Update(uid, dto);
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }

        // POST: Attendances/Delete
        [HttpPost]
        public IActionResult Delete(string uid)
        {
            _attendanceService.Delete(uid);
            return RedirectToAction(nameof(Index));
        }
    }
}