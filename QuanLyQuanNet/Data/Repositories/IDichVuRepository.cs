using QuanLyQuanNet.Models;

namespace QuanLyQuanNet.Data.Repositories
{
    public interface IDichVuRepository : IRepository<DichVu>
    {
        Task<IEnumerable<DichVu>> GetDichVuConHangAsync();
        Task<bool> CapNhatSoLuongTonAsync(int dichVuId, int soLuongMoi);
        Task<bool> TangSoLuongTonAsync(int dichVuId, int soLuongTang);
        Task<bool> GiamSoLuongTonAsync(int dichVuId, int soLuongGiam);
        Task<IEnumerable<DichVu>> TimKiemDichVuAsync(string keyword);
        Task<IEnumerable<DichVu>> GetDichVuTheoTrangThaiAsync(bool trangThai);
    }
}