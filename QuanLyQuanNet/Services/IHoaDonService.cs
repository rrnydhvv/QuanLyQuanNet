using QuanLyQuanNet.Models;

namespace QuanLyQuanNet.Services
{
    public interface IHoaDonService
    {
        // Tạo và quản lý hóa đơn
        Task<HoaDon> TaoHoaDonAsync(int userId, int mayId, int nhanVienId);
        Task<bool> ThemDichVuAsync(int hoaDonId, int dichVuId, int soLuong);
        Task<bool> XoaDichVuAsync(int hoaDonId, int dichVuId);
        Task<bool> CapNhatSoLuongDichVuAsync(int hoaDonId, int dichVuId, int soLuongMoi);
        
        // Tính toán
        Task<decimal> TinhTienMayAsync(int hoaDonId);
        Task<decimal> TinhTienDichVuAsync(int hoaDonId);
        Task<decimal> TinhTongTienAsync(int hoaDonId);
        Task<TimeSpan> TinhThoiGianSuDungAsync(int hoaDonId);
        
        // Thanh toán
        Task<bool> ThanhToanAsync(int hoaDonId, decimal tienKhachDua, string phuongThucThanhToan = "Tiền mặt");
        Task<bool> HuyHoaDonAsync(int hoaDonId);
        
        // Truy vấn
        Task<IEnumerable<HoaDon>> GetHoaDonChuaThanhToanAsync();
        Task<IEnumerable<HoaDon>> GetHoaDonTheoNgayAsync(DateTime ngay);
        Task<IEnumerable<HoaDon>> GetHoaDonTheoKhachHangAsync(int userId);
        Task<HoaDon?> GetHoaDonByIdAsync(int hoaDonId);
        Task<HoaDon?> GetHoaDonDangMoTheoMayAsync(int mayId);
        
        // Báo cáo
        Task<decimal> TinhDoanhThuTheoNgayAsync(DateTime ngay);
        Task<int> DemSoHoaDonTheoNgayAsync(DateTime ngay);
    }
}