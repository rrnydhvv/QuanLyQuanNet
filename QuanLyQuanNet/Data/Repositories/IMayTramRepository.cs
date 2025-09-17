using QuanLyQuanNet.Models;

namespace QuanLyQuanNet.Data.Repositories
{
    public interface IMayTramRepository : IRepository<MayTram>
    {
        Task<IEnumerable<MayTram>> GetAllWithUsersAsync();
        Task<MayTram?> GetByIdWithUserAsync(int mayId);
        Task<bool> BatMayAsync(int mayId, int userId);
        Task<bool> TatMayAsync(int mayId);
        Task<IEnumerable<MayTram>> GetMayTrongAsync();
        Task<IEnumerable<MayTram>> GetMayDangSuDungAsync();
        Task<bool> UpdateTrangThaiAsync(int mayId, TrangThaiMay trangThai);
        Task<TimeSpan> GetThoiGianSuDungAsync(int mayId);
        Task<decimal> TinhTienHienTaiAsync(int mayId);
    }
}