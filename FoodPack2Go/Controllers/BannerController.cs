using FoodPack2Go.Core;
using FoodPack2Go.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FoodPack2Go.UI.Controllers
{
    public class BannerController : Controller
    {
        private readonly IBannerRepository db;

        public BannerController(IBannerRepository db)
        {
            this.db = db;
        }

        public IActionResult Index()
        {
            var b = db.GetAll();
            return View(b);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Banner b)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Add(b);
                    TempData["successMessage"] = "Inserted!!!!";

                    return RedirectToAction("Index");

                }
                else
                {
                    TempData["errormessage"] = "model is invalid";
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
                Banner pro = await db.GetById(id);
                if (pro != null)
                {
                    return View(pro);
                }
            }
            catch (Exception ex)
            {
                TempData["errormessage"] = ex.Message;
                return View();
            }

            TempData["errormessage"] = $"not found with Id:{id}";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Banner b)
        {
            try
            {
                if (b.BannerImage == null)
                {

                    var Banner = await db.GetById(b.BannerId);
                    b.BannerImage = Banner.BannerImage;

                }

                await db.Update(b);
                TempData["successMessage"] = "Updated!!!!"; 
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                Banner banner = await db.GetById(id);
                if (banner != null)
                {
                    return View(banner);
                }
            }
            catch (Exception ex)
            {

                TempData["errorMessage"] = ex.Message;
                return View();

            }
            TempData["errorMessage"] = $"not found with Id:{id}";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await db.Delete(id);
                TempData["successMessage"] = "Deleted!!!!";

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

                TempData["errorMessage"] = ex.Message;
                return View();

            }
        }


    }
}
