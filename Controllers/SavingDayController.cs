using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Product_Display.Data;
using Product_Display.Models;
using Product_Display.Utilities;

namespace Product_Display.Controllers
{
    [Authorize(Roles = SD.Role_Checker + "," + SD.Role_Initiator)]
    public class SavingDayController : Controller
    {
        private readonly AppDbContext _db;

        public SavingDayController(AppDbContext db)
        {
            _db = db;
        }

        // GET: /SavingDay
        public IActionResult Index()
        {
            var list = _db.SavingDays.ToList();
            return View(list);
        }

        // CREATE (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(SavingDay savingDay)
        {
            if (ModelState.IsValid)
            {
                _db.SavingDays.Add(savingDay);
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            return PartialView("Create", savingDay);
        }

        // EDIT (GET - Load partial view)
        public IActionResult Edit(int id)
        {
            var savingDay = _db.SavingDays.Find(id);
            if (savingDay == null)
                return NotFound();

            return PartialView("Edit", savingDay);
        }

        // EDIT (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(SavingDay savingDay)
        {
            if (ModelState.IsValid)
            {
                _db.SavingDays.Update(savingDay);
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            return PartialView("Edit", savingDay);
        }

        // DELETE (AJAX POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var savingDay = _db.SavingDays.Find(id);
            if (savingDay == null)
                return NotFound();

            _db.SavingDays.Remove(savingDay);
            _db.SaveChanges();
            return Ok();
        }
    }
}
