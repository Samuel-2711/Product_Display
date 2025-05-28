using System.ComponentModel.DataAnnotations;

namespace Product_Display.Models
{
    public class FGNBond
    {
        [Key]
        [Required]
        [StringLength(12, MinimumLength = 12, ErrorMessage = "ISIN must be exactly 12 characters long.")]
        public string ISIN { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Maturity { get; set; }

        [Required]
        [Range(0.0, double.MaxValue, ErrorMessage = "Years to Maturity must be a positive number.")]
        public double YearsToMaturity { get; set; }

        [Required]
        [Range(0.0, double.MaxValue, ErrorMessage = "Price must be a positive number.")]
        public double Price { get; set; }

        [Required]
        [Range(0.0, 100.0, ErrorMessage = "Yield to Maturity must be between 0% and 100%.")]
        public double YieldToMaturity { get; set; }

        [Required]
        [Range(0.0, 100.0, ErrorMessage = "Coupon must be between 0% and 100%.")]
        public double Coupon { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Security name cannot exceed 50 characters.")]
        public string Security { get; set; }

        public bool Published { get; set; } = false;
    }
}

