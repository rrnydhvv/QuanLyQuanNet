using QuanLyQuanNet.Models;

namespace QuanLyQuanNet.Data.Repositories
{
    public interface INhanVienRepository : IRepository<NhanVien>
    {
        Task<NhanVien?> GetByUsernameAsync(string username);
        Task<bool> IsUsernameExistsAsync(string username);
        Task<NhanVien?> AuthenticateAsync(string username, string password);
    }
}