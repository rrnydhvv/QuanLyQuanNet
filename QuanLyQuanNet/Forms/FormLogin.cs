using QuanLyQuanNet.Data;
using QuanLyQuanNet.Data.Repositories;
using QuanLyQuanNet.Services;
using QuanLyQuanNet.Utils;
using Microsoft.EntityFrameworkCore;

namespace QuanLyQuanNet.Forms
{
    public partial class FormLogin : Form
    {
        private readonly IAuthenticationService _authService;

        public FormLogin()
        {
            try
            {
                Console.WriteLine("Initializing FormLogin...");
                InitializeComponent();
                Console.WriteLine("InitializeComponent completed");
                
                // Initialize services
                Console.WriteLine("Creating DbContext...");
                var dbContext = new QuanNetDbContext();
                Console.WriteLine("Creating NhanVienRepository...");
                var nhanVienRepository = new NhanVienRepository(dbContext);
                Console.WriteLine("Creating AuthenticationService...");
                _authService = new AuthenticationService(nhanVienRepository);
                Console.WriteLine("Services initialized successfully");

                // Set default values for testing
                txtUsername.Text = "admin";
                txtPassword.Text = "admin123";

                // Add Enter key handling
                this.AcceptButton = btnLogin;
                txtPassword.KeyPress += TxtPassword_KeyPress;
                
                Console.WriteLine("FormLogin initialization completed");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in FormLogin constructor: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
                throw;
            }
        }

        private async Task InitializeDatabaseAsync()
        {
            try
            {
                using var context = new QuanNetDbContext();
                await context.Database.EnsureCreatedAsync();
                
                // Check if admin user exists with proper password hash
                var admin = await context.NhanViens.FirstOrDefaultAsync(n => n.Username == "admin");
                if (admin != null)
                {
                    // Test if current hash works
                    bool hashWorks = PasswordHelper.VerifyPassword("admin123", admin.PasswordHash);
                    if (!hashWorks)
                    {
                        // Update with correct hash
                        admin.PasswordHash = PasswordHelper.HashPassword("admin123");
                        await context.SaveChangesAsync();
                        Console.WriteLine("Updated admin password hash");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Database initialization error: {ex.Message}");
            }
        }

        private void TxtPassword_KeyPress(object? sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnLogin_Click(this, e);
            }
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtUsername.Text))
                {
                    lblMessage.Text = "Vui lòng nhập tên đăng nhập!";
                    txtUsername.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtPassword.Text))
                {
                    lblMessage.Text = "Vui lòng nhập mật khẩu!";
                    txtPassword.Focus();
                    return;
                }

                // Disable login button during authentication
                btnLogin.Enabled = false;
                btnLogin.Text = "Đang xử lý...";
                lblMessage.Text = "";

                // Test database connection first
                using var testContext = new QuanNetDbContext();
                await testContext.Database.EnsureCreatedAsync();

                // Initialize database if needed
                await InitializeDatabaseAsync();

                var user = await _authService.LoginAsync(txtUsername.Text.Trim(), txtPassword.Text);

                if (user != null)
                {
                    lblMessage.ForeColor = Color.Green;
                    lblMessage.Text = $"Đăng nhập thành công! Chào {user.HoTen}";

                    // Open main form
                    this.Hide();
                    var mainForm = new FormMain(_authService);
                    mainForm.FormClosed += (s, args) => this.Close();
                    mainForm.Show();
                }
                else
                {
                    lblMessage.ForeColor = Color.Red;
                    lblMessage.Text = "Tên đăng nhập hoặc mật khẩu không đúng!";
                    txtPassword.Clear();
                    txtUsername.Focus();
                }
            }
            catch (Exception ex)
            {
                lblMessage.ForeColor = Color.Red;
                lblMessage.Text = $"Lỗi: {ex.Message}";
            }
            finally
            {
                btnLogin.Enabled = true;
                btnLogin.Text = "Đăng nhập";
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}