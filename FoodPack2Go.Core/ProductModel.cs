using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodPack2Go.Core
{
    public class ProductModel
    {
        [Key]
        public int ProductID { get; set; } // Primary Key

        [Required(ErrorMessage = "Product name is required")]
        public string? ProductName { get; set; }

        public int CategoryID { get; set; } // Foreign Key

        public int SubCategoryID { get; set; } // Foreign Key

        public int? QuantityPerUnit { get; set; }
        public decimal UnitPrice { get; set; }
        public string? Capacity { get; set; }
        public string? Size { get; set; }
        public string? Material { get; set; }
        public decimal? Discount { get; set; }
        public int? UnitInStock { get; set; }
        public string? ImageURL { get; set; }
        public string? AltText { get; set; }
        public string? Description { get; set; }
        public DateTime? CreatedBy { get; set; }
        public DateTime? ModifiedBy { get; set; }
        public int IsDeleted { get; set; }
        public double? AverageRating { get; set; }


        // Navigation properties
        [ForeignKey("CategoryID")]
        public CategoryModel? Category { get; set; }

        [ForeignKey("SubCategoryID")]
        public SubModel? SubCategory { get; set; }

    }
}




































//[Key]
//public int ProductID { get; set; } // Primary Key


//[Required(ErrorMessage = "Product name is required")]
//public string? ProductName { get; set; }


//[Required(ErrorMessage = "CategoryID is required")]
//public int CategoryID { get; set; } // Foreign Key


//[Required(ErrorMessage = "SubCategoryID is required")]
//public int SubCategoryID { get; set; } // Foreign Key


//[Required(ErrorMessage = "QuantityPerUnit is required")]
//public int? QuantityPerUnit { get; set; }


//[Required(ErrorMessage = "UnitPrice is required")]
//public decimal UnitPrice { get; set; }


//[Required(ErrorMessage = "Capacity  is required")]
//public string? Capacity { get; set; }


//[Required(ErrorMessage = "Size  is required")]
//[StringLength(100, ErrorMessage = "Size cannot be greater than 100 characters")]
//public string? Size { get; set; }


//[Required(ErrorMessage = "Material  is required")]
//public string? Material { get; set; }


//[Required(ErrorMessage = "Discount  is required")]
//public decimal? Discount { get; set; }


//[Required(ErrorMessage = "UnitInStock  is required")]
//public int? UnitInStock { get; set; }


//[Required(ErrorMessage = "ImageURL is required")]
//public string? ImageURL { get; set; }


//[Required(ErrorMessage = "AltText is required")]
//public string? AltText { get; set; }


//[Required(ErrorMessage = "Description is required")]
//public string? Description { get; set; }


//public DateTime? CreatedBy { get; set; }

//public DateTime? ModifiedBy { get; set; }

//public int IsDeleted { get; set; }

//public double? AverageRating { get; set; }

//// Navigation properties
//[ForeignKey("CategoryID")]
//public CategoryModel? Category { get; set; }

//[ForeignKey("SubCategoryID")]
//public SubModel? SubCategory { get; set; }

