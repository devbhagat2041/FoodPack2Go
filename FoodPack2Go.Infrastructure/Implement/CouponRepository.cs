using FoodPack2Go.Core;
using FoodPack2Go.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodPack2Go.Infrastructure.Implement
{
    public class CouponRepository : ICouponRepository
    {
        private readonly MyAppDbContext _app;

        public CouponRepository(MyAppDbContext app)
        {
            _app = app;
        }

        public IEnumerable<Coupon> GetAll()
        {
            return _app.Coupon.ToList();
        }

        public async Task<Coupon> GetById(int id)
        {
            return await _app.Coupon.FindAsync(id);
        }

        public void Add(Coupon coupon)
        {
            _app.Coupon.Add(coupon);
            _app.SaveChanges();
        }

        public async Task Update(Coupon coupon)
        {
            var existingCoupon = await _app.Coupon.FindAsync(coupon.Id);
            if (existingCoupon != null)
            {
                existingCoupon.Code = coupon.Code;
                existingCoupon.DiscountAmount = coupon.DiscountAmount;
                existingCoupon.ExpiryDate = coupon.ExpiryDate;

                _app.Update(existingCoupon);
                _app.SaveChanges();
            }
        }

        public async Task Delete(int id)
        {
            var coupon = await _app.Coupon.FindAsync(id);
            if (coupon != null)
            {
                _app.Coupon.Remove(coupon);
                _app.SaveChanges();
            }
        }
    }
}
