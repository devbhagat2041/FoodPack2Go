using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodPack2Go.Core
{
    public class CategoryModel
    {
        [Key]
        public int CategoryID { get; set; }

        [Required]
        public string? CategoryName { get; set; }

        public  ICollection<SubModel>? SubCategories { get; set; }
    }
}
