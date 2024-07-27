using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodPack2Go.Core
{
    public class Review
    {
        [Key]
        public int ReviewID { get; set; }

        [Required]
        public int ProductID { get; set; }

        [MaxLength(200)]
        public string? CustomerName { get; set; }

        [MaxLength(100)]
        public string? CustomerEmail { get; set; }

        [MaxLength(200)]
        public string? ReviewTitle { get; set; }

        public string? ReviewDescription { get; set; }

        public int? Rating { get; set; }

        public DateTime? ReviewDate { get; set; }

        [ForeignKey("ProductID")]
        public ProductModel Product { get; set; }
    }
}
