using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLyQuanNet.Models
{
    [Table("NguoiDung")]
    public class NguoiDung
    {
        [Key]
        public int UserID { get; set; }

        [Required]
        [StringLength(100)]
        public string HoTen { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string Username { get; set; } = string.Empty;

        [Required]
        [StringLength(255)]
        public string PasswordHash { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18,2)")]
        public decimal SoDu { get; set; } = 0;

        [Required]
        public DateTime NgayDangKy { get; set; } = DateTime.Now;

        [StringLength(15)]
        public string? SoDienThoai { get; set; }

        [StringLength(200)]
        public string? DiaChi { get; set; }

        // Navigation properties
        public virtual ICollection<HoaDon> HoaDons { get; set; } = new List<HoaDon>();
    }
}