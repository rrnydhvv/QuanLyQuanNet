using QuanLyQuanNet.Models;
using QuanLyQuanNet.Services;

namespace QuanLyQuanNet.Forms
{
    public partial class FormThanhToan : Form
    {
        private readonly MayTram _mayTram;
        private readonly List<ChiTietHoaDon> _chiTietHoaDon;
        private readonly IHoaDonService _hoaDonService;
        private readonly IAuthenticationService _authService;
        
        private HoaDon? _hoaDon;
        private decimal _tongTien;
        private decimal _tienMay;
        private decimal _tienDichVu;

        // Controls
        private Label lblThongTinKhach = null!;
        private Label lblThoiGianSuDung = null!;
        private Label lblTienMay = null!;
        private Label lblTienDichVu = null!;
        private Label lblTongTien = null!;
        private DataGridView dgvChiTiet = null!;
        private ComboBox cmbPhuongThuc = null!;
        private TextBox txtTienKhachDua = null!;
        private Label lblTienThua = null!;
        private Button btnThanhToan = null!;
        private Button btnHuy = null!;

        public FormThanhToan(MayTram mayTram, List<ChiTietHoaDon> chiTietHoaDon, 
                           IHoaDonService hoaDonService, IAuthenticationService authService)
        {
            _mayTram = mayTram;
            _chiTietHoaDon = chiTietHoaDon;
            _hoaDonService = hoaDonService;
            _authService = authService;
            
            InitializeComponent();
            SetupUI();
            LoadThongTin();
        }

        private void InitializeComponent()
        {
            this.Text = "Thanh toán";
            this.Size = new Size(600, 700);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
        }

