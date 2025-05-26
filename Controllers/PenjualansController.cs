using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MBSAdminApp.Data;
using MBSAdminApp.Models;
using System.Threading.Tasks;
using System.Linq;

namespace MBSAdminApp.Controllers
{
    public class PenjualanController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PenjualanController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Penjualan.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var penjualan = await _context.Penjualan.FirstOrDefaultAsync(m => m.Id == id);
            if (penjualan == null) return NotFound();

            return View(penjualan);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NamaPembeli,JenisProduk,JumlahKg,HargaPerKg,Tanggal")] Penjualan penjualan)
        {
            if (ModelState.IsValid)
            {
                _context.Add(penjualan);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(penjualan);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var penjualan = await _context.Penjualan.FindAsync(id);
            if (penjualan == null) return NotFound();

            return View(penjualan);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NamaPembeli,JenisProduk,JumlahKg,HargaPerKg,Tanggal")] Penjualan penjualan)
        {
            if (id != penjualan.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(penjualan);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PenjualanExists(penjualan.Id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(penjualan);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var penjualan = await _context.Penjualan.FirstOrDefaultAsync(m => m.Id == id);
            if (penjualan == null) return NotFound();

            return View(penjualan);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var penjualan = await _context.Penjualan.FindAsync(id);
            _context.Penjualan.Remove(penjualan);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PenjualanExists(int id)
        {
            return _context.Penjualan.Any(e => e.Id == id);
        }
    }
}
