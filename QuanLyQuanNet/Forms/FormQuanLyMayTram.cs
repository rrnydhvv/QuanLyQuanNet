using QuanLyQuanNet.Services;

namespace QuanLyQuanNet.Forms
{
    public partial class FormQuanLyMayTram : Form
    {
        private readonly IAuthenticationService _authService;

        public FormQuanLyMayTram(IAuthenticationService authService)
        {
            InitializeComponent();
            _authService = authService;
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // FormQuanLyMayTram
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Name = "FormQuanLyMayTram";
            this.Text = "Quản lý Máy trạm";
            this.StartPosition = FormStartPosition.CenterParent;
            this.ResumeLayout(false);

            // Tạm thời hiển thị thông báo
            var label = new Label
            {
                Text = "Form Quản lý Máy trạm\n(Đang trong quá trình phát triển)",
                AutoSize = false,
                Size = new Size(400, 100),
                Location = new Point(200, 250),
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold)
            };
            this.Controls.Add(label);
        }
    }
}