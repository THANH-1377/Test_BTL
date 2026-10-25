using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;
using BaiTap_WinForms.Data;

namespace BaiTap_WinForms.Forms
{
    public class FrmTaiKhoan : Form
    {
        private DataGridView dgv = null!;
        private Button btnAdd = null!;
        private Button btnReset = null!;

        public FrmTaiKhoan()
        {
            InitializeComponent();
            LoadData();
        }

        private void InitializeComponent()
        {
            this.Text = "Quản lý Tài khoản";
            this.StartPosition = FormStartPosition.CenterParent;
            this.ClientSize = new Size(900, 450);

            dgv = new DataGridView { Dock = DockStyle.Top, Height = 360, ReadOnly = true, SelectionMode = DataGridViewSelectionMode.FullRowSelect, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill };
            dgv.AutoGenerateColumns = false;
            dgv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "MaTaiKhoan", HeaderText = "ID", Visible = false });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "TenDangNhap", HeaderText = "Tên đăng nhập" });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "HoTen", HeaderText = "Nhân viên" });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "TenVaiTro", HeaderText = "Vai trò" });
            dgv.Columns.Add(new DataGridViewCheckBoxColumn { DataPropertyName = "DangHoatDong", HeaderText = "Hoạt động" });

            btnAdd = new Button { Text = "Thêm tài khoản", Left = 20, Top = 370, Width = 140 };
            btnReset = new Button { Text = "Đặt lại mật khẩu", Left = 180, Top = 370, Width = 140 };

            btnAdd.Click += (s, e) => AddDialog();
            btnReset.Click += BtnReset_Click;

            this.Controls.AddRange(new Control[] { dgv, btnAdd, btnReset });
        }

        private void LoadData()
        {
            try
            {
                string sql = @"SELECT tk.MaTaiKhoan, tk.TenDangNhap, tk.DangHoatDong, nv.HoTen, vt.TenVaiTro
                                FROM dbo.TaiKhoan tk
                                JOIN dbo.NhanVien nv ON tk.MaNhanVien = nv.MaNhanVien
                                JOIN dbo.VaiTro vt ON tk.MaVaiTro = vt.MaVaiTro
                                ORDER BY tk.MaTaiKhoan";
                DataTable dt = DatabaseHelper.ExecuteQuery(sql);
                dgv.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải tài khoản: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AddDialog()
        {
            using var dlg = new Form { Text = "Thêm tài khoản", StartPosition = FormStartPosition.CenterParent, Size = new Size(520, 320), FormBorderStyle = FormBorderStyle.FixedDialog };
            var lblNV = new Label { Text = "Nhân viên:", Left = 10, Top = 20, AutoSize = true };
            var cboNV = new ComboBox { Left = 140, Top = 16, Width = 340, DropDownStyle = ComboBoxStyle.DropDownList };
            var lblRole = new Label { Text = "Vai trò:", Left = 10, Top = 60, AutoSize = true };
            var cboRole = new ComboBox { Left = 140, Top = 56, Width = 340, DropDownStyle = ComboBoxStyle.DropDownList };
            var lblUser = new Label { Text = "Tên đăng nhập:", Left = 10, Top = 100, AutoSize = true };
            var txtUser = new TextBox { Left = 140, Top = 96, Width = 340 };
            var lblPwd = new Label { Text = "Mật khẩu:", Left = 10, Top = 140, AutoSize = true };
            var txtPwd = new TextBox { Left = 140, Top = 136, Width = 340, UseSystemPasswordChar = true };
            var chkActive = new CheckBox { Text = "Đang hoạt động", Left = 140, Top = 176, AutoSize = true, Checked = true };

            var btnOk = new Button { Text = "Lưu", Left = 140, Top = 210, Width = 120 }; btnOk.DialogResult = DialogResult.OK;
            var btnCancel = new Button { Text = "Hủy", Left = 280, Top = 210, Width = 120 }; btnCancel.DialogResult = DialogResult.Cancel;

            // Load employees and roles
            try
            {
                var dtNv = DatabaseHelper.ExecuteQuery("SELECT MaNhanVien, HoTen FROM dbo.NhanVien WHERE DangHoatDong = 1 ORDER BY HoTen");
                cboNV.DisplayMember = "HoTen"; cboNV.ValueMember = "MaNhanVien"; cboNV.DataSource = dtNv;
                var dtRole = DatabaseHelper.ExecuteQuery("SELECT MaVaiTro, TenVaiTro FROM dbo.VaiTro ORDER BY MaVaiTro");
                cboRole.DisplayMember = "TenVaiTro"; cboRole.ValueMember = "MaVaiTro"; cboRole.DataSource = dtRole;
            }
            catch { }

            dlg.Controls.AddRange(new Control[] { lblNV, cboNV, lblRole, cboRole, lblUser, txtUser, lblPwd, txtPwd, chkActive, btnOk, btnCancel });

            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                if (string.IsNullOrWhiteSpace(txtUser.Text) || string.IsNullOrWhiteSpace(txtPwd.Text))
                {
                    MessageBox.Show("Vui lòng nhập tên đăng nhập và mật khẩu.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                try
                {
                    string hash = DatabaseHelper.HashPasswordSHA256(txtPwd.Text.Trim());
                    string insertSql = @"INSERT INTO dbo.TaiKhoan (MaNhanVien, MaVaiTro, TenDangNhap, MatKhauBam, DangHoatDong) VALUES (@MaNhanVien, @MaVaiTro, @TenDangNhap, @MatKhauBam, @DangHoatDong)";
                    var p = new SqlParameter[] {
                        new SqlParameter("@MaNhanVien", System.Data.SqlDbType.Int) { Value = Convert.ToInt32(cboNV.SelectedValue) },
                        new SqlParameter("@MaVaiTro", System.Data.SqlDbType.Int) { Value = Convert.ToInt32(cboRole.SelectedValue) },
                        new SqlParameter("@TenDangNhap", System.Data.SqlDbType.NVarChar, 50) { Value = txtUser.Text.Trim() },
                        new SqlParameter("@MatKhauBam", System.Data.SqlDbType.NVarChar, 255) { Value = hash },
                        new SqlParameter("@DangHoatDong", System.Data.SqlDbType.Bit) { Value = chkActive.Checked }
                    };
                    DatabaseHelper.ExecuteNonQuery(insertSql, p);
                    LoadData();
                    MessageBox.Show("Tạo tài khoản thành công.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi tạo tài khoản: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BtnReset_Click(object? sender, EventArgs e)
        {
            if (dgv.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn tài khoản.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var result = MessageBox.Show("Bạn có chắc chắn muốn đặt lại mật khẩu cho tài khoản này về '123456'?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result != DialogResult.Yes) return;

            try
            {
                int id = Convert.ToInt32(dgv.CurrentRow.Cells["MaTaiKhoan"].Value);
                string newHash = DatabaseHelper.HashPasswordSHA256("123456");
                string upd = "UPDATE dbo.TaiKhoan SET MatKhauBam = @MatKhauBam WHERE MaTaiKhoan = @MaTaiKhoan";
                DatabaseHelper.ExecuteNonQuery(upd, new SqlParameter[] { new SqlParameter("@MatKhauBam", System.Data.SqlDbType.NVarChar, 255) { Value = newHash }, new SqlParameter("@MaTaiKhoan", System.Data.SqlDbType.Int) { Value = id } });
                MessageBox.Show("Đặt lại mật khẩu thành công.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi đặt lại mật khẩu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
