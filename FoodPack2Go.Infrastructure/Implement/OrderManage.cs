using FoodPack2Go.Core;
using FoodPack2Go.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodPack2Go.Infrastructure.Implement
{
    public class OrderManage : IOrderManage
    {
        private readonly MyAppDbContext app;

        public OrderManage(MyAppDbContext _app)
        {
            this.app = _app;
        }

        public void Add(Orders order)
        {
            app.Orders.Add(order);
            app.SaveChanges();
        }
        public async Task<Orders> GetById(int id)
        {
            return await app.Orders.FindAsync(id);
        }
        public Orders GetOrderById(int id)
        {
            return app.Orders.FirstOrDefault(p=>p.OrderID==id);
        }

        public IEnumerable<Orders> GetAll()
        {
            return app.Orders.ToList();
        }
        public async Task Update(Orders order)
        {
            var ord = await app.Orders.FindAsync(order.OrderID);
            if (ord != null)
            {
                //ord.CustomerID = order.CustomerID;
               
                ord.OrderStatus = order.OrderStatus;
               
                app.Update(ord);
                app.SaveChanges();
            }
        }
        public async Task Delete(int id)
        {
            var ord = await app.Orders.FindAsync(id);
            if (ord != null)
            {
                app.Orders.Remove(ord);
                app.SaveChanges();
            }
        }

        //Total Orders Per Day, Week, Month, Year 



        public IEnumerable<DailyOrderChartData> GetDailyOrderChartData()
        {
            return app.Orders
                .AsEnumerable()
                .GroupBy(o => o.OrderDateWithoutTime)
                .Select(group => new DailyOrderChartData { Date = group.Key, OrderCount = group.Count() })
                .OrderBy(item => item.Date)
                .ToList();
        }

        public IEnumerable<WeeklyOrderChartData> GetWeeklyOrderChartData()
        {
            return app.Orders
                .AsEnumerable()
                .GroupBy(o => CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(o.OrderDate, CalendarWeekRule.FirstDay, DayOfWeek.Sunday))
                .Select(group => new WeeklyOrderChartData { Week = group.Key, OrderCount = group.Count() })
                .OrderBy(item => item.Week)
                .ToList();
        }

        public IEnumerable<MonthlyOrderChartData> GetMonthlyOrderChartData()
        {
            return app.Orders
                .AsEnumerable()
                .GroupBy(o => new { o.OrderDate.Year, o.OrderDate.Month })
                .Select(group => new MonthlyOrderChartData { Month = new DateTime(group.Key.Year, group.Key.Month, 1), OrderCount = group.Count() })
                .OrderBy(item => item.Month)
                .ToList();
        }

        public IEnumerable<YearlyOrderChartData> GetYearlyOrderChartData()
        {
            return app.Orders
                .AsEnumerable()
                .GroupBy(o => o.OrderDate.Year)
                .Select(group => new YearlyOrderChartData { Year = group.Key, OrderCount = group.Count() })
                .OrderBy(item => item.Year)
                .ToList();
        }



    }
}
















