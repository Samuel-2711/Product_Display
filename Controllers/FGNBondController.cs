using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Product_Display.Data;
using Product_Display.Models;
using Product_Display.Models.View_Model;
using Product_Display.Utilities;

namespace Product_Display.Controllers
{
    [Authorize(Roles = SD.Role_Checker + "," + SD.Role_Initiator)]
    public class FGNBondController : Controller
    {
        private readonly AppDbContext _db;

        public FGNBondController(AppDbContext db)
        {
            _db = db;
        }

        // List all FGN Bonds
        public IActionResult Index()
        {
            var viewModel = new FGNBondVM
            {
                FGNBonds = _db.FGNBonds.ToList(),
                FGNBond = new FGNBond()  // For Create modal binding
            };
            return View(viewModel);
        }

        // POST: Create a new FGNBond
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(FGNBond bond)
        {
            if (ModelState.IsValid)
            {
                var existing = _db.FGNBonds.FirstOrDefault(f => f.ISIN == bond.ISIN);
                if (existing != null)
                {
                    ModelState.AddModelError("ISIN", "An FGNBond with this ISIN already exists.");
                }
                else
                {
                    _db.FGNBonds.Add(bond);
                    _db.SaveChanges();
                    return RedirectToAction(nameof(Index));
                }
            }

            // If invalid, reload index with model errors
            var vm = new FGNBondVM
            {
                FGNBonds = _db.FGNBonds.ToList(),
                FGNBond = bond
            };
            return View("Index", vm);
        }

        // POST: Edit an existing FGNBond
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(FGNBond bond)
        {
            if (ModelState.IsValid)
            {
                var existing = _db.FGNBonds.FirstOrDefault(f => f.ISIN == bond.ISIN);
                if (existing == null)
                {
                    return NotFound();
                }

                existing.Security = bond.Security;
                existing.Maturity = bond.Maturity;
                existing.YearsToMaturity = bond.YearsToMaturity;
                existing.Price = bond.Price;
                existing.YieldToMaturity = bond.YieldToMaturity;
                existing.Coupon = bond.Coupon;
                existing.Published = bond.Published;

                _db.FGNBonds.Update(existing);
                _db.SaveChanges();

                return RedirectToAction(nameof(Index));
            }

            // If invalid, reload index with errors
            var vm = new FGNBondVM
            {
                FGNBonds = _db.FGNBonds.ToList(),
                FGNBond = bond
            };
            return View("Index", vm);
        }
    }
}
