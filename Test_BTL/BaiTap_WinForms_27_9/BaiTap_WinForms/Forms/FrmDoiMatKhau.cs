using System;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;
using BaiTap_WinForms.Data;
using BaiTap_WinForms.Models;

namespace BaiTap_WinForms.Forms
{
    public class FrmDoiMatKhau : Form
    {
        private Label lblOld = null!;
        private Label lblNew = null!;
        private Label lblConfirm = null!;
        private TextBox txtOld = null!;
        private TextBox txtNew = null!;
        private TextBox txtConfirm = null!;
        private Button btnChange = null!;
        private Button btnClose = null!;

        public FrmDoiMatKhau()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.Text = "Đổi mật khẩu";
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.ClientSize = new System.Drawing.Size(420, 180);

            lblOld = new Label { Text = "Mật khẩu cũ:", Left = 20, Top = 20, AutoSize = true };
            txtOld = new TextBox { Left = 140, Top = 16, Width = 240, UseSystemPasswordChar = true };

            lblNew = new Label { Text = "Mật khẩu mới:", Left = 20, Top = 56, AutoSize = true };
            txtNew = new TextBox { Left = 140, Top = 52, Width = 240, UseSystemPasswordChar = true };

            lblConfirm = new Label { Text = "Xác nhận mật khẩu:", Left = 20, Top = 92, AutoSize = true };
            txtConfirm = new TextBox { Left = 140, Top = 88, Width = 240, UseSystemPasswordChar = true };

            btnChange = new Button { Text = "Đổi mật khẩu", Left = 140, Top = 128, Width = 120, BackColor = System.Drawing.Color.FromArgb(39, 174, 96), ForeColor = System.Drawing.Color.White }; 
            btnChange.Click += BtnChange_Click;

            btnClose = new Button { Text = "Đóng", Left = 260, Top = 128, Width = 120 }; 
            btnClose.Click += (s, e) => this.Close();

            this.Controls.AddRange(new Control[] { lblOld, txtOld, lblNew, txtNew, lblConfirm, txtConfirm, btnChange, btnClose });
        }

        private void BtnChange_Click(object? sender, EventArgs e)
        {
            string oldPwd = txtOld.Text.Trim();
            string newPwd = txtNew.Text.Trim();
            string confirm = txtConfirm.Text.Trim();

            if (string.IsNullOrEmpty(oldPwd) || string.IsNullOrEmpty(newPwd) || string.IsNullOrEmpty(confirm))
            {
                MessageBox.Show("Vui lòng điền đầy đủ các trường.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (newPwd != confirm)
            {
                MessageBox.Show("Mật khẩu xác nhận không khớp.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                string oldHash = DatabaseHelper.HashPasswordSHA256(oldPwd);
                string sqlCheck = "SELECT MatKhauBam FROM dbo.TaiKhoan WHERE MaTaiKhoan = @MaTaiKhoan";
                var paramCheck = new SqlParameter[] { new SqlParameter("@MaTaiKhoan", System.Data.SqlDbType.Int) { Value = UserSession.MaTaiKhoan } };
                object? dbHashObj = DatabaseHelper.ExecuteScalar(sqlCheck, paramCheck);
                if (dbHashObj == null || dbHashObj == DBNull.Value)
                {
                    MessageBox.Show("Không tìm thấy tài khoản hiện tại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string dbHash = dbHashObj.ToString() ?? string.Empty;
                if (!string.Equals(dbHash, oldHash, StringComparison.OrdinalIgnoreCase))
                {
                    MessageBox.Show("Mật khẩu cũ không đúng.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string newHash = DatabaseHelper.HashPasswordSHA256(newPwd);
                string updateSql = "UPDATE dbo.TaiKhoan SET MatKhauBam = @MatKhauBam WHERE MaTaiKhoan = @MaTaiKhoan";
                var updateParams = new SqlParameter[] {
                    new SqlParameter("@MatKhauBam", System.Data.SqlDbType.NVarChar, 255) { Value = newHash },
                    new SqlParameter("@MaTaiKhoan", System.Data.SqlDbType.Int) { Value = UserSession.MaTaiKhoan }
                };

                int rows = DatabaseHelper.ExecuteNonQuery(updateSql, updateParams);
                if (rows > 0)
                {
                    MessageBox.Show("Đổi mật khẩu thành công.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Không thể cập nhật mật khẩu. Vui lòng thử lại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi đổi mật khẩu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
