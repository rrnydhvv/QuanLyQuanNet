using QuanLyQuanNet.Services;
using QuanLyQuanNet.Models;
using System.ComponentModel;

namespace QuanLyQuanNet.Forms
{
    public partial class FormHoaDon : Form
    {
        private readonly IAuthenticationService _authService;
        private readonly IHoaDonService _hoaDonService;
        private readonly IDichVuService _dichVuService;
        private readonly IQuanLyMayService _quanLyMayService;

        private List<ChiTietHoaDon> _chiTietHoaDon;
        private MayTram? _selectedMayTram;
        private BindingList<ChiTietHoaDon> _chiTietBinding;

        public FormHoaDon(IAuthenticationService authService, IHoaDonService hoaDonService, 
                         IDichVuService dichVuService, IQuanLyMayService quanLyMayService)
        {
            InitializeComponent();
            _authService = authService;
            _hoaDonService = hoaDonService;
            _dichVuService = dichVuService;
            _quanLyMayService = quanLyMayService;
            
            _chiTietHoaDon = new List<ChiTietHoaDon>();
            _chiTietBinding = new BindingList<ChiTietHoaDon>(_chiTietHoaDon);
        }

        private async void FormHoaDon_Load(object sender, EventArgs e)
        {
            try
            {
                // Setup UI first
                SetupDataGridViews();
                
                // Set default date range for search
                dtpTuNgay.Value = DateTime.Today.AddDays(-30);
                dtpDenNgay.Value = DateTime.Today;
                
                // Load data sequentially to avoid DbContext threading issues
                await LoadMayTramDangSuDung();
                await LoadDichVu();
                await LoadDanhSachHoaDon();
                
                // Setup timer last
                SetupTimer();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu: {ex.Message}", "Lỗi", 
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetupDataGridViews()
        {
            // Setup dgvMayTram
            dgvMayTram.AutoGenerateColumns = false;
            dgvMayTram.Columns.Clear();
            dgvMayTram.Columns.Add(new DataGridViewTextBoxColumn 
            { 
                DataPropertyName = "TenMay", 
                HeaderText = "Tên máy",
                Width = 100
            });
            dgvMayTram.Columns.Add(new DataGridViewTextBoxColumn 
            { 
                DataPropertyName = "NguoiDungHienTai.Username", 
                HeaderText = "Khách hàng",
                Width = 150
            });
            dgvMayTram.Columns.Add(new DataGridViewTextBoxColumn 
            { 
                DataPropertyName = "ThoiGianBatDau", 
                HeaderText = "Giờ bắt đầu",
                Width = 120,
                DefaultCellStyle = { Format = "HH:mm:ss dd/MM/yyyy" }
            });

            // Setup dgvDichVu
            dgvDichVu.AutoGenerateColumns = false;
            dgvDichVu.Columns.Clear();
            dgvDichVu.Columns.Add(new DataGridViewTextBoxColumn 
            { 
                DataPropertyName = "TenDichVu", 
                HeaderText = "Tên dịch vụ",
                Width = 200
            });
            dgvDichVu.Columns.Add(new DataGridViewTextBoxColumn 
            { 
                DataPropertyName = "DonGia", 
                HeaderText = "Giá",
                Width = 100,
                DefaultCellStyle = { Format = "N0" }
            });
            dgvDichVu.Columns.Add(new DataGridViewTextBoxColumn 
            { 
                DataPropertyName = "DonViTinh", 
                HeaderText = "Đơn vị",
                Width = 80
            });
            dgvDichVu.Columns.Add(new DataGridViewTextBoxColumn 
            { 
                DataPropertyName = "MoTa", 
                HeaderText = "Mô tả",
                Width = 200
            });

            // Setup dgvChiTietHoaDon
            dgvChiTietHoaDon.AutoGenerateColumns = false;
            dgvChiTietHoaDon.DataSource = _chiTietBinding;
            dgvChiTietHoaDon.Columns.Clear();
            dgvChiTietHoaDon.Columns.Add(new DataGridViewTextBoxColumn 
            { 
                DataPropertyName = "DichVu.TenDichVu", 
                HeaderText = "Dịch vụ",
                Width = 150
            });
            dgvChiTietHoaDon.Columns.Add(new DataGridViewTextBoxColumn 
            { 
                DataPropertyName = "SoLuong", 
                HeaderText = "Số lượng",
                Width = 80
            });
            dgvChiTietHoaDon.Columns.Add(new DataGridViewTextBoxColumn 
            { 
                DataPropertyName = "DonGia", 
                HeaderText = "Đơn giá",
                Width = 100,
                DefaultCellStyle = { Format = "N0" }
            });
            dgvChiTietHoaDon.Columns.Add(new DataGridViewTextBoxColumn 
            { 
                DataPropertyName = "ThanhTien", 
                HeaderText = "Thành tiền",
                Width = 120,
                DefaultCellStyle = { Format = "N0" }
            });

            // Setup dgvDanhSachHoaDon
            dgvDanhSachHoaDon.AutoGenerateColumns = false;
            dgvDanhSachHoaDon.Columns.Clear();
            dgvDanhSachHoaDon.Columns.Add(new DataGridViewTextBoxColumn 
            { 
                DataPropertyName = "HoaDonID", 
                HeaderText = "Mã HĐ",
                Width = 100
            });
            dgvDanhSachHoaDon.Columns.Add(new DataGridViewTextBoxColumn 
            { 
                DataPropertyName = "NguoiDung.Username", 
                HeaderText = "Khách hàng",
                Width = 150
            });
            dgvDanhSachHoaDon.Columns.Add(new DataGridViewTextBoxColumn 
            { 
                DataPropertyName = "NgayTao", 
                HeaderText = "Ngày tạo",
                Width = 120,
                DefaultCellStyle = { Format = "dd/MM/yyyy HH:mm" }
            });
            dgvDanhSachHoaDon.Columns.Add(new DataGridViewTextBoxColumn 
            { 
                DataPropertyName = "TongTien", 
                HeaderText = "Tổng tiền",
                Width = 120,
                DefaultCellStyle = { Format = "N0" }
            });
            dgvDanhSachHoaDon.Columns.Add(new DataGridViewTextBoxColumn 
            { 
                DataPropertyName = "DaThanhToan", 
                HeaderText = "Trạng thái",
                Width = 100
            });
        }

        private void SetupTimer()
        {
            timer.Start();
        }

        private async Task LoadMayTramDangSuDung()
        {
            var mayTramList = await _quanLyMayService.GetAllMayTramAsync();
            var mayDangSuDung = mayTramList.Where(m => m.TrangThai == TrangThaiMay.DangSuDung).ToList();
            
            // Debug output
            MessageBox.Show($"Tổng số máy: {mayTramList.Count()}\nMáy đang sử dụng: {mayDangSuDung.Count}", 
                           "Debug Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            
            dgvMayTram.DataSource = mayDangSuDung;
        }

        private async Task LoadDichVu()
        {
            var dichVuList = await _dichVuService.GetAllDichVuAsync();
            
            // Debug output
            MessageBox.Show($"Số dịch vụ có sẵn: {dichVuList.Count()}", 
                           "Debug Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            
            dgvDichVu.DataSource = dichVuList;
        }

        private Task LoadDanhSachHoaDon()
        {
            // TODO: Implement GetHoaDonByDateRangeAsync in service
            var emptyList = new List<HoaDon>();
            dgvDanhSachHoaDon.DataSource = emptyList;
            return Task.CompletedTask;
        }

        private async void dgvMayTram_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvMayTram.SelectedRows.Count > 0)
            {
                _selectedMayTram = dgvMayTram.SelectedRows[0].DataBoundItem as MayTram;
                if (_selectedMayTram != null)
                {
                    await LoadThongTinKhachHang(_selectedMayTram);
                }
            }
        }

        private Task LoadThongTinKhachHang(MayTram mayTram)
        {
            try
            {
                txtKhachHang.Text = mayTram.NguoiDungHienTai?.Username ?? "Khách vãng lai";
                
                // Tính thời gian sử dụng
                if (mayTram.ThoiGianBatDau.HasValue)
                {
                    var thoiGianSuDung = DateTime.Now - mayTram.ThoiGianBatDau.Value;
                    txtThoiGianSuDung.Text = $"{(int)thoiGianSuDung.TotalHours:D2}:{thoiGianSuDung.Minutes:D2}:{thoiGianSuDung.Seconds:D2}";
                    
                    // Tính tiền máy dựa trên thời gian và giá theo giờ
                    var soGio = (decimal)thoiGianSuDung.TotalHours;
                    var tienMay = soGio * mayTram.GiaTheoGio;
                    txtTienMay.Text = tienMay.ToString("N0") + " VNĐ";
                }
                else
                {
                    txtThoiGianSuDung.Text = "00:00:00";
                    txtTienMay.Text = "0 VNĐ";
                }
                
                UpdateTongTien();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải thông tin khách hàng: {ex.Message}", "Lỗi", 
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return Task.CompletedTask;
        }

        private void txtTimDichVu_TextChanged(object sender, EventArgs e)
        {
            if (dgvDichVu.DataSource is List<DichVu> dichVuList)
            {
                var filtered = dichVuList.Where(dv => 
                    dv.TenDichVu.ToLower().Contains(txtTimDichVu.Text.ToLower()) ||
                    (dv.MoTa?.ToLower().Contains(txtTimDichVu.Text.ToLower()) ?? false)
                ).ToList();
                
                dgvDichVu.DataSource = filtered;
            }
        }

        private void dgvDichVu_DoubleClick(object sender, EventArgs e)
        {
            if (dgvDichVu.SelectedRows.Count > 0)
            {
                var dichVu = dgvDichVu.SelectedRows[0].DataBoundItem as DichVu;
                if (dichVu != null)
                {
                    ThemDichVuVaoHoaDon(dichVu);
                }
            }
        }

        private void ThemDichVuVaoHoaDon(DichVu dichVu)
        {
            // Kiểm tra xem dịch vụ đã có trong hóa đơn chưa
            var existing = _chiTietHoaDon.FirstOrDefault(ct => ct.DichVuID == dichVu.DichVuID);
            
            if (existing != null)
            {
                // Tăng số lượng
                existing.SoLuong++;
                existing.ThanhTien = existing.SoLuong * existing.DonGia;
            }
            else
            {
                // Thêm mới
                var chiTiet = new ChiTietHoaDon
                {
                    DichVuID = dichVu.DichVuID,
                    SoLuong = 1,
                    DonGia = dichVu.DonGia,
                    ThanhTien = dichVu.DonGia,
                    DichVu = dichVu
                };
                _chiTietHoaDon.Add(chiTiet);
                _chiTietBinding.Add(chiTiet);
            }
            
            UpdateTongTien();
            dgvChiTietHoaDon.Refresh();
        }

        private void dgvChiTietHoaDon_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete && dgvChiTietHoaDon.SelectedRows.Count > 0)
            {
                var chiTiet = dgvChiTietHoaDon.SelectedRows[0].DataBoundItem as ChiTietHoaDon;
                if (chiTiet != null)
                {
                    _chiTietHoaDon.Remove(chiTiet);
                    _chiTietBinding.Remove(chiTiet);
                    UpdateTongTien();
                }
            }
        }

        private void UpdateTongTien()
        {
            var tienDichVu = _chiTietHoaDon.Sum(ct => ct.ThanhTien);
            txtTienDichVu.Text = tienDichVu.ToString("N0") + " VNĐ";
            
            var tienMay = 0m;
            if (decimal.TryParse(txtTienMay.Text.Replace(" VNĐ", "").Replace(",", ""), out tienMay))
            {
                var tongTien = tienMay + tienDichVu;
                txtTongTien.Text = tongTien.ToString("N0") + " VNĐ";
            }
        }

        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            if (_selectedMayTram == null)
            {
                MessageBox.Show("Vui lòng chọn máy cần thanh toán!", "Thông báo", 
                               MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            try
            {
                // TODO: Tạo FormThanhToan
                /*
                // Hiển thị form thanh toán
                using (var formThanhToan = new FormThanhToan(_selectedMayTram, _chiTietHoaDon, _hoaDonService))
                {
                    if (formThanhToan.ShowDialog() == DialogResult.OK)
                    {
                        // Thanh toán thành công
                        MessageBox.Show("Thanh toán thành công!", "Thông báo", 
                                       MessageBoxButtons.OK, MessageBoxIcon.Information);
                        
                        // Reset form
                        ResetForm();
                        await LoadMayTramDangSuDung();
                        await LoadDanhSachHoaDon();
                    }
                }
                */
                MessageBox.Show("Chức năng thanh toán đang được phát triển!", "Thông báo", 
                               MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thanh toán: {ex.Message}", "Lỗi", 
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnInHoaDon_Click(object sender, EventArgs e)
        {
            if (_selectedMayTram == null || !_chiTietHoaDon.Any())
            {
                MessageBox.Show("Không có dữ liệu để in hóa đơn!", "Thông báo", 
                               MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            // TODO: Implement print functionality
            MessageBox.Show("Chức năng in hóa đơn đang được phát triển!", "Thông báo", 
                           MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnHuyHoaDon_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn muốn hủy hóa đơn này?", "Xác nhận", 
                               MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                ResetForm();
            }
        }

        private void ResetForm()
        {
            _selectedMayTram = null;
            _chiTietHoaDon.Clear();
            _chiTietBinding.Clear();
            
            txtKhachHang.Clear();
            txtThoiGianSuDung.Clear();
            txtTienMay.Clear();
            txtTienDichVu.Clear();
            txtTongTien.Clear();
            
            dgvMayTram.ClearSelection();
        }

        private async void btnTimKiem_Click(object sender, EventArgs e)
        {
            await LoadDanhSachHoaDon();
        }

        private async void btnLamMoi_Click(object sender, EventArgs e)
        {
            dtpTuNgay.Value = DateTime.Today.AddDays(-30);
            dtpDenNgay.Value = DateTime.Today;
            await LoadDanhSachHoaDon();
        }

        private void dgvDanhSachHoaDon_DoubleClick(object sender, EventArgs e)
        {
            if (dgvDanhSachHoaDon.SelectedRows.Count > 0)
            {
                var hoaDon = dgvDanhSachHoaDon.SelectedRows[0].DataBoundItem as HoaDon;
                if (hoaDon != null)
                {
                    // TODO: Hiển thị chi tiết hóa đơn
                    MessageBox.Show($"Chi tiết hóa đơn {hoaDon.HoaDonID}\n" +
                                   $"Khách hàng: {hoaDon.NguoiDung?.Username}\n" +
                                   $"Tổng tiền: {hoaDon.TongTien:N0} VNĐ", 
                                   "Chi tiết hóa đơn", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private async void timer_Tick(object sender, EventArgs e)
        {
            if (_selectedMayTram != null)
            {
                await LoadThongTinKhachHang(_selectedMayTram);
            }
        }
    }
}