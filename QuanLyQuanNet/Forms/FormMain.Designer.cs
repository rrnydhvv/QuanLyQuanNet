namespace QuanLyQuanNet.Forms
{
    partial class FormMain
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.hệThốngToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.đăngXuấtToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.thoátToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quảnLýToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.máyTrạmToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.kháchHàngToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dịchVụToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nhânViênToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hóaĐơnToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tạoHóaĐơnToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tạoHóaĐơnThủCôngToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.danhSáchHóaĐơnToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.báoCáoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.thốngKêDaoanhThuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblCurrentUser = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblCurrentTime = new System.Windows.Forms.ToolStripStatusLabel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panelMain = new System.Windows.Forms.Panel();
            this.lblWelcome = new System.Windows.Forms.Label();
            this.groupBoxQuickActions = new System.Windows.Forms.GroupBox();
            this.btnQuanLyMay = new System.Windows.Forms.Button();
            this.btnQuanLyKhach = new System.Windows.Forms.Button();
            this.btnTaoHoaDon = new System.Windows.Forms.Button();
            this.btnTaoHoaDonThuCong = new System.Windows.Forms.Button();
            this.btnThongKe = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.panelMain.SuspendLayout();
            this.groupBoxQuickActions.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.hệThốngToolStripMenuItem,
            this.quảnLýToolStripMenuItem,
            this.hóaĐơnToolStripMenuItem,
            this.báoCáoToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1200, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // hệThốngToolStripMenuItem
            // 
            this.hệThốngToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.đăngXuấtToolStripMenuItem,
            this.thoátToolStripMenuItem});
            this.hệThốngToolStripMenuItem.Name = "hệThốngToolStripMenuItem";
            this.hệThốngToolStripMenuItem.Size = new System.Drawing.Size(71, 20);
            this.hệThốngToolStripMenuItem.Text = "Hệ thống";
            // 
            // đăngXuấtToolStripMenuItem
            // 
            this.đăngXuấtToolStripMenuItem.Name = "đăngXuấtToolStripMenuItem";
            this.đăngXuấtToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            this.đăngXuấtToolStripMenuItem.Text = "Đăng xuất";
            this.đăngXuấtToolStripMenuItem.Click += new System.EventHandler(this.đăngXuấtToolStripMenuItem_Click);
            // 
            // thoátToolStripMenuItem
            // 
            this.thoátToolStripMenuItem.Name = "thoátToolStripMenuItem";
            this.thoátToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            this.thoátToolStripMenuItem.Text = "Thoát";
            this.thoátToolStripMenuItem.Click += new System.EventHandler(this.thoátToolStripMenuItem_Click);
            // 
            // quảnLýToolStripMenuItem
            // 
            this.quảnLýToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.máyTrạmToolStripMenuItem,
            this.kháchHàngToolStripMenuItem,
            this.dịchVụToolStripMenuItem,
            this.nhânViênToolStripMenuItem});
            this.quảnLýToolStripMenuItem.Name = "quảnLýToolStripMenuItem";
            this.quảnLýToolStripMenuItem.Size = new System.Drawing.Size(62, 20);
            this.quảnLýToolStripMenuItem.Text = "Quản lý";
            // 
            // máyTrạmToolStripMenuItem
            // 
            this.máyTrạmToolStripMenuItem.Name = "máyTrạmToolStripMenuItem";
            this.máyTrạmToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.máyTrạmToolStripMenuItem.Text = "Máy trạm";
            this.máyTrạmToolStripMenuItem.Click += new System.EventHandler(this.máyTrạmToolStripMenuItem_Click);
            // 
            // kháchHàngToolStripMenuItem
            // 
            this.kháchHàngToolStripMenuItem.Name = "kháchHàngToolStripMenuItem";
            this.kháchHàngToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.kháchHàngToolStripMenuItem.Text = "Khách hàng";
            this.kháchHàngToolStripMenuItem.Click += new System.EventHandler(this.kháchHàngToolStripMenuItem_Click);
            // 
            // dịchVụToolStripMenuItem
            // 
            this.dịchVụToolStripMenuItem.Name = "dịchVụToolStripMenuItem";
            this.dịchVụToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.dịchVụToolStripMenuItem.Text = "Dịch vụ";
            this.dịchVụToolStripMenuItem.Click += new System.EventHandler(this.dịchVụToolStripMenuItem_Click);
            // 
            // nhânViênToolStripMenuItem
            // 
            this.nhânViênToolStripMenuItem.Name = "nhânViênToolStripMenuItem";
            this.nhânViênToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.nhânViênToolStripMenuItem.Text = "Nhân viên";
            this.nhânViênToolStripMenuItem.Click += new System.EventHandler(this.nhânViênToolStripMenuItem_Click);
            // 
            // hóaĐơnToolStripMenuItem
            // 
            this.hóaĐơnToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tạoHóaĐơnToolStripMenuItem,
            this.tạoHóaĐơnThủCôngToolStripMenuItem,
            this.danhSáchHóaĐơnToolStripMenuItem});
            this.hóaĐơnToolStripMenuItem.Name = "hóaĐơnToolStripMenuItem";
            this.hóaĐơnToolStripMenuItem.Size = new System.Drawing.Size(65, 20);
            this.hóaĐơnToolStripMenuItem.Text = "Hóa đơn";
            // 
            // tạoHóaĐơnToolStripMenuItem
            // 
            this.tạoHóaĐơnToolStripMenuItem.Name = "tạoHóaĐơnToolStripMenuItem";
            this.tạoHóaĐơnToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            this.tạoHóaĐơnToolStripMenuItem.Text = "Tạo hóa đơn";
            this.tạoHóaĐơnToolStripMenuItem.Click += new System.EventHandler(this.tạoHóaĐơnToolStripMenuItem_Click);
            // 
            // tạoHóaĐơnThủCôngToolStripMenuItem
            // 
            this.tạoHóaĐơnThủCôngToolStripMenuItem.Name = "tạoHóaĐơnThủCôngToolStripMenuItem";
            this.tạoHóaĐơnThủCôngToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            this.tạoHóaĐơnThủCôngToolStripMenuItem.Text = "Tạo hóa đơn thủ công";
            this.tạoHóaĐơnThủCôngToolStripMenuItem.Click += new System.EventHandler(this.tạoHóaĐơnThủCôngToolStripMenuItem_Click);
            // 
            // danhSáchHóaĐơnToolStripMenuItem
            // 
            this.danhSáchHóaĐơnToolStripMenuItem.Name = "danhSáchHóaĐơnToolStripMenuItem";
            this.danhSáchHóaĐơnToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            this.danhSáchHóaĐơnToolStripMenuItem.Text = "Danh sách hóa đơn";
            // 
            // báoCáoToolStripMenuItem
            // 
            this.báoCáoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.thốngKêDaoanhThuToolStripMenuItem});
            this.báoCáoToolStripMenuItem.Name = "báoCáoToolStripMenuItem";
            this.báoCáoToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.báoCáoToolStripMenuItem.Text = "Báo cáo";
            // 
            // thốngKêDaoanhThuToolStripMenuItem
            // 
            this.thốngKêDaoanhThuToolStripMenuItem.Name = "thốngKêDaoanhThuToolStripMenuItem";
            this.thốngKêDaoanhThuToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.thốngKêDaoanhThuToolStripMenuItem.Text = "Thống kê doanh thu";
            this.thốngKêDaoanhThuToolStripMenuItem.Click += new System.EventHandler(this.thốngKêDaoanhThuToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblCurrentUser,
            this.lblCurrentTime});
            this.statusStrip1.Location = new System.Drawing.Point(0, 678);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1200, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblCurrentUser
            // 
            this.lblCurrentUser.Name = "lblCurrentUser";
            this.lblCurrentUser.Size = new System.Drawing.Size(86, 17);
            this.lblCurrentUser.Text = "Người dùng: ---";
            // 
            // lblCurrentTime
            // 
            this.lblCurrentTime.Name = "lblCurrentTime";
            this.lblCurrentTime.Size = new System.Drawing.Size(1099, 17);
            this.lblCurrentTime.Spring = true;
            this.lblCurrentTime.Text = "Thời gian: ---";
            this.lblCurrentTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // panelMain
            // 
            this.panelMain.BackColor = System.Drawing.Color.LightCyan;
            this.panelMain.Controls.Add(this.groupBoxQuickActions);
            this.panelMain.Controls.Add(this.lblWelcome);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 24);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(1200, 654);
            this.panelMain.TabIndex = 2;
            // 
            // lblWelcome
            // 
            this.lblWelcome.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblWelcome.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblWelcome.ForeColor = System.Drawing.Color.DarkBlue;
            this.lblWelcome.Location = new System.Drawing.Point(0, 0);
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Size = new System.Drawing.Size(1200, 80);
            this.lblWelcome.TabIndex = 0;
            this.lblWelcome.Text = "CHÀO MỪNG ĐÃ TRỞ LẠI HỆ THỐNG QUẢN LÝ QUÁN NET";
            this.lblWelcome.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBoxQuickActions
            // 
            this.groupBoxQuickActions.Controls.Add(this.btnThongKe);
            this.groupBoxQuickActions.Controls.Add(this.btnTaoHoaDonThuCong);
            this.groupBoxQuickActions.Controls.Add(this.btnTaoHoaDon);
            this.groupBoxQuickActions.Controls.Add(this.btnQuanLyKhach);
            this.groupBoxQuickActions.Controls.Add(this.btnQuanLyMay);
            this.groupBoxQuickActions.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.groupBoxQuickActions.Location = new System.Drawing.Point(50, 120);
            this.groupBoxQuickActions.Name = "groupBoxQuickActions";
            this.groupBoxQuickActions.Size = new System.Drawing.Size(1100, 260);
            this.groupBoxQuickActions.TabIndex = 1;
            this.groupBoxQuickActions.TabStop = false;
            this.groupBoxQuickActions.Text = "Chức năng nhanh";
            // 
            // btnQuanLyMay
            // 
            this.btnQuanLyMay.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnQuanLyMay.ForeColor = System.Drawing.Color.White;
            this.btnQuanLyMay.Location = new System.Drawing.Point(50, 50);
            this.btnQuanLyMay.Name = "btnQuanLyMay";
            this.btnQuanLyMay.Size = new System.Drawing.Size(200, 100);
            this.btnQuanLyMay.TabIndex = 0;
            this.btnQuanLyMay.Text = "Quản lý\r\nMáy trạm";
            this.btnQuanLyMay.UseVisualStyleBackColor = false;
            this.btnQuanLyMay.Click += new System.EventHandler(this.btnQuanLyMay_Click);
            // 
            // btnQuanLyKhach
            // 
            this.btnQuanLyKhach.BackColor = System.Drawing.Color.Green;
            this.btnQuanLyKhach.ForeColor = System.Drawing.Color.White;
            this.btnQuanLyKhach.Location = new System.Drawing.Point(300, 50);
            this.btnQuanLyKhach.Name = "btnQuanLyKhach";
            this.btnQuanLyKhach.Size = new System.Drawing.Size(200, 100);
            this.btnQuanLyKhach.TabIndex = 1;
            this.btnQuanLyKhach.Text = "Quản lý\r\nKhách hàng";
            this.btnQuanLyKhach.UseVisualStyleBackColor = false;
            this.btnQuanLyKhach.Click += new System.EventHandler(this.btnQuanLyKhach_Click);
            // 
            // btnTaoHoaDon
            // 
            this.btnTaoHoaDon.BackColor = System.Drawing.Color.Orange;
            this.btnTaoHoaDon.ForeColor = System.Drawing.Color.White;
            this.btnTaoHoaDon.Location = new System.Drawing.Point(550, 50);
            this.btnTaoHoaDon.Name = "btnTaoHoaDon";
            this.btnTaoHoaDon.Size = new System.Drawing.Size(200, 100);
            this.btnTaoHoaDon.TabIndex = 2;
            this.btnTaoHoaDon.Text = "Tạo\r\nHóa đơn";
            this.btnTaoHoaDon.UseVisualStyleBackColor = false;
            this.btnTaoHoaDon.Click += new System.EventHandler(this.btnTaoHoaDon_Click);
            // 
            // btnTaoHoaDonThuCong
            // 
            this.btnTaoHoaDonThuCong.BackColor = System.Drawing.Color.DarkOrange;
            this.btnTaoHoaDonThuCong.ForeColor = System.Drawing.Color.White;
            this.btnTaoHoaDonThuCong.Location = new System.Drawing.Point(50, 170);
            this.btnTaoHoaDonThuCong.Name = "btnTaoHoaDonThuCong";
            this.btnTaoHoaDonThuCong.Size = new System.Drawing.Size(200, 100);
            this.btnTaoHoaDonThuCong.TabIndex = 4;
            this.btnTaoHoaDonThuCong.Text = "Tạo hóa đơn\r\nthủ công (Admin)";
            this.btnTaoHoaDonThuCong.UseVisualStyleBackColor = false;
            this.btnTaoHoaDonThuCong.Click += new System.EventHandler(this.btnTaoHoaDonThuCong_Click);
            // 
            // btnThongKe
            // 
            this.btnThongKe.BackColor = System.Drawing.Color.Purple;
            this.btnThongKe.ForeColor = System.Drawing.Color.White;
            this.btnThongKe.Location = new System.Drawing.Point(800, 50);
            this.btnThongKe.Name = "btnThongKe";
            this.btnThongKe.Size = new System.Drawing.Size(200, 100);
            this.btnThongKe.TabIndex = 3;
            this.btnThongKe.Text = "Thống kê\r\nBáo cáo";
            this.btnThongKe.UseVisualStyleBackColor = false;
            this.btnThongKe.Click += new System.EventHandler(this.btnThongKe_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 700);
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Hệ thống Quản lý Quán Net";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.panelMain.ResumeLayout(false);
            this.groupBoxQuickActions.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem hệThốngToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem đăngXuấtToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem thoátToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem quảnLýToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem máyTrạmToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem kháchHàngToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dịchVụToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nhânViênToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hóaĐơnToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tạoHóaĐơnToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tạoHóaĐơnThủCôngToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem danhSáchHóaĐơnToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem báoCáoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem thốngKêDaoanhThuToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblCurrentUser;
        private System.Windows.Forms.ToolStripStatusLabel lblCurrentTime;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.Label lblWelcome;
        private System.Windows.Forms.GroupBox groupBoxQuickActions;
        private System.Windows.Forms.Button btnQuanLyMay;
        private System.Windows.Forms.Button btnQuanLyKhach;
        private System.Windows.Forms.Button btnTaoHoaDon;
        private System.Windows.Forms.Button btnTaoHoaDonThuCong;
        private System.Windows.Forms.Button btnThongKe;
    }
}