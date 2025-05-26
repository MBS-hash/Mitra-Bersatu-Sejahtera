using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MBSAdminApp.Models
{
    public class Penjualan
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Nama Pembeli")]
        public string NamaPembeli { get; set; }

        [Required]
        [Display(Name = "Jenis Produk")]
        public string JenisProduk { get; set; } // Contoh: Remahan Bersih

        [Required]
        [Display(Name = "Jumlah (Kg)")]
        public double JumlahKg { get; set; }

        [Required]
        [Display(Name = "Harga per Kg")]
        public decimal HargaPerKg { get; set; }

        [NotMapped]
        [Display(Name = "Total Harga")]
        public decimal TotalHarga
        {
            get { return HargaPerKg * (decimal)JumlahKg; }
        }

        [DataType(DataType.Date)]
        [Display(Name = "Tanggal Penjualan")]
        public DateTime Tanggal { get; set; } = DateTime.Now;
    }
}
