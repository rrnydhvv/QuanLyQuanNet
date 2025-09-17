using BCrypt.Net;

namespace QuanLyQuanNet.Utils
{
    public static class PasswordHelper
    {
        /// <summary>
        /// Tạo hash password từ plain text
        /// </summary>
        public static string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password, 11);
        }

        /// <summary>
        /// Verify password với hash
        /// </summary>
        public static bool VerifyPassword(string password, string hash)
        {
            try
            {
                return BCrypt.Net.BCrypt.Verify(password, hash);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Tạo hash cho password mặc định "admin123"
        /// </summary>
        public static string GetDefaultPasswordHash()
        {
            return HashPassword("admin123");
        }
    }
}