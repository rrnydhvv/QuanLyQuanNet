using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyQuanNet.Models
{
    [Table("ChiTietHoaDon")]
    public class ChiTietHoaDon
    {
        [Key]
        public int CTID { get; set; }

        [Required]
        public int HoaDonID { get; set; }

        [Required]
        public int DichVuID { get; set; }

        [Required]
        public int SoLuong { get; set; } = 1;

        [Column(TypeName = "decimal(18,2)")]
        public decimal DonGia { get; set; } = 0;

        [Column(TypeName = "decimal(18,2)")]
        public decimal ThanhTien { get; set; } = 0;

        public DateTime ThoiGianDat { get; set; } = DateTime.Now;

        [StringLength(200)]
        public string? GhiChu { get; set; }

        // Navigation properties
        [ForeignKey("HoaDonID")]
        public virtual HoaDon HoaDon { get; set; } = null!;

        [ForeignKey("DichVuID")]
        public virtual DichVu DichVu { get; set; } = null!;
    }
}