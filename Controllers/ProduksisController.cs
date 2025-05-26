using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MBSAdminApp.Data;
using MBSAdminApp.Models;
using MBSAdminApp.Helpers; // ✅ Tambahkan ini
using QRCoder; // ✅ Tambahkan ini
using System.Threading.Tasks;
using System.Linq;

namespace MBSAdminApp.Controllers
{
    public class ProduksiController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProduksiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Produksi
        public async Task<IActionResult> Index()
        {
            return View(await _context.Produksi.ToListAsync());
        }

        // GET: Produksi/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var produksi = await _context.Produksi.FirstOrDefaultAsync(m => m.Id == id);
            if (produksi == null) return NotFound();

            return View(produksi);
        }

        // GET: Produksi/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Produksi/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,JenisProses,BahanAwal,JumlahAwalKg,BahanHasil,JumlahHasilKg,NamaOperator,Tanggal")] Produksi produksi)
        {
            if (ModelState.IsValid)
            {
                _context.Add(produksi);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(produksi);
        }

        // GET: Produksi/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var produksi = await _context.Produksi.FindAsync(id);
            if (produksi == null) return NotFound();

            return View(produksi);
        }

        // POST: Produksi/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,JenisProses,BahanAwal,JumlahAwalKg,BahanHasil,JumlahHasilKg,NamaOperator,Tanggal")] Produksi produksi)
        {
            if (id != produksi.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(produksi);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProduksiExists(produksi.Id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(produksi);
        }

        // GET: Produksi/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var produksi = await _context.Produksi.FirstOrDefaultAsync(m => m.Id == id);
            if (produksi == null) return NotFound();

            return View(produksi);
        }

        // POST: Produksi/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var produksi = await _context.Produksi.FindAsync(id);
            _context.Produksi.Remove(produksi);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // ✅ QR CODE VIEW ACTION
        public IActionResult QrCode(int id)
        {
            var data = _context.Produksi.FirstOrDefault(p => p.Id == id);
            if (data == null) return NotFound();

            string content = $"Jenis Proses: {data.JenisProses}\nBahan Awal: {data.BahanAwal} ({data.JumlahAwalKg}kg)\nBahan Hasil: {data.BahanHasil} ({data.JumlahHasilKg}kg)\nOperator: {data.NamaOperator}\nTanggal: {data.Tanggal:yyyy-MM-dd}";
            var qrImage = QrCodeHelper.GenerateBase64Qr(content);

            ViewBag.QrCodeImage = qrImage;
            ViewBag.DataProduksi = data;

            return View();
        }

        // ✅ QR CODE DOWNLOAD ACTION
        public IActionResult DownloadQr(int id)
        {
            var data = _context.Produksi.FirstOrDefault(p => p.Id == id);
            if (data == null) return NotFound();

            string content = $"Jenis Proses: {data.JenisProses}\nBahan Awal: {data.BahanAwal} ({data.JumlahAwalKg}kg)\nBahan Hasil: {data.BahanHasil} ({data.JumlahHasilKg}kg)\nOperator: {data.NamaOperator}\nTanggal: {data.Tanggal:yyyy-MM-dd}";
            var qrGenerator = new QRCodeGenerator();
            var qrCodeData = qrGenerator.CreateQrCode(content, QRCodeGenerator.ECCLevel.Q);
            var qrCode = new BitmapByteQRCode(qrCodeData);
            byte[] byteImg = qrCode.GetGraphic(20);

            return File(byteImg, "image/png", $"QR_Produksi_{data.Id}.png");
        }

        private bool ProduksiExists(int id)
        {
            return _context.Produksi.Any(e => e.Id == id);
        }
    }
}
