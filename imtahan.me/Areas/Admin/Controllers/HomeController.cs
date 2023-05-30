using imtahan.me.Models;
using Microsoft.AspNetCore.Mvc;

namespace imtahan.me.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        public async Task<IActionResult> Index()
        {

            return View();
        }

    }
}
