using Microsoft.Extensions.DependencyInjection;
using QuanLyQuanNet.Data.Repositories;
using QuanLyQuanNet.Models;
using QuanLyQuanNet.Services;
using QuanLyQuanNet.Utils;

namespace QuanLyQuanNet.Forms
{
    public partial class FormQuanLyKhachHang : Form
    {
        private readonly INguoiDungRepository _nguoiDungRepository;
        private readonly IAuthenticationService _authService;
        private readonly SemaphoreSlim _dbSemaphore = new SemaphoreSlim(1, 1);
        
        // UI Controls
        private DataGridView? dgvKhachHang;
        private Panel? pnlThongTin;
        private Panel? pnlControls;
        
        // Input Controls
        private TextBox? txtHoTen;
        private TextBox? txtUsername;
        private TextBox? txtPassword;
        private TextBox? txtSoDienThoai;
        private TextBox? txtDiaChi;
        private TextBox? txtSoDu;
        private DateTimePicker? dtpNgayDangKy;
        
        // Button Controls
        private Button? btnThem;
        private Button? btnSua;
        private Button? btnXoa;
        private Button? btnLamMoi;
        private Button? btnTimKiem;
        private Button? btnNapTien;
        private TextBox? txtTimKiem;
        
        // Labels
        private Label? lblTongKhachHang;
        private Label? lblTongSoDu;
        
        private NguoiDung? _selectedKhachHang;
        private bool _isEditing = false;

        public FormQuanLyKhachHang()
        {
            var serviceProvider = Program.ServiceProvider ?? throw new InvalidOperationException("ServiceProvider chưa được khởi tạo");
            _nguoiDungRepository = serviceProvider.GetRequiredService<INguoiDungRepository>();
            _authService = serviceProvider.GetRequiredService<IAuthenticationService>();
            
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Quản lý Khách hàng";
            this.Size = new Size(1400, 800);
            this.StartPosition = FormStartPosition.CenterScreen;

            SetupLayout();
            SetupDataGridView();
            SetupThongTinPanel();
            SetupControlsPanel();
            
            this.Load += FormQuanLyKhachHang_Load;
        }

        private void SetupLayout()
        {
            // Panel controls (top)
            pnlControls = new Panel
            {
                Height = 80,
                Dock = DockStyle.Top,
                BackColor = Color.LightGray
            };

            // Panel thông tin (right)
            pnlThongTin = new Panel
            {
                Width = 400,
                Dock = DockStyle.Right,
                BackColor = Color.WhiteSmoke
            };

            // DataGridView (fill remaining space)
            dgvKhachHang = new DataGridView
            {
                Dock = DockStyle.Fill,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };
            dgvKhachHang.SelectionChanged += DgvKhachHang_SelectionChanged;

            // Add to form
            this.Controls.Add(dgvKhachHang);
            this.Controls.Add(pnlThongTin);
            this.Controls.Add(pnlControls);
        }

        private void SetupDataGridView()
        {
            if (dgvKhachHang == null) return;
            
            dgvKhachHang.Columns.Clear();

            dgvKhachHang.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "UserID",
                HeaderText = "ID",
                Width = 50,
                Visible = false
            });

