using FoodPack2Go.Core;
using FoodPack2Go.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodPack2Go.Infrastructure.Implement
{
    public class CartRepository : ICartRepository
    {
        private readonly MyAppDbContext db;

        public CartRepository(MyAppDbContext _db)
        {
            db = _db;
        }

        public IEnumerable<Cart> GetAll()
        {
            return db.Cart.ToList();
        }
    }
}
