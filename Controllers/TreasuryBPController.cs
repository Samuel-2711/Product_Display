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
        public IActionResult Create()
        {
            return PartialView("Create", new TreasuryBP());
        }

        // POST: Submit Create Form
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

        // GET: Load Edit Modal
        public IActionResult EditModal(string id)
        {
            if (string.IsNullOrEmpty(id))
                return NotFound();

            var obj = _db.Treasuries.FirstOrDefault(t => t.SecurityId == id);
            if (obj == null)
                return NotFound();

            return PartialView("Edit", obj);
        }

        // POST: Submit Edit Form
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

        // DELETE: Remove entry
        [HttpPost]
        public IActionResult Delete(string id)
        {
            var obj = _db.Treasuries.FirstOrDefault(t => t.SecurityId == id);
            if (obj == null)
                return Json(new { success = false });

            _db.Treasuries.Remove(obj);
            _db.SaveChanges();
            return Json(new { success = true });
        }
    }
}


 