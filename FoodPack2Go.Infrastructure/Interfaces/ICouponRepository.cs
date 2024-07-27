using FoodPack2Go.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FoodPack2Go.Infrastructure.Interfaces
{
    public interface ICouponRepository
    {
        IEnumerable<Coupon> GetAll();
        Task<Coupon> GetById(int id);
        void Add(Coupon coupon);
        Task Update(Coupon coupon);
        Task Delete(int id);
    }
}
