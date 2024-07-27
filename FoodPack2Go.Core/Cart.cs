using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodPack2Go.Core
{
    public class Cart
    {
        [Key]
        public int CartID { get; set; }

        [ForeignKey("Customer")]
        [Required(ErrorMessage = "Customer ID is required.")]
        public int CustomerID { get; set; }

        [ForeignKey("Product")]
        [Required(ErrorMessage = "Product ID is required.")]
        public int ProductID { get; set; }

        public DateTime? CreatedBy { get; set; }

        public DateTime? ModifiedBy { get; set; }

        [Required(ErrorMessage = "Quantity is required.")]
        public int Quantity { get; set; }

        // Navigation properties
        public Customer Customer { get; set; }

        public ProductModel Product { get; set; }
    }
}
