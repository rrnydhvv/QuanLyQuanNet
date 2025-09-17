using QuanLyQuanNet.Models;

namespace QuanLyQuanNet.Services
{
    public interface IAuthenticationService
    {
        Task<NhanVien?> LoginAsync(string username, string password);
        void Logout();
        NhanVien? CurrentUser { get; }
        bool IsLoggedIn { get; }
        bool HasPermission(ChucVu requiredRole);
    }
}