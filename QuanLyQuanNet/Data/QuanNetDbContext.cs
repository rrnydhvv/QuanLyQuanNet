using Microsoft.EntityFrameworkCore;
using QuanLyQuanNet.Models;

namespace QuanLyQuanNet.Data
{
    public class QuanNetDbContext : DbContext
    {
        public QuanNetDbContext(DbContextOptions<QuanNetDbContext> options) : base(options)
        {
        }

        public QuanNetDbContext() : base()
        {
        }

        // DbSets
        public DbSet<NguoiDung> NguoiDungs { get; set; }
        public DbSet<MayTram> MayTrams { get; set; }
        public DbSet<DichVu> DichVus { get; set; }
        public DbSet<HoaDon> HoaDons { get; set; }
        public DbSet<ChiTietHoaDon> ChiTietHoaDons { get; set; }
        public DbSet<NhanVien> NhanViens { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // Connection string - có thể di chuyển vào config file sau
                optionsBuilder.UseSqlServer("Server=.;Database=QuanLyQuanNet;Trusted_Connection=true;TrustServerCertificate=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Cấu hình relationships
            modelBuilder.Entity<HoaDon>()
                .HasOne(h => h.NguoiDung)
                .WithMany(u => u.HoaDons)
                .HasForeignKey(h => h.UserID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<HoaDon>()
                .HasOne(h => h.MayTram)
                .WithMany(m => m.HoaDons)
                .HasForeignKey(h => h.MayID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<HoaDon>()
                .HasOne(h => h.NhanVien)
                .WithMany(n => n.HoaDons)
                .HasForeignKey(h => h.NhanVienID)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<ChiTietHoaDon>()
                .HasOne(ct => ct.HoaDon)
                .WithMany(h => h.ChiTietHoaDons)
                .HasForeignKey(ct => ct.HoaDonID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ChiTietHoaDon>()
                .HasOne(ct => ct.DichVu)
                .WithMany(d => d.ChiTietHoaDons)
                .HasForeignKey(ct => ct.DichVuID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<MayTram>()
                .HasOne(m => m.NguoiDungHienTai)
                .WithMany()
                .HasForeignKey(m => m.UserIDHienTai)
                .OnDelete(DeleteBehavior.SetNull);

            // Seed data
            SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            // Seed NhanVien (Admin default)
            modelBuilder.Entity<NhanVien>().HasData(
                new NhanVien
                {
                    NhanVienID = 1,
                    HoTen = "Admin",
                    ChucVu = ChucVu.QuanTriVien,
                    Username = "admin",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin123"), // Sử dụng BCrypt để hash password
                    CaLamViec = "Toàn thời gian",
                    NgayVaoLam = DateTime.Now,
                    TrangThai = true,
                    Luong = 0
                }
            );

            // Seed một số máy trạm mẫu
            modelBuilder.Entity<MayTram>().HasData(
                new MayTram { MayID = 1, TenMay = "PC01", TrangThai = TrangThaiMay.Trong, ViTri = "Tầng 1 - Góc A", GiaTheoGio = 10000 },
                new MayTram { MayID = 2, TenMay = "PC02", TrangThai = TrangThaiMay.Trong, ViTri = "Tầng 1 - Góc A", GiaTheoGio = 10000 },
                new MayTram { MayID = 3, TenMay = "PC03", TrangThai = TrangThaiMay.Trong, ViTri = "Tầng 1 - Góc B", GiaTheoGio = 12000 },
                new MayTram { MayID = 4, TenMay = "PC04", TrangThai = TrangThaiMay.Trong, ViTri = "Tầng 1 - Góc B", GiaTheoGio = 12000 },
                new MayTram { MayID = 5, TenMay = "VIP01", TrangThai = TrangThaiMay.Trong, ViTri = "Phòng VIP", GiaTheoGio = 20000 }
            );

            // Seed một số dịch vụ mẫu
            modelBuilder.Entity<DichVu>().HasData(
                new DichVu { DichVuID = 1, TenDichVu = "Nước ngọt Coca", DonGia = 15000, MoTa = "Coca Cola 330ml", DonViTinh = "Lon", TrangThai = true, SoLuongTon = 100 },
                new DichVu { DichVuID = 2, TenDichVu = "Nước ngọt Pepsi", DonGia = 15000, MoTa = "Pepsi 330ml", DonViTinh = "Lon", TrangThai = true, SoLuongTon = 100 },
                new DichVu { DichVuID = 3, TenDichVu = "Mì tôm", DonGia = 8000, MoTa = "Mì tôm cung đình", DonViTinh = "Ly", TrangThai = true, SoLuongTon = 50 },
                new DichVu { DichVuID = 4, TenDichVu = "Kẹo", DonGia = 5000, MoTa = "Kẹo mút", DonViTinh = "Cái", TrangThai = true, SoLuongTon = 200 },
                new DichVu { DichVuID = 5, TenDichVu = "In tài liệu", DonGia = 500, MoTa = "In đen trắng A4", DonViTinh = "Trang", TrangThai = true, SoLuongTon = 1000 }
            );
        }
    }
}