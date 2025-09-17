using QuanLyQuanNet.Data.Repositories;
using QuanLyQuanNet.Models;

namespace QuanLyQuanNet.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly INhanVienRepository _nhanVienRepository;
        private NhanVien? _currentUser;

        public AuthenticationService(INhanVienRepository nhanVienRepository)
        {
            _nhanVienRepository = nhanVienRepository;
        }

        public NhanVien? CurrentUser => _currentUser;

        public bool IsLoggedIn => _currentUser != null;

        public async Task<NhanVien?> LoginAsync(string username, string password)
        {
            _currentUser = await _nhanVienRepository.AuthenticateAsync(username, password);
            return _currentUser;
        }

        public void Logout()
        {
            _currentUser = null;
        }

        public bool HasPermission(ChucVu requiredRole)
        {
            if (!IsLoggedIn)
                return false;

            return _currentUser!.ChucVu >= requiredRole;
        }
    }
}