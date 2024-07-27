using FoodPack2Go.Core;
using FoodPack2Go.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FoodPack2Go.UI.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerRepo db;

        public CustomerController(ICustomerRepo _db)
        {
            this.db = _db;
        }

        public IActionResult Index()
        {
            var c = db.GetAll();
            return View(c);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Customer c)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    c.CreatedBy = DateTime.Now;

                    db.Add(c);
                    return RedirectToAction("Index");

                }
                else
                {
                    return View();
                }
            }
            catch (Exception)
            {
                return View();
            }
        }



        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                Customer pro = await db.GetById(id);
                if (pro != null)
                {
                    return View(pro);
                }
            }
            catch (Exception)
            {
                return View();
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Customer c)
        {
            try
            {
                c.ModifiedBy = DateTime.Now;

                await db.Update(c);
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
                Customer c = await db.GetById(id);
                if (c != null)
                {
                    return View(c);
                }
            }
            catch (Exception)
            {
                return View();
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await db.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                return View();

            }
        }

    }
}
