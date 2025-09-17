using Microsoft.Extensions.DependencyInjection;
using QuanLyQuanNet.Models;
using QuanLyQuanNet.Services;

namespace QuanLyQuanNet.Forms
{
    public partial class FormMain : Form
    {
        private readonly IAuthenticationService _authService;
        private readonly IServiceProvider _serviceProvider;

        public FormMain(IAuthenticationService authService, IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _authService = authService;
            _serviceProvider = serviceProvider;
            
            InitializeForm();
            SetupPermissions();
        }

        private void InitializeForm()
        {
            if (_authService.CurrentUser != null)
            {
                lblCurrentUser.Text = $"Người dùng: {_authService.CurrentUser.HoTen} ({_authService.CurrentUser.ChucVu})";
                lblWelcome.Text = $"CHÀO MỪNG {_authService.CurrentUser.HoTen.ToUpper()} ĐÃ TRỞ LẠI HỆ THỐNG QUẢN LÝ QUÁN NET";
            }
        }

        private void SetupPermissions()
        {
            if (_authService.CurrentUser == null)
            {
                this.Close();
                return;
            }

            var user = _authService.CurrentUser;
            
            // Ẩn/hiện các menu và button dựa trên quyền
            if (user.ChucVu != ChucVu.QuanTriVien && user.ChucVu != ChucVu.QuanLy)
            {
                // Nhân viên chỉ có quyền cơ bản
                hệThốngToolStripMenuItem.Visible = false;
                máyTrạmToolStripMenuItem.Visible = false;
                kháchHàngToolStripMenuItem.Visible = false;
                dịchVụToolStripMenuItem.Visible = false;
                nhânViênToolStripMenuItem.Visible = false;
                
                btnQuanLyMay.Visible = false;
                btnQuanLyKhach.Visible = false;
                // btnQuanLyDichVu.Visible = false;
                // btnQuanLyNhanVien.Visible = false;
            }

            // Quản trị viên có đầy đủ quyền
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblCurrentTime.Text = $"Thời gian: {DateTime.Now:dd/MM/yyyy HH:mm:ss}";
        }

        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Bạn có chắc chắn muốn đăng xuất?", "Xác nhận", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            
            if (result == DialogResult.Yes)
            {
                _authService.Logout();
                this.Hide();
                var loginForm = new FormLogin();
                loginForm.Show();
                this.Close();
            }
        }

        private void thoátToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Bạn có chắc chắn muốn thoát ứng dụng?", "Xác nhận", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void máyTrạmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFormQuanLyMay();
        }

        private void btnQuanLyMay_Click(object sender, EventArgs e)
        {
            OpenFormQuanLyMay();
        }

        private void OpenFormQuanLyMay()
        {
            try
            {
                var quanLyMayService = _serviceProvider.GetRequiredService<IQuanLyMayService>();
                var form = new FormQuanLyMayTram();
                form.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi mở form quản lý máy: {ex.Message}", "Lỗi", 
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void kháchHàngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFormQuanLyKhach();
        }

        private void btnQuanLyKhach_Click(object sender, EventArgs e)
        {
            OpenFormQuanLyKhach();
        }

        private void OpenFormQuanLyKhach()
        {
            try
            {
                var form = new FormQuanLyKhachHang();
                form.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi mở form quản lý khách hàng: {ex.Message}", "Lỗi", 
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dịchVụToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFormQuanLyDichVu();
        }

        private void btnQuanLyDichVu_Click(object sender, EventArgs e)
        {
            OpenFormQuanLyDichVu();
        }

        private void OpenFormQuanLyDichVu()
        {
            try
            {
                var dichVuService = _serviceProvider.GetRequiredService<IDichVuService>();
                var form = new FormQuanLyDichVu(dichVuService);
                form.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi mở form quản lý dịch vụ: {ex.Message}", "Lỗi", 
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void nhânViênToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // TODO: Implement FormQuanLyNhanVien
            MessageBox.Show("Chức năng quản lý nhân viên đang được phát triển!", "Thông báo", 
                           MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnQuanLyNhanVien_Click(object sender, EventArgs e)
        {
            // TODO: Implement FormQuanLyNhanVien
            MessageBox.Show("Chức năng quản lý nhân viên đang được phát triển!", "Thông báo", 
                           MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void thốngKêToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // TODO: Implement FormThongKe
            MessageBox.Show("Chức năng thống kê đang được phát triển!", "Thông báo", 
                           MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnThongKe_Click(object sender, EventArgs e)
        {
            // TODO: Implement FormThongKe
            MessageBox.Show("Chức năng thống kê đang được phát triển!", "Thông báo", 
                           MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void tạoHóaĐơnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFormTaoHoaDon();
        }

        private void btnTaoHoaDon_Click(object sender, EventArgs e)
        {
            OpenFormTaoHoaDon();
        }

        private void OpenFormTaoHoaDon()
        {
            try
            {
                var hoaDonService = _serviceProvider.GetRequiredService<IHoaDonService>();
                var dichVuService = _serviceProvider.GetRequiredService<IDichVuService>();
                var quanLyMayService = _serviceProvider.GetRequiredService<IQuanLyMayService>();
                
                var form = new FormHoaDon(_authService, hoaDonService, dichVuService, quanLyMayService);
                form.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi mở form hóa đơn: {ex.Message}", "Lỗi", 
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void thốngKêDaoanhThuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // TODO: Implement FormThongKe
            MessageBox.Show("Chức năng thống kê doanh thu đang được phát triển!", "Thông báo", 
                           MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}