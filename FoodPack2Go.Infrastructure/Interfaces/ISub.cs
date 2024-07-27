using FoodPack2Go.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodPack2Go.Infrastructure.Interfaces
{
    public interface ISub
    {
        IEnumerable<SubModel> GetAll();
        Task<SubModel> GetById(int id);
        //Task<CategoryModel> GetById(int id);
        
        void Add(SubModel s);
        Task Update(SubModel s);
        Task Delete(int id);
        void Save();
    }
}
