using Microsoft.EntityFrameworkCore;
using QuanLyQuanNet.Models;

namespace QuanLyQuanNet.Data.Repositories
{
    public class MayTramRepository : Repository<MayTram>
    {
        public MayTramRepository(QuanNetDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<MayTram>> GetAvailableAsync()
        {
            return await _dbSet.Where(m => m.TrangThai == TrangThaiMay.Trong).ToListAsync();
        }

        public async Task<bool> UpdateTrangThaiAsync(int mayId, TrangThaiMay trangThai)
        {
            var may = await GetByIdAsync(mayId);
            if (may == null)
                return false;

            may.TrangThai = trangThai;
            await UpdateAsync(may);
            return true;
        }

        public async Task<bool> BatMayAsync(int mayId, int userId)
        {
            var may = await GetByIdAsync(mayId);
            if (may == null || may.TrangThai != TrangThaiMay.Trong)
                return false;

            may.TrangThai = TrangThaiMay.DangSuDung;
            may.UserIDHienTai = userId;
            may.ThoiGianBatDau = DateTime.Now;
            await UpdateAsync(may);
            return true;
        }

        public async Task<bool> TatMayAsync(int mayId)
        {
            var may = await GetByIdAsync(mayId);
            if (may == null)
                return false;

            may.TrangThai = TrangThaiMay.Trong;
            may.UserIDHienTai = null;
            may.ThoiGianBatDau = null;
            await UpdateAsync(may);
            return true;
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