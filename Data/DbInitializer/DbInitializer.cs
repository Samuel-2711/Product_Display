using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Product_Display.Data.DbInitializer;
using Product_Display.Models;
using Product_Display.Utilities;

namespace Product_Display.Data.DbInitializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DbInitializer(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public void Initialize()
        {
            // Only create users if they don't already exist.
            var initiatorUser = _userManager.FindByNameAsync("initiator").GetAwaiter().GetResult();
            if (initiatorUser == null)
            {
                initiatorUser = new IdentityUser
                {
                    UserName = "initiator",
                    Email = "initiator@app.com",
                    EmailConfirmed = true
                };

                var result = _userManager.CreateAsync(initiatorUser, "Initiator@123").GetAwaiter().GetResult();
                if (result.Succeeded)
                {
                    _userManager.AddToRoleAsync(initiatorUser, SD.Role_Initiator).GetAwaiter().GetResult();
                }
            }

            var checkerUser = _userManager.FindByNameAsync("checker").GetAwaiter().GetResult();
            if (checkerUser == null)
            {
                checkerUser = new IdentityUser
                {
                    UserName = "checker",
                    Email = "checker@app.com",
                    EmailConfirmed = true
                };

                var result = _userManager.CreateAsync(checkerUser, "Checker@123").GetAwaiter().GetResult();
                if (result.Succeeded)
                {
                    _userManager.AddToRoleAsync(checkerUser, SD.Role_Checker).GetAwaiter().GetResult();
                }
            }
        }
    }

}
