using System.ComponentModel.DataAnnotations;

namespace Product_Display.Models
{
    public class TreasuryBP
    {
        [Required]
            public string Maturity { get; set; } 
        [Key]
            public string SecurityId { get; set; }
        [Required]
            public int TenorDays { get; set; }
        [Required]
            public decimal DiscountPercentage { get; set; }

        public bool Published { get; set; } = false;
    }
}
