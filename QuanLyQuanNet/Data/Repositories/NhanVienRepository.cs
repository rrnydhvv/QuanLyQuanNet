using Microsoft.EntityFrameworkCore;
using QuanLyQuanNet.Models;

namespace QuanLyQuanNet.Data.Repositories
{
    public class NhanVienRepository : Repository<NhanVien>, INhanVienRepository
    {
        public NhanVienRepository(IDbContextFactory<QuanNetDbContext> contextFactory) : base(contextFactory)
        {
        }

        public async Task<NhanVien?> GetByUsernameAsync(string username)
        {
            using var context = await _contextFactory.CreateDbContextAsync();
            return await context.Set<NhanVien>().FirstOrDefaultAsync(n => n.Username == username);
        }

        public async Task<bool> UsernameExistsAsync(string username)
        {
            using var context = await _contextFactory.CreateDbContextAsync();
            return await context.Set<NhanVien>().AnyAsync(n => n.Username == username);
        }

        public async Task<bool> IsUsernameExistsAsync(string username)
        {
            return await UsernameExistsAsync(username);
        }

        public async Task<NhanVien?> AuthenticateAsync(string username, string password)
        {
            using var context = await _contextFactory.CreateDbContextAsync();
            var nhanVien = await context.Set<NhanVien>().FirstOrDefaultAsync(n => n.Username == username);
            
            if (nhanVien != null && BCrypt.Net.BCrypt.Verify(password, nhanVien.PasswordHash))
            {
                return nhanVien;
            }
            
            return null;
        }

        public async Task<bool> UpdateTrangThaiAsync(int nhanVienId, bool trangThai)
        {
            try
            {
                using var context = await _contextFactory.CreateDbContextAsync();
                var nhanVien = await context.Set<NhanVien>().FindAsync(nhanVienId);
                if (nhanVien == null) return false;

                nhanVien.TrangThai = trangThai;
                await context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<IEnumerable<NhanVien>> GetNhanVienHoatDongAsync()
        {
            using var context = await _contextFactory.CreateDbContextAsync();
            return await context.Set<NhanVien>().Where(n => n.TrangThai).ToListAsync();
        }
    }
}