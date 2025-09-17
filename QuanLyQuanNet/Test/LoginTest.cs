using QuanLyQuanNet.Data;
using QuanLyQuanNet.Data.Repositories;
using QuanLyQuanNet.Services;
using QuanLyQuanNet.Utils;
using Microsoft.EntityFrameworkCore;

namespace QuanLyQuanNet.Test
{
    public static class LoginTest
    {
        public static async Task<bool> TestLogin()
        {
            try
            {
                // Tạo hash mới cho "admin123"
                string password = "admin123";
                string newHash = PasswordHelper.HashPassword(password);
                
                Console.WriteLine($"New Hash generated: {newHash}");
                
                // Update database với hash mới
                using var context = new QuanNetDbContext();
                var admin = await context.NhanViens.FirstOrDefaultAsync(n => n.Username == "admin");
                if (admin != null)
                {
                    admin.PasswordHash = newHash;
                    await context.SaveChangesAsync();
                    Console.WriteLine("Updated admin password hash in database");
                }
                
                // Test login
                var nhanVienRepo = new NhanVienRepository(context);
                var authService = new AuthenticationService(nhanVienRepo);
                
                var user = await authService.LoginAsync("admin", password);
                
                if (user != null)
                {
                    Console.WriteLine($"Login successful! Welcome {user.HoTen}");
                    return true;
                }
                else
                {
                    Console.WriteLine("Login failed!");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }
    }
}