using Microsoft.EntityFrameworkCore;
using QuanLyQuanNet.Models;

namespace QuanLyQuanNet.Data.Repositories
{
    public class NhanVienRepository : Repository<NhanVien>, INhanVienRepository
    {
        public NhanVienRepository(QuanNetDbContext context) : base(context)
        {
        }

        public async Task<NhanVien?> GetByUsernameAsync(string username)
        {
            return await _dbSet.FirstOrDefaultAsync(n => n.Username == username);
        }

        public async Task<bool> IsUsernameExistsAsync(string username)
        {
            return await _dbSet.AnyAsync(n => n.Username == username);
        }

        public async Task<NhanVien?> AuthenticateAsync(string username, string password)
        {
            var nhanVien = await GetByUsernameAsync(username);
            if (nhanVien == null || !nhanVien.TrangThai)
                return null;

            bool isValidPassword = BCrypt.Net.BCrypt.Verify(password, nhanVien.PasswordHash);
            return isValidPassword ? nhanVien : null;
        }

        public override async Task<IEnumerable<NhanVien>> GetAllAsync()
        {
            return await _dbSet.Where(n => n.TrangThai).ToListAsync();
        }
    }
}