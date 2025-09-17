using QuanLyQuanNet.Data.Repositories;
using QuanLyQuanNet.Models;

namespace QuanLyQuanNet.Services
{
    public class QuanLyMayService : IQuanLyMayService
    {
        private readonly MayTramRepository _mayTramRepository;

        public QuanLyMayService(MayTramRepository mayTramRepository)
        {
            _mayTramRepository = mayTramRepository;
        }

        public async Task<IEnumerable<MayTram>> GetAllMayTramAsync()
        {
            return await _mayTramRepository.GetAllAsync();
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
            var mayTram = await _mayTramRepository.GetByIdAsync(mayId);
            if (mayTram?.ThoiGianBatDau == null)
                return TimeSpan.Zero;

            return DateTime.Now - mayTram.ThoiGianBatDau.Value;
        }

        public async Task<decimal> TinhTienGioAsync(int mayId)
        {
            var mayTram = await _mayTramRepository.GetByIdAsync(mayId);
            if (mayTram?.ThoiGianBatDau == null)
                return 0;

            var thoiGianSuDung = DateTime.Now - mayTram.ThoiGianBatDau.Value;
            var soGio = (decimal)thoiGianSuDung.TotalHours;
            
            // Làm tròn lên 15 phút (0.25 giờ)
            soGio = Math.Ceiling(soGio * 4) / 4;
            
            return soGio * mayTram.GiaTheoGio;
        }
    }
}