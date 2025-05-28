using Microsoft.AspNetCore.Mvc.Rendering;

namespace Product_Display.Models.View_Model
{
    public class RoleManagementVM
    {
        public ApplicationUser ApplicationUser { get; set; }
        public string Role { get; set; }
        public IEnumerable<SelectListItem> RoleList { get; set; }
    }
}
