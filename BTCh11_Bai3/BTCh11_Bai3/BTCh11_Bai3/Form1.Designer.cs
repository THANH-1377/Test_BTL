namespace BTCh11_Bai3
{
    partial class FormChuyenDi
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            lblNgonNgu = new Label();
            cboNgonNgu = new ComboBox();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            label7 = new Label();
            chkDaHoanThanh = new CheckBox();
            txtTenChuyen = new TextBox();
            txtDiaDiem = new TextBox();
            dtpNgayDi = new DateTimePicker();
            nudSoNguoi = new NumericUpDown();
            nudNganSach = new NumericUpDown();
            cboPhuongTien = new ComboBox();
            btnThem = new Button();
            btnSua = new Button();
            btnXoa = new Button();
            btnLamMoi = new Button();
            groupBox1 = new GroupBox();
            btnXemThoiTiet = new Button();
            prgLoading = new ProgressBar();
            pnlThoiTiet = new Panel();
            lblLoiKhuyen = new Label();
            lblXacSuatMua = new Label();
            lblNhietDo = new Label();
            lblMoTaThoiTiet = new Label();
            lblWeatherIcon = new Label();
            lblTieuDeThoiTiet = new Label();
            txtTimKiem = new TextBox();
            chkLocChuaHoanThanh = new CheckBox();
            btnTim = new Button();
            dgvChuyenDi = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)nudSoNguoi).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudNganSach).BeginInit();
            groupBox1.SuspendLayout();
            pnlThoiTiet.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvChuyenDi).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(11, 6);
            label1.Name = "label1";
            label1.Size = new Size(318, 31);
            label1.TabIndex = 0;
            label1.Text = "Kế hoạch dã ngoại cuối tuần";
            // 
            // lblNgonNgu
            // 
            lblNgonNgu.AutoSize = true;
            lblNgonNgu.Location = new Point(614, 17);
            lblNgonNgu.Name = "lblNgonNgu";
            lblNgonNgu.Size = new Size(76, 20);
            lblNgonNgu.TabIndex = 1;
            lblNgonNgu.Text = "Ngôn ngữ";
            // 
            // cboNgonNgu
            // 
            cboNgonNgu.FormattingEnabled = true;
            cboNgonNgu.Items.AddRange(new object[] { "Tiếng Việt", "English" });
            cboNgonNgu.Location = new Point(696, 12);
            cboNgonNgu.Name = "cboNgonNgu";
            cboNgonNgu.Size = new Size(206, 28);
            cboNgonNgu.TabIndex = 2;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(11, 35);
            label2.Name = "label2";
            label2.Size = new Size(82, 20);
            label2.TabIndex = 3;
            label2.Text = "Tên chuyến";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(10, 69);
            label3.Name = "label3";
            label3.Size = new Size(70, 20);
            label3.TabIndex = 4;
            label3.Text = "Địa điểm";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(11, 102);
            label4.Name = "label4";
            label4.Size = new Size(61, 20);
            label4.TabIndex = 5;
            label4.Text = "Ngày đi";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(11, 138);
            label5.Name = "label5";
            label5.Size = new Size(69, 20);
            label5.TabIndex = 6;
            label5.Text = "Số người";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(10, 170);
            label6.Name = "label6";
            label6.Size = new Size(78, 20);
            label6.TabIndex = 7;
            label6.Text = "Ngân sách";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(10, 204);
            label7.Name = "label7";
            label7.Size = new Size(89, 20);
            label7.TabIndex = 8;
            label7.Text = "Phương tiện";
            // 
            // chkDaHoanThanh
            // 
            chkDaHoanThanh.AutoSize = true;
            chkDaHoanThanh.Checked = true;
            chkDaHoanThanh.CheckState = CheckState.Checked;
            chkDaHoanThanh.Location = new Point(108, 241);
            chkDaHoanThanh.Name = "chkDaHoanThanh";
            chkDaHoanThanh.Size = new Size(128, 24);
            chkDaHoanThanh.TabIndex = 9;
            chkDaHoanThanh.Text = "Đã hoàn thành";
            chkDaHoanThanh.UseVisualStyleBackColor = true;
            // 
            // txtTenChuyen
            // 
            txtTenChuyen.Location = new Point(108, 28);
            txtTenChuyen.Name = "txtTenChuyen";
            txtTenChuyen.Size = new Size(216, 27);
            txtTenChuyen.TabIndex = 10;
            // 
            // txtDiaDiem
            // 
            txtDiaDiem.Location = new Point(108, 62);
            txtDiaDiem.Name = "txtDiaDiem";
            txtDiaDiem.Size = new Size(216, 27);
            txtDiaDiem.TabIndex = 11;
            // 
            // dtpNgayDi
            // 
            dtpNgayDi.CustomFormat = "dd/MM/yyyy";
            dtpNgayDi.Format = DateTimePickerFormat.Custom;
            dtpNgayDi.Location = new Point(108, 97);
            dtpNgayDi.Name = "dtpNgayDi";
            dtpNgayDi.Size = new Size(153, 27);
            dtpNgayDi.TabIndex = 12;
            // 
            // nudSoNguoi
            // 
            nudSoNguoi.Location = new Point(108, 130);
            nudSoNguoi.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            nudSoNguoi.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudSoNguoi.Name = "nudSoNguoi";
            nudSoNguoi.Size = new Size(126, 27);
            nudSoNguoi.TabIndex = 13;
            nudSoNguoi.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // nudNganSach
            // 
            nudNganSach.Increment = new decimal(new int[] { 100000, 0, 0, 0 });
            nudNganSach.Location = new Point(108, 163);
            nudNganSach.Maximum = new decimal(new int[] { 1000000000, 0, 0, 0 });
            nudNganSach.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudNganSach.Name = "nudNganSach";
            nudNganSach.Size = new Size(126, 27);
            nudNganSach.TabIndex = 14;
            nudNganSach.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // cboPhuongTien
            // 
            cboPhuongTien.FormattingEnabled = true;
            cboPhuongTien.Items.AddRange(new object[] { "Xe máy", "Ô tô", "Xe khách", "Tàu lửa" });
            cboPhuongTien.Location = new Point(108, 196);
            cboPhuongTien.Name = "cboPhuongTien";
            cboPhuongTien.Size = new Size(153, 28);
            cboPhuongTien.TabIndex = 15;
            // 
            // btnThem
            // 
            btnThem.Location = new Point(6, 271);
            btnThem.Name = "btnThem";
            btnThem.Size = new Size(111, 45);
            btnThem.TabIndex = 16;
            btnThem.Text = "Thêm";
            btnThem.UseVisualStyleBackColor = true;
            btnThem.Click += btnThem_Click;
            // 
            // btnSua
            // 
            btnSua.Location = new Point(135, 271);
            btnSua.Name = "btnSua";
            btnSua.Size = new Size(111, 45);
            btnSua.TabIndex = 17;
            btnSua.Text = "Sửa";
            btnSua.UseVisualStyleBackColor = true;
            btnSua.Click += btnSua_Click;
            // 
            // btnXoa
            // 
            btnXoa.Location = new Point(272, 271);
            btnXoa.Name = "btnXoa";
            btnXoa.Size = new Size(111, 45);
            btnXoa.TabIndex = 18;
            btnXoa.Text = "Xóa";
            btnXoa.UseVisualStyleBackColor = true;
            btnXoa.Click += btnXoa_Click;
            // 
            // btnLamMoi
            // 
            btnLamMoi.Location = new Point(411, 271);
            btnLamMoi.Name = "btnLamMoi";
            btnLamMoi.Size = new Size(111, 45);
            btnLamMoi.TabIndex = 19;
            btnLamMoi.Text = "Làm mới";
            btnLamMoi.UseVisualStyleBackColor = true;
            btnLamMoi.Click += btnLamMoi_Click;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(cboPhuongTien);
            groupBox1.Controls.Add(btnLamMoi);
            groupBox1.Controls.Add(nudNganSach);
            groupBox1.Controls.Add(btnXoa);
            groupBox1.Controls.Add(nudSoNguoi);
            groupBox1.Controls.Add(btnSua);
            groupBox1.Controls.Add(dtpNgayDi);
            groupBox1.Controls.Add(btnThem);
            groupBox1.Controls.Add(txtDiaDiem);
            groupBox1.Controls.Add(txtTenChuyen);
            groupBox1.Controls.Add(chkDaHoanThanh);
            groupBox1.Controls.Add(label7);
            groupBox1.Controls.Add(label6);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(label2);
            groupBox1.Location = new Point(12, 55);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(528, 327);
            groupBox1.TabIndex = 20;
            groupBox1.TabStop = false;
            groupBox1.Text = "THÔNG TIN CHUYẾN ĐI";
            // 
            // btnXemThoiTiet
            // 
            btnXemThoiTiet.Location = new Point(546, 67);
            btnXemThoiTiet.Name = "btnXemThoiTiet";
            btnXemThoiTiet.Size = new Size(139, 44);
            btnXemThoiTiet.TabIndex = 21;
            btnXemThoiTiet.Text = "Xem thời tiết";
            btnXemThoiTiet.UseVisualStyleBackColor = true;
            btnXemThoiTiet.Click += btnXemThoiTiet_Click;
            // 
            // prgLoading
            // 
            prgLoading.Location = new Point(691, 75);
            prgLoading.Name = "prgLoading";
            prgLoading.Size = new Size(211, 27);
            prgLoading.Style = ProgressBarStyle.Marquee;
            prgLoading.TabIndex = 22;
            prgLoading.Visible = false;
            // 
            // pnlThoiTiet
            // 
            pnlThoiTiet.Controls.Add(lblLoiKhuyen);
            pnlThoiTiet.Controls.Add(lblXacSuatMua);
            pnlThoiTiet.Controls.Add(lblNhietDo);
            pnlThoiTiet.Controls.Add(lblMoTaThoiTiet);
            pnlThoiTiet.Controls.Add(lblWeatherIcon);
            pnlThoiTiet.Controls.Add(lblTieuDeThoiTiet);
            pnlThoiTiet.Location = new Point(546, 117);
            pnlThoiTiet.Name = "pnlThoiTiet";
            pnlThoiTiet.Size = new Size(356, 265);
            pnlThoiTiet.TabIndex = 23;
            // 
            // lblLoiKhuyen
            // 
            lblLoiKhuyen.AutoSize = true;
            lblLoiKhuyen.BackColor = Color.Orange;
            lblLoiKhuyen.Location = new Point(3, 183);
            lblLoiKhuyen.Name = "lblLoiKhuyen";
            lblLoiKhuyen.Size = new Size(309, 20);
            lblLoiKhuyen.TabIndex = 5;
            lblLoiKhuyen.Text = "⚠️ Nên mang áo mưa hoặc cân nhắc dời lịch";
            lblLoiKhuyen.Visible = false;
            // 
            // lblXacSuatMua
            // 
            lblXacSuatMua.AutoSize = true;
            lblXacSuatMua.Location = new Point(160, 117);
            lblXacSuatMua.Name = "lblXacSuatMua";
            lblXacSuatMua.Size = new Size(140, 20);
            lblXacSuatMua.TabIndex = 4;
            lblXacSuatMua.Text = "Khả năng mưa: 65%";
            // 
            // lblNhietDo
            // 
            lblNhietDo.AutoSize = true;
            lblNhietDo.Location = new Point(160, 86);
            lblNhietDo.Name = "lblNhietDo";
            lblNhietDo.Size = new Size(150, 20);
            lblNhietDo.TabIndex = 3;
            lblNhietDo.Text = "Nhiệt độ: 14°C - 22°C";
            // 
            // lblMoTaThoiTiet
            // 
            lblMoTaThoiTiet.AutoSize = true;
            lblMoTaThoiTiet.Location = new Point(194, 54);
            lblMoTaThoiTiet.Name = "lblMoTaThoiTiet";
            lblMoTaThoiTiet.Size = new Size(59, 20);
            lblMoTaThoiTiet.TabIndex = 2;
            lblMoTaThoiTiet.Text = "Có mây";
            // 
            // lblWeatherIcon
            // 
            lblWeatherIcon.Font = new Font("Segoe UI", 48F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblWeatherIcon.Location = new Point(3, 33);
            lblWeatherIcon.Name = "lblWeatherIcon";
            lblWeatherIcon.Size = new Size(137, 117);
            lblWeatherIcon.TabIndex = 1;
            lblWeatherIcon.Text = "⛅";
            // 
            // lblTieuDeThoiTiet
            // 
            lblTieuDeThoiTiet.AutoSize = true;
            lblTieuDeThoiTiet.Location = new Point(38, 7);
            lblTieuDeThoiTiet.Name = "lblTieuDeThoiTiet";
            lblTieuDeThoiTiet.Size = new Size(252, 20);
            lblTieuDeThoiTiet.TabIndex = 0;
            lblTieuDeThoiTiet.Text = "Dự báo thời tiết 26/10/2026 - Đà Lạt";
            // 
            // txtTimKiem
            // 
            txtTimKiem.Location = new Point(16, 389);
            txtTimKiem.Name = "txtTimKiem";
            txtTimKiem.PlaceholderText = "Tìm địa điểm...";
            txtTimKiem.Size = new Size(147, 27);
            txtTimKiem.TabIndex = 24;
            // 
            // chkLocChuaHoanThanh
            // 
            chkLocChuaHoanThanh.AutoSize = true;
            chkLocChuaHoanThanh.Location = new Point(182, 394);
            chkLocChuaHoanThanh.Name = "chkLocChuaHoanThanh";
            chkLocChuaHoanThanh.Size = new Size(248, 24);
            chkLocChuaHoanThanh.TabIndex = 25;
            chkLocChuaHoanThanh.Text = "Chỉ hiện chuyến chưa hoàn thành";
            chkLocChuaHoanThanh.UseVisualStyleBackColor = true;
            // 
            // btnTim
            // 
            btnTim.Location = new Point(436, 388);
            btnTim.Name = "btnTim";
            btnTim.Size = new Size(87, 33);
            btnTim.TabIndex = 26;
            btnTim.Text = "Tìm";
            btnTim.UseVisualStyleBackColor = true;
            btnTim.Click += btnTim_Click;
            // 
            // dgvChuyenDi
            // 
            dgvChuyenDi.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvChuyenDi.Location = new Point(3, 422);
            dgvChuyenDi.Name = "dgvChuyenDi";
            dgvChuyenDi.ReadOnly = true;
            dgvChuyenDi.RowHeadersWidth = 51;
            dgvChuyenDi.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvChuyenDi.Size = new Size(899, 177);
            dgvChuyenDi.TabIndex = 27;
            dgvChuyenDi.CellClick += dgvChuyenDi_CellClick;
            // 
            // FormChuyenDi
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(914, 603);
            Controls.Add(dgvChuyenDi);
            Controls.Add(btnTim);
            Controls.Add(chkLocChuaHoanThanh);
            Controls.Add(txtTimKiem);
            Controls.Add(pnlThoiTiet);
            Controls.Add(prgLoading);
            Controls.Add(btnXemThoiTiet);
            Controls.Add(groupBox1);
            Controls.Add(cboNgonNgu);
            Controls.Add(lblNgonNgu);
            Controls.Add(label1);
            Name = "FormChuyenDi";
            Text = "Kế hoạch dã ngoại cuối tuần";
            Load += FormChuyenDi_Load;
            ((System.ComponentModel.ISupportInitialize)nudSoNguoi).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudNganSach).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            pnlThoiTiet.ResumeLayout(false);
            pnlThoiTiet.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvChuyenDi).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label lblNgonNgu;
        private ComboBox cboNgonNgu;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private CheckBox chkDaHoanThanh;
        private TextBox txtTenChuyen;
        private TextBox txtDiaDiem;
        private DateTimePicker dtpNgayDi;
        private NumericUpDown nudSoNguoi;
        private NumericUpDown nudNganSach;
        private ComboBox cboPhuongTien;
        private Button btnThem;
        private Button btnSua;
        private Button btnXoa;
        private Button btnLamMoi;
        private GroupBox groupBox1;
        private Button btnXemThoiTiet;
        private ProgressBar prgLoading;
        private Panel pnlThoiTiet;
        private Label lblWeatherIcon;
        private Label lblTieuDeThoiTiet;
        private Label lblLoiKhuyen;
        private Label lblXacSuatMua;
        private Label lblNhietDo;
        private Label lblMoTaThoiTiet;
        private TextBox txtTimKiem;
        private CheckBox chkLocChuaHoanThanh;
        private Button btnTim;
        private DataGridView dgvChuyenDi;
    }
}
