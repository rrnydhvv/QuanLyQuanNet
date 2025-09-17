using QuanLyQuanNet.Models;

namespace QuanLyQuanNet.Services
{
    public interface IHoaDonService
    {
        Task<HoaDon> TaoHoaDonAsync(int userId, int mayId, int nhanVienId);
        Task<bool> ThemDichVuAsync(int hoaDonId, int dichVuId, int soLuong);
        Task<decimal> TinhTongTienAsync(int hoaDonId);
        Task<bool> ThanhToanAsync(int hoaDonId);
        Task<IEnumerable<HoaDon>> GetHoaDonChuaThanhToanAsync();
    }
}