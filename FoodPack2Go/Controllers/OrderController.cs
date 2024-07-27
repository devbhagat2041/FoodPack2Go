using FoodPack2Go.Core;
using FoodPack2Go.Infrastructure.Interfaces;
using IronBarCode;
using Microsoft.AspNetCore.Mvc;
using NReco.PdfGenerator;
using System.Drawing;
using ZXing;
using ZXing.QrCode;

namespace FoodPack2Go.UI.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderManage db;
        private readonly IWebHostEnvironment _environment;

        public OrderController(IOrderManage db, IWebHostEnvironment environment)
        {
            this.db = db;
            this._environment = environment;
        }

        public IActionResult Index(string? text,int? orderid)
        {
            var ord = db.GetAll();
            if (!string.IsNullOrEmpty(text))
            {
                ViewData["BarCode"] = text;
                ViewData["orderid"] = orderid;
            }
            else
            {
                ViewData["BarCode"] = null;
                ViewData["orderid"] = null;
            }
            return View(ord);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Orders orders)
        {
            if(ModelState.IsValid)
            {
                db.Add(orders);
                TempData["successMessage"] = "Inserted!!!!";

                return RedirectToAction("Index");
            }
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                Orders orders = await db.GetById(id);
                if (orders != null)
                {
                    return View(orders);
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
        public async Task<IActionResult> Edit(Orders orders)
        {
            try
            {

                await db.Update(orders);
                TempData["successMessage"] = "Updated!!!!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }

        //[HttpGet]
        //public async Task<IActionResult> Edit(int id)
        //{
        //    Orders orders = await db.GetById(id);
        //    if (orders != null)
        //    {
        //        return View(orders);
        //    }

        //    return RedirectToAction("Index");
        //}

        //[HttpPost]
        //public async Task<IActionResult> Edit(Orders orders)
        //{
        //    await db.Update(orders);
        //    return RedirectToAction("Index");
        //}

        public IActionResult Details(int id)
        {
            var order = db.GetById(id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }


        //Method For Charts

        public IActionResult DailyChart()
        {
            var dailyOrders = db.GetDailyOrderChartData();
            return View(dailyOrders);
        }

        public IActionResult WeeklyChart()
        {
            var weeklyOrders = db.GetWeeklyOrderChartData();
            return View(weeklyOrders);
        }

        public IActionResult MonthlyChart()
        {
            var monthlyOrders = db.GetMonthlyOrderChartData();
            return View(monthlyOrders);
        }

        public IActionResult YearlyChart()
        {
            var yearlyOrders = db.GetYearlyOrderChartData();
            return View(yearlyOrders);
        }


        //Barcode



        //pdf


        [HttpGet]
        public IActionResult GeneratePdf(int id)
        {
            var order = db.GetOrderById(id);
            var customer = db.GetById(id);

            var htmlContent = $@"
        <html>
        <head>
            <title>Order Details</title>
            <style>
                body {{
                    margin-top: 80px;
                    background: #eee;
                    font-family: Arial, sans-serif;
                }}
                .container {{
                    width: 80%;
                    margin: 0 auto;
                    padding: 20px;
                    background-color: #fff;
                }}
                .invoice-details ul {{
                    list-style-type: none;
                    padding: 0;
                }}
                .invoice-details li {{
                    margin-bottom: 10px;
                }}
                .invoice-items table {{
                    width: 100%;
                    border-collapse: collapse;
                    margin-top: 20px;
                }}
                .invoice-items th, .invoice-items td {{
                    border: 1px solid #ddd;
                    padding: 8px;
                    text-align: left;
                }}
                .invoice-items th {{
                    background-color: #f2f2f2;
                }}
                .invoice-footer p {{
                    margin-top: 20px;
                    text-align: center;
                }}
            </style>
        </head>
        <body>

        <div class='container bootdey'>
            <div class='row invoice row-printable'>
                <div class='col-md-10'>
                    <div class='panel panel-default plain' id='dash_0'>
                        <div class='panel-body p30'>
                            <div class='row'>
                                <div class='col-lg-12'>
                                    <div class='invoice-details mt25'>
                                        <div class='well'>
                                            <ul class='list-unstyled mb0'>
                                                <li><strong>Order ID :</strong> #{order.OrderID}</li>
                                                <li><strong>Customer ID : </strong> {order.CustomerID}</li>

                                                <li><strong>Order Date :</strong> {order.OrderDate.ToShortDateString()}</li>
                                                <li><strong>Sub Total : </strong> {order.SubTotal}</li>
                                                <li><strong>Discount : </strong> {order.Discount}</li>
                                                <li><strong>Total Amount : </strong> {order.TotalAmount}</li>

                                                <li><strong>Order Status : </strong> {order.OrderStatus}</li>
                                            </ul>
                                        </div>
                                    </div>
                                   
                                    <div class='invoice-footer mt25'>
                                        <p>Generated on {DateTime.UtcNow.Date.ToShortDateString()}</p>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        </body>
        </html>";

            var converter = new HtmlToPdfConverter();
            var pdfBytes = converter.GeneratePdf(htmlContent);

            return File(pdfBytes, "application/pdf", "details.pdf");
        }








    }
}
