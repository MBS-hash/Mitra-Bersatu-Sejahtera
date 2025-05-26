using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MBSAdminApp.Data;
using System.Linq;
using System.Threading.Tasks;

namespace MBSAdminApp.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DashboardController(ApplicationDbContext context)
        {
            _context = context;
        }

        // ✅ Halaman utama (home) yang menampilkan grafik pembelian dan produksi
        public async Task<IActionResult> Index()
        {
            // Ambil data pembelian per bulan dan tahun
            var pembelianData = await _context.Pembelian
                .GroupBy(p => new { p.Tanggal.Year, p.Tanggal.Month })
                .Select(g => new
                {
                    Bulan = g.Key.Month,
                    Tahun = g.Key.Year,
                    TotalKg = g.Sum(x => x.JumlahKg)
                })
                .OrderBy(g => g.Tahun).ThenBy(g => g.Bulan)
                .ToListAsync();

            // Ambil data produksi per bulan dan tahun
            var produksiData = await _context.Produksi
                .GroupBy(p => new { p.Tanggal.Year, p.Tanggal.Month })
                .Select(g => new
                {
                    Bulan = g.Key.Month,
                    Tahun = g.Key.Year,
                    TotalHasil = g.Sum(x => x.JumlahHasilKg)
                })
                .OrderBy(g => g.Tahun).ThenBy(g => g.Bulan)
                .ToListAsync();

            // Kirim data ke View
            ViewBag.PembelianData = pembelianData;
            ViewBag.ProduksiData = produksiData;

            return View(); // Akan membuka Views/Dashboard/Index.cshtml
        }
    }
}
