using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyQuanNet.Models
{
    [Table("DichVu")]
    public class DichVu
    {
        [Key]
        public int DichVuID { get; set; }

        [Required]
        [StringLength(100)]
        public string TenDichVu { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18,2)")]
        public decimal DonGia { get; set; } = 0;

        [StringLength(500)]
        public string? MoTa { get; set; }

        [StringLength(20)]
        public string? DonViTinh { get; set; }

        public bool TrangThai { get; set; } = true; // true: còn hàng, false: hết hàng

        public int SoLuongTon { get; set; } = 0;

        // Navigation properties
        public virtual ICollection<ChiTietHoaDon> ChiTietHoaDons { get; set; } = new List<ChiTietHoaDon>();
    }
}