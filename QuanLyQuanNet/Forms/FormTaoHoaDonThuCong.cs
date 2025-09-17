using QuanLyQuanNet.Models;
using QuanLyQuanNet.Services;
using System.ComponentModel;

namespace QuanLyQuanNet.Forms
{
    public partial class FormTaoHoaDonThuCong : Form
    {
        private readonly IAuthenticationService _authService;
        private readonly IHoaDonService _hoaDonService;
        private readonly IDichVuService _dichVuService;
        private readonly IQuanLyMayService _quanLyMayService;

        private List<ChiTietHoaDon> _chiTietHoaDon;
        private BindingList<ChiTietHoaDon> _chiTietBinding;
        private List<DichVu> _allServices;

        // UI Controls
        private TextBox txtCustomerName = null!;
        private TextBox txtPhone = null!;
        private ComboBox cmbComputer = null!;
        private DateTimePicker dtpStartTime = null!;
        private DateTimePicker dtpEndTime = null!;
        private TextBox txtHourlyRate = null!;
        private TextBox txtComputerFee = null!;
        private TextBox txtSearchService = null!;
        private DataGridView dgvServices = null!;
        private DataGridView dgvInvoiceDetails = null!;
        private Label lblServiceFee = null!;
        private Label lblTotal = null!;

        public FormTaoHoaDonThuCong(IAuthenticationService authService, IHoaDonService hoaDonService,
                                   IDichVuService dichVuService, IQuanLyMayService quanLyMayService)
        {
            _authService = authService;
            _hoaDonService = hoaDonService;
            _dichVuService = dichVuService;
            _quanLyMayService = quanLyMayService;

            _chiTietHoaDon = new List<ChiTietHoaDon>();
            _chiTietBinding = new BindingList<ChiTietHoaDon>(_chiTietHoaDon);
            _allServices = new List<DichVu>();

            InitializeComponent();
            SetupUI();
        }

        private void InitializeComponent()
        {
            this.Text = "Tạo hóa đơn thủ công";
            this.Size = new Size(1200, 800);
            this.StartPosition = FormStartPosition.CenterParent;
            this.WindowState = FormWindowState.Normal;
        }

        private void SetupUI()
        {
            // Main layout
            var mainLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 3,
                Padding = new Padding(10)
            };

            mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 200));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 80));

            // Customer info panel
            var customerGroup = CreateCustomerInfoPanel();
            mainLayout.Controls.Add(customerGroup, 0, 0);

            // Time calculation panel
            var timeGroup = CreateTimeCalculationPanel();
            mainLayout.Controls.Add(timeGroup, 1, 0);

            // Services panel
            var servicesGroup = CreateServicesPanel();
            mainLayout.Controls.Add(servicesGroup, 0, 1);

            // Invoice details panel
            var invoiceGroup = CreateInvoiceDetailsPanel();
            mainLayout.Controls.Add(invoiceGroup, 1, 1);

            // Action buttons
            var actionPanel = CreateActionPanel();
            mainLayout.Controls.Add(actionPanel, 0, 2);
            mainLayout.SetColumnSpan(actionPanel, 2);

            this.Controls.Add(mainLayout);
        }

        private GroupBox CreateCustomerInfoPanel()
        {
            var group = new GroupBox { Text = "Thông tin khách hàng", Dock = DockStyle.Fill };
            var layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 4,
                Padding = new Padding(10)
            };

            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));

            // Customer name
            layout.Controls.Add(new Label { Text = "Tên khách:", TextAlign = ContentAlignment.MiddleLeft }, 0, 0);
            txtCustomerName = new TextBox { Dock = DockStyle.Fill };
            layout.Controls.Add(txtCustomerName, 1, 0);

            // Phone
            layout.Controls.Add(new Label { Text = "Điện thoại:", TextAlign = ContentAlignment.MiddleLeft }, 0, 1);
            txtPhone = new TextBox { Dock = DockStyle.Fill };
            layout.Controls.Add(txtPhone, 1, 1);

            // Computer
            layout.Controls.Add(new Label { Text = "Máy tính:", TextAlign = ContentAlignment.MiddleLeft }, 0, 2);
            cmbComputer = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, Dock = DockStyle.Fill };
            layout.Controls.Add(cmbComputer, 1, 2);

            // Load computers button
            var btnLoadComputers = new Button
            {
                Text = "Tải máy",
                BackColor = Color.FromArgb(52, 152, 219),
                ForeColor = Color.White,
                Dock = DockStyle.Fill
            };
            btnLoadComputers.Click += BtnLoadComputers_Click;
            layout.Controls.Add(btnLoadComputers, 1, 3);

            group.Controls.Add(layout);
            return group;
        }

        private GroupBox CreateTimeCalculationPanel()
        {
            var group = new GroupBox { Text = "Tính toán thời gian & tiền", Dock = DockStyle.Fill };
            var layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 6,
                Padding = new Padding(10)
            };

            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 100));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));

            // Start time
            layout.Controls.Add(new Label { Text = "Giờ bắt đầu:", TextAlign = ContentAlignment.MiddleLeft }, 0, 0);
            dtpStartTime = new DateTimePicker
            {
                Format = DateTimePickerFormat.Custom,
                CustomFormat = "dd/MM/yyyy HH:mm",
                Value = DateTime.Now.AddHours(-1),
                Dock = DockStyle.Fill
            };
            layout.Controls.Add(dtpStartTime, 1, 0);

            // End time
            layout.Controls.Add(new Label { Text = "Giờ kết thúc:", TextAlign = ContentAlignment.MiddleLeft }, 0, 1);
            dtpEndTime = new DateTimePicker
            {
                Format = DateTimePickerFormat.Custom,
                CustomFormat = "dd/MM/yyyy HH:mm",
                Value = DateTime.Now,
                Dock = DockStyle.Fill
            };
            layout.Controls.Add(dtpEndTime, 1, 1);

            // Hourly rate
            layout.Controls.Add(new Label { Text = "Giá/giờ:", TextAlign = ContentAlignment.MiddleLeft }, 0, 2);
            txtHourlyRate = new TextBox { Text = "10000", Dock = DockStyle.Fill };
            layout.Controls.Add(txtHourlyRate, 1, 2);

            // Computer fee
            layout.Controls.Add(new Label { Text = "Tiền máy:", TextAlign = ContentAlignment.MiddleLeft }, 0, 3);
            txtComputerFee = new TextBox
            {
                ReadOnly = true,
                BackColor = SystemColors.Control,
                Dock = DockStyle.Fill
            };
            layout.Controls.Add(txtComputerFee, 1, 3);

            // Calculate button
            var btnCalculate = new Button
            {
                Text = "Tính toán",
                BackColor = Color.FromArgb(39, 174, 96),
                ForeColor = Color.White,
                Dock = DockStyle.Fill
            };
            btnCalculate.Click += BtnCalculate_Click;
            layout.Controls.Add(btnCalculate, 1, 4);

            group.Controls.Add(layout);
            return group;
        }

        private GroupBox CreateServicesPanel()
        {
            var group = new GroupBox { Text = "Dịch vụ", Dock = DockStyle.Fill };
            var panel = new Panel { Dock = DockStyle.Fill, Padding = new Padding(10) };

            // Search
            var searchPanel = new Panel { Height = 35, Dock = DockStyle.Top };
            searchPanel.Controls.Add(new Label { Text = "Tìm:", Location = new Point(0, 8) });
            txtSearchService = new TextBox { Location = new Point(40, 5), Width = 200 };
            txtSearchService.TextChanged += TxtSearchService_TextChanged;
            searchPanel.Controls.Add(txtSearchService);

            // Services grid
            dgvServices = new DataGridView
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                AllowUserToAddRows = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };
            dgvServices.DoubleClick += DgvServices_DoubleClick;

            panel.Controls.Add(dgvServices);
            panel.Controls.Add(searchPanel);
            group.Controls.Add(panel);
            return group;
        }

        private GroupBox CreateInvoiceDetailsPanel()
        {
            var group = new GroupBox { Text = "Chi tiết hóa đơn", Dock = DockStyle.Fill };
            var panel = new Panel { Dock = DockStyle.Fill, Padding = new Padding(10) };

            // Invoice details grid
            dgvInvoiceDetails = new DataGridView
            {
                Height = 300,
                Dock = DockStyle.Top,
                AllowUserToAddRows = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                DataSource = _chiTietBinding
            };
            dgvInvoiceDetails.KeyDown += DgvInvoiceDetails_KeyDown;

            // Setup columns
            SetupInvoiceDetailsGrid();

            // Summary panel
            var summaryPanel = new Panel { Height = 100, Dock = DockStyle.Bottom };

            lblServiceFee = new Label
            {
                Text = "Tiền dịch vụ: 0 VNĐ",
                Location = new Point(10, 10),
                Size = new Size(200, 25),
                Font = new Font("Microsoft Sans Serif", 10, FontStyle.Bold),
                ForeColor = Color.Orange
            };

            lblTotal = new Label
            {
                Text = "TỔNG TIỀN: 0 VNĐ",
                Location = new Point(10, 40),
                Size = new Size(300, 30),
                Font = new Font("Microsoft Sans Serif", 14, FontStyle.Bold),
                ForeColor = Color.Red
            };

            summaryPanel.Controls.Add(lblServiceFee);
            summaryPanel.Controls.Add(lblTotal);

            panel.Controls.Add(summaryPanel);
            panel.Controls.Add(dgvInvoiceDetails);
            group.Controls.Add(panel);
            return group;
        }

        private Panel CreateActionPanel()
        {
            var panel = new Panel { Dock = DockStyle.Fill };
            var flowPanel = new FlowLayoutPanel
            {
                FlowDirection = FlowDirection.LeftToRight,
                Dock = DockStyle.Fill,
                Padding = new Padding(10)
            };

            var btnCreate = new Button
            {
                Text = "Tạo hóa đơn",
                Size = new Size(120, 40),
                BackColor = Color.FromArgb(39, 174, 96),
                ForeColor = Color.White,
                Font = new Font("Microsoft Sans Serif", 12, FontStyle.Bold)
            };
            btnCreate.Click += BtnCreate_Click;

            var btnClear = new Button
            {
                Text = "Làm mới",
                Size = new Size(100, 40),
                BackColor = Color.FromArgb(149, 165, 166),
                ForeColor = Color.White
            };
            btnClear.Click += BtnClear_Click;

            var btnPreview = new Button
            {
                Text = "Xem trước",
                Size = new Size(100, 40),
                BackColor = Color.FromArgb(52, 152, 219),
                ForeColor = Color.White
            };
            btnPreview.Click += BtnPreview_Click;

            flowPanel.Controls.Add(btnCreate);
            flowPanel.Controls.Add(btnClear);
            flowPanel.Controls.Add(btnPreview);

            panel.Controls.Add(flowPanel);
            return panel;
        }

        private void SetupInvoiceDetailsGrid()
        {
            dgvInvoiceDetails.Columns.Clear();
            dgvInvoiceDetails.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "DichVu.TenDichVu",
                HeaderText = "Dịch vụ",
                Width = 150
            });
            dgvInvoiceDetails.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "SoLuong",
                HeaderText = "Số lượng",
                Width = 80
            });
            dgvInvoiceDetails.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "DonGia",
                HeaderText = "Đơn giá",
                Width = 100,
                DefaultCellStyle = { Format = "N0" }
            });
            dgvInvoiceDetails.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "ThanhTien",
                HeaderText = "Thành tiền",
                Width = 120,
                DefaultCellStyle = { Format = "N0" }
            });
        }

        private async void FormTaoHoaDonThuCong_Load(object sender, EventArgs e)
        {
            await LoadServices();
        }

        private async Task LoadServices()
        {
            try
            {
                _allServices = (await _dichVuService.GetAllDichVuAsync()).ToList();
                SetupServicesGrid();
                dgvServices.DataSource = _allServices;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dịch vụ: {ex.Message}", "Lỗi",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetupServicesGrid()
        {
            dgvServices.Columns.Clear();
            dgvServices.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "TenDichVu",
                HeaderText = "Tên dịch vụ",
                Width = 200
            });
            dgvServices.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "DonGia",
                HeaderText = "Giá",
                Width = 100,
                DefaultCellStyle = { Format = "N0" }
            });
            dgvServices.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "DonViTinh",
                HeaderText = "Đơn vị",
                Width = 80
            });
        }

        private async void BtnLoadComputers_Click(object? sender, EventArgs e)
        {
            try
            {
                var computers = await _quanLyMayService.GetAllMayTramAsync();
                cmbComputer.Items.Clear();
                cmbComputer.Items.Add("Không chọn máy");

                foreach (var computer in computers)
                {
                    cmbComputer.Items.Add($"{computer.TenMay} - {computer.GiaTheoGio:N0} VNĐ/giờ");
                    cmbComputer.Tag = computers.ToList(); // Store for later use
                }

                cmbComputer.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải máy tính: {ex.Message}", "Lỗi",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnCalculate_Click(object? sender, EventArgs e)
        {
            try
            {
                var startTime = dtpStartTime.Value;
                var endTime = dtpEndTime.Value;

                if (endTime <= startTime)
                {
                    MessageBox.Show("Giờ kết thúc phải sau giờ bắt đầu!", "Thông báo",
                                   MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var duration = endTime - startTime;
                var hours = (decimal)duration.TotalHours;

                if (decimal.TryParse(txtHourlyRate.Text, out decimal hourlyRate))
                {
                    // Round up to 15 minutes
                    hours = Math.Ceiling(hours * 4) / 4;
                    var computerFee = hours * hourlyRate;
                    txtComputerFee.Text = computerFee.ToString("N0") + " VNĐ";

                    UpdateTotal();
                }
                else
                {
                    MessageBox.Show("Giá/giờ không hợp lệ!", "Thông báo",
                                   MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tính toán: {ex.Message}", "Lỗi",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TxtSearchService_TextChanged(object? sender, EventArgs e)
        {
            var searchText = txtSearchService.Text.ToLower();
            var filtered = _allServices.Where(s =>
                s.TenDichVu.ToLower().Contains(searchText) ||
                (s.MoTa?.ToLower().Contains(searchText) ?? false)
            ).ToList();

            dgvServices.DataSource = filtered;
        }

        private void DgvServices_DoubleClick(object? sender, EventArgs e)
        {
            if (dgvServices.SelectedRows.Count > 0)
            {
                var service = dgvServices.SelectedRows[0].DataBoundItem as DichVu;
                if (service != null)
                {
                    AddServiceToInvoice(service);
                }
            }
        }

        private void AddServiceToInvoice(DichVu service)
        {
            var existing = _chiTietHoaDon.FirstOrDefault(ct => ct.DichVuID == service.DichVuID);

            if (existing != null)
            {
                existing.SoLuong++;
                existing.ThanhTien = existing.SoLuong * existing.DonGia;
            }
            else
            {
                var chiTiet = new ChiTietHoaDon
                {
                    DichVuID = service.DichVuID,
                    SoLuong = 1,
                    DonGia = service.DonGia,
                    ThanhTien = service.DonGia,
                    DichVu = service
                };
                _chiTietHoaDon.Add(chiTiet);
                _chiTietBinding.Add(chiTiet);
            }

            UpdateTotal();
            dgvInvoiceDetails.Refresh();
        }

        private void DgvInvoiceDetails_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete && dgvInvoiceDetails.SelectedRows.Count > 0)
            {
                var chiTiet = dgvInvoiceDetails.SelectedRows[0].DataBoundItem as ChiTietHoaDon;
                if (chiTiet != null)
                {
                    _chiTietHoaDon.Remove(chiTiet);
                    _chiTietBinding.Remove(chiTiet);
                    UpdateTotal();
                }
            }
        }

        private void UpdateTotal()
        {
            var serviceFee = _chiTietHoaDon.Sum(ct => ct.ThanhTien);
            lblServiceFee.Text = $"Tiền dịch vụ: {serviceFee:N0} VNĐ";

            var computerFeeText = txtComputerFee.Text.Replace(" VNĐ", "").Replace(",", "");
            if (decimal.TryParse(computerFeeText, out decimal computerFee))
            {
                var total = computerFee + serviceFee;
                lblTotal.Text = $"TỔNG TIỀN: {total:N0} VNĐ";
            }
        }

        private async void BtnCreate_Click(object? sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtCustomerName.Text))
                {
                    MessageBox.Show("Vui lòng nhập tên khách hàng!", "Thông báo",
                                   MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var computerFeeText = txtComputerFee.Text.Replace(" VNĐ", "").Replace(",", "");
                if (!decimal.TryParse(computerFeeText, out decimal computerFee) || computerFee <= 0)
                {
                    MessageBox.Show("Vui lòng tính toán tiền máy trước!", "Thông báo",
                                   MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var currentUser = _authService.CurrentUser;
                if (currentUser == null)
                {
                    MessageBox.Show("Không thể xác định nhân viên hiện tại!", "Lỗi",
                                   MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Create manual invoice
                var hoaDon = new HoaDon
                {
                    UserID = 0, // Anonymous customer
                    MayID = 1, // Default computer
                    GioBatDau = dtpStartTime.Value,
                    GioKetThuc = dtpEndTime.Value,
                    NgayTao = DateTime.Now,
                    TienGio = computerFee,
                    TienDichVu = _chiTietHoaDon.Sum(ct => ct.ThanhTien),
                    TongTien = computerFee + _chiTietHoaDon.Sum(ct => ct.ThanhTien),
                    DaThanhToan = false,
                    NhanVienID = currentUser.NhanVienID,
                    // Add customer info to notes
                    GhiChu = $"Khách: {txtCustomerName.Text.Trim()}" +
                            (string.IsNullOrWhiteSpace(txtPhone.Text) ? "" : $" - SĐT: {txtPhone.Text.Trim()}")
                };

                var createdInvoice = await _hoaDonService.TaoHoaDonAsync(0, 1, currentUser.NhanVienID);

                // Add services
                foreach (var chiTiet in _chiTietHoaDon)
                {
                    await _hoaDonService.ThemDichVuAsync(createdInvoice.HoaDonID, chiTiet.DichVuID, chiTiet.SoLuong);
                }

                // Update totals
                await _hoaDonService.TinhTongTienAsync(createdInvoice.HoaDonID);

                MessageBox.Show($"Tạo hóa đơn thành công!\nMã hóa đơn: {createdInvoice.HoaDonID}\nTổng tiền: {hoaDon.TongTien:N0} VNĐ",
                               "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Show payment form
                var paymentForm = new FormThanhToan(null!, _chiTietHoaDon, _hoaDonService, _authService);
                if (paymentForm.ShowDialog() == DialogResult.OK)
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tạo hóa đơn: {ex.Message}", "Lỗi",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnClear_Click(object? sender, EventArgs e)
        {
            txtCustomerName.Clear();
            txtPhone.Clear();
            cmbComputer.SelectedIndex = -1;
            dtpStartTime.Value = DateTime.Now.AddHours(-1);
            dtpEndTime.Value = DateTime.Now;
            txtHourlyRate.Text = "10000";
            txtComputerFee.Clear();
            txtSearchService.Clear();

            _chiTietHoaDon.Clear();
            _chiTietBinding.Clear();
            UpdateTotal();
        }

        private void BtnPreview_Click(object? sender, EventArgs e)
        {
            try
            {
                var customerName = string.IsNullOrWhiteSpace(txtCustomerName.Text) ? "Khách vãng lai" : txtCustomerName.Text.Trim();
                var phone = string.IsNullOrWhiteSpace(txtPhone.Text) ? "" : $" - SĐT: {txtPhone.Text.Trim()}";

                var computerFeeText = txtComputerFee.Text.Replace(" VNĐ", "").Replace(",", "");
                decimal.TryParse(computerFeeText, out decimal computerFee);

                var serviceFee = _chiTietHoaDon.Sum(ct => ct.ThanhTien);
                var total = computerFee + serviceFee;

                var preview = new System.Text.StringBuilder();
                preview.AppendLine("========== XEM TRƯỚC HÓA ĐƠN ==========");
                preview.AppendLine($"Khách hàng: {customerName}{phone}");
                preview.AppendLine($"Thời gian: {dtpStartTime.Value:dd/MM/yyyy HH:mm} - {dtpEndTime.Value:dd/MM/yyyy HH:mm}");
                preview.AppendLine($"Tiền máy: {computerFee:N0} VNĐ");
                
                if (_chiTietHoaDon.Any())
                {
                    preview.AppendLine("\nDịch vụ:");
                    foreach (var ct in _chiTietHoaDon)
                    {
                        preview.AppendLine($"- {ct.DichVu?.TenDichVu}: {ct.SoLuong} x {ct.DonGia:N0} = {ct.ThanhTien:N0} VNĐ");
                    }
                    preview.AppendLine($"Tiền dịch vụ: {serviceFee:N0} VNĐ");
                }
                
                preview.AppendLine($"\nTỔNG TIỀN: {total:N0} VNĐ");
                preview.AppendLine("=====================================");

                MessageBox.Show(preview.ToString(), "Xem trước hóa đơn", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xem trước: {ex.Message}", "Lỗi",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            FormTaoHoaDonThuCong_Load(this, e);
        }
    }
}