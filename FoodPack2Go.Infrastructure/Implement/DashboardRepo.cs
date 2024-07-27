//using FoodPack2Go.Core;
//using FoodPack2Go.Infrastructure.Interfaces;
//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace FoodPack2Go.Infrastructure.Implement
//{
//    public class DashboardRepo : IDashboardRepo
//    {
//        private readonly MyAppDbContext app;

//        public DashboardRepo(MyAppDbContext _app)
//        {
//            this.app = _app;
//        }

//        //public async Task<int> GetTotalProducts()
//        //{
//        //    return await app.Products.CountAsync();
//        //}
//        public async Task<int> GetTotalProducts()
//        {
//            return await app.Products.CountAsync();
//        }

//        public async Task<int> GetTotalUsers()
//        {
//            return await app.Customer.CountAsync();
//        }
//    }
//}
