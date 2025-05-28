using Product_Display.Models;

namespace Product_Display.Data.Repository.IRepository
{
    public interface ITreasuryBPRepository
    {
        IEnumerable<TreasuryBP> GetAll();
        TreasuryBP GetById(string securityid);
        void Add(TreasuryBP treasuryBP);
        void Update(TreasuryBP treasuryBP);
        void Delete(string securityid);
    }
}
