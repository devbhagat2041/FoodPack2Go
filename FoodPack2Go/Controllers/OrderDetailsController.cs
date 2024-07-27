//using FoodPack2Go.Infrastructure.Interfaces;
//using Microsoft.AspNetCore.Mvc;

//namespace FoodPack2Go.UI.Controllers
//{
//    public class OrderDetailsController : Controller
//    {
//        private readonly IOrderDetailsRepository _orderDetailsRepository;

//        public OrderDetailsController(IOrderDetailsRepository orderDetailsRepository)
//        {
//            _orderDetailsRepository = orderDetailsRepository;
//        }

//        // GET: OrderDetails
//        public async Task<IActionResult> Index()
//        {
//            var orderDetails = await _orderDetailsRepository.GetAllOrderDetailsAsync();
//            return View(orderDetails);
//        }

//        // Other read-only actions can be added if needed

//        #region IDisposable

//        protected override void Dispose(bool disposing)
//        {
//            if (disposing)
//            {
//                _orderDetailsRepository.Dispose();
//            }
//            base.Dispose(disposing);
//        }

//        #endregion
//    }
//}
//using FoodPack2Go.Infrastructure.Interfaces;
//using Microsoft.AspNetCore.Mvc;



using FoodPack2Go.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FoodPack2Go.UI.Controllers
{
    public class OrderDetailsController : Controller
    {
        private readonly IOrderDetailsRepository _orderDetailsRepository;
        private const int PageSize = 10; // Number of items per page

        public OrderDetailsController(IOrderDetailsRepository orderDetailsRepository)
        {
            _orderDetailsRepository = orderDetailsRepository;
        }

        // GET: OrderDetails
        public async Task<IActionResult> Index(int page = 1)
        {
            var orderDetails = await _orderDetailsRepository.GetPaginatedOrderDetailsAsync(page, PageSize);
            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling(await _orderDetailsRepository.GetTotalOrderDetailsCountAsync() / (double)PageSize);
            return View(orderDetails);
        }

        // Other read-only actions can be added if needed

        #region IDisposable

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _orderDetailsRepository.Dispose();
            }
            base.Dispose(disposing);
        }

        #endregion
    }
}
