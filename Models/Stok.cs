using System.ComponentModel.DataAnnotations;

namespace MBSAdminApp.Models
{
    public class Stok
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Jenis Bahan")]
        public string JenisBahan { get; set; } // Daun Basah, Remahan Kotor, dll

        [Display(Name = "Total Masuk (Kg)")]
        public double TotalMasuk { get; set; }

        [Display(Name = "Total Keluar (Kg)")]
        public double TotalKeluar { get; set; }

        [Display(Name = "Sisa Stok (Kg)")]
        public double SisaStok
        {
            get { return TotalMasuk - TotalKeluar; }
        }
    }
}
