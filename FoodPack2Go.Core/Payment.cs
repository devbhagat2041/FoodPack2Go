using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodPack2Go.Core
{
    public class Payment
    {
        [Key]
        public int PaymentId { get; set; }

        [Required(ErrorMessage = "Amount is required.")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        [MaxLength(3)]
        public string? Currency { get; set; }

        [MaxLength(55)]
        public string? Status { get; set; }

        [MaxLength(255)]
        public string? PaymentMethod { get; set; }

        public int? CustomerID { get; set; }

        public DateTime? CreatedAt { get; set; }

        [ForeignKey("CustomerID")]
        public Customer Customer { get; set; }
    }
}
