using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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


        // Load all records (AJAX)
        [HttpGet]
        public IActionResult GetAll()
        {
            var allRecords = _db.Treasuries.OrderBy(t => t.TenorDays).ToList();
            return Json(new { data = allRecords });
        }

        // GET: Load Create modal
        [HttpGet]
        public IActionResult Create()
        {
            return PartialView("Create", new TreasuryBP());
        }

        // POST: Submit Create form
        [HttpPost]
        public IActionResult Create(TreasuryBP obj)
        {
            if (ModelState.IsValid)
            {
                _db.Treasuries.Add(obj);
                _db.SaveChanges();
                return Json(new { success = true });
            }

            return PartialView("Create", obj);
        }

        // GET: Load Edit modal
        [HttpGet]
        public IActionResult Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
                return NotFound();

            var obj = _db.Treasuries.FirstOrDefault(t => t.SecurityId == id);
            if (obj == null)
                return NotFound();

            return PartialView("Edit", obj);
        }

        // POST: Submit Edit form
        [HttpPost]
        public IActionResult Edit(TreasuryBP obj)
        {
            if (ModelState.IsValid)
            {
                _db.Treasuries.Update(obj);
                _db.SaveChanges();
                return Json(new { success = true });
            }

            return PartialView("Edit", obj);
        }

        // DELETE: Delete record
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(string id)
        {
            var obj = _db.Treasuries.FirstOrDefault(t => t.SecurityId == id);
            if (obj == null)
                return Json(new { success = false, message = "Record not found." });

            _db.Treasuries.Remove(obj);
            _db.SaveChanges();
            return Json(new { success = true, message = "Record deleted successfully." });
        }
    }
}



 