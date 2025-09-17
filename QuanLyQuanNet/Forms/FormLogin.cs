using QuanLyQuanNet.Data;
using QuanLyQuanNet.Data.Repositories;
using QuanLyQuanNet.Services;
using QuanLyQuanNet.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

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
                
                // Get services from DI container
                Console.WriteLine("Getting services from DI container...");
                _authService = Program.ServiceProvider?.GetRequiredService<IAuthenticationService>()
                    ?? throw new InvalidOperationException("AuthenticationService not found in DI container");
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

                var user = await _authService.LoginAsync(txtUsername.Text.Trim(), txtPassword.Text);

                if (user != null)
                {
                    lblMessage.ForeColor = Color.Green;
                    lblMessage.Text = $"Đăng nhập thành công! Chào {user.HoTen}";

                    // Open main form
                    this.Hide();
                    var mainForm = new FormMain(_authService, Program.ServiceProvider!);
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