using FoodPack2Go.Core;
using FoodPack2Go.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FoodPack2Go.UI.Controllers
{
    public class CategoryListController : Controller
    {
        private readonly ICategoylistRepository db;

        public CategoryListController(ICategoylistRepository db)
        {
            this.db = db;
        }

        public IActionResult Index()
        {
            var s = db.GetAll();
            return View(s);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(CategoryList s)
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
                    return View();
                }
            }
            catch (Exception )
            {
                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                CategoryList cat = await db.GetById(id);
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
        public async Task<IActionResult> Edit(CategoryList category)
        {
            try
            {
                if (category.CategoryListImage == null)
                {

                    var categorylist = await db.GetById(category.CategoryListId);
                    category.CategoryListImage = categorylist.CategoryListImage;

                }

                if (category.CategoryListImage2 == null)
                {

                    var categorylist = await db.GetById(category.CategoryListId);
                    category.CategoryListImage2 = categorylist.CategoryListImage2;

                }

                if (category.CategoryListImage3 == null)
                {

                    var categorylist = await db.GetById(category.CategoryListId);
                    category.CategoryListImage3 = categorylist.CategoryListImage3;

                }

                if (category.CategoryListImage4 == null)
                {

                    var categorylist = await db.GetById(category.CategoryListId);
                    category.CategoryListImage4 = categorylist.CategoryListImage4;

                }

                if (category.CategoryListImage5 == null)
                {

                    var categorylist = await db.GetById(category.CategoryListId);
                    category.CategoryListImage5 = categorylist.CategoryListImage5;

                }

                await db.Update(category);
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
                CategoryList cat = await db.GetById(id);
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

        //[HttpGet]
        //public async Task<IActionResult> Edit(int id)
        //{
        //    try
        //    {
        //        CategoryList pro = await db.GetById(id);
        //        if (pro != null)
        //        {
        //            return View(pro);
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        return View();
        //    }
        //    return RedirectToAction(nameof(Index));
        //}

        //[HttpPost]
        //public async Task<IActionResult> Edit(CategoryList s)
        //{
        //    try
        //    {
        //        await db.Update(s);
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch (Exception)
        //    {
        //        return View();
        //    }
        //}

        //[HttpGet]
        //public async Task<IActionResult> Delete(int id)
        //{
        //    try
        //    {
        //        CategoryList pro = await db.GetById(id);
        //        if (pro != null)
        //        {
        //            return View(pro);
        //        }
        //    }
        //    catch (Exception )
        //    {
        //        return View(); 
        //    }

        //    return RedirectToAction(nameof(Index));
        //}

        //[HttpPost, ActionName("Delete")]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    try
        //    {
        //        await db.Delete(id);
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch (Exception)
        //    {
        //        return View();

        //    }
        //}



    }
}
