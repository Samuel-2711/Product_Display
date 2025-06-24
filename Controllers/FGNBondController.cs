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

        public IActionResult Index()
        {
            var vm = new FGNBondVM
            {
                FGNBonds = _db.FGNBonds.ToList(),
                FGNBond = new FGNBond()
            };
            return View(vm);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var bonds = _db.FGNBonds.ToList();
            return Json(new { data = bonds });
        }

        [HttpGet]
        public IActionResult Create()
        {
            return PartialView("_CreateModal", new FGNBond());
        }

        [HttpPost]
        public IActionResult Create(FGNBond bond)
        {
            if (ModelState.IsValid)
            {
                _db.FGNBonds.Add(bond);
                _db.SaveChanges();
                return Json(new { success = true });
            }
            return PartialView("_CreateModal", bond);
        }

        [HttpGet]
        public IActionResult Edit(string id)
        {
            if (string.IsNullOrEmpty(id)) return NotFound();
            var bond = _db.FGNBonds.FirstOrDefault(f => f.ISIN == id);
            if (bond == null) return NotFound();
            return PartialView("_EditModal", bond);
        }

        [HttpPost]
        public IActionResult Edit(FGNBond bond)
        {
            if (ModelState.IsValid)
            {
                _db.FGNBonds.Update(bond);
                _db.SaveChanges();
                return Json(new { success = true });
            }
            return PartialView("_EditModal", bond);
        }

        [HttpPost]
        public IActionResult Delete(string id)
        {
            var bond = _db.FGNBonds.FirstOrDefault(f => f.ISIN == id);
            if (bond == null)
            {
                return Json(new { success = false, message = "Bond not found" });
            }

            _db.FGNBonds.Remove(bond);
            _db.SaveChanges();
            return Json(new { success = true });
        }

    }
}
