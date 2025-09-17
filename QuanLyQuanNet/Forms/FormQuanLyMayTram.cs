using Microsoft.Extensions.DependencyInjection;
using QuanLyQuanNet.Data.Repositories;
using QuanLyQuanNet.Models;
using QuanLyQuanNet.Services;

namespace QuanLyQuanNet.Forms
{
    public partial class FormQuanLyMayTram : Form
    {
        private readonly IMayTramRepository _mayTramRepository;
        private readonly INguoiDungRepository _nguoiDungRepository;
        private readonly IQuanLyMayService _quanLyMayService;
        private System.Windows.Forms.Timer? _refreshTimer;
        private DataGridView? dgvMayTram;
        private Panel? pnlControls;
        private Button? btnBatMay;
        private Button? btnTatMay;
        private Button? btnTatCa;
        private Button? btnRefresh;
        private Button? btnKhachHangMoi;
        private ComboBox? cmbNguoiDung;
        private Label? lblTongMay;
        private Label? lblMayTrong;
        private Label? lblMayDangSuDung;
        private MayTram? _selectedMayTram;

        public FormQuanLyMayTram()
        {
            var serviceProvider = Program.ServiceProvider ?? throw new InvalidOperationException("ServiceProvider chưa được khởi tạo");
            _mayTramRepository = serviceProvider.GetRequiredService<IMayTramRepository>();
            _nguoiDungRepository = serviceProvider.GetRequiredService<INguoiDungRepository>();
            _quanLyMayService = serviceProvider.GetRequiredService<IQuanLyMayService>();
            
            InitializeComponent();
            SetupRefreshTimer();
        }

        private void InitializeComponent()
        {
            this.Text = "Quản lý Máy trạm";
            this.Size = new Size(1200, 800);
            this.StartPosition = FormStartPosition.CenterScreen;

            // Panel điều khiển
            pnlControls = new Panel
            {
                Height = 100,
                Dock = DockStyle.Top,
                BackColor = Color.LightGray
            };

            // ComboBox chọn người dùng
            var lblNguoiDung = new Label
            {
                Text = "Khách hàng:",
                Location = new Point(10, 15),
                Size = new Size(80, 23)
            };

            cmbNguoiDung = new ComboBox
            {
                Location = new Point(100, 12),
                Size = new Size(200, 23),
                DropDownStyle = ComboBoxStyle.DropDownList
            };

            // Buttons
            btnBatMay = new Button
            {
                Text = "Bật máy",
                Location = new Point(320, 10),
                Size = new Size(80, 30),
                BackColor = Color.LightGreen
            };
            btnBatMay.Click += BtnBatMay_Click;

            btnTatMay = new Button
            {
                Text = "Tắt máy",
                Location = new Point(410, 10),
                Size = new Size(80, 30),
                BackColor = Color.LightCoral
            };
            btnTatMay.Click += BtnTatMay_Click;

            btnTatCa = new Button
            {
                Text = "Tắt tất cả",
                Location = new Point(500, 10),
                Size = new Size(80, 30),
                BackColor = Color.Red,
                ForeColor = Color.White
            };
            btnTatCa.Click += BtnTatCa_Click;

            btnRefresh = new Button
            {
                Text = "Làm mới",
                Location = new Point(590, 10),
                Size = new Size(80, 30)
            };
            btnRefresh.Click += BtnRefresh_Click;

            btnKhachHangMoi = new Button
            {
                Text = "KH mới",
                Location = new Point(320, 50),
                Size = new Size(80, 30),
                BackColor = Color.LightBlue
            };
            btnKhachHangMoi.Click += BtnKhachHangMoi_Click;

            // Labels thống kê
            lblTongMay = new Label
            {
                Text = "Tổng máy: 0",
                Location = new Point(700, 15),
                Size = new Size(100, 23),
                Font = new Font("Arial", 9, FontStyle.Bold)
            };

            lblMayTrong = new Label
            {
                Text = "Máy trống: 0",
                Location = new Point(800, 15),
                Size = new Size(100, 23),
                Font = new Font("Arial", 9, FontStyle.Bold),
                ForeColor = Color.Green
            };

            lblMayDangSuDung = new Label
            {
                Text = "Đang sử dụng: 0",
                Location = new Point(900, 15),
                Size = new Size(120, 23),
                Font = new Font("Arial", 9, FontStyle.Bold),
                ForeColor = Color.Red
            };

            // DataGridView
            dgvMayTram = new DataGridView
            {
                Dock = DockStyle.Fill,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };
            dgvMayTram.SelectionChanged += DgvMayTram_SelectionChanged;

            // Thêm controls vào panel
            pnlControls.Controls.AddRange(new Control[] {
                lblNguoiDung, cmbNguoiDung, btnBatMay, btnTatMay, btnTatCa,
                btnRefresh, btnKhachHangMoi, lblTongMay, lblMayTrong, lblMayDangSuDung
            });

            // Thêm vào form
            this.Controls.Add(dgvMayTram);
            this.Controls.Add(pnlControls);

            SetupDataGridView();
            this.Load += FormQuanLyMayTram_Load;
        }

