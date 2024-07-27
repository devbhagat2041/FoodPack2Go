using FoodPack2Go.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodPack2Go.Infrastructure.Interfaces
{
    public interface IOrderManage
    {
        IEnumerable<Orders> GetAll();
        Task<Orders> GetById(int id);
        void Add(Orders order);
        Orders GetOrderById(int id);
        Task Update(Orders order);
        Task Delete(int id);
        // IEnumerable<TotalOrdersModel> GetOrders();

        IEnumerable<DailyOrderChartData> GetDailyOrderChartData();
        IEnumerable<WeeklyOrderChartData> GetWeeklyOrderChartData();
        IEnumerable<MonthlyOrderChartData> GetMonthlyOrderChartData();
        IEnumerable<YearlyOrderChartData> GetYearlyOrderChartData();





    }
}
