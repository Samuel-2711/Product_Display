using Microsoft.AspNetCore.Mvc;
using Product_Display.Data;
using Product_Display.Models;

namespace Product_Display.Controllers
{
    public class TextRollerController : Controller
    {
        private readonly AppDbContext _db;

        public TextRollerController(AppDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var rollers = _db.TextRollers.ToList();
            return View(rollers);
        }

        [HttpPost]
        public IActionResult Create(TextRoller roller)
        {
            if (ModelState.IsValid)
            {
                _db.TextRollers.Add(roller);
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return PartialView("Create", roller);
        }

        public IActionResult Edit(int id)
        {
            var roller = _db.TextRollers.Find(id);
            if (roller == null) return NotFound();
            return PartialView("Edit", roller);
        }

        [HttpPost]
        public IActionResult Edit(TextRoller roller)
        {
            if (ModelState.IsValid)
            {
                _db.TextRollers.Update(roller);
                _db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return PartialView("Edit", roller);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var roller = _db.TextRollers.Find(id);
            if (roller == null) return NotFound();
            _db.TextRollers.Remove(roller);
            _db.SaveChanges();
            return Ok();
        }
    }
}