        private void SetupDataGridView()
        {
            if (dgvMayTram == null) return;
            
            dgvMayTram.Columns.Clear();

            dgvMayTram.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "MayID",
                HeaderText = "ID",
                Width = 50,
                Visible = false
            });

            dgvMayTram.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "TenMay",
                HeaderText = "Tên máy",
                Width = 100
            });

            dgvMayTram.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "TrangThai",
                HeaderText = "Trạng thái",
                Width = 100
            });

            dgvMayTram.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "NguoiDung",
                HeaderText = "Người dùng",
                Width = 150
            });

            dgvMayTram.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "ThoiGianBatDau",
                HeaderText = "Bắt đầu",
                Width = 120
            });

            dgvMayTram.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "ThoiGianSuDung",
                HeaderText = "Thời gian sử dụng",
                Width = 120
            });

            dgvMayTram.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "TienHienTai",
                HeaderText = "Tiền hiện tại",
                Width = 100
            });

            dgvMayTram.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "GiaTheoGio",
                HeaderText = "Giá/giờ",
                Width = 80
            });
        }

        private void SetupRefreshTimer()
        {
            _refreshTimer = new System.Windows.Forms.Timer();
            _refreshTimer.Interval = 5000; // 5 giây
            _refreshTimer.Tick += async (s, e) => await LoadDataAsync();
            _refreshTimer.Start();
        }

        private async void FormQuanLyMayTram_Load(object? sender, EventArgs e)
        {
            await LoadNguoiDungAsync();
            await LoadDataAsync();
        }

        private async Task LoadNguoiDungAsync()
        {
            try
            {
                if (cmbNguoiDung == null) return;
                
                var nguoiDungs = await _nguoiDungRepository.GetAllAsync();
                cmbNguoiDung.DisplayMember = "Username";
                cmbNguoiDung.ValueMember = "UserID";
                cmbNguoiDung.DataSource = nguoiDungs.ToList();
                cmbNguoiDung.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải danh sách người dùng: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task LoadDataAsync()
        {
            try
            {
                if (dgvMayTram == null || lblTongMay == null || lblMayTrong == null || lblMayDangSuDung == null) return;
                
                var mayTrams = await _mayTramRepository.GetAllWithUsersAsync();
                dgvMayTram.Rows.Clear();

                int tongMay = 0, mayTrong = 0, mayDangSuDung = 0;

                foreach (var may in mayTrams)
                {
                    tongMay++;
                    if (may.TrangThai == TrangThaiMay.Trong) mayTrong++;
                    else if (may.TrangThai == TrangThaiMay.DangSuDung) mayDangSuDung++;

                    var trangThaiText = may.TrangThai switch
                    {
                        TrangThaiMay.Trong => "Trống",
                        TrangThaiMay.DangSuDung => "Đang sử dụng",
                        TrangThaiMay.BaoTri => "Bảo trì",
                        _ => "Không xác định"
                    };

                    var nguoiDungText = may.NguoiDungHienTai?.Username ?? "";
                    var thoiGianBatDauText = may.ThoiGianBatDau?.ToString("HH:mm:ss dd/MM") ?? "";
                    
                    var thoiGianSuDungText = "";
                    var tienHienTaiText = "";

                    if (may.TrangThai == TrangThaiMay.DangSuDung && may.ThoiGianBatDau.HasValue)
                    {
                        var thoiGianSuDung = await _mayTramRepository.GetThoiGianSuDungAsync(may.MayID);
                        thoiGianSuDungText = $"{(int)thoiGianSuDung.TotalHours:D2}:{thoiGianSuDung.Minutes:D2}:{thoiGianSuDung.Seconds:D2}";
                        
                        var tienHienTai = await _mayTramRepository.TinhTienHienTaiAsync(may.MayID);
                        tienHienTaiText = tienHienTai.ToString("N0") + " đ";
                    }

                    var row = dgvMayTram.Rows.Add(
                        may.MayID,
                        may.TenMay,
                        trangThaiText,
                        nguoiDungText,
                        thoiGianBatDauText,
                        thoiGianSuDungText,
                        tienHienTaiText,
                        may.GiaTheoGio.ToString("N0") + " đ"
                    );

                    // Màu sắc theo trạng thái
                    switch (may.TrangThai)
                    {
                        case TrangThaiMay.Trong:
                            dgvMayTram.Rows[row].DefaultCellStyle.BackColor = Color.LightGreen;
                            break;
                        case TrangThaiMay.DangSuDung:
                            dgvMayTram.Rows[row].DefaultCellStyle.BackColor = Color.LightCoral;
                            break;
                        case TrangThaiMay.BaoTri:
                            dgvMayTram.Rows[row].DefaultCellStyle.BackColor = Color.LightGray;
                            break;
                    }
                }

                // Cập nhật thống kê
                lblTongMay.Text = $"Tổng máy: {tongMay}";
                lblMayTrong.Text = $"Máy trống: {mayTrong}";
                lblMayDangSuDung.Text = $"Đang sử dụng: {mayDangSuDung}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DgvMayTram_SelectionChanged(object? sender, EventArgs e)
        {
            if (dgvMayTram?.SelectedRows.Count > 0)
            {
                var row = dgvMayTram.SelectedRows[0];
                var mayId = Convert.ToInt32(row.Cells["MayID"].Value);
                // Lưu thông tin máy được chọn để sử dụng trong các thao tác
                LoadSelectedMayTram(mayId);
            }
        }

        private async void LoadSelectedMayTram(int mayId)
        {
            try
            {
                _selectedMayTram = await _mayTramRepository.GetByIdWithUserAsync(mayId);
                UpdateButtonStates();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải thông tin máy: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateButtonStates()
        {
            if (btnBatMay == null || btnTatMay == null || cmbNguoiDung == null)
                return;
                
            if (_selectedMayTram == null)
            {
                btnBatMay.Enabled = false;
                btnTatMay.Enabled = false;
                return;
            }

            btnBatMay.Enabled = _selectedMayTram.TrangThai == TrangThaiMay.Trong && cmbNguoiDung.SelectedValue != null;
            btnTatMay.Enabled = _selectedMayTram.TrangThai == TrangThaiMay.DangSuDung;
        }

        private async void BtnBatMay_Click(object? sender, EventArgs e)
        {
            if (_selectedMayTram == null || cmbNguoiDung?.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn máy và khách hàng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                var userId = Convert.ToInt32(cmbNguoiDung.SelectedValue);
                var success = await _quanLyMayService.BatMayAsync(_selectedMayTram.MayID, userId);
                
                if (success)
                {
                    MessageBox.Show($"Đã bật máy {_selectedMayTram.TenMay} cho khách hàng {cmbNguoiDung.Text}.", 
                        "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await LoadDataAsync();
                }
                else
                {
                    MessageBox.Show("Không thể bật máy. Vui lòng kiểm tra lại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi bật máy: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void BtnTatMay_Click(object? sender, EventArgs e)
        {
            if (_selectedMayTram == null)
            {
                MessageBox.Show("Vui lòng chọn máy.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                var result = MessageBox.Show($"Bạn có chắc muốn tắt máy {_selectedMayTram.TenMay}?", 
                    "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                
                if (result == DialogResult.Yes)
                {
                    var success = await _quanLyMayService.TatMayAsync(_selectedMayTram.MayID);
                    
                    if (success)
                    {
                        MessageBox.Show($"Đã tắt máy {_selectedMayTram.TenMay}.", 
                            "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        await LoadDataAsync();
                    }
                    else
                    {
                        MessageBox.Show("Không thể tắt máy. Vui lòng kiểm tra lại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tắt máy: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void BtnTatCa_Click(object? sender, EventArgs e)
        {
            try
            {
                var result = MessageBox.Show("Bạn có chắc muốn tắt TẤT CẢ máy đang sử dụng?", 
                    "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                
                if (result == DialogResult.Yes)
                {
                    var mayDangSuDung = await _mayTramRepository.GetMayDangSuDungAsync();
                    int count = 0;
                    
                    foreach (var may in mayDangSuDung)
                    {
                        if (await _quanLyMayService.TatMayAsync(may.MayID))
                            count++;
                    }
                    
                    MessageBox.Show($"Đã tắt {count} máy.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await LoadDataAsync();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tắt tất cả máy: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void BtnRefresh_Click(object? sender, EventArgs e)
        {
            await LoadDataAsync();
        }

        private void BtnKhachHangMoi_Click(object? sender, EventArgs e)
        {
            // Mở form thêm khách hàng mới (sẽ implement sau)
            MessageBox.Show("Tính năng thêm khách hàng mới sẽ được phát triển.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _refreshTimer?.Stop();
                _refreshTimer?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}