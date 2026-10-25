namespace BaiTap_WinForms.Forms
{
    partial class FormMain
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Panel panelSidebar;
        private System.Windows.Forms.Panel panelContent;
        private System.Windows.Forms.Label lblHeaderTitle;
        private System.Windows.Forms.Label lblUserInfo;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.Button btnNavPOS;
        private System.Windows.Forms.Button btnNavProducts;
        private System.Windows.Forms.Button btnNavIngredients;
        private System.Windows.Forms.Button btnNavReports;
        private System.Windows.Forms.Button btnNavEmployees;
        private System.Windows.Forms.TabControl tabMain;
        private System.Windows.Forms.TabPage tabPOS;
        private System.Windows.Forms.TabPage tabProducts;
        private System.Windows.Forms.TabPage tabIngredients;
        private System.Windows.Forms.TabPage tabReports;
        private System.Windows.Forms.TabPage tabEmployees;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            panelTop = new Panel();
            lblHeaderTitle = new Label();
            lblUserInfo = new Label();
            btnLogout = new Button();
            panelSidebar = new Panel();
            btnNavEmployees = new Button();
            btnNavReports = new Button();
            btnNavIngredients = new Button();
            btnNavProducts = new Button();
            btnNavPOS = new Button();
            panelContent = new Panel();
            tabMain = new TabControl();
            tabPOS = new TabPage();
            tabProducts = new TabPage();
            tabIngredients = new TabPage();
            tabReports = new TabPage();
            tabEmployees = new TabPage();
            panelTop.SuspendLayout();
            panelSidebar.SuspendLayout();
            panelContent.SuspendLayout();
            tabMain.SuspendLayout();
            SuspendLayout();
            // 
            // panelTop
            // 
            panelTop.BackColor = Color.FromArgb(211, 84, 0);
            panelTop.Controls.Add(lblHeaderTitle);
            panelTop.Controls.Add(lblUserInfo);
            panelTop.Controls.Add(btnLogout);
            panelTop.Dock = DockStyle.Top;
            panelTop.Location = new Point(0, 0);
            panelTop.Name = "panelTop";
            panelTop.Size = new Size(1389, 65);
            panelTop.TabIndex = 2;
            panelTop.Paint += panelTop_Paint;
            // 
            // lblHeaderTitle
            // 
            lblHeaderTitle.AutoSize = true;
            lblHeaderTitle.Font = new Font("Segoe UI", 15F, FontStyle.Bold);
            lblHeaderTitle.ForeColor = Color.White;
            lblHeaderTitle.Location = new Point(20, 16);
            lblHeaderTitle.Name = "lblHeaderTitle";
            lblHeaderTitle.Size = new Size(596, 35);
            lblHeaderTitle.TabIndex = 0;
            lblHeaderTitle.Text = "\U0001f96a TIỆM BÁNH MÌ NGON - QUẢN LÝ BÁN HÀNG";
            // 
            // lblUserInfo
            // 
            lblUserInfo.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblUserInfo.AutoSize = true;
            lblUserInfo.Font = new Font("Segoe UI", 10.5F);
            lblUserInfo.ForeColor = Color.FromArgb(254, 237, 222);
            lblUserInfo.Location = new Point(2089, 22);
            lblUserInfo.Name = "lblUserInfo";
            lblUserInfo.Size = new Size(218, 25);
            lblUserInfo.TabIndex = 1;
            lblUserInfo.Text = "Xin chào: Admin (Quản lý)";
            // 
            // btnLogout
            // 
            btnLogout.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnLogout.BackColor = Color.FromArgb(192, 57, 43);
            btnLogout.Cursor = Cursors.Hand;
            btnLogout.FlatAppearance.BorderSize = 0;
            btnLogout.FlatStyle = FlatStyle.Flat;
            btnLogout.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnLogout.ForeColor = Color.White;
            btnLogout.Location = new Point(2359, 16);
            btnLogout.Name = "btnLogout";
            btnLogout.Size = new Size(95, 34);
            btnLogout.TabIndex = 2;
            btnLogout.Text = "Đăng xuất";
            btnLogout.UseVisualStyleBackColor = false;
            btnLogout.Click += btnLogout_Click;
            // 
            // panelSidebar
            // 
            panelSidebar.BackColor = Color.FromArgb(44, 62, 80);
            panelSidebar.Controls.Add(btnNavEmployees);
            panelSidebar.Controls.Add(btnNavReports);
            panelSidebar.Controls.Add(btnNavIngredients);
            panelSidebar.Controls.Add(btnNavProducts);
            panelSidebar.Controls.Add(btnNavPOS);
            panelSidebar.Dock = DockStyle.Left;
            panelSidebar.Location = new Point(0, 65);
            panelSidebar.Name = "panelSidebar";
            panelSidebar.Size = new Size(220, 695);
            panelSidebar.TabIndex = 1;
            // 
            // btnNavEmployees
            // 
            btnNavEmployees.BackColor = Color.FromArgb(44, 62, 80);
            btnNavEmployees.Cursor = Cursors.Hand;
            btnNavEmployees.Dock = DockStyle.Top;
            btnNavEmployees.FlatAppearance.BorderSize = 0;
            btnNavEmployees.FlatStyle = FlatStyle.Flat;
            btnNavEmployees.Font = new Font("Segoe UI", 11F);
            btnNavEmployees.ForeColor = Color.Gainsboro;
            btnNavEmployees.Location = new Point(0, 220);
            btnNavEmployees.Name = "btnNavEmployees";
            btnNavEmployees.Padding = new Padding(15, 0, 0, 0);
            btnNavEmployees.Size = new Size(220, 55);
            btnNavEmployees.TabIndex = 0;
            btnNavEmployees.Text = "👥  Nhân Viên";
            btnNavEmployees.TextAlign = ContentAlignment.MiddleLeft;
            btnNavEmployees.UseVisualStyleBackColor = false;
            btnNavEmployees.Click += btnNav_Click;
            // 
            // btnNavReports
            // 
            btnNavReports.BackColor = Color.FromArgb(44, 62, 80);
            btnNavReports.Cursor = Cursors.Hand;
            btnNavReports.Dock = DockStyle.Top;
            btnNavReports.FlatAppearance.BorderSize = 0;
            btnNavReports.FlatStyle = FlatStyle.Flat;
            btnNavReports.Font = new Font("Segoe UI", 11F);
            btnNavReports.ForeColor = Color.Gainsboro;
            btnNavReports.Location = new Point(0, 165);
            btnNavReports.Name = "btnNavReports";
            btnNavReports.Padding = new Padding(15, 0, 0, 0);
            btnNavReports.Size = new Size(220, 55);
            btnNavReports.TabIndex = 1;
            btnNavReports.Text = "📊  Báo Cáo Doanh Thu";
            btnNavReports.TextAlign = ContentAlignment.MiddleLeft;
            btnNavReports.UseVisualStyleBackColor = false;
            btnNavReports.Click += btnNav_Click;
            // 
            // btnNavIngredients
            // 
            btnNavIngredients.BackColor = Color.FromArgb(44, 62, 80);
            btnNavIngredients.Cursor = Cursors.Hand;
            btnNavIngredients.Dock = DockStyle.Top;
            btnNavIngredients.FlatAppearance.BorderSize = 0;
            btnNavIngredients.FlatStyle = FlatStyle.Flat;
            btnNavIngredients.Font = new Font("Segoe UI", 11F);
            btnNavIngredients.ForeColor = Color.Gainsboro;
            btnNavIngredients.Location = new Point(0, 110);
            btnNavIngredients.Name = "btnNavIngredients";
            btnNavIngredients.Padding = new Padding(15, 0, 0, 0);
            btnNavIngredients.Size = new Size(220, 55);
            btnNavIngredients.TabIndex = 2;
            btnNavIngredients.Text = "\U0001f96c  Kho Nguyên Liệu";
            btnNavIngredients.TextAlign = ContentAlignment.MiddleLeft;
            btnNavIngredients.UseVisualStyleBackColor = false;
            btnNavIngredients.Click += btnNav_Click;
            // 
            // btnNavProducts
            // 
            btnNavProducts.BackColor = Color.FromArgb(44, 62, 80);
            btnNavProducts.Cursor = Cursors.Hand;
            btnNavProducts.Dock = DockStyle.Top;
            btnNavProducts.FlatAppearance.BorderSize = 0;
            btnNavProducts.FlatStyle = FlatStyle.Flat;
            btnNavProducts.Font = new Font("Segoe UI", 11F);
            btnNavProducts.ForeColor = Color.Gainsboro;
            btnNavProducts.Location = new Point(0, 55);
            btnNavProducts.Name = "btnNavProducts";
            btnNavProducts.Padding = new Padding(15, 0, 0, 0);
            btnNavProducts.Size = new Size(220, 55);
            btnNavProducts.TabIndex = 3;
            btnNavProducts.Text = "\U0001f96a  Thực Đơn (Món)";
            btnNavProducts.TextAlign = ContentAlignment.MiddleLeft;
            btnNavProducts.UseVisualStyleBackColor = false;
            btnNavProducts.Click += btnNav_Click;
            // 
            // btnNavPOS
            // 
            btnNavPOS.BackColor = Color.FromArgb(230, 126, 34);
            btnNavPOS.Cursor = Cursors.Hand;
            btnNavPOS.Dock = DockStyle.Top;
            btnNavPOS.FlatAppearance.BorderSize = 0;
            btnNavPOS.FlatStyle = FlatStyle.Flat;
            btnNavPOS.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnNavPOS.ForeColor = Color.White;
            btnNavPOS.Location = new Point(0, 0);
            btnNavPOS.Name = "btnNavPOS";
            btnNavPOS.Padding = new Padding(15, 0, 0, 0);
            btnNavPOS.Size = new Size(220, 55);
            btnNavPOS.TabIndex = 4;
            btnNavPOS.Text = "\U0001f6d2  BÁN HÀNG (POS)";
            btnNavPOS.TextAlign = ContentAlignment.MiddleLeft;
            btnNavPOS.UseVisualStyleBackColor = false;
            btnNavPOS.Click += btnNav_Click;
            // 
            // panelContent
            // 
            panelContent.Controls.Add(tabMain);
            panelContent.Dock = DockStyle.Fill;
            panelContent.Location = new Point(220, 65);
            panelContent.Name = "panelContent";
            panelContent.Size = new Size(1169, 695);
            panelContent.TabIndex = 0;
            // 
            // tabMain
            // 
            tabMain.Appearance = TabAppearance.FlatButtons;
            tabMain.Controls.Add(tabPOS);
            tabMain.Controls.Add(tabProducts);
            tabMain.Controls.Add(tabIngredients);
            tabMain.Controls.Add(tabReports);
            tabMain.Controls.Add(tabEmployees);
            tabMain.Dock = DockStyle.Fill;
            tabMain.ItemSize = new Size(0, 1);
            tabMain.Location = new Point(0, 0);
            tabMain.Name = "tabMain";
            tabMain.SelectedIndex = 0;
            tabMain.Size = new Size(1169, 695);
            tabMain.SizeMode = TabSizeMode.Fixed;
            tabMain.TabIndex = 0;
            // 
            // tabPOS
            // 
            tabPOS.Location = new Point(4, 5);
            tabPOS.Name = "tabPOS";
            tabPOS.Size = new Size(1161, 686);
            tabPOS.TabIndex = 0;
            // 
            // tabProducts
            // 
            tabProducts.Location = new Point(4, 5);
            tabProducts.Name = "tabProducts";
            tabProducts.Size = new Size(1161, 686);
            tabProducts.TabIndex = 1;
            // 
            // tabIngredients
            // 
            tabIngredients.Location = new Point(4, 5);
            tabIngredients.Name = "tabIngredients";
            tabIngredients.Size = new Size(1161, 686);
            tabIngredients.TabIndex = 2;
            // 
            // tabReports
            // 
            tabReports.Location = new Point(4, 5);
            tabReports.Name = "tabReports";
            tabReports.Size = new Size(1161, 686);
            tabReports.TabIndex = 3;
            // 
            // tabEmployees
            // 
            tabEmployees.Location = new Point(4, 5);
            tabEmployees.Name = "tabEmployees";
            tabEmployees.Size = new Size(1161, 686);
            tabEmployees.TabIndex = 4;
            // 
            // FormMain
            // 
            ClientSize = new Size(1389, 760);
            Controls.Add(panelContent);
            Controls.Add(panelSidebar);
            Controls.Add(panelTop);
            MinimumSize = new Size(1100, 700);
            Name = "FormMain";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Hệ Thống Quản Lý & Bán Hàng Quán Bánh Mì";
            WindowState = FormWindowState.Maximized;
            Load += FormMain_Load;
            panelTop.ResumeLayout(false);
            panelTop.PerformLayout();
            panelSidebar.ResumeLayout(false);
            panelContent.ResumeLayout(false);
            tabMain.ResumeLayout(false);
            ResumeLayout(false);
        }
    }
}
