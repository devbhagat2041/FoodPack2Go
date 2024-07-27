using FoodPack2Go.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodPack2Go.Infrastructure.Interfaces
{
    public interface IMasterHomePageRepo
    {
        IEnumerable<MasterHomePage> GetAll();
        Task<MasterHomePage> GetById(int id);
        void Add(MasterHomePage b);
        Task Update(MasterHomePage b);
        Task Delete(int id);
    }
}
