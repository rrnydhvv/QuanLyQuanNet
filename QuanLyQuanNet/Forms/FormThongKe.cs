using QuanLyQuanNet.Services;
using QuanLyQuanNet.Models;

namespace QuanLyQuanNet.Forms
{
    public partial class FormThongKe : Form
    {
        private readonly IAuthenticationService _authService;
        private readonly IHoaDonService _hoaDonService;
        private readonly IQuanLyMayService _quanLyMayService;
        private readonly IDichVuService _dichVuService;

        // UI Controls
        private DateTimePicker dtpTuNgay = null!, dtpDenNgay = null!;
        private Button btnXemThongKe = null!, btnXuatBaoCao = null!;
        private DataGridView dgvBaoCao = null!;
        private Label lblTongDoanhThu = null!, lblSoHoaDon = null!, lblTrungBinhHoaDon = null!;
        private Label lblMayHoatDong = null!, lblTyLeSuDung = null!, lblDichVuBanChay = null!;
        private TabControl tabControl = null!;

        public FormThongKe(IAuthenticationService authService, IHoaDonService hoaDonService, 
                          IQuanLyMayService quanLyMayService, IDichVuService dichVuService)
        {
            InitializeComponent();
            _authService = authService;
            _hoaDonService = hoaDonService;
            _quanLyMayService = quanLyMayService;
            _dichVuService = dichVuService;
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            
            // Form properties
            this.Size = new Size(1200, 800);
            this.Text = "Thống kê - Báo cáo";
            this.StartPosition = FormStartPosition.CenterParent;

            // Create Tab Control
            tabControl = new TabControl
            {
                Dock = DockStyle.Fill,
                Padding = new Point(10, 10)
            };

            // Tab 1: Tổng quan
            var tabTongQuan = new TabPage("Tổng quan");
            CreateTongQuanTab(tabTongQuan);
            tabControl.TabPages.Add(tabTongQuan);

            // Tab 2: Doanh thu
            var tabDoanhThu = new TabPage("Doanh thu");
            CreateDoanhThuTab(tabDoanhThu);
            tabControl.TabPages.Add(tabDoanhThu);

            // Tab 3: Máy tính
            var tabMayTinh = new TabPage("Máy tính");
            CreateMayTinhTab(tabMayTinh);
            tabControl.TabPages.Add(tabMayTinh);

            // Tab 4: Dịch vụ
            var tabDichVu = new TabPage("Dịch vụ");
            CreateDichVuTab(tabDichVu);
            tabControl.TabPages.Add(tabDichVu);

            this.Controls.Add(tabControl);
            this.ResumeLayout(false);
        }

        private void CreateTongQuanTab(TabPage tab)
        {
            var panel = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 4,
                Padding = new Padding(20)
            };

            // Date range controls
            var pnlDateRange = new Panel { Height = 50 };
            pnlDateRange.Controls.Add(new Label { Text = "Từ ngày:", Location = new Point(0, 15), AutoSize = true });
            dtpTuNgay = new DateTimePicker { Location = new Point(70, 12), Value = DateTime.Today.AddDays(-30) };
            pnlDateRange.Controls.Add(dtpTuNgay);
            
            pnlDateRange.Controls.Add(new Label { Text = "Đến ngày:", Location = new Point(250, 15), AutoSize = true });
            dtpDenNgay = new DateTimePicker { Location = new Point(330, 12), Value = DateTime.Today };
            pnlDateRange.Controls.Add(dtpDenNgay);

            btnXemThongKe = new Button { Text = "Xem thống kê", Location = new Point(510, 12), Size = new Size(120, 25) };
            btnXemThongKe.Click += BtnXemThongKe_Click;
            pnlDateRange.Controls.Add(btnXemThongKe);

            panel.Controls.Add(pnlDateRange, 0, 0);
            panel.SetColumnSpan(pnlDateRange, 2);

