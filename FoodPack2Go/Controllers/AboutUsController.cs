using FoodPack2Go.Core;
using FoodPack2Go.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FoodPack2Go.UI.Controllers
{
    public class AboutUsController : Controller
    {
        private readonly IAboutUsRepo _aboutUsRepo;

        public AboutUsController(IAboutUsRepo aboutUsRepo)
        {
            _aboutUsRepo = aboutUsRepo;
        }

        public IActionResult Index()
        {
            var aboutUsList = _aboutUsRepo.GetAll();
            return View(aboutUsList);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        


        [HttpPost]
        public IActionResult Create(AboutUsModel aboutUs)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _aboutUsRepo.Add(aboutUs);
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
            try
            {
                var aboutUs = await _aboutUsRepo.GetById(id);

                if (aboutUs != null)
                {
                    return View(aboutUs);
                }
            }
            catch (Exception ex)
            {
                TempData["errormessage"] = ex.Message;
            }

            TempData["errormessage"] = $"Not found with Id: {id}";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(AboutUsModel aboutUs)
        {
            try
            {
                if (aboutUs.AboutImage == null)
                {

                    var AboutUs = await _aboutUsRepo.GetById(aboutUs.AboutUsId);
                    aboutUs.AboutImage = AboutUs.AboutImage;

                }

                if (aboutUs.AboutImage2 == null)
                {

                    var AboutUs = await _aboutUsRepo.GetById(aboutUs.AboutUsId);
                    aboutUs.AboutImage2 = AboutUs.AboutImage2;

                }

                await _aboutUsRepo.Update(aboutUs);
                TempData["successmessage"] = "Updated!";
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
                AboutUsModel aboutUs = await _aboutUsRepo.GetById(id);
                if (aboutUs != null)
                {
                    return View(aboutUs);
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
                await _aboutUsRepo.Delete(id);
                TempData["successmessage"] = "Deleted!";
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
