using FoodPack2Go.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodPack2Go.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FoodPack2Go.Infrastructure.Implement
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly MyAppDbContext _context;

        public PaymentRepository(MyAppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Payment>> GetAllPaymentAsync()
        {
            return await _context.Payment.ToListAsync();
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
