using imtahan.me.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using imtahan.me.Models;

namespace imtahan.me.Areas.ProniaAdmin.Controllers
{
    [Area("Admin")]
    [AutoValidateAntiforgeryToken]
    public class ServiceController : Controller
    {
        private readonly AppDbContext _context;

        public ServiceController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            List<Service> services = await _context.services.ToListAsync();
            return View(services);
        }
        public async Task<IActionResult> Create()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Service Service)
        {

            bool result = await _context.services.AnyAsync(s=> s.Id == Service.Id);
            if (!result)
            {
                ModelState.AddModelError("ID", "Bele idye sahib Service yoxdur");
                ViewBag.services = await _context.services.ToListAsync();
                return View();
            }
            await _context.services.AddAsync(Service);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Update(int? id)
        {
            if (id == null || id < 1) return BadRequest();

            Service existed = await _context.services.FirstOrDefaultAsync(e => e.Id == id);

            if (existed == null) return NotFound();

            ViewBag.services = await _context.services.ToListAsync();

            return View(existed);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int? id, Service Service)
        {

            if (id == null || id < 1) return BadRequest();

            Service existed = await _context.services.FirstOrDefaultAsync(e => e.Id == id);

            if (existed == null) return NotFound();

            if (!ModelState.IsValid)
            {
                ViewBag.services = await _context.services.ToListAsync();
                return View(existed);
            }

            if (existed.Id != Service.Id)
            {
                bool result = await _context.services.AnyAsync(p => p.Id == Service.Id);
                if (!result)
                {
                    ModelState.AddModelError("Name", "Bele bir Servic yoxdur");
                    ViewBag.services = await _context.services.ToListAsync();

                    return View(existed);
                }
                existed.Id = Service.Id;
            }


            existed.Name = Service.Name;
            existed.Description = Service.Description;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }


    }
}
