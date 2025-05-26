using System;
using System.ComponentModel.DataAnnotations;

namespace MBSAdminApp.Models
{
    public class Keuangan
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Jenis Transaksi")]
        public string JenisTransaksi { get; set; } // Pemasukan / Pengeluaran

        [Required]
        [Display(Name = "Kategori")]
        public string Kategori { get; set; } // Misal: Gaji, Operasional, Penjualan, dll

        [Required]
        [Display(Name = "Nominal (Rp)")]
        public decimal Nominal { get; set; }

        [Display(Name = "Keterangan")]
        public string Keterangan { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Tanggal")]
        public DateTime Tanggal { get; set; } = DateTime.Now;
    }
}
