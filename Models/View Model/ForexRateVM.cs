namespace Product_Display.Models.View_Model
{
    public class ForexRateVM
    {
        public ForexRate ForexRate { get; set; } = new ForexRate();
        public IEnumerable<ForexRate> Rates { get; set; } = new List<ForexRate>();
    }
}
