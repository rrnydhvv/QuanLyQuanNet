using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyQuanNet.Models
{
    public enum ChucVu
    {
        NhanVien = 0,
        QuanLy = 1,
        QuanTriVien = 2
    }

    [Table("NhanVien")]
    public class NhanVien
    {
        [Key]
        public int NhanVienID { get; set; }

        [Required]
        [StringLength(100)]
        public string HoTen { get; set; } = string.Empty;

        public ChucVu ChucVu { get; set; } = ChucVu.NhanVien;

        [Required]
        [StringLength(50)]
        public string Username { get; set; } = string.Empty;

        [Required]
        [StringLength(255)]
        public string PasswordHash { get; set; } = string.Empty;

        [StringLength(100)]
        public string? CaLamViec { get; set; }

        [StringLength(15)]
        public string? SoDienThoai { get; set; }

        [StringLength(200)]
        public string? DiaChi { get; set; }

        public DateTime NgayVaoLam { get; set; } = DateTime.Now;

        public bool TrangThai { get; set; } = true; // true: đang làm việc, false: đã nghỉ

        [Column(TypeName = "decimal(18,2)")]
        public decimal Luong { get; set; } = 0;

        // Navigation properties
        public virtual ICollection<HoaDon> HoaDons { get; set; } = new List<HoaDon>();
    }
}