using FoodPack2Go.Core;using FoodPack2Go.Infrastructure.Interfaces;using Microsoft.AspNetCore.Mvc;using Microsoft.AspNetCore.Http;using System.Drawing.Printing;namespace FoodPack2Go.Controllers{    public class HomeController : Controller    {        private readonly IRepository db;        private readonly IOrderManage _orderRepository;        private readonly IPaymentRepository _paymentRepository;        public HomeController(IRepository db, IOrderManage ob, IPaymentRepository paymentRepository)        {            this.db = db;            this._orderRepository = ob;            _paymentRepository = paymentRepository;        }        public IActionResult Index()        {            return View();        }        [HttpGet]        public IActionResult Login()        {            if (HttpContext.Session.GetString("AdminSession") != null)            {                return RedirectToAction("Dashboard");            }            return View("Index");        }        [HttpPost]        public IActionResult Login(string Email, string Password)        {            if (string.IsNullOrEmpty(Email)        || string.IsNullOrEmpty(Password))            {                ViewBag.Message = "Please enter both email and password.";                return View("Index");  // Return the login view with error message
            }            var myadmin = db.LoginAdmin(Email, Password);            if (myadmin != null)            {                HttpContext.Session.SetString("AdminSession", myadmin.Email); // Set AdminSession here
                HttpContext.Session.SetString("AdminEmail", myadmin.Email);                HttpContext.Session.SetString("AdminPassword", myadmin.Password);
                // Pass the AdminModel as route data when redirecting to the Dashboard action
                return RedirectToAction("Dashboard", "Home");            }            else            {                ViewBag.Message = "Invalid email or password.";                return View("Index");            }        }        public IActionResult Logout()        {            if (HttpContext.Session.GetString("AdminSession") != null)            {                HttpContext.Session.Remove("AdminSession");                HttpContext.Session.Remove("AdminEmail");                HttpContext.Session.Remove("AdminPassword");                return RedirectToAction("Index");            }            return View();        }        public IActionResult Dashboard(string? charttype, int page = 1, int pageSize = 10)        {            var tc = db.GetTotalCustomers();            ViewData["TotalCustomers"] = tc;            var tp = db.GetTotalProducts();            ViewData["TotalProducts"] = tp;            var to = db.GetTotalOrders();            ViewData["TotalOrders"] = to;            if (HttpContext.Session.GetString("AdminEmail") != null)            {                var chart = db.OrderDistributionChart();                var daily_chart = _orderRepository.GetDailyOrderChartData();                var weekly_chart = _orderRepository.GetWeeklyOrderChartData();                var yearly_chart = _orderRepository.GetYearlyOrderChartData();                var monthly_chart = _orderRepository.GetMonthlyOrderChartData();                var model = new AdminModel                {                    Email = HttpContext.Session.GetString("AdminEmail").ToString(),                    Password = HttpContext.Session.GetString("AdminPassword").ToString(),                    LastLogin = DateTime.UtcNow,                    chart = chart,                    DailyChart = daily_chart,                    WeeklyChart = weekly_chart,                    MonthlyChart = monthly_chart,                    YearlyChart = yearly_chart                };                ViewBag.MySession = HttpContext.Session.GetString("AdminEmail").ToString();
                // Retrieve payment data
                var payments = _paymentRepository.GetAllPaymentAsync().Result;
                ViewBag.Payments = payments;

                if (string.IsNullOrEmpty(charttype))
                {
                    ViewData["type"] = "no";
                }
                else
                {
                    ViewData["type"] = charttype;
                }


                return View(model);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }        public IActionResult ActivityLog()        {            if (HttpContext.Session.GetString("AdminSession") != null)            {
                // Retrieve the last login information from the database
                string userEmail = HttpContext.Session.GetString("AdminSession");                var lastLogin = db.GetLastLogin(userEmail);

                // Pass the last login information to the view
                return View(lastLogin);            }            else            {                return RedirectToAction("Index");            }        }    }}