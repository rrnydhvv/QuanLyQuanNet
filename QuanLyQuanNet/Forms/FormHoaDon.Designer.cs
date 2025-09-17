namespace QuanLyQuanNet.Forms
{
    partial class FormHoaDon
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.panel1 = new Panel();
            this.lblTitle = new Label();
            this.tabControl = new TabControl();
            this.tabHoaDonMoi = new TabPage();
            this.splitContainer1 = new SplitContainer();
            this.groupMayTram = new GroupBox();
            this.dgvMayTram = new DataGridView();
            this.groupThongTinKhach = new GroupBox();
            this.txtKhachHang = new TextBox();
            this.lblKhachHang = new Label();
            this.txtThoiGianSuDung = new TextBox();
            this.lblThoiGianSuDung = new Label();
            this.txtTienMay = new TextBox();
            this.lblTienMay = new Label();
            this.splitContainer2 = new SplitContainer();
            this.groupDichVu = new GroupBox();
            this.dgvDichVu = new DataGridView();
            this.txtTimDichVu = new TextBox();
            this.lblTimDichVu = new Label();
            this.groupHoaDon = new GroupBox();
            this.dgvChiTietHoaDon = new DataGridView();
            this.panel2 = new Panel();
            this.btnThanhToan = new Button();
            this.btnInHoaDon = new Button();
            this.btnHuyHoaDon = new Button();
            this.txtTongTien = new TextBox();
            this.lblTongTien = new Label();
            this.txtTienDichVu = new TextBox();
            this.lblTienDichVu = new Label();
            this.tabQuanLyHoaDon = new TabPage();
            this.panel3 = new Panel();
            this.groupTimKiem = new GroupBox();
            this.dtpDenNgay = new DateTimePicker();
            this.dtpTuNgay = new DateTimePicker();
            this.lblDenNgay = new Label();
            this.lblTuNgay = new Label();
            this.btnTimKiem = new Button();
            this.btnLamMoi = new Button();
            this.dgvDanhSachHoaDon = new DataGridView();
            this.timer = new System.Windows.Forms.Timer(this.components);

            this.panel1.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabHoaDonMoi.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupMayTram.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMayTram)).BeginInit();
            this.groupThongTinKhach.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.groupDichVu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDichVu)).BeginInit();
            this.groupHoaDon.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChiTietHoaDon)).BeginInit();
            this.panel2.SuspendLayout();
            this.tabQuanLyHoaDon.SuspendLayout();
            this.panel3.SuspendLayout();
            this.groupTimKiem.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDanhSachHoaDon)).BeginInit();
            this.SuspendLayout();

            // panel1
            this.panel1.BackColor = Color.FromArgb(41, 128, 185);
            this.panel1.Controls.Add(this.lblTitle);
            this.panel1.Dock = DockStyle.Top;
            this.panel1.Location = new Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(1400, 60);
            this.panel1.TabIndex = 0;

            // lblTitle
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new Font("Microsoft Sans Serif", 16F, FontStyle.Bold);
            this.lblTitle.ForeColor = Color.White;
            this.lblTitle.Location = new Point(20, 18);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new Size(188, 26);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "QUẢN LÝ HÓA ĐƠN";

            // tabControl
            this.tabControl.Dock = DockStyle.Fill;
            this.tabControl.Font = new Font("Microsoft Sans Serif", 10F);
            this.tabControl.Location = new Point(0, 60);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new Size(1400, 690);
            this.tabControl.TabIndex = 1;

            // tabHoaDonMoi
            this.tabHoaDonMoi.Controls.Add(this.splitContainer1);
            this.tabHoaDonMoi.Location = new Point(4, 25);
            this.tabHoaDonMoi.Name = "tabHoaDonMoi";
            this.tabHoaDonMoi.Padding = new Padding(3);
            this.tabHoaDonMoi.Size = new Size(1392, 661);
            this.tabHoaDonMoi.TabIndex = 0;
            this.tabHoaDonMoi.Text = "Tạo hóa đơn mới";
            this.tabHoaDonMoi.UseVisualStyleBackColor = true;

            // splitContainer1
            this.splitContainer1.Dock = DockStyle.Fill;
            this.splitContainer1.Location = new Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = Orientation.Horizontal;

            // splitContainer1.Panel1
            this.splitContainer1.Panel1.Controls.Add(this.groupMayTram);
            this.splitContainer1.Panel1.Controls.Add(this.groupThongTinKhach);

            // splitContainer1.Panel2
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new Size(1386, 655);
            this.splitContainer1.SplitterDistance = 200;
            this.splitContainer1.TabIndex = 0;

            // groupMayTram
            this.groupMayTram.Controls.Add(this.dgvMayTram);
            this.groupMayTram.Dock = DockStyle.Left;
            this.groupMayTram.Location = new Point(0, 0);
            this.groupMayTram.Name = "groupMayTram";
            this.groupMayTram.Size = new Size(500, 200);
            this.groupMayTram.TabIndex = 0;
            this.groupMayTram.TabStop = false;
            this.groupMayTram.Text = "Máy đang sử dụng";

            // dgvMayTram
            this.dgvMayTram.AllowUserToAddRows = false;
            this.dgvMayTram.AllowUserToDeleteRows = false;
            this.dgvMayTram.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvMayTram.BackgroundColor = Color.White;
            this.dgvMayTram.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMayTram.Dock = DockStyle.Fill;
            this.dgvMayTram.Location = new Point(3, 19);
            this.dgvMayTram.MultiSelect = false;
            this.dgvMayTram.Name = "dgvMayTram";
            this.dgvMayTram.ReadOnly = true;
            this.dgvMayTram.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvMayTram.Size = new Size(494, 178);
            this.dgvMayTram.TabIndex = 0;
            this.dgvMayTram.SelectionChanged += new EventHandler(this.dgvMayTram_SelectionChanged);

            // groupThongTinKhach
            this.groupThongTinKhach.Controls.Add(this.txtKhachHang);
            this.groupThongTinKhach.Controls.Add(this.lblKhachHang);
            this.groupThongTinKhach.Controls.Add(this.txtThoiGianSuDung);
            this.groupThongTinKhach.Controls.Add(this.lblThoiGianSuDung);
            this.groupThongTinKhach.Controls.Add(this.txtTienMay);
            this.groupThongTinKhach.Controls.Add(this.lblTienMay);
            this.groupThongTinKhach.Dock = DockStyle.Right;
            this.groupThongTinKhach.Location = new Point(510, 0);
            this.groupThongTinKhach.Name = "groupThongTinKhach";
            this.groupThongTinKhach.Size = new Size(876, 200);
            this.groupThongTinKhach.TabIndex = 1;
            this.groupThongTinKhach.TabStop = false;
            this.groupThongTinKhach.Text = "Thông tin khách hàng";

            // lblKhachHang
            this.lblKhachHang.AutoSize = true;
            this.lblKhachHang.Location = new Point(20, 30);
            this.lblKhachHang.Name = "lblKhachHang";
            this.lblKhachHang.Size = new Size(84, 17);
            this.lblKhachHang.TabIndex = 0;
            this.lblKhachHang.Text = "Khách hàng:";

            // txtKhachHang
            this.txtKhachHang.Font = new Font("Microsoft Sans Serif", 11F, FontStyle.Bold);
            this.txtKhachHang.Location = new Point(120, 27);
            this.txtKhachHang.Name = "txtKhachHang";
            this.txtKhachHang.ReadOnly = true;
            this.txtKhachHang.Size = new Size(300, 24);
            this.txtKhachHang.TabIndex = 1;

            // lblThoiGianSuDung
            this.lblThoiGianSuDung.AutoSize = true;
            this.lblThoiGianSuDung.Location = new Point(20, 70);
            this.lblThoiGianSuDung.Name = "lblThoiGianSuDung";
            this.lblThoiGianSuDung.Size = new Size(118, 17);
            this.lblThoiGianSuDung.TabIndex = 2;
            this.lblThoiGianSuDung.Text = "Thời gian sử dụng:";

            // txtThoiGianSuDung
            this.txtThoiGianSuDung.Font = new Font("Microsoft Sans Serif", 11F, FontStyle.Bold);
            this.txtThoiGianSuDung.ForeColor = Color.Blue;
            this.txtThoiGianSuDung.Location = new Point(150, 67);
            this.txtThoiGianSuDung.Name = "txtThoiGianSuDung";
            this.txtThoiGianSuDung.ReadOnly = true;
            this.txtThoiGianSuDung.Size = new Size(150, 24);
            this.txtThoiGianSuDung.TabIndex = 3;

            // lblTienMay
            this.lblTienMay.AutoSize = true;
            this.lblTienMay.Location = new Point(20, 110);
            this.lblTienMay.Name = "lblTienMay";
            this.lblTienMay.Size = new Size(69, 17);
            this.lblTienMay.TabIndex = 4;
            this.lblTienMay.Text = "Tiền máy:";

            // txtTienMay
            this.txtTienMay.Font = new Font("Microsoft Sans Serif", 11F, FontStyle.Bold);
            this.txtTienMay.ForeColor = Color.Green;
            this.txtTienMay.Location = new Point(120, 107);
            this.txtTienMay.Name = "txtTienMay";
            this.txtTienMay.ReadOnly = true;
            this.txtTienMay.Size = new Size(150, 24);
            this.txtTienMay.TabIndex = 5;

            // splitContainer2
            this.splitContainer2.Dock = DockStyle.Fill;
            this.splitContainer2.Location = new Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";

            // splitContainer2.Panel1
            this.splitContainer2.Panel1.Controls.Add(this.groupDichVu);

            // splitContainer2.Panel2
            this.splitContainer2.Panel2.Controls.Add(this.groupHoaDon);
            this.splitContainer2.Size = new Size(1386, 451);
            this.splitContainer2.SplitterDistance = 600;
            this.splitContainer2.TabIndex = 0;

            // groupDichVu
            this.groupDichVu.Controls.Add(this.dgvDichVu);
            this.groupDichVu.Controls.Add(this.txtTimDichVu);
            this.groupDichVu.Controls.Add(this.lblTimDichVu);
            this.groupDichVu.Dock = DockStyle.Fill;
            this.groupDichVu.Location = new Point(0, 0);
            this.groupDichVu.Name = "groupDichVu";
            this.groupDichVu.Size = new Size(600, 451);
            this.groupDichVu.TabIndex = 0;
            this.groupDichVu.TabStop = false;
            this.groupDichVu.Text = "Danh sách dịch vụ";

            // lblTimDichVu
            this.lblTimDichVu.AutoSize = true;
            this.lblTimDichVu.Location = new Point(10, 25);
            this.lblTimDichVu.Name = "lblTimDichVu";
            this.lblTimDichVu.Size = new Size(69, 17);
            this.lblTimDichVu.TabIndex = 0;
            this.lblTimDichVu.Text = "Tìm kiếm:";

            // txtTimDichVu
            this.txtTimDichVu.Location = new Point(85, 22);
            this.txtTimDichVu.Name = "txtTimDichVu";
            this.txtTimDichVu.Size = new Size(200, 23);
            this.txtTimDichVu.TabIndex = 1;
            this.txtTimDichVu.TextChanged += new EventHandler(this.txtTimDichVu_TextChanged);

            // dgvDichVu
            this.dgvDichVu.AllowUserToAddRows = false;
            this.dgvDichVu.AllowUserToDeleteRows = false;
            this.dgvDichVu.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDichVu.BackgroundColor = Color.White;
            this.dgvDichVu.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDichVu.Location = new Point(6, 55);
            this.dgvDichVu.MultiSelect = false;
            this.dgvDichVu.Name = "dgvDichVu";
            this.dgvDichVu.ReadOnly = true;
            this.dgvDichVu.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvDichVu.Size = new Size(588, 390);
            this.dgvDichVu.TabIndex = 2;
            this.dgvDichVu.DoubleClick += new EventHandler(this.dgvDichVu_DoubleClick);

            // groupHoaDon
            this.groupHoaDon.Controls.Add(this.dgvChiTietHoaDon);
            this.groupHoaDon.Controls.Add(this.panel2);
            this.groupHoaDon.Dock = DockStyle.Fill;
            this.groupHoaDon.Location = new Point(0, 0);
            this.groupHoaDon.Name = "groupHoaDon";
            this.groupHoaDon.Size = new Size(782, 451);
            this.groupHoaDon.TabIndex = 0;
            this.groupHoaDon.TabStop = false;
            this.groupHoaDon.Text = "Chi tiết hóa đơn";

            // dgvChiTietHoaDon
            this.dgvChiTietHoaDon.AllowUserToAddRows = false;
            this.dgvChiTietHoaDon.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvChiTietHoaDon.BackgroundColor = Color.White;
            this.dgvChiTietHoaDon.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvChiTietHoaDon.Dock = DockStyle.Fill;
            this.dgvChiTietHoaDon.Location = new Point(3, 19);
            this.dgvChiTietHoaDon.MultiSelect = false;
            this.dgvChiTietHoaDon.Name = "dgvChiTietHoaDon";
            this.dgvChiTietHoaDon.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvChiTietHoaDon.Size = new Size(776, 279);
            this.dgvChiTietHoaDon.TabIndex = 0;
            this.dgvChiTietHoaDon.KeyDown += new KeyEventHandler(this.dgvChiTietHoaDon_KeyDown);

            // panel2
            this.panel2.Controls.Add(this.btnThanhToan);
            this.panel2.Controls.Add(this.btnInHoaDon);
            this.panel2.Controls.Add(this.btnHuyHoaDon);
            this.panel2.Controls.Add(this.txtTongTien);
            this.panel2.Controls.Add(this.lblTongTien);
            this.panel2.Controls.Add(this.txtTienDichVu);
            this.panel2.Controls.Add(this.lblTienDichVu);
            this.panel2.Dock = DockStyle.Bottom;
            this.panel2.Location = new Point(3, 298);
            this.panel2.Name = "panel2";
            this.panel2.Size = new Size(776, 150);
            this.panel2.TabIndex = 1;

            // lblTienDichVu
            this.lblTienDichVu.AutoSize = true;
            this.lblTienDichVu.Location = new Point(20, 20);
            this.lblTienDichVu.Name = "lblTienDichVu";
            this.lblTienDichVu.Size = new Size(85, 17);
            this.lblTienDichVu.TabIndex = 0;
            this.lblTienDichVu.Text = "Tiền dịch vụ:";

            // txtTienDichVu
            this.txtTienDichVu.Font = new Font("Microsoft Sans Serif", 11F, FontStyle.Bold);
            this.txtTienDichVu.ForeColor = Color.Orange;
            this.txtTienDichVu.Location = new Point(120, 17);
            this.txtTienDichVu.Name = "txtTienDichVu";
            this.txtTienDichVu.ReadOnly = true;
            this.txtTienDichVu.Size = new Size(150, 24);
            this.txtTienDichVu.TabIndex = 1;

            // lblTongTien
            this.lblTongTien.AutoSize = true;
            this.lblTongTien.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold);
            this.lblTongTien.Location = new Point(20, 60);
            this.lblTongTien.Name = "lblTongTien";
            this.lblTongTien.Size = new Size(95, 20);
            this.lblTongTien.TabIndex = 2;
            this.lblTongTien.Text = "TỔNG TIỀN:";

            // txtTongTien
            this.txtTongTien.Font = new Font("Microsoft Sans Serif", 14F, FontStyle.Bold);
            this.txtTongTien.ForeColor = Color.Red;
            this.txtTongTien.Location = new Point(120, 57);
            this.txtTongTien.Name = "txtTongTien";
            this.txtTongTien.ReadOnly = true;
            this.txtTongTien.Size = new Size(200, 29);
            this.txtTongTien.TabIndex = 3;

            // btnThanhToan
            this.btnThanhToan.BackColor = Color.FromArgb(39, 174, 96);
            this.btnThanhToan.FlatStyle = FlatStyle.Flat;
            this.btnThanhToan.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold);
            this.btnThanhToan.ForeColor = Color.White;
            this.btnThanhToan.Location = new Point(400, 20);
            this.btnThanhToan.Name = "btnThanhToan";
            this.btnThanhToan.Size = new Size(120, 40);
            this.btnThanhToan.TabIndex = 4;
            this.btnThanhToan.Text = "Thanh toán";
            this.btnThanhToan.UseVisualStyleBackColor = false;
            this.btnThanhToan.Click += new EventHandler(this.btnThanhToan_Click);

            // btnInHoaDon
            this.btnInHoaDon.BackColor = Color.FromArgb(52, 152, 219);
            this.btnInHoaDon.FlatStyle = FlatStyle.Flat;
            this.btnInHoaDon.Font = new Font("Microsoft Sans Serif", 10F);
            this.btnInHoaDon.ForeColor = Color.White;
            this.btnInHoaDon.Location = new Point(400, 70);
            this.btnInHoaDon.Name = "btnInHoaDon";
            this.btnInHoaDon.Size = new Size(120, 35);
            this.btnInHoaDon.TabIndex = 5;
            this.btnInHoaDon.Text = "In hóa đơn";
            this.btnInHoaDon.UseVisualStyleBackColor = false;
            this.btnInHoaDon.Click += new EventHandler(this.btnInHoaDon_Click);

            // btnHuyHoaDon
            this.btnHuyHoaDon.BackColor = Color.FromArgb(231, 76, 60);
            this.btnHuyHoaDon.FlatStyle = FlatStyle.Flat;
            this.btnHuyHoaDon.Font = new Font("Microsoft Sans Serif", 10F);
            this.btnHuyHoaDon.ForeColor = Color.White;
            this.btnHuyHoaDon.Location = new Point(530, 70);
            this.btnHuyHoaDon.Name = "btnHuyHoaDon";
            this.btnHuyHoaDon.Size = new Size(120, 35);
            this.btnHuyHoaDon.TabIndex = 6;
            this.btnHuyHoaDon.Text = "Hủy hóa đơn";
            this.btnHuyHoaDon.UseVisualStyleBackColor = false;
            this.btnHuyHoaDon.Click += new EventHandler(this.btnHuyHoaDon_Click);

            // tabQuanLyHoaDon
            this.tabQuanLyHoaDon.Controls.Add(this.panel3);
            this.tabQuanLyHoaDon.Location = new Point(4, 25);
            this.tabQuanLyHoaDon.Name = "tabQuanLyHoaDon";
            this.tabQuanLyHoaDon.Padding = new Padding(3);
            this.tabQuanLyHoaDon.Size = new Size(1392, 661);
            this.tabQuanLyHoaDon.TabIndex = 1;
            this.tabQuanLyHoaDon.Text = "Quản lý hóa đơn";
            this.tabQuanLyHoaDon.UseVisualStyleBackColor = true;

            // panel3
            this.panel3.Controls.Add(this.dgvDanhSachHoaDon);
            this.panel3.Controls.Add(this.groupTimKiem);
            this.panel3.Dock = DockStyle.Fill;
            this.panel3.Location = new Point(3, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new Size(1386, 655);
            this.panel3.TabIndex = 0;

            // groupTimKiem
            this.groupTimKiem.Controls.Add(this.dtpDenNgay);
            this.groupTimKiem.Controls.Add(this.dtpTuNgay);
            this.groupTimKiem.Controls.Add(this.lblDenNgay);
            this.groupTimKiem.Controls.Add(this.lblTuNgay);
            this.groupTimKiem.Controls.Add(this.btnTimKiem);
            this.groupTimKiem.Controls.Add(this.btnLamMoi);
            this.groupTimKiem.Dock = DockStyle.Top;
            this.groupTimKiem.Location = new Point(0, 0);
            this.groupTimKiem.Name = "groupTimKiem";
            this.groupTimKiem.Size = new Size(1386, 80);
            this.groupTimKiem.TabIndex = 0;
            this.groupTimKiem.TabStop = false;
            this.groupTimKiem.Text = "Tìm kiếm hóa đơn";

            // lblTuNgay
            this.lblTuNgay.AutoSize = true;
            this.lblTuNgay.Location = new Point(20, 30);
            this.lblTuNgay.Name = "lblTuNgay";
            this.lblTuNgay.Size = new Size(62, 17);
            this.lblTuNgay.TabIndex = 0;
            this.lblTuNgay.Text = "Từ ngày:";

            // dtpTuNgay
            this.dtpTuNgay.Format = DateTimePickerFormat.Short;
            this.dtpTuNgay.Location = new Point(90, 27);
            this.dtpTuNgay.Name = "dtpTuNgay";
            this.dtpTuNgay.Size = new Size(120, 23);
            this.dtpTuNgay.TabIndex = 1;

            // lblDenNgay
            this.lblDenNgay.AutoSize = true;
            this.lblDenNgay.Location = new Point(230, 30);
            this.lblDenNgay.Name = "lblDenNgay";
            this.lblDenNgay.Size = new Size(71, 17);
            this.lblDenNgay.TabIndex = 2;
            this.lblDenNgay.Text = "Đến ngày:";

            // dtpDenNgay
            this.dtpDenNgay.Format = DateTimePickerFormat.Short;
            this.dtpDenNgay.Location = new Point(310, 27);
            this.dtpDenNgay.Name = "dtpDenNgay";
            this.dtpDenNgay.Size = new Size(120, 23);
            this.dtpDenNgay.TabIndex = 3;

            // btnTimKiem
            this.btnTimKiem.BackColor = Color.FromArgb(52, 152, 219);
            this.btnTimKiem.FlatStyle = FlatStyle.Flat;
            this.btnTimKiem.ForeColor = Color.White;
            this.btnTimKiem.Location = new Point(450, 25);
            this.btnTimKiem.Name = "btnTimKiem";
            this.btnTimKiem.Size = new Size(80, 28);
            this.btnTimKiem.TabIndex = 4;
            this.btnTimKiem.Text = "Tìm kiếm";
            this.btnTimKiem.UseVisualStyleBackColor = false;
            this.btnTimKiem.Click += new EventHandler(this.btnTimKiem_Click);

            // btnLamMoi
            this.btnLamMoi.BackColor = Color.FromArgb(149, 165, 166);
            this.btnLamMoi.FlatStyle = FlatStyle.Flat;
            this.btnLamMoi.ForeColor = Color.White;
            this.btnLamMoi.Location = new Point(540, 25);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Size = new Size(80, 28);
            this.btnLamMoi.TabIndex = 5;
            this.btnLamMoi.Text = "Làm mới";
            this.btnLamMoi.UseVisualStyleBackColor = false;
            this.btnLamMoi.Click += new EventHandler(this.btnLamMoi_Click);

            // dgvDanhSachHoaDon
            this.dgvDanhSachHoaDon.AllowUserToAddRows = false;
            this.dgvDanhSachHoaDon.AllowUserToDeleteRows = false;
            this.dgvDanhSachHoaDon.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDanhSachHoaDon.BackgroundColor = Color.White;
            this.dgvDanhSachHoaDon.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDanhSachHoaDon.Dock = DockStyle.Fill;
            this.dgvDanhSachHoaDon.Location = new Point(0, 80);
            this.dgvDanhSachHoaDon.MultiSelect = false;
            this.dgvDanhSachHoaDon.Name = "dgvDanhSachHoaDon";
            this.dgvDanhSachHoaDon.ReadOnly = true;
            this.dgvDanhSachHoaDon.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvDanhSachHoaDon.Size = new Size(1386, 575);
            this.dgvDanhSachHoaDon.TabIndex = 1;
            this.dgvDanhSachHoaDon.DoubleClick += new EventHandler(this.dgvDanhSachHoaDon_DoubleClick);

            // timer
            this.timer.Interval = 1000;
            this.timer.Tick += new EventHandler(this.timer_Tick);

            // FormHoaDon
            this.AutoScaleDimensions = new SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(1400, 750);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.panel1);
            this.Name = "FormHoaDon";
            this.Text = "Quản lý Hóa đơn";
            this.WindowState = FormWindowState.Maximized;
            this.Load += new EventHandler(this.FormHoaDon_Load);

            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tabControl.ResumeLayout(false);
            this.tabHoaDonMoi.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.groupMayTram.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMayTram)).EndInit();
            this.groupThongTinKhach.ResumeLayout(false);
            this.groupThongTinKhach.PerformLayout();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.groupDichVu.ResumeLayout(false);
            this.groupDichVu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDichVu)).EndInit();
            this.groupHoaDon.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvChiTietHoaDon)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.tabQuanLyHoaDon.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.groupTimKiem.ResumeLayout(false);
            this.groupTimKiem.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDanhSachHoaDon)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Label lblTitle;
        private TabControl tabControl;
        private TabPage tabHoaDonMoi;
        private TabPage tabQuanLyHoaDon;
        private SplitContainer splitContainer1;
        private SplitContainer splitContainer2;
        private GroupBox groupMayTram;
        private DataGridView dgvMayTram;
        private GroupBox groupThongTinKhach;
        private GroupBox groupDichVu;
        private GroupBox groupHoaDon;
        private DataGridView dgvDichVu;
        private DataGridView dgvChiTietHoaDon;
        private Panel panel2;
        private Panel panel3;
        private GroupBox groupTimKiem;
        private DataGridView dgvDanhSachHoaDon;
        private TextBox txtKhachHang;
        private Label lblKhachHang;
        private TextBox txtThoiGianSuDung;
        private Label lblThoiGianSuDung;
        private TextBox txtTienMay;
        private Label lblTienMay;
        private TextBox txtTimDichVu;
        private Label lblTimDichVu;
        private TextBox txtTienDichVu;
        private Label lblTienDichVu;
        private TextBox txtTongTien;
        private Label lblTongTien;
        private Button btnThanhToan;
        private Button btnInHoaDon;
        private Button btnHuyHoaDon;
        private DateTimePicker dtpTuNgay;
        private Label lblTuNgay;
        private DateTimePicker dtpDenNgay;
        private Label lblDenNgay;
        private Button btnTimKiem;
        private Button btnLamMoi;
        private System.Windows.Forms.Timer timer;
    }
}