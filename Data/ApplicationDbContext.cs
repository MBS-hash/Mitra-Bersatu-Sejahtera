using Microsoft.EntityFrameworkCore;
using MBSAdminApp.Models;

namespace MBSAdminApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        // Konstruktor utama dengan konfigurasi dari dependency injection
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Daftar entitas yang akan dibuat sebagai tabel di database SQLite
        public DbSet<Pembelian> Pembelian { get; set; }
        public DbSet<Produksi> Produksi { get; set; }
        public DbSet<Penjualan> Penjualan { get; set; }
        public DbSet<Keuangan> Keuangan { get; set; }
        public DbSet<Stok> Stok { get; set; }

        // Opsional: override OnModelCreating jika kamu ingin custom konfigurasi
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Contoh: kamu bisa tambahkan konfigurasi tambahan di sini
            // modelBuilder.Entity<Pembelian>().Property(p => p.NamaPetani).IsRequired();
        }
    }
}
