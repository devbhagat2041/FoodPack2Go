using FoodPack2Go.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodPack2Go.Infrastructure.Interfaces
{
    public interface ICategoylistRepository
    {
        IEnumerable<CategoryList> GetAll();
        Task<CategoryList> GetById(int id);
        void Add(CategoryList s);
        Task Update(CategoryList s);
        Task Delete(int id);
    }
}
