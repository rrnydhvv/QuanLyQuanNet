using Microsoft.EntityFrameworkCore;
using QuanLyQuanNet.Models;

namespace QuanLyQuanNet.Data.Repositories
{
    public class DichVuRepository : Repository<DichVu>, IDichVuRepository
    {
        public DichVuRepository(IDbContextFactory<QuanNetDbContext> contextFactory) : base(contextFactory)
        {
        }

        public async Task<IEnumerable<DichVu>> GetDichVuConHangAsync()
        {
            using var context = await _contextFactory.CreateDbContextAsync();
            return await context.DichVus
                .Where(d => d.SoLuongTon > 0 && d.TrangThai)
                .OrderBy(d => d.TenDichVu)
                .ToListAsync();
        }

        public async Task<IEnumerable<DichVu>> GetDichVuTheoTrangThaiAsync(bool trangThai)
        {
            using var context = await _contextFactory.CreateDbContextAsync();
            return await context.DichVus
                .Where(d => d.TrangThai == trangThai)
                .OrderBy(d => d.TenDichVu)
                .ToListAsync();
        }

        public async Task<bool> CapNhatSoLuongTonAsync(int dichVuId, int soLuongMoi)
        {
            using var context = await _contextFactory.CreateDbContextAsync();
            var dichVu = await context.DichVus.FindAsync(dichVuId);
            if (dichVu == null) return false;

            dichVu.SoLuongTon = soLuongMoi;
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> TangSoLuongTonAsync(int dichVuId, int soLuongTang)
        {
            using var context = await _contextFactory.CreateDbContextAsync();
            var dichVu = await context.DichVus.FindAsync(dichVuId);
            if (dichVu == null) return false;

            dichVu.SoLuongTon += soLuongTang;
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> GiamSoLuongTonAsync(int dichVuId, int soLuongGiam)
        {
            using var context = await _contextFactory.CreateDbContextAsync();
            var dichVu = await context.DichVus.FindAsync(dichVuId);
            if (dichVu == null) return false;

            dichVu.SoLuongTon = Math.Max(0, dichVu.SoLuongTon - soLuongGiam);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<DichVu>> TimKiemDichVuAsync(string keyword)
        {
            using var context = await _contextFactory.CreateDbContextAsync();
            return await context.DichVus
                .Where(d => d.TenDichVu.ToLower().Contains(keyword.ToLower()) ||
                           (d.MoTa != null && d.MoTa.ToLower().Contains(keyword.ToLower())) ||
                           (d.DonViTinh != null && d.DonViTinh.ToLower().Contains(keyword.ToLower())))
                .OrderBy(d => d.TenDichVu)
                .ToListAsync();
        }
    }
}