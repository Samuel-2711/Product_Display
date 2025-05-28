using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Product_Display.Models.View_Model;
using Product_Display.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Product_Display.Utilities;

namespace Product_Display.Controllers
{
    [Authorize(Roles = SD.Role_Checker)]
    public class UserController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
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

            var userList = new List<ApplicationUser>();

            foreach (var user in users)
            {
                var appUser = (ApplicationUser)user;
                appUser.CustomRole = (await _userManager.GetRolesAsync(user)).FirstOrDefault() ?? "None";
                userList.Add(appUser);
            }

            return Json(new { data = userList });
        }

        [HttpPost]
        public async Task<IActionResult> LockUnlock([FromBody] string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return Json(new { success = false, message = "User not found" });

            if (user.LockoutEnd != null && user.LockoutEnd > DateTime.Now)
            {
                // unlock user
                user.LockoutEnd = DateTime.Now;
            }
            else
            {
                // lock user
                user.LockoutEnd = DateTime.Now.AddYears(1000);
            }

            await _userManager.UpdateAsync(user);

            return Json(new { success = true, message = "Operation successful" });
        }

        #endregion
    }
}
