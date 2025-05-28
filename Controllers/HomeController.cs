using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Product_Display.Models;

namespace Product_Display.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return Redirect("/Identity/Account/Login");
        }
    }
}
