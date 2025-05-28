using Product_Display.Data.Repository.IRepository;
using Product_Display.Models;

namespace Product_Display.Data.Repository
{
    public class TreasuryBPRepository : ITreasuryBPRepository
    {
        private readonly AppDbContext _db;

        public TreasuryBPRepository(AppDbContext db)
        {
            _db = db;
        }

        public IEnumerable<TreasuryBP> GetAll()
        {
            return _db.Treasuries.ToList();
        }

        public TreasuryBP GetById(string securityid)
        {
            return _db.Treasuries.FirstOrDefault(u => u.SecurityId == securityid);
        }

        public void Add(TreasuryBP treasuryBP)
        {
            _db.Treasuries.Add(treasuryBP);
            _db.SaveChanges();
        }

        public void Update(TreasuryBP treasuryBP)
        {
            _db.Treasuries.Update(treasuryBP);
            _db.SaveChanges();
        }

        public void Delete(string  securityid)
        {
            var treasuryBP = _db.Treasuries.FirstOrDefault(p => p.SecurityId == securityid);
            if (treasuryBP != null)
            {
                _db.Treasuries.Remove(treasuryBP);
                _db.SaveChanges();
            }
        }
    }
}
