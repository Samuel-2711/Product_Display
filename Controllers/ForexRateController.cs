using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Product_Display.Data;
using Product_Display.Models;
using Product_Display.Models.View_Model;
using Product_Display.Utilities;

namespace Product_Display.Controllers
{
    [Authorize(Roles = SD.Role_Checker + "," + SD.Role_Initiator)]
    public class ForexRateController : Controller
    {
        private readonly AppDbContext _db;

        public ForexRateController(AppDbContext db)
        {
            _db = db;
        }
        public async Task<IActionResult> Index()
        {
            var viewModel = new ForexRateVM
            {
                Rates = _db.ForexRates.ToList()
            };

            return View(viewModel);
        }

        // CREATE: Add a new forex rate
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CurrencyName,Symbol,BuyRate,SellRate,IsUpwardTrend")] ForexRate forexRate)
        {
            if (ModelState.IsValid)
            {
                _db.Add(forexRate);
                await _db.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
