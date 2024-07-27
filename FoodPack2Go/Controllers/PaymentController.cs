using FoodPack2Go.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FoodPack2Go.UI.Controllers
{
    public class PaymentController : Controller
    {
        private readonly IPaymentRepository _paymentRepository;

        public PaymentController(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        // GET: Payment
        public async Task<IActionResult> Index()
        {
            var Payment = await _paymentRepository.GetAllPaymentAsync();
            return View(Payment);
        }

        // Other read-only actions can be added if needed

        #region IDisposable

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _paymentRepository.Dispose();
            }
            base.Dispose(disposing);
        }

        #endregion
    }
}
