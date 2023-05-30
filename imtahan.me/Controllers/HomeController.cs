
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace imtahan.me.Controllers
{
    public class HomeController : Controller
    {
        

        public IActionResult Index()
        {
            return View();
        }

    
    }
}