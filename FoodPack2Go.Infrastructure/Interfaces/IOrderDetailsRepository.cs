using FoodPack2Go.Core;
using FoodPack2Go.Infrastructure.Interfaces;



using FoodPack2Go.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FoodPack2Go.Infrastructure.Interfaces
{
    public interface IOrderDetailsRepository : IDisposable
    {
        Task<List<OrderDetails>> GetAllOrderDetailsAsync();
        Task<List<OrderDetails>> GetPaginatedOrderDetailsAsync(int page, int pageSize);
        Task<int> GetTotalOrderDetailsCountAsync();
        // Other read-only methods can be added if needed
    }
}
