using Microsoft.EntityFrameworkCore;
using QuanLyQuanNet.Models;

namespace QuanLyQuanNet.Data.Repositories
{
    public class MayTramRepository : Repository<MayTram>, IMayTramRepository
    {
        public MayTramRepository(IDbContextFactory<QuanNetDbContext> contextFactory) : base(contextFactory)
        {
        }

        public async Task<IEnumerable<MayTram>> GetMayTramTheoTrangThaiAsync(TrangThaiMay trangThai)
        {
            using var context = await _contextFactory.CreateDbContextAsync();
            return await context.Set<MayTram>()
                .Where(m => m.TrangThai == trangThai)
                .OrderBy(m => m.TenMay)
                .ToListAsync();
        }

        public async Task<IEnumerable<MayTram>> GetMayTramTrongAsync()
        {
            using var context = await _contextFactory.CreateDbContextAsync();
            return await context.Set<MayTram>()
                .Where(m => m.TrangThai == TrangThaiMay.Trong)
                .OrderBy(m => m.TenMay)
                .ToListAsync();
        }

        public async Task<bool> BatDauSuDungMayAsync(int mayId, int userId)
        {
            try
            {
                using var context = await _contextFactory.CreateDbContextAsync();
                var mayTram = await context.Set<MayTram>().FindAsync(mayId);
                if (mayTram == null || mayTram.TrangThai != TrangThaiMay.Trong)
                    return false;

                mayTram.TrangThai = TrangThaiMay.DangSuDung;
                mayTram.UserIDHienTai = userId;
                mayTram.ThoiGianBatDau = DateTime.Now;

                await context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> KetThucSuDungMayAsync(int mayId)
        {
            try
            {
                using var context = await _contextFactory.CreateDbContextAsync();
                var mayTram = await context.Set<MayTram>().FindAsync(mayId);
                if (mayTram == null || mayTram.TrangThai != TrangThaiMay.DangSuDung)
                    return false;

                mayTram.TrangThai = TrangThaiMay.Trong;
                mayTram.UserIDHienTai = null;
                mayTram.ThoiGianBatDau = null;

                await context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<IEnumerable<MayTram>> GetMayTramDangSuDungAsync()
        {
            using var context = await _contextFactory.CreateDbContextAsync();
            return await context.Set<MayTram>()
                .Where(m => m.TrangThai == TrangThaiMay.DangSuDung)
                .OrderBy(m => m.TenMay)
                .ToListAsync();
        }

        public async Task<IEnumerable<MayTram>> GetMayTramBaoTriAsync()
        {
            using var context = await _contextFactory.CreateDbContextAsync();
            return await context.Set<MayTram>()
                .Where(m => m.TrangThai == TrangThaiMay.BaoTri)
                .OrderBy(m => m.TenMay)
                .ToListAsync();
        }

        public async Task<bool> ChuyenTrangThaiMayAsync(int mayId, TrangThaiMay trangThaiMoi)
        {
            try
            {
                using var context = await _contextFactory.CreateDbContextAsync();
                var mayTram = await context.Set<MayTram>().FindAsync(mayId);
                if (mayTram == null)
                    return false;

                // Reset usage info when changing to available
                if (trangThaiMoi == TrangThaiMay.Trong)
                {
                    mayTram.UserIDHienTai = null;
                    mayTram.ThoiGianBatDau = null;
                }

                mayTram.TrangThai = trangThaiMoi;
                await context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<int> GetTongSoMayAsync()
        {
            using var context = await _contextFactory.CreateDbContextAsync();
            return await context.Set<MayTram>().CountAsync();
        }

        public async Task<int> GetSoMayTrongAsync()
        {
            using var context = await _contextFactory.CreateDbContextAsync();
            return await context.Set<MayTram>().CountAsync(m => m.TrangThai == TrangThaiMay.Trong);
        }

        public async Task<int> GetSoMayDangSuDungAsync()
        {
            using var context = await _contextFactory.CreateDbContextAsync();
            return await context.Set<MayTram>().CountAsync(m => m.TrangThai == TrangThaiMay.DangSuDung);
        }

        public async Task<IEnumerable<MayTram>> GetAllWithUserAsync()
        {
            using var context = await _contextFactory.CreateDbContextAsync();
            return await context.Set<MayTram>().Include(m => m.NguoiDungHienTai).ToListAsync();
        }

        public async Task<MayTram?> GetByIdWithUserAsync(int id)
        {
            using var context = await _contextFactory.CreateDbContextAsync();
            return await context.Set<MayTram>().Include(m => m.NguoiDungHienTai).FirstOrDefaultAsync(m => m.MayID == id);
        }

        // Additional interface methods
        public async Task<IEnumerable<MayTram>> GetAllWithUsersAsync()
        {
            return await GetAllWithUserAsync();
        }

        public async Task<bool> BatMayAsync(int mayId, int userId)
        {
            return await BatDauSuDungMayAsync(mayId, userId);
        }

        public async Task<bool> TatMayAsync(int mayId)
        {
            return await KetThucSuDungMayAsync(mayId);
        }

        public async Task<IEnumerable<MayTram>> GetMayTrongAsync()
        {
            return await GetMayTramTrongAsync();
        }

        public async Task<IEnumerable<MayTram>> GetMayDangSuDungAsync()
        {
            return await GetMayTramDangSuDungAsync();
        }

        public async Task<bool> UpdateTrangThaiAsync(int mayId, TrangThaiMay trangThai)
        {
            return await ChuyenTrangThaiMayAsync(mayId, trangThai);
        }

        public async Task<TimeSpan> GetThoiGianSuDungAsync(int mayId)
        {
            using var context = await _contextFactory.CreateDbContextAsync();
            var mayTram = await context.Set<MayTram>().FindAsync(mayId);
            if (mayTram?.ThoiGianBatDau == null)
                return TimeSpan.Zero;

            return DateTime.Now - mayTram.ThoiGianBatDau.Value;
        }

        public async Task<decimal> TinhTienHienTaiAsync(int mayId)
        {
            var thoiGian = await GetThoiGianSuDungAsync(mayId);
            if (thoiGian == TimeSpan.Zero) return 0;

            // Giả sử 10,000 VNĐ/giờ
            const decimal giaTheoGio = 10000m;
            var soGio = (decimal)thoiGian.TotalHours;
            return Math.Round(soGio * giaTheoGio, 0);
        }
    }
}