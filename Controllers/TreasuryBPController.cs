using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Product_Display.Data;
using Product_Display.Data.Repository.IRepository;
using Product_Display.Models;
using Product_Display.Utilities;

namespace Product_Display.Controllers
{
    [Authorize(Roles = SD.Role_Checker + "," + SD.Role_Initiator)]
    public class TreasuryBPController : Controller
    {
        private readonly AppDbContext _db;

        public TreasuryBPController(AppDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            List<TreasuryBP> objTreasuryBPList = _db.Treasuries.ToList();
            return View(objTreasuryBPList);
        }
        public IActionResult Create ()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create (TreasuryBP obj)
        {
            _db.Treasuries.Add(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}


 