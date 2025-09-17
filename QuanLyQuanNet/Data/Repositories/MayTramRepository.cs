using Microsoft.EntityFrameworkCore;
using QuanLyQuanNet.Models;

namespace QuanLyQuanNet.Data.Repositories
{
    public class MayTramRepository : Repository<MayTram>, IMayTramRepository
    {
        public MayTramRepository(QuanNetDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<MayTram>> GetAllWithUsersAsync()
        {
            return await _dbSet
                .Include(m => m.NguoiDungHienTai)
                .OrderBy(m => m.TenMay)
                .ToListAsync();
        }

        public async Task<MayTram?> GetByIdWithUserAsync(int mayId)
        {
            return await _dbSet
                .Include(m => m.NguoiDungHienTai)
                .FirstOrDefaultAsync(m => m.MayID == mayId);
        }

        public async Task<bool> BatMayAsync(int mayId, int userId)
        {
            var mayTram = await GetByIdAsync(mayId);
            if (mayTram == null || mayTram.TrangThai != TrangThaiMay.Trong)
                return false;

            mayTram.TrangThai = TrangThaiMay.DangSuDung;
            mayTram.UserIDHienTai = userId;
            mayTram.ThoiGianBatDau = DateTime.Now;

            await UpdateAsync(mayTram);
            return true;
        }

        public async Task<bool> TatMayAsync(int mayId)
        {
            var mayTram = await GetByIdAsync(mayId);
            if (mayTram == null)
                return false;

            mayTram.TrangThai = TrangThaiMay.Trong;
            mayTram.UserIDHienTai = null;
            mayTram.ThoiGianBatDau = null;

            await UpdateAsync(mayTram);
            return true;
        }

        public async Task<IEnumerable<MayTram>> GetMayTrongAsync()
        {
            return await _dbSet
                .Where(m => m.TrangThai == TrangThaiMay.Trong)
                .OrderBy(m => m.TenMay)
                .ToListAsync();
        }

        public async Task<IEnumerable<MayTram>> GetMayDangSuDungAsync()
        {
            return await _dbSet
                .Include(m => m.NguoiDungHienTai)
                .Where(m => m.TrangThai == TrangThaiMay.DangSuDung)
                .OrderBy(m => m.TenMay)
                .ToListAsync();
        }

        public async Task<bool> UpdateTrangThaiAsync(int mayId, TrangThaiMay trangThai)
        {
            var mayTram = await GetByIdAsync(mayId);
            if (mayTram == null)
                return false;

            mayTram.TrangThai = trangThai;
            if (trangThai == TrangThaiMay.Trong)
            {
                mayTram.UserIDHienTai = null;
                mayTram.ThoiGianBatDau = null;
            }

            await UpdateAsync(mayTram);
            return true;
        }

        public async Task<TimeSpan> GetThoiGianSuDungAsync(int mayId)
        {
            var mayTram = await GetByIdAsync(mayId);
            if (mayTram?.ThoiGianBatDau == null)
                return TimeSpan.Zero;

            return DateTime.Now - mayTram.ThoiGianBatDau.Value;
        }

        public async Task<decimal> TinhTienHienTaiAsync(int mayId)
        {
            var mayTram = await GetByIdAsync(mayId);
            if (mayTram?.ThoiGianBatDau == null)
                return 0;

            var thoiGianSuDung = DateTime.Now - mayTram.ThoiGianBatDau.Value;
            var soGio = (decimal)thoiGianSuDung.TotalHours;
            
            // Làm tròn lên 15 phút (0.25 giờ)
            soGio = Math.Ceiling(soGio * 4) / 4;
            
            return soGio * mayTram.GiaTheoGio;
        }

        public override async Task<IEnumerable<MayTram>> GetAllAsync()
        {
            return await _dbSet.Include(m => m.NguoiDungHienTai).ToListAsync();
        }

        public override async Task<MayTram?> GetByIdAsync(int id)
        {
            return await _dbSet.Include(m => m.NguoiDungHienTai).FirstOrDefaultAsync(m => m.MayID == id);
        }
    }
}