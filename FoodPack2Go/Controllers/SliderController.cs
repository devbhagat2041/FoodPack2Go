using FoodPack2Go.Core;
using FoodPack2Go.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FoodPack2Go.UI.Controllers
{
    public class SliderController : Controller
    {
        private readonly ISliderRepository db;

        public SliderController(ISliderRepository _db)
        {
            this.db = _db;
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
        public IActionResult Create(Slider s)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Add(s);
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
                Slider pro = await db.GetById(id);
                if (pro != null)
                {
                    return View(pro);
                }
            }
            catch (Exception )
            {
               
                return View();
            }

            
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Slider s)
        {
            try
            {
                if (s.SliderImage == null)
                {

                    var slider = await db.GetById(s.SliderId);
                    s.SliderImage = slider.SliderImage;

                }
                await db.Update(s);
               
                return RedirectToAction(nameof(Index));
            }
            catch (Exception )
            {
               
                return View();
            }
        }





    }
}
