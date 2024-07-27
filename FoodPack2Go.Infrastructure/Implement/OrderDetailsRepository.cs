//using Microsoft.EntityFrameworkCore;
//using System.Collections.Generic;
//using System.Threading.Tasks;
//using FoodPack2Go.Core;
//using FoodPack2Go.Infrastructure.Interfaces;

//namespace FoodPack2Go.Infrastructure.Implement
//{
//    public class OrderDetailsRepository : IOrderDetailsRepository
//    {
//        private readonly MyAppDbContext _context;

//        public OrderDetailsRepository(MyAppDbContext context)
//        {
//            _context = context;
//        }

//        public async Task<List<OrderDetails>> GetAllOrderDetailsAsync()
//        {
//            return await _context.OrderDetails.ToListAsync();
//        }

//        // Other read-only methods can be implemented as needed

//        #region IDisposable

//        public void Dispose()
//        {
//            _context.Dispose();
//        }

//        #endregion
//    }
//}


using FoodPack2Go.Core;
using FoodPack2Go.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodPack2Go.Infrastructure.Implement
{
    public class OrderDetailsRepository : IOrderDetailsRepository
    {
        private readonly MyAppDbContext _context;

        public OrderDetailsRepository(MyAppDbContext context)
        {
            _context = context;
        }

        public async Task<List<OrderDetails>> GetAllOrderDetailsAsync()
        {
            return await _context.OrderDetails.ToListAsync();
        }

        public async Task<List<OrderDetails>> GetPaginatedOrderDetailsAsync(int page, int pageSize)
        {
            return await _context.OrderDetails
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }


        public async Task<int> GetTotalOrderDetailsCountAsync()
        {
            return await _context.OrderDetails.CountAsync();
        }

        // Other read-only methods can be implemented as needed

        #region IDisposable

        public void Dispose()
        {
            _context.Dispose();
        }

        #endregion
    }
}
