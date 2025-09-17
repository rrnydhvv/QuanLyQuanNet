using QuanLyQuanNet.Models;

namespace QuanLyQuanNet.Services
{
    public interface IQuanLyMayService
    {
        Task<IEnumerable<MayTram>> GetAllMayTramAsync();
        Task<bool> BatMayAsync(int mayId, int userId);
        Task<bool> TatMayAsync(int mayId);
        Task<TimeSpan> GetThoiGianSuDungAsync(int mayId);
        Task<decimal> TinhTienGioAsync(int mayId);
    }
}