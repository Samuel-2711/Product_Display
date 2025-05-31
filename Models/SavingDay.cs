using System.ComponentModel.DataAnnotations;

namespace Product_Display.Models
{
    public class SavingDay
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Days { get; set; } = string.Empty;

        [Required]
        [Range(0, 100)]
        public decimal Percent { get; set; }
    }
}
