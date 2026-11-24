using GymSystem.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace GymSystem.Web.Controllers
{
    public class DashboardsController : Controller
    {
        private readonly IDashboardService _dashboardService;

        public DashboardsController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        public IActionResult Index()
        {
            var data = _dashboardService.GetDashboardData();
            return View(data);
        }
    }
}