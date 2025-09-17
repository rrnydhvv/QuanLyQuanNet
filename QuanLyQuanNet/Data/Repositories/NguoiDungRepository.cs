using Microsoft.EntityFrameworkCore;
using QuanLyQuanNet.Models;

namespace QuanLyQuanNet.Data.Repositories
{
    public class NguoiDungRepository : Repository<NguoiDung>, INguoiDungRepository
    {
        public NguoiDungRepository(IDbContextFactory<QuanNetDbContext> contextFactory) : base(contextFactory)
        {
        }

        public async Task<NguoiDung?> GetByUsernameAsync(string username)
        {
            using var context = await _contextFactory.CreateDbContextAsync();
            return await context.Set<NguoiDung>().FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task<bool> UsernameExistsAsync(string username)
        {
            using var context = await _contextFactory.CreateDbContextAsync();
            return await context.Set<NguoiDung>().AnyAsync(u => u.Username == username);
        }

        public async Task<bool> IsUsernameExistsAsync(string username)
        {
            return await UsernameExistsAsync(username);
        }

        public async Task<bool> UpdateSoDuAsync(int userId, decimal soDuMoi)
        {
            try
            {
                using var context = await _contextFactory.CreateDbContextAsync();
                var nguoiDung = await context.Set<NguoiDung>().FindAsync(userId);
                if (nguoiDung == null) return false;

                nguoiDung.SoDu = soDuMoi;
                await context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<IEnumerable<NguoiDung>> GetAllWithHoaDonsAsync()
        {
            using var context = await _contextFactory.CreateDbContextAsync();
            return await context.Set<NguoiDung>().Include(u => u.HoaDons).ToListAsync();
        }

        public async Task<NguoiDung?> GetByIdWithHoaDonsAsync(int id)
        {
            using var context = await _contextFactory.CreateDbContextAsync();
            return await context.Set<NguoiDung>().Include(u => u.HoaDons).FirstOrDefaultAsync(u => u.UserID == id);
        }
    }
}