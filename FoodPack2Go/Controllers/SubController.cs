using FoodPack2Go.Core;
using FoodPack2Go.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Threading.Tasks;

namespace FoodPack2Go.UI.Controllers
{
    public class SubController : Controller
    {
        private readonly ISub db;
        private readonly ICategoryRepo categoryRepo;

        public SubController(ISub _db, ICategoryRepo _categoryRepo)
        {
            db = _db;
            categoryRepo = _categoryRepo;
        }

        public IActionResult Index()
        {
            var subcat = db.GetAll();
            return View(subcat);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Categories = new SelectList(categoryRepo.GetAll(), "CategoryID", "CategoryName");
            return View();
        }

        [HttpPost]
        public IActionResult Create(SubModel s)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Add(s);
                    TempData["successmessage"] = "Inserted!!!!";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["errormessage"] = "Model is invalid";
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
            var sub = await db.GetById(id);
            if (sub == null)
            {
                return NotFound();
            }

            ViewBag.Categories = new SelectList(categoryRepo.GetAll(), "CategoryID", "CategoryName", sub.CategoryID);
            return View(sub);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, SubModel model)
        {
            if (id != model.SubCategoryID)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await db.Update(model);
                    TempData["successmessage"] = "Updated!!!!";

                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    TempData["errormessage"] = ex.Message;
                    return View(model);
                }
            }
            else
            {
                TempData["errormessage"] = "Model is invalid";
                return View(model);
            }
        }


        [HttpGet, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await db.Delete(id);
                TempData["successmessage"] = "Deleted!!!!";
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
