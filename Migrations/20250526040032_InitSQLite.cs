using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MBSAdminApp.Migrations
{
    /// <inheritdoc />
    public partial class InitSQLite : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Keuangan",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    JenisTransaksi = table.Column<string>(type: "TEXT", nullable: false),
                    Kategori = table.Column<string>(type: "TEXT", nullable: false),
                    Nominal = table.Column<decimal>(type: "TEXT", nullable: false),
                    Keterangan = table.Column<string>(type: "TEXT", nullable: false),
                    Tanggal = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Keuangan", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pembelian",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NamaPetani = table.Column<string>(type: "TEXT", nullable: false),
                    Wilayah = table.Column<string>(type: "TEXT", nullable: false),
                    JenisBahan = table.Column<string>(type: "TEXT", nullable: false),
                    JumlahKg = table.Column<double>(type: "REAL", nullable: false),
                    HargaPerKg = table.Column<decimal>(type: "TEXT", nullable: false),
                    Tanggal = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pembelian", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Penjualan",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NamaPembeli = table.Column<string>(type: "TEXT", nullable: false),
                    JenisProduk = table.Column<string>(type: "TEXT", nullable: false),
                    JumlahKg = table.Column<double>(type: "REAL", nullable: false),
                    HargaPerKg = table.Column<decimal>(type: "TEXT", nullable: false),
                    Tanggal = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Penjualan", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Produksi",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    JenisProses = table.Column<string>(type: "TEXT", nullable: false),
                    BahanAwal = table.Column<string>(type: "TEXT", nullable: false),
                    JumlahAwalKg = table.Column<double>(type: "REAL", nullable: false),
                    BahanHasil = table.Column<string>(type: "TEXT", nullable: false),
                    JumlahHasilKg = table.Column<double>(type: "REAL", nullable: false),
                    NamaOperator = table.Column<string>(type: "TEXT", nullable: false),
                    Tanggal = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produksi", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Stok",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    JenisBahan = table.Column<string>(type: "TEXT", nullable: false),
                    TotalMasuk = table.Column<double>(type: "REAL", nullable: false),
                    TotalKeluar = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stok", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Keuangan");

            migrationBuilder.DropTable(
                name: "Pembelian");

            migrationBuilder.DropTable(
                name: "Penjualan");

            migrationBuilder.DropTable(
                name: "Produksi");

            migrationBuilder.DropTable(
                name: "Stok");
        }
    }
}
