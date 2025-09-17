using QuanLyQuanNet.Models;

namespace QuanLyQuanNet.Services
{
    public interface IDichVuService
    {
        Task<IEnumerable<DichVu>> GetAllDichVuAsync();
        Task<DichVu?> GetDichVuByIdAsync(int id);
        Task<bool> AddDichVuAsync(DichVu dichVu);
        Task<bool> UpdateDichVuAsync(DichVu dichVu);
        Task<bool> DeleteDichVuAsync(int id);
        Task<IEnumerable<DichVu>> GetDichVuConHangAsync();
        Task<bool> CapNhatSoLuongTonAsync(int dichVuId, int soLuongMoi);
        Task<bool> NhapHangAsync(int dichVuId, int soLuongNhap);
        Task<bool> XuatHangAsync(int dichVuId, int soLuongXuat);
        Task<IEnumerable<DichVu>> TimKiemDichVuAsync(string keyword);
        Task<bool> DoiTrangThaiDichVuAsync(int dichVuId, bool trangThai);
    }
}