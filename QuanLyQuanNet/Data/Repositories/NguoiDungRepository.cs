using Microsoft.EntityFrameworkCore;
using QuanLyQuanNet.Models;

namespace QuanLyQuanNet.Data.Repositories
{
    public class NguoiDungRepository : Repository<NguoiDung>, INguoiDungRepository
    {
        public NguoiDungRepository(QuanNetDbContext context) : base(context)
        {
        }

        public async Task<NguoiDung?> GetByUsernameAsync(string username)
        {
            return await _dbSet.FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task<bool> IsUsernameExistsAsync(string username)
        {
            return await _dbSet.AnyAsync(u => u.Username == username);
        }

        public async Task<bool> UpdateSoDuAsync(int userId, decimal soTien)
        {
            var user = await GetByIdAsync(userId);
            if (user == null)
                return false;

            user.SoDu += soTien;
            await UpdateAsync(user);
            return true;
        }

        public override async Task<IEnumerable<NguoiDung>> GetAllAsync()
        {
            return await _dbSet.Include(u => u.HoaDons).ToListAsync();
        }

        public override async Task<NguoiDung?> GetByIdAsync(int id)
        {
            return await _dbSet.Include(u => u.HoaDons).FirstOrDefaultAsync(u => u.UserID == id);
        }
    }
}