using GymSystem.Application.Dtos.PaymentDtos;
using GymSystem.Application.Services;
using GymSystem.Application.Services.SubscriptionService;
using Microsoft.AspNetCore.Mvc;

namespace GymSystem.Web.Controllers
{
    public class PaymentsController : Controller
    {
        private readonly IPaymentService _paymentService;
        private readonly ISubscriptionService _subscriptionService;
        public PaymentsController(IPaymentService paymentService, ISubscriptionService subscriptionService)
        {
            _paymentService = paymentService;
            _subscriptionService = subscriptionService;
        }

        // GET: Payments
        public IActionResult Index()
        {
            var payments = _paymentService.GetAll();

            // جلب الاشتراكات لعرضها في القائمة المنسدلة بالمودال
            ViewBag.Subscriptions = _subscriptionService.GetAll(); // أو الـ Repo المناسب لديك

            return View(payments);
        }

        // POST: Payments/Add
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(CreatePaymentDto dto)
        {
            if (ModelState.IsValid)
            {
                _paymentService.Add(dto);
                return RedirectToAction(nameof(Index));
            }
            // في حال وجود خطأ في البيانات المدخلة، يمكنك إرجاع نفس الصفحة أو رسالة خطأ
            return RedirectToAction(nameof(Index));
        }

        // POST: Payments/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(string uid, UpdatePaymentDto dto)
        {
            if (ModelState.IsValid)
            {
                _paymentService.Update(uid, dto);
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }

        // POST: Payments/Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(string uid)
        {
            _paymentService.Delete(uid);
            return RedirectToAction(nameof(Index));
        }
    }
}