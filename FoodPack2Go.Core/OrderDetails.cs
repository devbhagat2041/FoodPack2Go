using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodPack2Go.Core
{
    public class OrderDetails
    {
        [Key]
        public int OrderDetailID { get; set; }

        [ForeignKey("Order")]
        [Required(ErrorMessage = "Order ID is required.")]
        public int OrderID { get; set; }

        [ForeignKey("Product")]
        [Required(ErrorMessage = "Product ID is required.")]
        public int ProductID { get; set; }

        [Required(ErrorMessage = "Quantity is required.")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Unit Price is required.")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal UnitPrice { get; set; }

        // Navigation properties
        public Orders Order { get; set; }

        public ProductModel Product { get; set; }
    }
}
