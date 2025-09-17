using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyQuanNet.Models
{
    public enum TrangThaiMay
    {
        Trong = 0,
        DangSuDung = 1,
        Hong = 2,
        BaoTri = 3
    }

    [Table("MayTram")]
    public class MayTram
    {
        [Key]
        public int MayID { get; set; }

        [Required]
        [StringLength(50)]
        public string TenMay { get; set; } = string.Empty;

        public TrangThaiMay TrangThai { get; set; } = TrangThaiMay.Trong;

        [StringLength(100)]
        public string? ViTri { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal GiaTheoGio { get; set; } = 0;

        public DateTime? ThoiGianBatDau { get; set; }

        public int? UserIDHienTai { get; set; }

        // Navigation properties
        public virtual ICollection<HoaDon> HoaDons { get; set; } = new List<HoaDon>();
        
        [ForeignKey("UserIDHienTai")]
        public virtual NguoiDung? NguoiDungHienTai { get; set; }
    }
}