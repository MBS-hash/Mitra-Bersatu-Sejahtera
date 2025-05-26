using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MBSAdminApp.Data;
using MBSAdminApp.Models;
using MBSAdminApp.Helpers; // ✅ Tetap dipakai
using System.Threading.Tasks;
using System.Linq;
using QRCoder; // ✅ Tambahkan ini

namespace MBSAdminApp.Controllers
{
    public class PembelianController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PembelianController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Pembelian
        public async Task<IActionResult> Index()
        {
            return View(await _context.Pembelian.ToListAsync());
        }

        // GET: Pembelian/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var pembelian = await _context.Pembelian.FirstOrDefaultAsync(m => m.Id == id);
            if (pembelian == null) return NotFound();

            return View(pembelian);
        }

        // GET: Pembelian/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Pembelian/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NamaPetani,Wilayah,JenisBahan,JumlahKg,HargaPerKg,Tanggal")] Pembelian pembelian)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pembelian);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pembelian);
        }

        // GET: Pembelian/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var pembelian = await _context.Pembelian.FindAsync(id);
            if (pembelian == null) return NotFound();

            return View(pembelian);
        }

        // POST: Pembelian/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,NamaPetani,Wilayah,JenisBahan,JumlahKg,HargaPerKg,Tanggal")] Pembelian pembelian)
        {
            if (id != pembelian.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pembelian);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PembelianExists(pembelian.Id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(pembelian);
        }

        // GET: Pembelian/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var pembelian = await _context.Pembelian.FirstOrDefaultAsync(m => m.Id == id);
            if (pembelian == null) return NotFound();

            return View(pembelian);
        }

        // POST: Pembelian/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pembelian = await _context.Pembelian.FindAsync(id);
            _context.Pembelian.Remove(pembelian);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // ✅ QR CODE VIEW ACTION
        public IActionResult QrCode(int id)
        {
            var data = _context.Pembelian.FirstOrDefault(p => p.Id == id);
            if (data == null) return NotFound();

            string content = $"Nama: {data.NamaPetani}\nWilayah: {data.Wilayah}\nJenis: {data.JenisBahan}\nJumlah: {data.JumlahKg}kg\nTanggal: {data.Tanggal:yyyy-MM-dd}";
            var qrImage = QrCodeHelper.GenerateBase64Qr(content);

            ViewBag.QrCodeImage = qrImage;
            ViewBag.DataPembelian = data;

            return View();
        }

        // ✅ QR CODE DOWNLOAD ACTION
        public IActionResult DownloadQr(int id)
        {
            var data = _context.Pembelian.FirstOrDefault(p => p.Id == id);
            if (data == null) return NotFound();

            string content = $"Nama: {data.NamaPetani}\nWilayah: {data.Wilayah}\nJenis: {data.JenisBahan}\nJumlah: {data.JumlahKg}kg\nTanggal: {data.Tanggal:yyyy-MM-dd}";

            var qrGenerator = new QRCodeGenerator();
            var qrCodeData = qrGenerator.CreateQrCode(content, QRCodeGenerator.ECCLevel.Q);
            var qrCode = new BitmapByteQRCode(qrCodeData);
            byte[] byteImg = qrCode.GetGraphic(20);

            return File(byteImg, "image/png", $"QR_Pembelian_{data.Id}.png");
        }

        private bool PembelianExists(int id)
        {
            return _context.Pembelian.Any(e => e.Id == id);
        }
    }
}
