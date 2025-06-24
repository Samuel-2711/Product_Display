using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Product_Display.Data;
using Product_Display.Models;
using Product_Display.Utilities;

namespace Product_Display.Controllers
{
    [Authorize(Roles = SD.Role_Checker + "," + SD.Role_Initiator)]
    public class FXRateController : Controller
    {
        private readonly AppDbContext _db;

        public FXRateController(AppDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var fxRates = _db.FXRates.OrderBy(x => x.Id).ToList();
            return View(fxRates);
        }

        [HttpPost]
        public IActionResult SaveAll(List<FXRate> fxRates)
        {
            foreach (var rate in fxRates)
            {
                var existing = _db.FXRates.FirstOrDefault(f => f.Id == rate.Id);
                if (existing != null)
                {
                    existing.BuyRate = rate.BuyRate;
                    existing.SellRate = rate.SellRate;
                }
            }

            _db.SaveChanges();
            TempData["success"] = "FX Rates updated successfully!";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult UpdateRates(List<FXRate> model)
        {
            foreach (var rate in model)
            {
                var existing = _db.FXRates.FirstOrDefault(x => x.Id == rate.Id);
                if (existing != null)
                {
                    existing.BuyRate = rate.BuyRate;
                    existing.SellRate = rate.SellRate;
                    existing.IsUpwardTrend = rate.IsUpwardTrend;
                }
            }

            _db.SaveChanges();
            TempData["success"] = "Rates updated successfully!";
            return RedirectToAction("Index");
        }

    }
}
