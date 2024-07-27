using FoodPack2Go.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodPack2Go.Infrastructure.Interfaces
{
    public interface IRepository
    {
        AdminModel LoginAdmin(string Email, string Password);
        DateTime? GetLastLogin(string Email);
        string OrderDistributionChart();

        int GetTotalCustomers();

        int GetTotalProducts();

        int GetTotalOrders();




    }
}
