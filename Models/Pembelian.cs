using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MBSAdminApp.Models
{
    public class Pembelian
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Nama Petani")]
        public string NamaPetani { get; set; }

        [Required]
        public string Wilayah { get; set; }

        [Required]
        [Display(Name = "Jenis Bahan")]
        public string JenisBahan { get; set; }

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
        [Display(Name = "Tanggal Pembelian")]
        public DateTime Tanggal { get; set; } = DateTime.Now;
    }
}
