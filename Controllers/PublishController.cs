using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Product_Display.Data;
using Product_Display.Models.View_Model;

namespace Product_Display.Controllers
{
    [Authorize(Roles = "Checker")]
    public class PublishController : Controller
    {
        private readonly AppDbContext _db;

        public PublishController(AppDbContext db)
        {
            _db = db;
        }

        // Show all data before publishing
        public async Task<IActionResult> Index()
        {
            var viewModel = new PublishVM
            {
                FXRates = await _db.FXRates.ToListAsync(),
                FGNBonds = await _db.FGNBonds.ToListAsync(),
                Treasuries = await _db.Treasuries.ToListAsync(),
                SavingDays = await _db.SavingDays.ToListAsync(),           
                TextRollers = await _db.TextRollers.ToListAsync()
            };

            return View(viewModel);
        }

        // Publish all data
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PublishAll()
        {
            var fxRates = await _db.FXRates.ToListAsync();
            var fGNBonds = await _db.FGNBonds.ToListAsync();
            var treasuries = await _db.Treasuries.ToListAsync();

            foreach (var f in fxRates)
               

            foreach (var e in fGNBonds)
                e.Published = true;

            foreach (var t in treasuries)
                t.Published = true;

            await _db.SaveChangesAsync();

            TempData["success"] = "Published successfully";
            return RedirectToAction("Index"); // Optional redirect
        }

        // ==== Separate Views for Clients ====

        [AllowAnonymous]
        public async Task<IActionResult> ClientForex()
        {
            var fxRates = await _db.FXRates.ToListAsync();
            return View(fxRates);
        }


        [AllowAnonymous]
        public async Task<IActionResult> ClientBonds()
        {
            var fgnBonds = await _db.FGNBonds
                .Where(b => b.Published)
                .ToListAsync();

            return View(fgnBonds);
        }

        [AllowAnonymous]
        public async Task<IActionResult> ClientTreasuries()
        {
            var treasuries = await _db.Treasuries
                .Where(t => t.Published)
                .ToListAsync();

            return View(treasuries);
        }
    }
}
