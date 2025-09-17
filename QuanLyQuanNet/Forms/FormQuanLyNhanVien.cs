using QuanLyQuanNet.Services;
using QuanLyQuanNet.Models;
using QuanLyQuanNet.Utils;
using QuanLyQuanNet.Data.Repositories;

namespace QuanLyQuanNet.Forms
{
    public partial class FormQuanLyNhanVien : Form
    {
        private readonly IAuthenticationService _authService;
        private readonly INhanVienRepository _nhanVienRepository;
        
        // UI Controls
        private DataGridView dgvNhanVien = null!;
        private TextBox txtHoTen = null!, txtUsername = null!, txtSoDienThoai = null!;
        private ComboBox cmbChucVu = null!;
        private CheckBox chkTrangThai = null!;
        private Button btnThem = null!, btnSua = null!, btnXoa = null!, btnLamMoi = null!;

        private NhanVien? _selectedNhanVien;

        public FormQuanLyNhanVien(IAuthenticationService authService, INhanVienRepository nhanVienRepository)
        {
            InitializeComponent();
            _authService = authService;
            _nhanVienRepository = nhanVienRepository;
            Load += FormQuanLyNhanVien_Load!;
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            
            // Form properties
            this.Size = new Size(1000, 700);
            this.Text = "Quản lý nhân viên";
            this.StartPosition = FormStartPosition.CenterParent;

            // Create main layout
            var mainPanel = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 1,
                Padding = new Padding(10)
            };
            mainPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 60F));
            mainPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));

            // Left panel - Data grid
            CreateDataGridPanel(mainPanel);

            // Right panel - Input form
            CreateInputPanel(mainPanel);

            this.Controls.Add(mainPanel);
            this.ResumeLayout(false);
        }

        private void CreateDataGridPanel(TableLayoutPanel parent)
        {
            var panel = new Panel { Dock = DockStyle.Fill };
            
            var lblTitle = new Label
            {
                Text = "DANH SÁCH NHÂN VIÊN",
                Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold),
                Dock = DockStyle.Top,
                Height = 30,
                TextAlign = ContentAlignment.MiddleCenter
            };

            dgvNhanVien = new DataGridView
            {
                Dock = DockStyle.Fill,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                AllowUserToAddRows = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false
            };
            dgvNhanVien.SelectionChanged += DgvNhanVien_SelectionChanged;

            panel.Controls.Add(dgvNhanVien);
            panel.Controls.Add(lblTitle);
            parent.Controls.Add(panel, 0, 0);
        }

        private void CreateInputPanel(TableLayoutPanel parent)
        {
            var panel = new Panel 
            { 
                Dock = DockStyle.Fill,
                Padding = new Padding(20, 10, 20, 10)
            };

            var y = 10;
            var labelWidth = 100;
            var inputWidth = 200;
            var rowHeight = 35;

            // Title
            var lblTitle = new Label
            {
                Text = "THÔNG TIN NHÂN VIÊN",
                Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold),
                Location = new Point(10, y),
                Size = new Size(300, 25)
            };
            panel.Controls.Add(lblTitle);
            y += 35;

            // Họ tên
            panel.Controls.Add(new Label { Text = "Họ tên:", Location = new Point(10, y + 3), Size = new Size(labelWidth, 20) });
            txtHoTen = new TextBox { Location = new Point(120, y), Size = new Size(inputWidth, 23) };
            panel.Controls.Add(txtHoTen);
            y += rowHeight;

            // Username
            panel.Controls.Add(new Label { Text = "Username:", Location = new Point(10, y + 3), Size = new Size(labelWidth, 20) });
            txtUsername = new TextBox { Location = new Point(120, y), Size = new Size(inputWidth, 23) };
            panel.Controls.Add(txtUsername);
            y += rowHeight;

            // Số điện thoại
            panel.Controls.Add(new Label { Text = "SĐT:", Location = new Point(10, y + 3), Size = new Size(labelWidth, 20) });
            txtSoDienThoai = new TextBox { Location = new Point(120, y), Size = new Size(inputWidth, 23) };
            panel.Controls.Add(txtSoDienThoai);
            y += rowHeight;

            // Chức vụ
            panel.Controls.Add(new Label { Text = "Chức vụ:", Location = new Point(10, y + 3), Size = new Size(labelWidth, 20) });
            cmbChucVu = new ComboBox 
            { 
                Location = new Point(120, y), 
                Size = new Size(inputWidth, 23),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cmbChucVu.Items.AddRange(new[] { "Admin", "Nhân viên", "Quản lý ca" });
            panel.Controls.Add(cmbChucVu);
            y += rowHeight;

            // Trạng thái
            chkTrangThai = new CheckBox 
            { 
                Text = "Đang hoạt động",
                Location = new Point(120, y),
                Size = new Size(150, 20),
                Checked = true
            };
            panel.Controls.Add(chkTrangThai);
            y += 40;

            // Buttons
            var buttonY = y;
            btnThem = new Button { Text = "Thêm", Location = new Point(10, buttonY), Size = new Size(75, 30) };
            btnThem.Click += BtnThem_Click;
            panel.Controls.Add(btnThem);

            btnSua = new Button { Text = "Sửa", Location = new Point(95, buttonY), Size = new Size(75, 30) };
            btnSua.Click += BtnSua_Click;
            panel.Controls.Add(btnSua);

            btnXoa = new Button { Text = "Xóa", Location = new Point(180, buttonY), Size = new Size(75, 30) };
            btnXoa.Click += BtnXoa_Click;
            panel.Controls.Add(btnXoa);

            btnLamMoi = new Button { Text = "Làm mới", Location = new Point(265, buttonY), Size = new Size(75, 30) };
            btnLamMoi.Click += BtnLamMoi_Click;
            panel.Controls.Add(btnLamMoi);

            parent.Controls.Add(panel, 1, 0);
        }

        private async void FormQuanLyNhanVien_Load(object sender, EventArgs e)
        {
            await LoadNhanVien();
        }

        private async Task LoadNhanVien()
        {
            try
            {
                var nhanViens = await _nhanVienRepository.GetAllAsync();
                var displayData = nhanViens.Select(nv => new
                {
                    ID = nv.NhanVienID,
                    HoTen = nv.HoTen,
                    Username = nv.Username,
                    SoDienThoai = nv.SoDienThoai ?? "",
                    ChucVu = nv.ChucVu.ToString(),
                    CaLamViec = nv.CaLamViec ?? "",
                    DiaChi = nv.DiaChi ?? "",
                    TrangThai = nv.TrangThai ? "Hoạt động" : "Ngưng",
                    NgayVaoLam = nv.NgayVaoLam.ToString("dd/MM/yyyy")
                }).ToList();

                dgvNhanVien.DataSource = displayData;
                
                var colID = dgvNhanVien.Columns["ID"];
                if (colID != null)
                    colID.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh sách nhân viên: {ex.Message}", "Lỗi", 
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DgvNhanVien_SelectionChanged(object? sender, EventArgs e)
        {
            if (dgvNhanVien.SelectedRows.Count > 0)
            {
                var row = dgvNhanVien.SelectedRows[0];
                var idCell = row.Cells["ID"];
                if (idCell?.Value is int id)
                {
                    LoadEmployeeToForm(id);
                }
            }
        }

        private async void LoadEmployeeToForm(int nhanVienId)
        {
            try
            {
                _selectedNhanVien = await _nhanVienRepository.GetByIdAsync(nhanVienId);
                if (_selectedNhanVien != null)
                {
                    txtHoTen.Text = _selectedNhanVien.HoTen;
                    txtUsername.Text = _selectedNhanVien.Username;
                    txtSoDienThoai.Text = _selectedNhanVien.SoDienThoai ?? "";
                    cmbChucVu.Text = _selectedNhanVien.ChucVu.ToString();
                    chkTrangThai.Checked = _selectedNhanVien.TrangThai;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải thông tin nhân viên: {ex.Message}", "Lỗi", 
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void BtnThem_Click(object? sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtHoTen.Text) || string.IsNullOrWhiteSpace(txtUsername.Text))
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ họ tên và username!", "Thông báo", 
                                  MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var nhanVien = new NhanVien
                {
                    HoTen = txtHoTen.Text.Trim(),
                    Username = txtUsername.Text.Trim(),
                    SoDienThoai = txtSoDienThoai.Text.Trim(),
                    ChucVu = Enum.Parse<ChucVu>(cmbChucVu.Text),
                    TrangThai = chkTrangThai.Checked,
                    NgayVaoLam = DateTime.Now,
                    PasswordHash = PasswordHelper.HashPassword("123456")
                };

                await _nhanVienRepository.AddAsync(nhanVien);
                MessageBox.Show("Thêm nhân viên thành công!\nMật khẩu mặc định: 123456", "Thành công", 
                              MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                await LoadNhanVien();
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thêm nhân viên: {ex.Message}", "Lỗi", 
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void BtnSua_Click(object? sender, EventArgs e)
        {
            try
            {
                if (_selectedNhanVien == null)
                {
                    MessageBox.Show("Vui lòng chọn nhân viên cần sửa!", "Thông báo", 
                                  MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                _selectedNhanVien.HoTen = txtHoTen.Text.Trim();
                _selectedNhanVien.Username = txtUsername.Text.Trim();
                _selectedNhanVien.SoDienThoai = txtSoDienThoai.Text.Trim();
                _selectedNhanVien.ChucVu = Enum.Parse<ChucVu>(cmbChucVu.Text);
                _selectedNhanVien.TrangThai = chkTrangThai.Checked;

                await _nhanVienRepository.UpdateAsync(_selectedNhanVien);
                MessageBox.Show("Cập nhật thông tin nhân viên thành công!", "Thành công", 
                              MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                await LoadNhanVien();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi cập nhật nhân viên: {ex.Message}", "Lỗi", 
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void BtnXoa_Click(object? sender, EventArgs e)
        {
            try
            {
                if (_selectedNhanVien == null)
                {
                    MessageBox.Show("Vui lòng chọn nhân viên cần xóa!", "Thông báo", 
                                  MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var result = MessageBox.Show($"Bạn có chắc chắn muốn xóa nhân viên '{_selectedNhanVien.HoTen}'?", 
                                           "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                
                if (result == DialogResult.Yes)
                {
                    await _nhanVienRepository.DeleteAsync(_selectedNhanVien.NhanVienID);
                    MessageBox.Show("Xóa nhân viên thành công!", "Thành công", 
                                  MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    await LoadNhanVien();
                    ClearForm();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xóa nhân viên: {ex.Message}", "Lỗi", 
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnLamMoi_Click(object? sender, EventArgs e)
        {
            ClearForm();
        }

        private void ClearForm()
        {
            _selectedNhanVien = null;
            txtHoTen.Clear();
            txtUsername.Clear();
            txtSoDienThoai.Clear();
            cmbChucVu.SelectedIndex = -1;
            chkTrangThai.Checked = true;
        }
    }
}