using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodPack2Go.Core
{
    public class Coupon
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Code is required.")]
        [StringLength(50, ErrorMessage = "Code must be at most 50 characters.")]
        public string Code { get; set; }

        [Required(ErrorMessage = "DiscountAmount is required.")]
        public decimal DiscountAmount { get; set; }

        [Required(ErrorMessage = "ExpiryDate is required.")]
        public DateTime ExpiryDate { get; set; }
    }
}
