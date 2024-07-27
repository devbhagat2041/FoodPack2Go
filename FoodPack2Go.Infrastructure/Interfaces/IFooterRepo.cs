using FoodPack2Go.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodPack2Go.Infrastructure.Interfaces
{
    public interface IFooterRepo
    {
        IEnumerable<Footer> GetAll();
        Task<Footer> GetById(int id);

        void Add(Footer footer);
        Task Update(Footer footer);
    }
}
