using FoodPack2Go.Core;
using FoodPack2Go.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FoodPack2Go.UI.Controllers
{
    public class ContactController : Controller
    {
        private readonly IContactRepo db;

        public ContactController(IContactRepo _db)
        {
            this.db = _db;
        }

        public IActionResult Index()
        {
            var contact = db.GetAll();
            return View(contact);
        }





        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(ContactUs c)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Add(c);
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
                ContactUs c = await db.GetById(id);
                if (c != null)
                {
                    return View(c);
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
        public async Task<IActionResult> Edit(ContactUs c)
        {
            try
            {
                if (c.ContactImage == null)
                {

                    var contact = await db.GetById(c.ContactId);
                    c.ContactImage = contact.ContactImage;

                }

                await db.Update(c);
                TempData["successmessage"] = "Updated!!!!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["errormessage"] = ex.Message;
                return View();
            }
        }





        //[HttpGet]
        //public IActionResult Create()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public IActionResult Create(ContactUs c)
        //{
        //    if(ModelState.IsValid)
        //    {
        //        db.Add(c);
        //        return RedirectToAction("Index");

        //    }
        //    else
        //    {
        //        return View();
        //    }

        //}


        //[HttpGet]
        //public async Task<IActionResult> Edit(int id)
        //{
        //    ContactUs contact = await db.GetById(id);
        //    if(contact != null)
        //    {
        //       return View(contact);
        //    }

        //    return RedirectToAction("Index");
        //}

        //[HttpPost]
        //public async Task<IActionResult> Edit(ContactUs c)
        //{
        //    await db.Update(c);
        //    return RedirectToAction("Index");
        //}








        //public IActionResult Edit(int id)
        //{
        //    var product = _productRepository.GetById(id);
        //    if (product == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(product);
        //}

        //[HttpPost]
        //public IActionResult Edit(Product product)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _productRepository.Update(product);
        //        return RedirectToAction("Index");
        //    }
        //    return View(product);
        //}




    }
}
