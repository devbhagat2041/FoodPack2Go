using FoodPack2Go.Core;
using FoodPack2Go.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FoodPack2Go.UI.Controllers
{
    public class ReviewController : Controller
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IProductRepo _productRepository;

        public ReviewController(IReviewRepository reviewRepository, IProductRepo productRepository)
        {
            _reviewRepository = reviewRepository;
            _productRepository = productRepository;
        }

        // GET: Reviews
        public async Task<IActionResult> Index()
        {
            var reviews = await _reviewRepository.GetAllReviewAsync();

            // Calculate average ratings for products
            var productIds = reviews.Select(r => r.ProductID).Distinct();
            foreach (var productId in productIds)
            {
                var averageRating = await _reviewRepository.GetAverageRatingForProductAsync(productId);

                // Update ProductModel with average rating
                var product = await _productRepository.GetById(productId);
                if (product != null)
                {
                    product.AverageRating = averageRating;

                    // Update the ProductModel in the database
                    await _productRepository.Update(product);
                }
            }

            return View(reviews);
        }

        [HttpPost]
        public async Task<IActionResult> SoftDelete(int productId)
        {
            try
            {
                // Set IsDeleted to 1 for the product if average rating is 2 or less
                var averageRating = await _reviewRepository.GetAverageRatingForProductAsync(productId);
                if (averageRating.HasValue && averageRating.Value <= 2)
                {
                    var product = await _productRepository.GetById(productId);
                    if (product != null)
                    {
                        product.IsDeleted = 1;
                        await _productRepository.Update(product);
                    }
                }

                TempData["successmessage"] = "Soft deleted product with low average rating.";
            }
            catch (Exception ex)
            {
                TempData["errormessage"] = ex.Message;
            }

            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        public async Task<IActionResult> UndoSoftDelete(int productId)
        {
            try
            {
                // Reset IsDeleted to 0 for the product
                var product = await _productRepository.GetById(productId);
                if (product != null)
                {
                    product.IsDeleted = 0;
                    await _productRepository.Update(product);
                }

                TempData["successmessage"] = "Undone soft delete for the product.";
            }
            catch (Exception ex)
            {
                TempData["errormessage"] = ex.Message;
            }

            return RedirectToAction(nameof(Index));
        }

        #region IDisposable

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _reviewRepository.Dispose();
            }
            base.Dispose(disposing);
        }

        #endregion
    }
}