        private void SetupUI()
        {
            var panel = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 12,
                Padding = new Padding(20)
            };

            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 150));
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));

            int row = 0;

            // Thông tin khách hàng
            panel.Controls.Add(new Label { Text = "Khách hàng:", Font = new Font("Segoe UI", 10, FontStyle.Bold) }, 0, row);
            lblThongTinKhach = new Label { Font = new Font("Segoe UI", 10), ForeColor = Color.Blue };
            panel.Controls.Add(lblThongTinKhach, 1, row++);

            // Thời gian sử dụng
            panel.Controls.Add(new Label { Text = "Thời gian:", Font = new Font("Segoe UI", 10, FontStyle.Bold) }, 0, row);
            lblThoiGianSuDung = new Label { Font = new Font("Segoe UI", 10) };
            panel.Controls.Add(lblThoiGianSuDung, 1, row++);

            // Chi tiết dịch vụ
            panel.Controls.Add(new Label { Text = "Chi tiết:", Font = new Font("Segoe UI", 10, FontStyle.Bold) }, 0, row);
            dgvChiTiet = new DataGridView
            {
                Height = 200,
                ReadOnly = true,
                AllowUserToAddRows = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect
            };
            panel.Controls.Add(dgvChiTiet, 1, row++);

            // Tiền máy
            panel.Controls.Add(new Label { Text = "Tiền máy:", Font = new Font("Segoe UI", 10, FontStyle.Bold) }, 0, row);
            lblTienMay = new Label { Font = new Font("Segoe UI", 12, FontStyle.Bold), ForeColor = Color.Green };
            panel.Controls.Add(lblTienMay, 1, row++);

            // Tiền dịch vụ
            panel.Controls.Add(new Label { Text = "Tiền dịch vụ:", Font = new Font("Segoe UI", 10, FontStyle.Bold) }, 0, row);
            lblTienDichVu = new Label { Font = new Font("Segoe UI", 12, FontStyle.Bold), ForeColor = Color.Orange };
            panel.Controls.Add(lblTienDichVu, 1, row++);

            // Tổng tiền
            panel.Controls.Add(new Label { Text = "TỔNG TIỀN:", Font = new Font("Segoe UI", 12, FontStyle.Bold) }, 0, row);
            lblTongTien = new Label { Font = new Font("Segoe UI", 16, FontStyle.Bold), ForeColor = Color.Red };
            panel.Controls.Add(lblTongTien, 1, row++);

            // Phương thức thanh toán
            panel.Controls.Add(new Label { Text = "Phương thức:", Font = new Font("Segoe UI", 10, FontStyle.Bold) }, 0, row);
            cmbPhuongThuc = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Items = { "Tiền mặt", "Chuyển khoản", "Thẻ" }
            };
            cmbPhuongThuc.SelectedIndex = 0;
            panel.Controls.Add(cmbPhuongThuc, 1, row++);

            // Tiền khách đưa
            panel.Controls.Add(new Label { Text = "Khách đưa:", Font = new Font("Segoe UI", 10, FontStyle.Bold) }, 0, row);
            txtTienKhachDua = new TextBox
            {
                Font = new Font("Segoe UI", 12),
                TextAlign = HorizontalAlignment.Right
            };
            txtTienKhachDua.TextChanged += TxtTienKhachDua_TextChanged;
            txtTienKhachDua.KeyPress += TxtTienKhachDua_KeyPress;
            panel.Controls.Add(txtTienKhachDua, 1, row++);

            // Tiền thừa
            panel.Controls.Add(new Label { Text = "Tiền thừa:", Font = new Font("Segoe UI", 10, FontStyle.Bold) }, 0, row);
            lblTienThua = new Label { Font = new Font("Segoe UI", 14, FontStyle.Bold), ForeColor = Color.Blue };
            panel.Controls.Add(lblTienThua, 1, row++);

            // Buttons
            var buttonPanel = new FlowLayoutPanel
            {
                FlowDirection = FlowDirection.RightToLeft,
                Height = 50,
                Dock = DockStyle.Fill
            };

            btnHuy = new Button
            {
                Text = "Hủy",
                Size = new Size(100, 35),
                BackColor = Color.Gray,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            btnHuy.Click += BtnHuy_Click;

            btnThanhToan = new Button
            {
                Text = "Thanh toán",
                Size = new Size(120, 35),
                BackColor = Color.Green,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            btnThanhToan.Click += BtnThanhToan_Click;

            buttonPanel.Controls.Add(btnHuy);
            buttonPanel.Controls.Add(btnThanhToan);

            panel.Controls.Add(buttonPanel, 1, row);
            panel.SetColumnSpan(buttonPanel, 2);

            // Set row styles
            for (int i = 0; i < panel.RowCount - 1; i++)
            {
                panel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            }
            panel.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

            this.Controls.Add(panel);
        }

        private void LoadThongTin()
        {
            try
            {
                // Thông tin khách hàng
                lblThongTinKhach.Text = $"{_mayTram.NguoiDungHienTai?.Username ?? "Khách vãng lai"} - {_mayTram.TenMay}";

                // Thời gian sử dụng
                if (_mayTram.ThoiGianBatDau.HasValue)
                {
                    var thoiGian = DateTime.Now - _mayTram.ThoiGianBatDau.Value;
                    lblThoiGianSuDung.Text = $"{(int)thoiGian.TotalHours:D2}:{thoiGian.Minutes:D2}:{thoiGian.Seconds:D2} " +
                                          $"({_mayTram.ThoiGianBatDau.Value:HH:mm dd/MM/yyyy} - {DateTime.Now:HH:mm dd/MM/yyyy})";

                    // Tính tiền máy
                    var soGio = (decimal)thoiGian.TotalHours;
                    soGio = Math.Ceiling(soGio * 4) / 4; // Làm tròn lên 15 phút
                    _tienMay = soGio * _mayTram.GiaTheoGio;
                }

                // Setup chi tiết dịch vụ
                SetupChiTietGrid();

                // Tính tiền dịch vụ
                _tienDichVu = _chiTietHoaDon.Sum(ct => ct.ThanhTien);

                // Tổng tiền
                _tongTien = _tienMay + _tienDichVu;

                // Hiển thị
                lblTienMay.Text = _tienMay.ToString("N0") + " VNĐ";
                lblTienDichVu.Text = _tienDichVu.ToString("N0") + " VNĐ";
                lblTongTien.Text = _tongTien.ToString("N0") + " VNĐ";

                // Set default tiền khách đưa
                txtTienKhachDua.Text = _tongTien.ToString("0");
                CalculateTienThua();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải thông tin: {ex.Message}", "Lỗi", 
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetupChiTietGrid()
        {
            dgvChiTiet.Columns.Clear();
            dgvChiTiet.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "DichVu.TenDichVu",
                HeaderText = "Dịch vụ",
                Width = 150
            });
            dgvChiTiet.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "SoLuong",
                HeaderText = "SL",
                Width = 50
            });
            dgvChiTiet.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "DonGia",
                HeaderText = "Đơn giá",
                Width = 80,
                DefaultCellStyle = { Format = "N0" }
            });
            dgvChiTiet.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "ThanhTien",
                HeaderText = "Thành tiền",
                Width = 100,
                DefaultCellStyle = { Format = "N0" }
            });

            dgvChiTiet.DataSource = _chiTietHoaDon;
        }

        private void TxtTienKhachDua_KeyPress(object? sender, KeyPressEventArgs e)
        {
            // Chỉ cho phép số và phím điều khiển
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void TxtTienKhachDua_TextChanged(object? sender, EventArgs e)
        {
            CalculateTienThua();
        }

        private void CalculateTienThua()
        {
            if (decimal.TryParse(txtTienKhachDua.Text, out decimal tienKhachDua))
            {
                var tienThua = tienKhachDua - _tongTien;
                lblTienThua.Text = tienThua.ToString("N0") + " VNĐ";
                lblTienThua.ForeColor = tienThua >= 0 ? Color.Blue : Color.Red;
                
                btnThanhToan.Enabled = tienKhachDua >= _tongTien;
            }
            else
            {
                lblTienThua.Text = "0 VNĐ";
                btnThanhToan.Enabled = false;
            }
        }

        private async void BtnThanhToan_Click(object? sender, EventArgs e)
        {
            try
            {
                if (!decimal.TryParse(txtTienKhachDua.Text, out decimal tienKhachDua))
                {
                    MessageBox.Show("Vui lòng nhập số tiền hợp lệ!", "Thông báo",
                                   MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (tienKhachDua < _tongTien)
                {
                    MessageBox.Show("Tiền khách đưa không đủ!", "Thông báo",
                                   MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                btnThanhToan.Enabled = false;
                btnThanhToan.Text = "Đang xử lý...";

                // Tạo hóa đơn nếu chưa có
                if (_hoaDon == null)
                {
                    var currentUser = _authService.CurrentUser;
                    if (currentUser == null)
                    {
                        MessageBox.Show("Không thể xác định nhân viên hiện tại!", "Lỗi",
                                       MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    _hoaDon = await _hoaDonService.TaoHoaDonAsync(
                        _mayTram.NguoiDungHienTai?.UserID ?? 0,
                        _mayTram.MayID,
                        currentUser.NhanVienID
                    );

                    // Thêm dịch vụ vào hóa đơn
                    foreach (var chiTiet in _chiTietHoaDon)
                    {
                        await _hoaDonService.ThemDichVuAsync(_hoaDon.HoaDonID, chiTiet.DichVuID, chiTiet.SoLuong);
                    }
                }

                // Thanh toán
                var success = await _hoaDonService.ThanhToanAsync(_hoaDon.HoaDonID, tienKhachDua, cmbPhuongThuc.Text);

                if (success)
                {
                    MessageBox.Show($"Thanh toán thành công!\n" +
                                   $"Tổng tiền: {_tongTien:N0} VNĐ\n" +
                                   $"Khách đưa: {tienKhachDua:N0} VNĐ\n" +
                                   $"Tiền thừa: {tienKhachDua - _tongTien:N0} VNĐ",
                                   "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Có lỗi xảy ra khi thanh toán!", "Lỗi",
                                   MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thanh toán: {ex.Message}", "Lỗi",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnThanhToan.Enabled = true;
                btnThanhToan.Text = "Thanh toán";
            }
        }

        private void BtnHuy_Click(object? sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}