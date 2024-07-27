using FoodPack2Go.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodPack2Go.Infrastructure.Interfaces
{
    public interface ICustomerRepo
    {
        IEnumerable<Customer> GetAll();
        Task<Customer> GetById(int id);
        void Add(Customer p);
        Task Update(Customer p);
        Task Delete(int id);

    }
}
