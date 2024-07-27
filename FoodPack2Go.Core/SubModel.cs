using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodPack2Go.Core
{
    public class SubModel
    {
        [Key]
        public int SubCategoryID { get; set; }

        [Required]
        public string? SubCategoryName { get; set; }
        public int CategoryID { get; set; } // foreign key
                                            
        [ForeignKey("CategoryID")]
        public CategoryModel? Category { get; set; }

        public ICollection<ProductModel>? Products { get; set; }
    }
}
