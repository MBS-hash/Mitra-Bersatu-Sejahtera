using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MBSAdminApp.Data;
using MBSAdminApp.Models;
using System.Threading.Tasks;
using System.Linq;

namespace MBSAdminApp.Controllers
{
    public class StokController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StokController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Stok.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var stok = await _context.Stok.FirstOrDefaultAsync(m => m.Id == id);
            if (stok == null) return NotFound();

            return View(stok);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,JenisBahan,TotalMasuk,TotalKeluar")] Stok stok)
        {
            if (ModelState.IsValid)
            {
                _context.Add(stok);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(stok);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var stok = await _context.Stok.FindAsync(id);
            if (stok == null) return NotFound();

            return View(stok);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,JenisBahan,TotalMasuk,TotalKeluar")] Stok stok)
        {
            if (id != stok.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(stok);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StokExists(stok.Id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(stok);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var stok = await _context.Stok.FirstOrDefaultAsync(m => m.Id == id);
            if (stok == null) return NotFound();

            return View(stok);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var stok = await _context.Stok.FindAsync(id);
            _context.Stok.Remove(stok);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StokExists(int id)
        {
            return _context.Stok.Any(e => e.Id == id);
        }
    }
}
