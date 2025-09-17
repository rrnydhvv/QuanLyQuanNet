namespace QuanLyQuanNet.Forms
{
    partial class FormQuanLyDichVu
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
            this.panel1 = new Panel();
            this.lblTitle = new Label();
            this.panel2 = new Panel();
            this.groupBox1 = new GroupBox();
            this.txtTimKiem = new TextBox();
            this.lblTimKiem = new Label();
            this.btnTimKiem = new Button();
            this.btnLamMoi = new Button();
            this.panel3 = new Panel();
            this.dgvDichVu = new DataGridView();
            this.panel4 = new Panel();
            this.groupBox2 = new GroupBox();
            this.txtTenDichVu = new TextBox();
            this.lblTenDichVu = new Label();
            this.txtDonGia = new NumericUpDown();
            this.lblDonGia = new Label();
            this.txtMoTa = new TextBox();
            this.lblMoTa = new Label();
            this.txtDonViTinh = new TextBox();
            this.lblDonViTinh = new Label();
            this.txtSoLuongTon = new NumericUpDown();
            this.lblSoLuongTon = new Label();
            this.chkTrangThai = new CheckBox();
            this.btnThem = new Button();
            this.btnSua = new Button();
            this.btnXoa = new Button();
            this.btnNhapHang = new Button();
            this.btnXuatHang = new Button();
            
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDichVu)).BeginInit();
            this.panel4.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtDonGia)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSoLuongTon)).BeginInit();
            this.SuspendLayout();
            
            // panel1
            this.panel1.BackColor = Color.FromArgb(41, 128, 185);
            this.panel1.Controls.Add(this.lblTitle);
            this.panel1.Dock = DockStyle.Top;
            this.panel1.Location = new Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(1200, 60);
            this.panel1.TabIndex = 0;
            
            // lblTitle
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new Font("Microsoft Sans Serif", 16F, FontStyle.Bold);
            this.lblTitle.ForeColor = Color.White;
            this.lblTitle.Location = new Point(20, 18);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new Size(208, 26);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "QUẢN LÝ DỊCH VỤ";
            
            // panel2
            this.panel2.Controls.Add(this.groupBox1);
            this.panel2.Dock = DockStyle.Top;
            this.panel2.Location = new Point(0, 60);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new Padding(10);
            this.panel2.Size = new Size(1200, 80);
            this.panel2.TabIndex = 1;
            
            // groupBox1
            this.groupBox1.Controls.Add(this.txtTimKiem);
            this.groupBox1.Controls.Add(this.lblTimKiem);
            this.groupBox1.Controls.Add(this.btnTimKiem);
            this.groupBox1.Controls.Add(this.btnLamMoi);
            this.groupBox1.Dock = DockStyle.Fill;
            this.groupBox1.Font = new Font("Microsoft Sans Serif", 10F);
            this.groupBox1.Location = new Point(10, 10);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new Size(1180, 60);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Tìm kiếm";
            
            // lblTimKiem
            this.lblTimKiem.AutoSize = true;
            this.lblTimKiem.Location = new Point(15, 25);
            this.lblTimKiem.Name = "lblTimKiem";
            this.lblTimKiem.Size = new Size(103, 17);
            this.lblTimKiem.TabIndex = 0;
            this.lblTimKiem.Text = "Từ khóa tìm kiếm:";
            
            // txtTimKiem
            this.txtTimKiem.Font = new Font("Microsoft Sans Serif", 10F);
            this.txtTimKiem.Location = new Point(124, 22);
            this.txtTimKiem.Name = "txtTimKiem";
            this.txtTimKiem.Size = new Size(300, 23);
            this.txtTimKiem.TabIndex = 1;
            
            // btnTimKiem
            this.btnTimKiem.BackColor = Color.FromArgb(52, 152, 219);
            this.btnTimKiem.FlatStyle = FlatStyle.Flat;
            this.btnTimKiem.ForeColor = Color.White;
            this.btnTimKiem.Location = new Point(440, 20);
            this.btnTimKiem.Name = "btnTimKiem";
            this.btnTimKiem.Size = new Size(80, 28);
            this.btnTimKiem.TabIndex = 2;
            this.btnTimKiem.Text = "Tìm kiếm";
            this.btnTimKiem.UseVisualStyleBackColor = false;
            this.btnTimKiem.Click += new EventHandler(this.btnTimKiem_Click);
            
            // btnLamMoi
            this.btnLamMoi.BackColor = Color.FromArgb(149, 165, 166);
            this.btnLamMoi.FlatStyle = FlatStyle.Flat;
            this.btnLamMoi.ForeColor = Color.White;
            this.btnLamMoi.Location = new Point(530, 20);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Size = new Size(80, 28);
            this.btnLamMoi.TabIndex = 3;
            this.btnLamMoi.Text = "Làm mới";
            this.btnLamMoi.UseVisualStyleBackColor = false;
            this.btnLamMoi.Click += new EventHandler(this.btnLamMoi_Click);
            
            // panel3
            this.panel3.Controls.Add(this.dgvDichVu);
            this.panel3.Dock = DockStyle.Fill;
            this.panel3.Location = new Point(0, 140);
            this.panel3.Name = "panel3";
            this.panel3.Padding = new Padding(10, 0, 10, 0);
            this.panel3.Size = new Size(800, 480);
            this.panel3.TabIndex = 2;
            
            // dgvDichVu
            this.dgvDichVu.AllowUserToAddRows = false;
            this.dgvDichVu.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDichVu.BackgroundColor = Color.White;
            this.dgvDichVu.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDichVu.Dock = DockStyle.Fill;
            this.dgvDichVu.Location = new Point(10, 0);
            this.dgvDichVu.MultiSelect = false;
            this.dgvDichVu.Name = "dgvDichVu";
            this.dgvDichVu.ReadOnly = true;
            this.dgvDichVu.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvDichVu.Size = new Size(780, 480);
            this.dgvDichVu.TabIndex = 0;
            this.dgvDichVu.SelectionChanged += new EventHandler(this.dgvDichVu_SelectionChanged);
            
            // panel4
            this.panel4.Controls.Add(this.groupBox2);
            this.panel4.Dock = DockStyle.Right;
            this.panel4.Location = new Point(800, 140);
            this.panel4.Name = "panel4";
            this.panel4.Padding = new Padding(10);
            this.panel4.Size = new Size(400, 480);
            this.panel4.TabIndex = 3;
            
            // groupBox2
            this.groupBox2.Controls.Add(this.txtTenDichVu);
            this.groupBox2.Controls.Add(this.lblTenDichVu);
            this.groupBox2.Controls.Add(this.txtDonGia);
            this.groupBox2.Controls.Add(this.lblDonGia);
            this.groupBox2.Controls.Add(this.txtMoTa);
            this.groupBox2.Controls.Add(this.lblMoTa);
            this.groupBox2.Controls.Add(this.txtDonViTinh);
            this.groupBox2.Controls.Add(this.lblDonViTinh);
            this.groupBox2.Controls.Add(this.txtSoLuongTon);
            this.groupBox2.Controls.Add(this.lblSoLuongTon);
            this.groupBox2.Controls.Add(this.chkTrangThai);
            this.groupBox2.Controls.Add(this.btnThem);
            this.groupBox2.Controls.Add(this.btnSua);
            this.groupBox2.Controls.Add(this.btnXoa);
            this.groupBox2.Controls.Add(this.btnNhapHang);
            this.groupBox2.Controls.Add(this.btnXuatHang);
            this.groupBox2.Dock = DockStyle.Fill;
            this.groupBox2.Font = new Font("Microsoft Sans Serif", 10F);
            this.groupBox2.Location = new Point(10, 10);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new Size(380, 460);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Thông tin dịch vụ";
            
            // lblTenDichVu
            this.lblTenDichVu.AutoSize = true;
            this.lblTenDichVu.Location = new Point(15, 30);
            this.lblTenDichVu.Name = "lblTenDichVu";
            this.lblTenDichVu.Size = new Size(86, 17);
            this.lblTenDichVu.TabIndex = 0;
            this.lblTenDichVu.Text = "Tên dịch vụ:";
            
            // txtTenDichVu
            this.txtTenDichVu.Font = new Font("Microsoft Sans Serif", 10F);
            this.txtTenDichVu.Location = new Point(15, 50);
            this.txtTenDichVu.Name = "txtTenDichVu";
            this.txtTenDichVu.Size = new Size(350, 23);
            this.txtTenDichVu.TabIndex = 1;
            
            // lblDonGia
            this.lblDonGia.AutoSize = true;
            this.lblDonGia.Location = new Point(15, 85);
            this.lblDonGia.Name = "lblDonGia";
            this.lblDonGia.Size = new Size(60, 17);
            this.lblDonGia.TabIndex = 2;
            this.lblDonGia.Text = "Đơn giá:";
            
            // txtDonGia
            this.txtDonGia.DecimalPlaces = 2;
            this.txtDonGia.Font = new Font("Microsoft Sans Serif", 10F);
            this.txtDonGia.Location = new Point(15, 105);
            this.txtDonGia.Maximum = new decimal(new int[] { 999999999, 0, 0, 0 });
            this.txtDonGia.Name = "txtDonGia";
            this.txtDonGia.Size = new Size(160, 23);
            this.txtDonGia.TabIndex = 3;
            this.txtDonGia.ThousandsSeparator = true;
            
            // lblMoTa
            this.lblMoTa.AutoSize = true;
            this.lblMoTa.Location = new Point(15, 140);
            this.lblMoTa.Name = "lblMoTa";
            this.lblMoTa.Size = new Size(48, 17);
            this.lblMoTa.TabIndex = 4;
            this.lblMoTa.Text = "Mô tả:";
            
            // txtMoTa
            this.txtMoTa.Font = new Font("Microsoft Sans Serif", 10F);
            this.txtMoTa.Location = new Point(15, 160);
            this.txtMoTa.Multiline = true;
            this.txtMoTa.Name = "txtMoTa";
            this.txtMoTa.ScrollBars = ScrollBars.Vertical;
            this.txtMoTa.Size = new Size(350, 60);
            this.txtMoTa.TabIndex = 5;
            
            // lblDonViTinh
            this.lblDonViTinh.AutoSize = true;
            this.lblDonViTinh.Location = new Point(190, 85);
            this.lblDonViTinh.Name = "lblDonViTinh";
            this.lblDonViTinh.Size = new Size(84, 17);
            this.lblDonViTinh.TabIndex = 6;
            this.lblDonViTinh.Text = "Đơn vị tính:";
            
            // txtDonViTinh
            this.txtDonViTinh.Font = new Font("Microsoft Sans Serif", 10F);
            this.txtDonViTinh.Location = new Point(190, 105);
            this.txtDonViTinh.Name = "txtDonViTinh";
            this.txtDonViTinh.Size = new Size(175, 23);
            this.txtDonViTinh.TabIndex = 7;
            
            // lblSoLuongTon
            this.lblSoLuongTon.AutoSize = true;
            this.lblSoLuongTon.Location = new Point(15, 235);
            this.lblSoLuongTon.Name = "lblSoLuongTon";
            this.lblSoLuongTon.Size = new Size(92, 17);
            this.lblSoLuongTon.TabIndex = 8;
            this.lblSoLuongTon.Text = "Số lượng tồn:";
            
            // txtSoLuongTon
            this.txtSoLuongTon.Font = new Font("Microsoft Sans Serif", 10F);
            this.txtSoLuongTon.Location = new Point(15, 255);
            this.txtSoLuongTon.Maximum = new decimal(new int[] { 999999, 0, 0, 0 });
            this.txtSoLuongTon.Name = "txtSoLuongTon";
            this.txtSoLuongTon.Size = new Size(120, 23);
            this.txtSoLuongTon.TabIndex = 9;
            
            // chkTrangThai
            this.chkTrangThai.AutoSize = true;
            this.chkTrangThai.Checked = true;
            this.chkTrangThai.CheckState = CheckState.Checked;
            this.chkTrangThai.Location = new Point(15, 290);
            this.chkTrangThai.Name = "chkTrangThai";
            this.chkTrangThai.Size = new Size(91, 21);
            this.chkTrangThai.TabIndex = 10;
            this.chkTrangThai.Text = "Còn hàng";
            this.chkTrangThai.UseVisualStyleBackColor = true;
            
            // btnThem
            this.btnThem.BackColor = Color.FromArgb(39, 174, 96);
            this.btnThem.FlatStyle = FlatStyle.Flat;
            this.btnThem.ForeColor = Color.White;
            this.btnThem.Location = new Point(15, 330);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new Size(75, 35);
            this.btnThem.TabIndex = 11;
            this.btnThem.Text = "Thêm";
            this.btnThem.UseVisualStyleBackColor = false;
            this.btnThem.Click += new EventHandler(this.btnThem_Click);
            
            // btnSua
            this.btnSua.BackColor = Color.FromArgb(241, 196, 15);
            this.btnSua.FlatStyle = FlatStyle.Flat;
            this.btnSua.ForeColor = Color.White;
            this.btnSua.Location = new Point(100, 330);
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new Size(75, 35);
            this.btnSua.TabIndex = 12;
            this.btnSua.Text = "Sửa";
            this.btnSua.UseVisualStyleBackColor = false;
            this.btnSua.Click += new EventHandler(this.btnSua_Click);
            
            // btnXoa
            this.btnXoa.BackColor = Color.FromArgb(231, 76, 60);
            this.btnXoa.FlatStyle = FlatStyle.Flat;
            this.btnXoa.ForeColor = Color.White;
            this.btnXoa.Location = new Point(185, 330);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new Size(75, 35);
            this.btnXoa.TabIndex = 13;
            this.btnXoa.Text = "Xóa";
            this.btnXoa.UseVisualStyleBackColor = false;
            this.btnXoa.Click += new EventHandler(this.btnXoa_Click);
            
            // btnNhapHang
            this.btnNhapHang.BackColor = Color.FromArgb(155, 89, 182);
            this.btnNhapHang.FlatStyle = FlatStyle.Flat;
            this.btnNhapHang.ForeColor = Color.White;
            this.btnNhapHang.Location = new Point(15, 380);
            this.btnNhapHang.Name = "btnNhapHang";
            this.btnNhapHang.Size = new Size(100, 35);
            this.btnNhapHang.TabIndex = 14;
            this.btnNhapHang.Text = "Nhập hàng";
            this.btnNhapHang.UseVisualStyleBackColor = false;
            this.btnNhapHang.Click += new EventHandler(this.btnNhapHang_Click);
            
            // btnXuatHang
            this.btnXuatHang.BackColor = Color.FromArgb(230, 126, 34);
            this.btnXuatHang.FlatStyle = FlatStyle.Flat;
            this.btnXuatHang.ForeColor = Color.White;
            this.btnXuatHang.Location = new Point(125, 380);
            this.btnXuatHang.Name = "btnXuatHang";
            this.btnXuatHang.Size = new Size(100, 35);
            this.btnXuatHang.TabIndex = 15;
            this.btnXuatHang.Text = "Xuất hàng";
            this.btnXuatHang.UseVisualStyleBackColor = false;
            this.btnXuatHang.Click += new EventHandler(this.btnXuatHang_Click);
            
            // FormQuanLyDichVu
            this.AutoScaleDimensions = new SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(1200, 620);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "FormQuanLyDichVu";
            this.Text = "Quản lý Dịch vụ";
            this.WindowState = FormWindowState.Maximized;
            this.Load += new EventHandler(this.FormQuanLyDichVu_Load);
            
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDichVu)).EndInit();
            this.panel4.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtDonGia)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSoLuongTon)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Label lblTitle;
        private Panel panel2;
        private GroupBox groupBox1;
        private TextBox txtTimKiem;
        private Label lblTimKiem;
        private Button btnTimKiem;
        private Button btnLamMoi;
        private Panel panel3;
        private DataGridView dgvDichVu;
        private Panel panel4;
        private GroupBox groupBox2;
        private TextBox txtTenDichVu;
        private Label lblTenDichVu;
        private NumericUpDown txtDonGia;
        private Label lblDonGia;
        private TextBox txtMoTa;
        private Label lblMoTa;
        private TextBox txtDonViTinh;
        private Label lblDonViTinh;
        private NumericUpDown txtSoLuongTon;
        private Label lblSoLuongTon;
        private CheckBox chkTrangThai;
        private Button btnThem;
        private Button btnSua;
        private Button btnXoa;
        private Button btnNhapHang;
        private Button btnXuatHang;
    }
}