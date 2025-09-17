using QuanLyQuanNet.Data.Repositories;
using QuanLyQuanNet.Models;

namespace QuanLyQuanNet.Services
{
    public class QuanLyMayService : IQuanLyMayService
    {
        private readonly IMayTramRepository _mayTramRepository;

        public QuanLyMayService(IMayTramRepository mayTramRepository)
        {
            _mayTramRepository = mayTramRepository;
        }

        public async Task<IEnumerable<MayTram>> GetAllMayTramAsync()
        {
            return await _mayTramRepository.GetAllWithUsersAsync();
        }

        public async Task<bool> BatMayAsync(int mayId, int userId)
        {
            return await _mayTramRepository.BatMayAsync(mayId, userId);
        }

        public async Task<bool> TatMayAsync(int mayId)
        {
            return await _mayTramRepository.TatMayAsync(mayId);
        }

        public async Task<TimeSpan> GetThoiGianSuDungAsync(int mayId)
        {
            return await _mayTramRepository.GetThoiGianSuDungAsync(mayId);
        }

        public async Task<decimal> TinhTienGioAsync(int mayId)
        {
            return await _mayTramRepository.TinhTienHienTaiAsync(mayId);
        }
    }
}