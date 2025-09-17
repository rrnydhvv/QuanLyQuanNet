using QuanLyQuanNet.Models;
using QuanLyQuanNet.Services;

namespace QuanLyQuanNet.Forms
{
    public partial class FormQuanLyDichVu : Form
    {
        private readonly IDichVuService _dichVuService;
        private BindingSource _bindingSource;
        private DichVu? _selectedDichVu;

        public FormQuanLyDichVu(IDichVuService dichVuService)
        {
            InitializeComponent();
            _dichVuService = dichVuService;
            _bindingSource = new BindingSource();
        }

        private async void FormQuanLyDichVu_Load(object sender, EventArgs e)
        {
            ConfigureDataGridView();
            await LoadDichVuData();
            ClearInputFields();
        }

        private void ConfigureDataGridView()
        {
            dgvDichVu.AutoGenerateColumns = false;
            
            dgvDichVu.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "DichVuID",
                HeaderText = "ID",
                Name = "DichVuID",
                Width = 60,
                ReadOnly = true
            });

            dgvDichVu.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "TenDichVu",
                HeaderText = "Tên dịch vụ",
                Name = "TenDichVu",
                Width = 200
            });

            dgvDichVu.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "DonGia",
                HeaderText = "Đơn giá",
                Name = "DonGia",
                Width = 100,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "N0" }
            });

            dgvDichVu.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "DonViTinh",
                HeaderText = "Đơn vị tính",
                Name = "DonViTinh",
                Width = 100
            });

            dgvDichVu.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "SoLuongTon",
                HeaderText = "Số lượng tồn",
                Name = "SoLuongTon",
                Width = 100
            });

            dgvDichVu.Columns.Add(new DataGridViewCheckBoxColumn
            {
                DataPropertyName = "TrangThai",
                HeaderText = "Còn hàng",
                Name = "TrangThai",
                Width = 80
            });

            dgvDichVu.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "MoTa",
                HeaderText = "Mô tả",
                Name = "MoTa",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });

            dgvDichVu.DataSource = _bindingSource;
        }

        private async Task LoadDichVuData()
        {
            try
            {
                var dichVus = await _dichVuService.GetAllDichVuAsync();
                _bindingSource.DataSource = dichVus.ToList();
                
                // Color rows based on stock status
                foreach (DataGridViewRow row in dgvDichVu.Rows)
                {
                    if (row.DataBoundItem is DichVu dichVu)
                    {
                        if (!dichVu.TrangThai)
                        {
                            row.DefaultCellStyle.BackColor = Color.LightGray;
                            row.DefaultCellStyle.ForeColor = Color.DarkGray;
                        }
                        else if (dichVu.SoLuongTon <= 5)
                        {
                            row.DefaultCellStyle.BackColor = Color.LightYellow;
                        }
                        else if (dichVu.SoLuongTon == 0)
                        {
                            row.DefaultCellStyle.BackColor = Color.LightCoral;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvDichVu_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvDichVu.CurrentRow?.DataBoundItem is DichVu selectedDichVu)
            {
                _selectedDichVu = selectedDichVu;
                PopulateInputFields(selectedDichVu);
            }
        }

        private void PopulateInputFields(DichVu dichVu)
        {
            txtTenDichVu.Text = dichVu.TenDichVu;
            txtDonGia.Value = dichVu.DonGia;
            txtMoTa.Text = dichVu.MoTa ?? "";
            txtDonViTinh.Text = dichVu.DonViTinh ?? "";
            txtSoLuongTon.Value = dichVu.SoLuongTon;
            chkTrangThai.Checked = dichVu.TrangThai;
        }

        private void ClearInputFields()
        {
            txtTenDichVu.Clear();
            txtDonGia.Value = 0;
            txtMoTa.Clear();
            txtDonViTinh.Clear();
            txtSoLuongTon.Value = 0;
            chkTrangThai.Checked = true;
            _selectedDichVu = null;
        }

        private DichVu CreateDichVuFromInput()
        {
            return new DichVu
            {
                TenDichVu = txtTenDichVu.Text.Trim(),
                DonGia = txtDonGia.Value,
                MoTa = string.IsNullOrWhiteSpace(txtMoTa.Text) ? null : txtMoTa.Text.Trim(),
                DonViTinh = string.IsNullOrWhiteSpace(txtDonViTinh.Text) ? null : txtDonViTinh.Text.Trim(),
                SoLuongTon = (int)txtSoLuongTon.Value,
                TrangThai = chkTrangThai.Checked
            };
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtTenDichVu.Text))
            {
                MessageBox.Show("Vui lòng nhập tên dịch vụ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenDichVu.Focus();
                return false;
            }

            if (txtDonGia.Value <= 0)
            {
                MessageBox.Show("Đơn giá phải lớn hơn 0!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDonGia.Focus();
                return false;
            }

            return true;
        }

        private async void btnThem_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;

            try
            {
                var dichVu = CreateDichVuFromInput();
                var result = await _dichVuService.AddDichVuAsync(dichVu);

                if (result)
                {
                    MessageBox.Show("Thêm dịch vụ thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await LoadDichVuData();
                    ClearInputFields();
                }
                else
                {
                    MessageBox.Show("Thêm dịch vụ thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thêm dịch vụ: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnSua_Click(object sender, EventArgs e)
        {
            if (_selectedDichVu == null)
            {
                MessageBox.Show("Vui lòng chọn dịch vụ cần sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!ValidateInput()) return;

            try
            {
                _selectedDichVu.TenDichVu = txtTenDichVu.Text.Trim();
                _selectedDichVu.DonGia = txtDonGia.Value;
                _selectedDichVu.MoTa = string.IsNullOrWhiteSpace(txtMoTa.Text) ? null : txtMoTa.Text.Trim();
                _selectedDichVu.DonViTinh = string.IsNullOrWhiteSpace(txtDonViTinh.Text) ? null : txtDonViTinh.Text.Trim();
                _selectedDichVu.SoLuongTon = (int)txtSoLuongTon.Value;
                _selectedDichVu.TrangThai = chkTrangThai.Checked;

                var result = await _dichVuService.UpdateDichVuAsync(_selectedDichVu);

                if (result)
                {
                    MessageBox.Show("Cập nhật dịch vụ thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    await LoadDichVuData();
                }
                else
                {
                    MessageBox.Show("Cập nhật dịch vụ thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi cập nhật dịch vụ: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnXoa_Click(object sender, EventArgs e)
        {
            if (_selectedDichVu == null)
            {
                MessageBox.Show("Vui lòng chọn dịch vụ cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var result = MessageBox.Show($"Bạn có chắc chắn muốn xóa dịch vụ '{_selectedDichVu.TenDichVu}'?", 
                "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    var deleteResult = await _dichVuService.DeleteDichVuAsync(_selectedDichVu.DichVuID);

                    if (deleteResult)
                    {
                        MessageBox.Show("Xóa dịch vụ thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        await LoadDichVuData();
                        ClearInputFields();
                    }
                    else
                    {
                        MessageBox.Show("Xóa dịch vụ thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi xóa dịch vụ: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private async void btnTimKiem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTimKiem.Text))
            {
                await LoadDichVuData();
                return;
            }

            try
            {
                var dichVus = await _dichVuService.TimKiemDichVuAsync(txtTimKiem.Text.Trim());
                _bindingSource.DataSource = dichVus.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tìm kiếm: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtTimKiem.Clear();
            await LoadDichVuData();
            ClearInputFields();
        }

        private async void btnNhapHang_Click(object sender, EventArgs e)
        {
            if (_selectedDichVu == null)
            {
                MessageBox.Show("Vui lòng chọn dịch vụ cần nhập hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string input = Microsoft.VisualBasic.Interaction.InputBox(
                "Nhập số lượng hàng cần nhập:", 
                "Nhập hàng", 
                "0");

            if (int.TryParse(input, out int soLuong) && soLuong > 0)
            {
                try
                {
                    var result = await _dichVuService.NhapHangAsync(_selectedDichVu.DichVuID, soLuong);

                    if (result)
                    {
                        MessageBox.Show($"Nhập hàng thành công! Đã thêm {soLuong} sản phẩm.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        await LoadDichVuData();
                    }
                    else
                    {
                        MessageBox.Show("Nhập hàng thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi nhập hàng: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (!string.IsNullOrEmpty(input))
            {
                MessageBox.Show("Số lượng không hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private async void btnXuatHang_Click(object sender, EventArgs e)
        {
            if (_selectedDichVu == null)
            {
                MessageBox.Show("Vui lòng chọn dịch vụ cần xuất hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string input = Microsoft.VisualBasic.Interaction.InputBox(
                $"Nhập số lượng hàng cần xuất (tồn kho hiện tại: {_selectedDichVu.SoLuongTon}):", 
                "Xuất hàng", 
                "0");

            if (int.TryParse(input, out int soLuong) && soLuong > 0)
            {
                if (soLuong > _selectedDichVu.SoLuongTon)
                {
                    MessageBox.Show("Số lượng xuất không được vượt quá số lượng tồn kho!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                try
                {
                    var result = await _dichVuService.XuatHangAsync(_selectedDichVu.DichVuID, soLuong);

                    if (result)
                    {
                        MessageBox.Show($"Xuất hàng thành công! Đã xuất {soLuong} sản phẩm.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        await LoadDichVuData();
                    }
                    else
                    {
                        MessageBox.Show("Xuất hàng thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi xuất hàng: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else if (!string.IsNullOrEmpty(input))
            {
                MessageBox.Show("Số lượng không hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}