using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodPack2Go.Core
{
    public class AdminModel
    {
        [Key]
        public int AID { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Display(Name = "Last Login")]
        public DateTime? LastLogin { get; set; }
        [NotMapped]
        public string? chart {  get; set; }
        [NotMapped]
        public IEnumerable<DailyOrderChartData>? DailyChart { get; set; }
        [NotMapped]
        public IEnumerable<WeeklyOrderChartData>? WeeklyChart { get; set; }
        [NotMapped]
        public IEnumerable<MonthlyOrderChartData>? MonthlyChart { get; set; }
        [NotMapped]
        public IEnumerable<YearlyOrderChartData>? YearlyChart { get; set; }
    }
}
