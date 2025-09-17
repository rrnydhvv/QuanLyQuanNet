using QuanLyQuanNet.Models;

namespace QuanLyQuanNet.Data.Repositories
{
    public interface INguoiDungRepository : IRepository<NguoiDung>
    {
        Task<NguoiDung?> GetByUsernameAsync(string username);
        Task<bool> IsUsernameExistsAsync(string username);
        Task<bool> UpdateSoDuAsync(int userId, decimal soTien);
    }
}