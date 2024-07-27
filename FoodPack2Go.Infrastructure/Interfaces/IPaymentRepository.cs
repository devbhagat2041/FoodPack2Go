using FoodPack2Go.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FoodPack2Go.Infrastructure.Interfaces
{
    public interface IPaymentRepository : IDisposable
    {
        Task<List<Payment>> GetAllPaymentAsync();
        // Other read-only methods can be added if needed
    }
}
