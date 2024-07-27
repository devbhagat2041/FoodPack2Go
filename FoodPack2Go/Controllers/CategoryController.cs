using FoodPack2Go.Core;
using FoodPack2Go.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;



namespace FoodPack2Go.UI.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepo db;


        public CategoryController(ICategoryRepo _db)
        {
            db = _db;
        }


        public IActionResult Index()
        {
            var cat = db.GetAll();
            return View(cat);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(CategoryModel category)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Addcategory(category);
                    TempData["successmessage"] = "Inserted!!!!";

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
                CategoryModel cat = await db.GetById(id);
                if (cat != null)
                {
                    return View(cat);
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
        public async Task<IActionResult> Edit(CategoryModel category)
        {
            try
            {

                await db.Updatecategory(category);
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
                CategoryModel cat = await db.GetById(id);
                if (cat != null)
                {
                    return View(cat);
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
                await db.Deletecategory(id);
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
