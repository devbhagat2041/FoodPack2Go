using FoodPack2Go.Core;
using FoodPack2Go.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;

namespace FoodPack2Go.UI.Controllers
{
    public class MasterHomePageController : Controller
    {
        private readonly IMasterHomePageRepo db;

        public MasterHomePageController(IMasterHomePageRepo _db)
        {
            this.db = _db;
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
        public IActionResult Create(MasterHomePage b)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Add(b);
                    TempData["successmessage"] = "Inserted!!!!";
                    return RedirectToAction("Index");

                }
                else
                {
                    TempData["errormessage"] = "it is invalid";
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
                MasterHomePage pro = await db.GetById(id);
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
        public async Task<IActionResult> Edit(MasterHomePage b)
        {
            try
            {
                if (b.PromoImage == null)
                {

                    var masterhome = await db.GetById(b.HomeId);
                    b.PromoImage = masterhome.PromoImage;

                }
                if (b.BgImage == null)
                {

                    var masterhome = await db.GetById(b.HomeId);
                    b.BgImage = masterhome.BgImage;

                }

                await db.Update(b);
                TempData["successmessage"] = "Updated!!!!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["errormessage"] = ex.Message;
                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                MasterHomePage pro = await db.GetById(id);
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

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await db.Delete(id);
                TempData["successmessage"] = "deleted!!!!";

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
