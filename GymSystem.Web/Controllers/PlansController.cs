using GymSystem.Application.Dtos.PlanDtos;
using GymSystem.Application.Services.PlanService;
using Microsoft.AspNetCore.Mvc;

namespace GymSystem.Web.Controllers
{
    public class PlansController : Controller
    {
        private readonly IPlanService _planService;

        public PlansController(IPlanService planService)
        {
            _planService = planService;
        }

        public IActionResult Index()
        {
            var plans = _planService.GetAll();
            return View(plans);
        }

        public IActionResult Details(string uid)
        {
            var plan = _planService.GetByUid(uid);
            if (plan == null) return NotFound();
            return View(plan);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreatePlanDto dto)
        {
            if (!ModelState.IsValid) return View(dto);

            _planService.Add(dto);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(string uid)
        {
            var plan = _planService.GetByUid(uid);
            if (plan == null) return NotFound();

            var updateDto = new UpdatePlanDto
            {
                Name = plan.Name,
                Category = plan.Category,
                DurationDays = plan.DurationDays,
                Price = plan.Price
            };

            return View(updateDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(string uid, UpdatePlanDto dto)
        {
            if (!ModelState.IsValid) return View(dto);

            _planService.Update(uid, dto);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(string uid)
        {
            var plan = _planService.GetByUid(uid);
            if (plan == null) return NotFound();
            return View(plan);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(string uid)
        {
            _planService.Delete(uid);
            return RedirectToAction(nameof(Index));
        }
    }
}