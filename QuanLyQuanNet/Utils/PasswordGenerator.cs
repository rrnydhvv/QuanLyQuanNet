using QuanLyQuanNet.Utils;

namespace QuanLyQuanNet
{
    public class PasswordGenerator
    {
        public static void GeneratePasswordHashes()
        {
            string password = "admin123";
            string hash = PasswordHelper.HashPassword(password);
            
            Console.WriteLine($"Password: {password}");
            Console.WriteLine($"Hash: {hash}");
            Console.WriteLine($"Verify: {PasswordHelper.VerifyPassword(password, hash)}");
        }
    }
}