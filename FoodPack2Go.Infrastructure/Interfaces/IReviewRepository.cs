using FoodPack2Go.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodPack2Go.Infrastructure.Interfaces
{
    public interface IReviewRepository : IDisposable
    {
        Task<List<Review>> GetAllReviewAsync();
        Task<double?> GetAverageRatingForProductAsync(int productId);

        // Other read-only methods can be added if needed
    }
}
