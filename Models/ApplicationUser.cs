using Microsoft.AspNetCore.Identity;

namespace Product_Display.Models
{
    public class ApplicationUser:IdentityUser
    {
        // Add custom properties here (but avoid duplicating what's already in IdentityUser)

        // Example: Only add Role string if absolutely needed (display purpose only)
        public string? CustomRole { get; set; }
    }
}
