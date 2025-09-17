using Microsoft.EntityFrameworkCore;
using QuanLyQuanNet.Models;

namespace QuanLyQuanNet.Data.Repositories
{
    public class DichVuRepository : Repository<DichVu>, IDichVuRepository
    {
        public DichVuRepository(QuanNetDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<DichVu>> GetDichVuConHangAsync()
        {
            return await _context.DichVus
                .Where(d => d.SoLuongTon > 0 && d.TrangThai)
                .OrderBy(d => d.TenDichVu)
                .ToListAsync();
        }

        public async Task<IEnumerable<DichVu>> GetDichVuTheoTrangThaiAsync(bool trangThai)
        {
            return await _context.DichVus
                .Where(d => d.TrangThai == trangThai)
                .OrderBy(d => d.TenDichVu)
                .ToListAsync();
        }

        public async Task<bool> CapNhatSoLuongTonAsync(int dichVuId, int soLuongMoi)
        {
            var dichVu = await _context.DichVus.FindAsync(dichVuId);
            if (dichVu == null) return false;

            dichVu.SoLuongTon = soLuongMoi;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> TangSoLuongTonAsync(int dichVuId, int soLuongTang)
        {
            var dichVu = await _context.DichVus.FindAsync(dichVuId);
            if (dichVu == null) return false;

            dichVu.SoLuongTon += soLuongTang;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> GiamSoLuongTonAsync(int dichVuId, int soLuongGiam)
        {
            var dichVu = await _context.DichVus.FindAsync(dichVuId);
            if (dichVu == null) return false;

            dichVu.SoLuongTon = Math.Max(0, dichVu.SoLuongTon - soLuongGiam);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<DichVu>> TimKiemDichVuAsync(string keyword)
        {
            return await _context.DichVus
                .Where(d => d.TenDichVu.ToLower().Contains(keyword.ToLower()) ||
                           (d.MoTa != null && d.MoTa.ToLower().Contains(keyword.ToLower())) ||
                           (d.DonViTinh != null && d.DonViTinh.ToLower().Contains(keyword.ToLower())))
                .OrderBy(d => d.TenDichVu)
                .ToListAsync();
        }
    }
}