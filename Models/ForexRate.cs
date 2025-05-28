using System.ComponentModel.DataAnnotations;

namespace Product_Display.Models
{
    public class ForexRate
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string CurrencyName { get; set; }

        [Required]
        public string Symbol { get; set; }

        [Required]
        public decimal BuyRate { get; set; }

        [Required]
        public decimal SellRate { get; set; }

        public bool IsUpwardTrend { get; set; }

        public bool Published { get; set; } = false;
    }
}
