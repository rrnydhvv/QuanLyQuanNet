using QuanLyQuanNet.Models;
using QuanLyQuanNet.Services;

namespace QuanLyQuanNet.Forms
{
    public partial class FormMain : Form
    {
        private readonly IAuthenticationService _authService;

        public FormMain(IAuthenticationService authService)
        {
            InitializeComponent();
            _authService = authService;
            
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
                return;

            // Phân quyền theo chức vụ
            var chucVu = _authService.CurrentUser.ChucVu;

            // Nhân viên chỉ có quyền cơ bản
            if (chucVu == ChucVu.NhanVien)
            {
                nhânViênToolStripMenuItem.Enabled = false;
                báoCáoToolStripMenuItem.Enabled = false;
                btnThongKe.Enabled = false;
            }

            // Quản lý có đầy đủ quyền trừ quản lý nhân viên
            if (chucVu == ChucVu.QuanLy)
            {
                nhânViênToolStripMenuItem.Enabled = false;
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
                var form = new FormQuanLyMayTram(_authService);
                form.ShowDialog();
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
                var form = new FormQuanLyKhachHang(_authService);
                form.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi mở form quản lý khách hàng: {ex.Message}", "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dịchVụToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var form = new FormQuanLyDichVu(_authService);
                form.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi mở form quản lý dịch vụ: {ex.Message}", "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void nhânViênToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!_authService.HasPermission(ChucVu.QuanTriVien))
            {
                MessageBox.Show("Bạn không có quyền truy cập chức năng này!", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var form = new FormQuanLyNhanVien(_authService);
                form.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi mở form quản lý nhân viên: {ex.Message}", "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
                var form = new FormHoaDon(_authService);
                form.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi mở form hóa đơn: {ex.Message}", "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void thốngKêDaoanhThuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFormThongKe();
        }

        private void btnThongKe_Click(object sender, EventArgs e)
        {
            OpenFormThongKe();
        }

        private void OpenFormThongKe()
        {
            if (!_authService.HasPermission(ChucVu.QuanLy))
            {
                MessageBox.Show("Bạn không có quyền truy cập chức năng này!", "Thông báo", 
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var form = new FormThongKe(_authService);
                form.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi mở form thống kê: {ex.Message}", "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}