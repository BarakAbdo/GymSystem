using GymSystem.Application.Services;
using GymSystem.Application.Services.PlanService;
using GymSystem.Application.Services.SubscriptionService;
using Microsoft.AspNetCore.Mvc;

namespace GymSystem.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMemberService _memberService;
        private readonly IPlanService _planService;
        private readonly ISubscriptionService _subscriptionService;

        public HomeController(
            IMemberService memberService,
            IPlanService planService,
            ISubscriptionService subscriptionService)
        {
            _memberService = memberService;
            _planService = planService;
            _subscriptionService = subscriptionService;
        }

        public IActionResult Index()
        {
            ViewBag.TotalMembers = _memberService.GetAll().Count();
            ViewBag.TotalPlans = _planService.GetAll().Count();
            ViewBag.TotalSubscriptions = _subscriptionService.GetAll().Count();

            return View();
        }
    }
}