            dgvKhachHang.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "HoTen",
                HeaderText = "Họ tên",
                Width = 150
            });

            dgvKhachHang.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Username",
                HeaderText = "Tên đăng nhập",
                Width = 120
            });

            dgvKhachHang.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "SoDienThoai",
                HeaderText = "Số điện thoại",
                Width = 120
            });

            dgvKhachHang.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "DiaChi",
                HeaderText = "Địa chỉ",
                Width = 200
            });

            dgvKhachHang.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "SoDu",
                HeaderText = "Số dư",
                Width = 100
            });

            dgvKhachHang.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "NgayDangKy",
                HeaderText = "Ngày đăng ký",
                Width = 120
            });
        }

        private void SetupThongTinPanel()
        {
            if (pnlThongTin == null) return;

            var lblTitle = new Label
            {
                Text = "THÔNG TIN KHÁCH HÀNG",
                Font = new Font("Arial", 12, FontStyle.Bold),
                Location = new Point(10, 10),
                Size = new Size(380, 25),
                TextAlign = ContentAlignment.MiddleCenter,
                BackColor = Color.Navy,
                ForeColor = Color.White
            };

            // Họ tên
            var lblHoTen = new Label { Text = "Họ tên:", Location = new Point(10, 50), Size = new Size(80, 23) };
            txtHoTen = new TextBox { Location = new Point(100, 47), Size = new Size(280, 23) };

            // Username
            var lblUsername = new Label { Text = "Tên đăng nhập:", Location = new Point(10, 80), Size = new Size(85, 23) };
            txtUsername = new TextBox { Location = new Point(100, 77), Size = new Size(280, 23) };

            // Password
            var lblPassword = new Label { Text = "Mật khẩu:", Location = new Point(10, 110), Size = new Size(80, 23) };
            txtPassword = new TextBox { Location = new Point(100, 107), Size = new Size(280, 23), UseSystemPasswordChar = true };

            // Số điện thoại
            var lblSoDienThoai = new Label { Text = "Số điện thoại:", Location = new Point(10, 140), Size = new Size(85, 23) };
            txtSoDienThoai = new TextBox { Location = new Point(100, 137), Size = new Size(280, 23) };

            // Địa chỉ
            var lblDiaChi = new Label { Text = "Địa chỉ:", Location = new Point(10, 170), Size = new Size(80, 23) };
            txtDiaChi = new TextBox { Location = new Point(100, 167), Size = new Size(280, 50), Multiline = true };

            // Số dư
            var lblSoDu = new Label { Text = "Số dư:", Location = new Point(10, 230), Size = new Size(80, 23) };
            txtSoDu = new TextBox { Location = new Point(100, 227), Size = new Size(280, 23) };

            // Ngày đăng ký
            var lblNgayDangKy = new Label { Text = "Ngày đăng ký:", Location = new Point(10, 260), Size = new Size(85, 23) };
            dtpNgayDangKy = new DateTimePicker { Location = new Point(100, 257), Size = new Size(280, 23) };

            // Buttons
            btnThem = new Button
            {
                Text = "Thêm",
                Location = new Point(10, 300),
                Size = new Size(80, 35),
                BackColor = Color.LightGreen
            };
            btnThem.Click += BtnThem_Click;

            btnSua = new Button
            {
                Text = "Sửa",
                Location = new Point(100, 300),
                Size = new Size(80, 35),
                BackColor = Color.LightBlue
            };
            btnSua.Click += BtnSua_Click;

            btnXoa = new Button
            {
                Text = "Xóa",
                Location = new Point(190, 300),
                Size = new Size(80, 35),
                BackColor = Color.LightCoral
            };
            btnXoa.Click += BtnXoa_Click;

            btnNapTien = new Button
            {
                Text = "Nạp tiền",
                Location = new Point(280, 300),
                Size = new Size(80, 35),
                BackColor = Color.Gold
            };
            btnNapTien.Click += BtnNapTien_Click;

            var btnHuy = new Button
            {
                Text = "Hủy",
                Location = new Point(190, 350),
                Size = new Size(80, 35)
            };
            btnHuy.Click += BtnHuy_Click;

            var btnLuu = new Button
            {
                Text = "Lưu",
                Location = new Point(280, 350),
                Size = new Size(80, 35),
                BackColor = Color.LightGreen
            };
            btnLuu.Click += BtnLuu_Click;

            // Add controls to panel
            pnlThongTin.Controls.AddRange(new Control[] {
                lblTitle, lblHoTen, txtHoTen, lblUsername, txtUsername, lblPassword, txtPassword,
                lblSoDienThoai, txtSoDienThoai, lblDiaChi, txtDiaChi, lblSoDu, txtSoDu,
                lblNgayDangKy, dtpNgayDangKy, btnThem, btnSua, btnXoa, btnNapTien, btnHuy, btnLuu
            });
        }

        private void SetupControlsPanel()
        {
            if (pnlControls == null) return;

            // Search
            var lblTimKiem = new Label
            {
                Text = "Tìm kiếm:",
                Location = new Point(10, 25),
                Size = new Size(70, 23)
            };

            txtTimKiem = new TextBox
            {
                Location = new Point(90, 22),
                Size = new Size(200, 23)
            };

            btnTimKiem = new Button
            {
                Text = "Tìm",
                Location = new Point(300, 20),
                Size = new Size(60, 27)
            };
            btnTimKiem.Click += BtnTimKiem_Click;

            btnLamMoi = new Button
            {
                Text = "Làm mới",
                Location = new Point(370, 20),
                Size = new Size(80, 27)
            };
            btnLamMoi.Click += BtnLamMoi_Click;

            // Statistics
            lblTongKhachHang = new Label
            {
                Text = "Tổng khách hàng: 0",
                Location = new Point(500, 15),
                Size = new Size(150, 23),
                Font = new Font("Arial", 9, FontStyle.Bold)
            };

            lblTongSoDu = new Label
            {
                Text = "Tổng số dư: 0 đ",
                Location = new Point(500, 40),
                Size = new Size(150, 23),
                Font = new Font("Arial", 9, FontStyle.Bold),
                ForeColor = Color.Green
            };

            pnlControls.Controls.AddRange(new Control[] {
                lblTimKiem, txtTimKiem, btnTimKiem, btnLamMoi, lblTongKhachHang, lblTongSoDu
            });
        }

        private async void FormQuanLyKhachHang_Load(object? sender, EventArgs e)
        {
            await LoadDataAsync();
            ClearInputs();
            UpdateButtonStates();
        }

        private async Task LoadDataAsync()
        {
            await _dbSemaphore.WaitAsync();
            try
            {
                if (dgvKhachHang == null || lblTongKhachHang == null || lblTongSoDu == null) return;

                var khachHangs = await _nguoiDungRepository.GetAllAsync();
                dgvKhachHang.Rows.Clear();

                decimal tongSoDu = 0;
                int tongKhachHang = 0;

                foreach (var kh in khachHangs)
                {
                    tongKhachHang++;
                    tongSoDu += kh.SoDu;

                    dgvKhachHang.Rows.Add(
                        kh.UserID,
                        kh.HoTen,
                        kh.Username,
                        kh.SoDienThoai ?? "",
                        kh.DiaChi ?? "",
                        kh.SoDu.ToString("N0") + " đ",
                        kh.NgayDangKy.ToString("dd/MM/yyyy")
                    );
                }

                lblTongKhachHang.Text = $"Tổng khách hàng: {tongKhachHang}";
                lblTongSoDu.Text = $"Tổng số dư: {tongSoDu:N0} đ";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải thông tin khách hàng: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _dbSemaphore.Release();
            }
        }

        private void DgvKhachHang_SelectionChanged(object? sender, EventArgs e)
        {
            if (dgvKhachHang?.SelectedRows.Count > 0)
            {
                var row = dgvKhachHang.SelectedRows[0];
                var userId = Convert.ToInt32(row.Cells["UserID"].Value);
                LoadSelectedKhachHang(userId);
            }
        }

        private async void LoadSelectedKhachHang(int userId)
        {
            await _dbSemaphore.WaitAsync();
            try
            {
                _selectedKhachHang = await _nguoiDungRepository.GetByIdAsync(userId);
                if (_selectedKhachHang != null && !_isEditing)
                {
                    DisplayKhachHangInfo(_selectedKhachHang);
                }
                UpdateButtonStates();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải thông tin khách hàng: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _dbSemaphore.Release();
            }
        }

        private void DisplayKhachHangInfo(NguoiDung khachHang)
        {
            if (txtHoTen == null || txtUsername == null || txtSoDienThoai == null || 
                txtDiaChi == null || txtSoDu == null || dtpNgayDangKy == null) return;

            txtHoTen.Text = khachHang.HoTen;
            txtUsername.Text = khachHang.Username;
            txtSoDienThoai.Text = khachHang.SoDienThoai ?? "";
            txtDiaChi.Text = khachHang.DiaChi ?? "";
            txtSoDu.Text = khachHang.SoDu.ToString();
            dtpNgayDangKy.Value = khachHang.NgayDangKy;
        }

        private void ClearInputs()
        {
            if (txtHoTen == null || txtUsername == null || txtPassword == null || 
                txtSoDienThoai == null || txtDiaChi == null || txtSoDu == null || 
                dtpNgayDangKy == null) return;

            txtHoTen.Text = "";
            txtUsername.Text = "";
            txtPassword.Text = "";
            txtSoDienThoai.Text = "";
            txtDiaChi.Text = "";
            txtSoDu.Text = "0";
            dtpNgayDangKy.Value = DateTime.Now;
            
            _selectedKhachHang = null;
            _isEditing = false;
        }

        private void UpdateButtonStates()
        {
            if (btnThem == null || btnSua == null || btnXoa == null || btnNapTien == null) return;

            bool hasSelection = _selectedKhachHang != null;
            
            btnSua.Enabled = hasSelection && !_isEditing;
            btnXoa.Enabled = hasSelection && !_isEditing;
            btnNapTien.Enabled = hasSelection && !_isEditing;
            btnThem.Enabled = !_isEditing;
        }

        private void BtnThem_Click(object? sender, EventArgs e)
        {
            _isEditing = true;
            ClearInputs();
            UpdateButtonStates();
            txtHoTen?.Focus();
        }

        private void BtnSua_Click(object? sender, EventArgs e)
        {
            if (_selectedKhachHang == null)
            {
                MessageBox.Show("Vui lòng chọn khách hàng cần sửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            _isEditing = true;
            UpdateButtonStates();
            txtHoTen?.Focus();
        }

        private async void BtnXoa_Click(object? sender, EventArgs e)
        {
            if (_selectedKhachHang == null)
            {
                MessageBox.Show("Vui lòng chọn khách hàng cần xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var result = MessageBox.Show($"Bạn có chắc muốn xóa khách hàng '{_selectedKhachHang.HoTen}'?", 
                "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                await _dbSemaphore.WaitAsync();
                try
                {
                    await _nguoiDungRepository.DeleteAsync(_selectedKhachHang.UserID);
                    MessageBox.Show("Xóa khách hàng thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await LoadDataAsync();
                    ClearInputs();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi xóa khách hàng: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    _dbSemaphore.Release();
                }
            }
        }

        private async void BtnLuu_Click(object? sender, EventArgs e)
        {
            if (!ValidateInputs()) return;

            await _dbSemaphore.WaitAsync();
            try
            {
                if (_selectedKhachHang == null) // Thêm mới
                {
                    var newKhachHang = new NguoiDung
                    {
                        HoTen = txtHoTen!.Text.Trim(),
                        Username = txtUsername!.Text.Trim(),
                        PasswordHash = PasswordHelper.HashPassword(txtPassword!.Text),
                        SoDienThoai = txtSoDienThoai!.Text.Trim(),
                        DiaChi = txtDiaChi!.Text.Trim(),
                        SoDu = decimal.Parse(txtSoDu!.Text),
                        NgayDangKy = dtpNgayDangKy!.Value
                    };

                    await _nguoiDungRepository.AddAsync(newKhachHang);
                    MessageBox.Show("Thêm khách hàng thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else // Cập nhật
                {
                    _selectedKhachHang.HoTen = txtHoTen!.Text.Trim();
                    _selectedKhachHang.Username = txtUsername!.Text.Trim();
                    if (!string.IsNullOrEmpty(txtPassword!.Text))
                    {
                        _selectedKhachHang.PasswordHash = PasswordHelper.HashPassword(txtPassword.Text);
                    }
                    _selectedKhachHang.SoDienThoai = txtSoDienThoai!.Text.Trim();
                    _selectedKhachHang.DiaChi = txtDiaChi!.Text.Trim();
                    _selectedKhachHang.SoDu = decimal.Parse(txtSoDu!.Text);

                    await _nguoiDungRepository.UpdateAsync(_selectedKhachHang);
                    MessageBox.Show("Cập nhật khách hàng thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                await LoadDataAsync();
                ClearInputs();
                UpdateButtonStates();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi lưu khách hàng: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                _dbSemaphore.Release();
            }
        }

        private void BtnHuy_Click(object? sender, EventArgs e)
        {
            ClearInputs();
            UpdateButtonStates();
        }

        private async void BtnNapTien_Click(object? sender, EventArgs e)
        {
            if (_selectedKhachHang == null)
            {
                MessageBox.Show("Vui lòng chọn khách hàng cần nạp tiền.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Create a simple input dialog
            var inputForm = new Form()
            {
                Width = 400,
                Height = 200,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = "Nạp tiền",
                StartPosition = FormStartPosition.CenterParent,
                MaximizeBox = false,
                MinimizeBox = false
            };

            var lblPrompt = new Label()
            {
                Left = 20,
                Top = 20,
                Width = 350,
                Height = 40,
                Text = $"Nhập số tiền cần nạp cho khách hàng '{_selectedKhachHang.HoTen}':"
            };

            var txtInput = new TextBox()
            {
                Left = 20,
                Top = 70,
                Width = 350,
                Text = "0"
            };

            var btnOK = new Button()
            {
                Text = "OK",
                Left = 200,
                Width = 80,
                Top = 110,
                DialogResult = DialogResult.OK
            };

            var btnCancel = new Button()
            {
                Text = "Hủy",
                Left = 290,
                Width = 80,
                Top = 110,
                DialogResult = DialogResult.Cancel
            };

            inputForm.Controls.Add(lblPrompt);
            inputForm.Controls.Add(txtInput);
            inputForm.Controls.Add(btnOK);
            inputForm.Controls.Add(btnCancel);
            inputForm.AcceptButton = btnOK;
            inputForm.CancelButton = btnCancel;

            if (inputForm.ShowDialog() == DialogResult.OK)
            {
                if (decimal.TryParse(txtInput.Text, out decimal soTien) && soTien > 0)
                {
                    try
                    {
                        _selectedKhachHang.SoDu += soTien;
                        await _nguoiDungRepository.UpdateAsync(_selectedKhachHang);
                        
                        MessageBox.Show($"Nạp tiền thành công! Số dư hiện tại: {_selectedKhachHang.SoDu:N0} đ", 
                            "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        
                        await LoadDataAsync();
                        DisplayKhachHangInfo(_selectedKhachHang);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Lỗi nạp tiền: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Số tiền không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            inputForm.Dispose();
        }

        private async void BtnTimKiem_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTimKiem?.Text))
            {
                await LoadDataAsync();
                return;
            }

            try
            {
                if (dgvKhachHang == null) return;

                var keyword = txtTimKiem.Text.Trim().ToLower();
                var allKhachHangs = await _nguoiDungRepository.GetAllAsync();
                var filteredKhachHangs = allKhachHangs.Where(kh => 
                    kh.HoTen.ToLower().Contains(keyword) ||
                    kh.Username.ToLower().Contains(keyword) ||
                    (kh.SoDienThoai?.ToLower().Contains(keyword) ?? false) ||
                    (kh.DiaChi?.ToLower().Contains(keyword) ?? false)
                ).ToList();

                dgvKhachHang.Rows.Clear();
                foreach (var kh in filteredKhachHangs)
                {
                    dgvKhachHang.Rows.Add(
                        kh.UserID,
                        kh.HoTen,
                        kh.Username,
                        kh.SoDienThoai ?? "",
                        kh.DiaChi ?? "",
                        kh.SoDu.ToString("N0") + " đ",
                        kh.NgayDangKy.ToString("dd/MM/yyyy")
                    );
                }

                if (lblTongKhachHang != null)
                    lblTongKhachHang.Text = $"Kết quả tìm kiếm: {filteredKhachHangs.Count}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tìm kiếm: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void BtnLamMoi_Click(object? sender, EventArgs e)
        {
            if (txtTimKiem != null)
                txtTimKiem.Text = "";
            await LoadDataAsync();
            ClearInputs();
        }

        private bool ValidateInputs()
        {
            if (string.IsNullOrEmpty(txtHoTen?.Text))
            {
                MessageBox.Show("Vui lòng nhập họ tên!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtHoTen?.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtUsername?.Text))
            {
                MessageBox.Show("Vui lòng nhập tên đăng nhập!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtUsername?.Focus();
                return false;
            }

            if (_selectedKhachHang == null && string.IsNullOrEmpty(txtPassword?.Text))
            {
                MessageBox.Show("Vui lòng nhập mật khẩu!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPassword?.Focus();
                return false;
            }

            if (!decimal.TryParse(txtSoDu?.Text, out decimal soDu) || soDu < 0)
            {
                MessageBox.Show("Số dư không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtSoDu?.Focus();
                return false;
            }

            return true;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _dbSemaphore?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}