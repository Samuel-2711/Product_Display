using System.ComponentModel.DataAnnotations;

namespace Product_Display.Models
{
    public class TextRoller
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Marquee Text")]
        public string Text { get; set; } = string.Empty;
    }
}
