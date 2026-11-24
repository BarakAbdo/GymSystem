using GymSystem.Application.Dtos.SubscriptionsDtos;
using GymSystem.Application.Services;

using GymSystem.Application.Services.PlanService;
using GymSystem.Application.Services.SubscriptionService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;

namespace GymSystem.Web.Controllers
{
    public class SubscriptionsController : Controller
    {
        private readonly ISubscriptionService _subscriptionService;
        private readonly IMemberService _memberService;
        private readonly IPlanService _planService;

        public SubscriptionsController(
            ISubscriptionService subscriptionService,
            IMemberService memberService,
            IPlanService planService)
        {
            _subscriptionService = subscriptionService;
            _memberService = memberService;
            _planService = planService;
        }

        public IActionResult Index()
        {
            var subscriptions = _subscriptionService.GetAll();

            // أضفنا هذي السطور عشان تتعبى القوائم أول ما تفتح صفحة الـ Index ويظهرون في البوب أب
            ViewBag.Members = new SelectList(_memberService.GetAll(), "Id", "Name");
            ViewBag.Plans = new SelectList(_planService.GetAll(), "Id", "Name");

            return View(subscriptions);
        }

        public IActionResult Details(string uid)
        {
            var subscription = _subscriptionService.GetByUid(uid);
            if (subscription == null) return NotFound();
            return View(subscription);
        }

        public IActionResult Create()
        {
            // تجهيز القوائم المنسدلة للأعضاء والباقات في الـ ViewBag
            ViewBag.Members = new SelectList(_memberService.GetAll(), "Id", "Name");
            ViewBag.Plans = new SelectList(_planService.GetAll(), "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateSubscriptionDto dto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Members = new SelectList(_memberService.GetAll(), "Id", "Name", dto.MemberId);
                ViewBag.Plans = new SelectList(_planService.GetAll(), "Id", "Name", dto.PlanId);
                return View(dto);
            }

            _subscriptionService.Add(dto);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(string uid)
        {
            var subscription = _subscriptionService.GetByUid(uid);
            if (subscription == null) return NotFound();

            var updateDto = new UpdateSubscriptionDto
            {
                MemberId = subscription.MemberId,
                PlanId = subscription.PlanId,
                StartDate = subscription.StartDate
            };

            ViewBag.Members = new SelectList(_memberService.GetAll(), "Id", "Name", updateDto.MemberId);
            ViewBag.Plans = new SelectList(_planService.GetAll(), "Id", "Name", updateDto.PlanId);

            return View(updateDto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(string uid, UpdateSubscriptionDto dto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Members = new SelectList(_memberService.GetAll(), "Id", "Name", dto.MemberId);
                ViewBag.Plans = new SelectList(_planService.GetAll(), "Id", "Name", dto.PlanId);
                return View(dto);
            }

            _subscriptionService.Update(uid, dto);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(string uid)
        {
            var subscription = _subscriptionService.GetByUid(uid);
            if (subscription == null) return NotFound();
            return View(subscription);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(string uid)
        {
            _subscriptionService.Delete(uid);
            return RedirectToAction(nameof(Index));
        }
    }
}