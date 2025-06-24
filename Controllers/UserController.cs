using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Product_Display.Models.View_Model;
using Product_Display.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Product_Display.Utilities;
using Product_Display.Data;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Product_Display.Controllers
{
    [Authorize(Roles = SD.Role_Checker)]
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly AppDbContext _db;
        public UserController(UserManager<ApplicationUser> userManager, AppDbContext db, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        public async Task<IActionResult> RoleManagement(string userId)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null)
                return NotFound();

            var roles = await _userManager.GetRolesAsync(user);

            var roleVM = new RoleManagementVM
            {
                ApplicationUser = (ApplicationUser)user,
                Role = roles.FirstOrDefault(),
                RoleList = _roleManager.Roles.Select(r => new SelectListItem
                {
                    Text = r.Name,
                    Value = r.Name
                })
            };

            return View(roleVM);
        }

        [HttpPost]
        public async Task<IActionResult> RoleManagement(RoleManagementVM vm)
        {
            var user = await _userManager.FindByIdAsync(vm.ApplicationUser.Id);
            if (user == null)
                return NotFound();

            var currentRoles = await _userManager.GetRolesAsync(user);
            var oldRole = currentRoles.FirstOrDefault();

            if (oldRole != vm.Role)
            {
                if (!string.IsNullOrEmpty(oldRole))
                    await _userManager.RemoveFromRoleAsync(user, oldRole);

                if (!string.IsNullOrEmpty(vm.Role))
                    await _userManager.AddToRoleAsync(user, vm.Role);
            }

            return RedirectToAction("Index");
        }

        #region API CALLS

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userManager.Users.ToListAsync();

            var userList = new List<UserVM>();

            foreach (var user in users)
            {
                var role = (await _userManager.GetRolesAsync(user)).FirstOrDefault() ?? "None";

                userList.Add(new UserVM
                {
                    Id = user.Id,
                    UserName = user.UserName,         
                    Email = user.Email,
                    Role = role
                });
            }

            return Json(new { data = userList });
        }




        [HttpPost]
        public IActionResult LockUnlock([FromBody] string id)
        {

            var objFromDb = _db.Users.FirstOrDefault(u => u.Id == id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error while Locking/Unlocking" });
            }

            if (objFromDb.LockoutEnd != null && objFromDb.LockoutEnd > DateTime.Now)
            {
                //user is currently locked and we need to unlock them
                objFromDb.LockoutEnd = DateTime.Now;
            }
            else
            {
                objFromDb.LockoutEnd = DateTime.Now.AddYears(1000);
            }
            _db.Users.Update(objFromDb);
            _db.SaveChanges();
            return Json(new { success = true, message = "Operation Successful" });
        }

        #endregion
    }
}
