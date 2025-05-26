using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MBSAdminApp.Data;
using MBSAdminApp.Models;
using System.Threading.Tasks;
using System.Linq;

namespace MBSAdminApp.Controllers
{
    public class KeuanganController : Controller
    {
        private readonly ApplicationDbContext _context;

        public KeuanganController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Keuangan.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var keuangan = await _context.Keuangan.FirstOrDefaultAsync(m => m.Id == id);
            if (keuangan == null) return NotFound();

            return View(keuangan);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,JenisTransaksi,Kategori,Nominal,Keterangan,Tanggal")] Keuangan keuangan)
        {
            if (ModelState.IsValid)
            {
                _context.Add(keuangan);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(keuangan);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var keuangan = await _context.Keuangan.FindAsync(id);
            if (keuangan == null) return NotFound();

            return View(keuangan);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,JenisTransaksi,Kategori,Nominal,Keterangan,Tanggal")] Keuangan keuangan)
        {
            if (id != keuangan.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(keuangan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KeuanganExists(keuangan.Id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(keuangan);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var keuangan = await _context.Keuangan.FirstOrDefaultAsync(m => m.Id == id);
            if (keuangan == null) return NotFound();

            return View(keuangan);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var keuangan = await _context.Keuangan.FindAsync(id);
            _context.Keuangan.Remove(keuangan);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KeuanganExists(int id)
        {
            return _context.Keuangan.Any(e => e.Id == id);
        }
    }
}