            // Statistics cards
            CreateStatCard(panel, "Tổng doanh thu", "0 VNĐ", ref lblTongDoanhThu, 0, 1);
            CreateStatCard(panel, "Số hóa đơn", "0", ref lblSoHoaDon, 1, 1);
            CreateStatCard(panel, "Trung bình/hóa đơn", "0 VNĐ", ref lblTrungBinhHoaDon, 0, 2);
            CreateStatCard(panel, "Máy hoạt động", "0/0", ref lblMayHoatDong, 1, 2);

            // Report grid
            dgvBaoCao = new DataGridView
            {
                Dock = DockStyle.Fill,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                AllowUserToAddRows = false,
                ReadOnly = true
            };
            panel.Controls.Add(dgvBaoCao, 0, 3);
            panel.SetColumnSpan(dgvBaoCao, 2);

            tab.Controls.Add(panel);
        }

        private void CreateStatCard(TableLayoutPanel parent, string title, string value, ref Label valueLabel, int col, int row)
        {
            var card = new Panel
            {
                BorderStyle = BorderStyle.FixedSingle,
                Padding = new Padding(15),
                BackColor = Color.White,
                Dock = DockStyle.Fill
            };

            var titleLabel = new Label
            {
                Text = title,
                Font = new Font("Microsoft Sans Serif", 10F, FontStyle.Bold),
                ForeColor = Color.Gray,
                Dock = DockStyle.Top,
                Height = 25
            };

            valueLabel = new Label
            {
                Text = value,
                Font = new Font("Microsoft Sans Serif", 14F, FontStyle.Bold),
                ForeColor = Color.Blue,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter
            };

            card.Controls.Add(valueLabel);
            card.Controls.Add(titleLabel);
            parent.Controls.Add(card, col, row);
        }

        private void CreateDoanhThuTab(TabPage tab)
        {
            var label = new Label
            {
                Text = "Báo cáo doanh thu chi tiết\n(Đang phát triển)",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold)
            };
            tab.Controls.Add(label);
        }

        private void CreateMayTinhTab(TabPage tab)
        {
            var label = new Label
            {
                Text = "Thống kê sử dụng máy tính\n(Đang phát triển)",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold)
            };
            tab.Controls.Add(label);
        }

        private void CreateDichVuTab(TabPage tab)
        {
            var label = new Label
            {
                Text = "Thống kê dịch vụ bán chạy\n(Đang phát triển)",
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold)
            };
            tab.Controls.Add(label);
        }

        private async void BtnXemThongKe_Click(object? sender, EventArgs e)
        {
            try
            {
                await LoadThongKe();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải thống kê: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task LoadThongKe()
        {
            var tuNgay = dtpTuNgay.Value.Date;
            var denNgay = dtpDenNgay.Value.Date.AddDays(1);

            // Load basic statistics
            var doanhThu = await _hoaDonService.TinhDoanhThuTheoNgayAsync(DateTime.Today);
            var soHoaDon = await _hoaDonService.DemSoHoaDonTheoNgayAsync(DateTime.Today);
            var trungBinh = soHoaDon > 0 ? doanhThu / soHoaDon : 0;

            // Update UI
            lblTongDoanhThu.Text = doanhThu.ToString("N0") + " VNĐ";
            lblSoHoaDon.Text = soHoaDon.ToString();
            lblTrungBinhHoaDon.Text = trungBinh.ToString("N0") + " VNĐ";

            // Load detailed report
            var hoaDons = await _hoaDonService.GetHoaDonTheoNgayAsync(DateTime.Today);
            var reportData = hoaDons.Select(h => new
            {
                HoaDonID = h.HoaDonID,
                KhachHang = h.NguoiDung?.HoTen ?? "N/A",
                MayTinh = h.MayTram?.TenMay ?? "N/A",
                GioBatDau = h.GioBatDau.ToString("HH:mm"),
                GioKetThuc = h.GioKetThuc?.ToString("HH:mm") ?? "Đang sử dụng",
                TongTien = h.TongTien,
                TrangThai = h.DaThanhToan ? "Đã thanh toán" : "Chưa thanh toán"
            }).ToList();

            dgvBaoCao.DataSource = reportData;
        }
    }
}