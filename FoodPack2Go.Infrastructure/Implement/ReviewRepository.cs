using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using FoodPack2Go.Core;
using FoodPack2Go.Infrastructure.Interfaces;

namespace FoodPack2Go.Infrastructure.Implement
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly MyAppDbContext _context;

        public ReviewRepository(MyAppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Review>> GetAllReviewAsync()
        {
            return await _context.Review.ToListAsync();
        }

        public async Task<double?> GetAverageRatingForProductAsync(int productId)
        {
            var averageRating = await _context.Review
                .Where(r => r.ProductID == productId && r.Rating.HasValue)
                .AverageAsync(r => r.Rating.Value);

            return averageRating;
        }

        #region IDisposable

        public void Dispose()
        {
            _context.Dispose();
        }

        #endregion
    }

}
