using FoodPack2Go.Core;
using FoodPack2Go.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Mvc;

namespace FoodPack2Go.UI.Controllers
{
    public class FootersController : Controller
    {
        private readonly IFooterRepo db;

        public FootersController(IFooterRepo _db)
        {
            this.db = _db;
        }

        public IActionResult Index()
        {
            var ft = db.GetAll();
            return View(ft);
        }

        //[HttpGet]
        //public IActionResult Create()
        //{
        //    return View();
        //}
        //[HttpPost]
        //public IActionResult Create(Footer footer)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Add(footer);
        //        TempData["successmessage"] = "inserted successfully!";

        //        return RedirectToAction("Index");
        //    }
        //    return View();
        //}


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Footer footer)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Add(footer);
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


        //[HttpGet]
        //public async Task<IActionResult> Edit(int id)
        //{
        //    Footer footer = await db.GetById(id);
        //    if (footer != null)
        //    {
        //        return View(footer);


        //    }

        //    return RedirectToAction("Index");
        //}
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                Footer footer = await db.GetById(id);
                if (footer != null)
                {
                    return View(footer);
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
        public async Task<IActionResult> Edit(Footer footer)
        {
            try
            {

                await db.Update(footer);
                TempData["successmessage"] = "Updated!!!!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["errormessage"] = ex.Message;
                return View();
            }
        }
        //[HttpPost]
        //public async Task<IActionResult> Edit(Footer footer)
        //{
        //    await db.Update(footer);
        //    return RedirectToAction("Index");
        //}


    }
}
