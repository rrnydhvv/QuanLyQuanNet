using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyQuanNet.Models
{
    [Table("HoaDon")]
    public class HoaDon
    {
        [Key]
        public int HoaDonID { get; set; }

        [Required]
        public int UserID { get; set; }

        [Required]
        public int MayID { get; set; }

        [Required]
        public DateTime GioBatDau { get; set; }

        public DateTime? GioKetThuc { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal TienGio { get; set; } = 0;

        [Column(TypeName = "decimal(18,2)")]
        public decimal TienDichVu { get; set; } = 0;

        [Column(TypeName = "decimal(18,2)")]
        public decimal TongTien { get; set; } = 0;

        public DateTime NgayTao { get; set; } = DateTime.Now;

        public bool DaThanhToan { get; set; } = false;

        public int? NhanVienID { get; set; }

        [StringLength(500)]
        public string? GhiChu { get; set; }

        // Navigation properties
        [ForeignKey("UserID")]
        public virtual NguoiDung NguoiDung { get; set; } = null!;

        [ForeignKey("MayID")]
        public virtual MayTram MayTram { get; set; } = null!;

        [ForeignKey("NhanVienID")]
        public virtual NhanVien? NhanVien { get; set; }

        public virtual ICollection<ChiTietHoaDon> ChiTietHoaDons { get; set; } = new List<ChiTietHoaDon>();
    }
}