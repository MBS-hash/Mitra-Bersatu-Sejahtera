using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MBSAdminApp.Models
{
    public class Produksi
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Jenis Proses")]
        public string JenisProses { get; set; } // Contoh: "Daun Basah ke Remahan Kotor"

        [Required]
        [Display(Name = "Bahan Awal")]
        public string BahanAwal { get; set; } // Contoh: "Daun Basah"

        [Required]
        [Display(Name = "Jumlah Awal (Kg)")]
        public double JumlahAwalKg { get; set; }

        [Required]
        [Display(Name = "Bahan Hasil")]
        public string BahanHasil { get; set; } // Contoh: "Remahan Kotor"

        [Required]
        [Display(Name = "Jumlah Hasil (Kg)")]
        public double JumlahHasilKg { get; set; }

        [Display(Name = "Nama Operator")]
        public string NamaOperator { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Tanggal Produksi")]
        public DateTime Tanggal { get; set; } = DateTime.Now;
    }
}
