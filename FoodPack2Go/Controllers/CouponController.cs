using FoodPack2Go.Core;
using FoodPack2Go.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FoodPack2Go.UI.Controllers
{
     public class CouponController : Controller
    {
        private readonly ICouponRepository _couponRepository;

        public CouponController(ICouponRepository couponRepository)
        {
            _couponRepository = couponRepository;
        }

        public IActionResult Index()
        {
            var coupons = _couponRepository.GetAll();
            return View(coupons);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Coupon coupon)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _couponRepository.Add(coupon);
                    TempData["successmessage"] = "Inserted successfully!";

                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["errormessage"] = "Model is invalid.";
                    return View();
                }
            }
            catch (Exception ex)
            {
                TempData["errormessage"] = ex.Message;
                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var coupon = await _couponRepository.GetById(id);
                if (coupon != null)
                {
                    return View(coupon);
                }
            }
            catch (Exception)
            {
                return View();
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Coupon coupon)
        {
            try
            {
                await _couponRepository.Update(coupon);
                TempData["successmessage"] = "Update successfully!";

                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var coupon = await _couponRepository.GetById(id);
                if (coupon != null)
                {
                    return View(coupon);
                }
            }
            catch (Exception ex)
            {
                TempData["errormessage"] = ex.Message;
                return View();
            }

            TempData["errormessage"] = $"Not found with Id:{id}";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _couponRepository.Delete(id);
                TempData["successmessage"] = "Deleted successfully!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["errormessage"] = ex.Message;
                return View();
            }
        }
    }
}
