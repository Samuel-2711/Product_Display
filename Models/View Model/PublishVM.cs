namespace Product_Display.Models.View_Model
{
    public class PublishVM
    {
        public List<FXRate> FXRates { get; set; }
        public List<TreasuryBP> Treasuries { get; set; }
        public List<FGNBond> FGNBonds { get; set; }
        public List<SavingDay> SavingDays { get; set; }
        public List<TextRoller> TextRollers { get; set; }
    }
}
