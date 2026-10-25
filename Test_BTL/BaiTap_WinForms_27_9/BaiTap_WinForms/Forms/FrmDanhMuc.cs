using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;
using BaiTap_WinForms.Data;

namespace BaiTap_WinForms.Forms
{
    public class FrmDanhMuc : Form
    {
        private DataGridView dgv = null!;
        private Button btnAdd = null!;
        private Button btnEdit = null!;
        private Button btnDelete = null!;

        public FrmDanhMuc()
        {
            InitializeComponent();
            LoadData();
        }

        private void InitializeComponent()
        {
            this.Text = "Danh mục";
            this.StartPosition = FormStartPosition.CenterParent;
            this.ClientSize = new Size(600, 400);

            dgv = new DataGridView { Dock = DockStyle.Top, Height = 320, ReadOnly = true, SelectionMode = DataGridViewSelectionMode.FullRowSelect, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill };
            dgv.AutoGenerateColumns = false;
            dgv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "MaDanhMuc", HeaderText = "ID", Visible = false });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "TenDanhMuc", HeaderText = "Tên danh mục" });
            dgv.Columns.Add(new DataGridViewCheckBoxColumn { DataPropertyName = "DangHoatDong", HeaderText = "Hoạt động" });

            btnAdd = new Button { Text = "Thêm", Left = 20, Top = 330, Width = 100 };
            btnEdit = new Button { Text = "Sửa", Left = 140, Top = 330, Width = 100 };
            btnDelete = new Button { Text = "Xóa", Left = 260, Top = 330, Width = 100 };

            btnAdd.Click += (s, e) => EditDialog(null);
            btnEdit.Click += (s, e) => { if (dgv.CurrentRow != null) EditDialog(dgv.CurrentRow); };
            btnDelete.Click += BtnDelete_Click;

            this.Controls.AddRange(new Control[] { dgv, btnAdd, btnEdit, btnDelete });
        }

        private void LoadData()
        {
            try
            {
                string sql = "SELECT MaDanhMuc, TenDanhMuc, DangHoatDong FROM dbo.DanhMuc ORDER BY MaDanhMuc";
                DataTable dt = DatabaseHelper.ExecuteQuery(sql);
                dgv.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh mục: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void EditDialog(DataGridViewRow? row)
        {
            using var dlg = new Form { Text = row == null ? "Thêm danh mục" : "Sửa danh mục", StartPosition = FormStartPosition.CenterParent, Size = new Size(420, 200), FormBorderStyle = FormBorderStyle.FixedDialog };
            var lblName = new Label { Text = "Tên danh mục:", Left = 10, Top = 20, AutoSize = true };
            var txtName = new TextBox { Left = 140, Top = 16, Width = 240 };
            var chkActive = new CheckBox { Text = "Đang hoạt động", Left = 140, Top = 56, AutoSize = true, Checked = true };
            var btnOk = new Button { Text = "Lưu", Left = 140, Top = 96, Width = 100 }; btnOk.DialogResult = DialogResult.OK;
            var btnCancel = new Button { Text = "Hủy", Left = 260, Top = 96, Width = 100 }; btnCancel.DialogResult = DialogResult.Cancel;

            if (row != null)
            {
                txtName.Text = row.Cells["TenDanhMuc"].Value?.ToString() ?? string.Empty;
                chkActive.Checked = Convert.ToBoolean(row.Cells["DangHoatDong"].Value ?? true);
            }

            dlg.Controls.AddRange(new Control[] { lblName, txtName, chkActive, btnOk, btnCancel });
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                try
                {
                    if (row == null)
                    {
                        string insertSql = "INSERT INTO dbo.DanhMuc (TenDanhMuc, DangHoatDong) VALUES (@Ten, @Active)";
                        var p = new SqlParameter[] {
                            new SqlParameter("@Ten", System.Data.SqlDbType.NVarChar, 100) { Value = txtName.Text.Trim() },
                            new SqlParameter("@Active", System.Data.SqlDbType.Bit) { Value = chkActive.Checked }
                        };
                        DatabaseHelper.ExecuteNonQuery(insertSql, p);
                    }
                    else
                    {
                        string updateSql = "UPDATE dbo.DanhMuc SET TenDanhMuc=@Ten, DangHoatDong=@Active WHERE MaDanhMuc=@MaDanhMuc";
                        var p = new SqlParameter[] {
                            new SqlParameter("@Ten", System.Data.SqlDbType.NVarChar, 100) { Value = txtName.Text.Trim() },
                            new SqlParameter("@Active", System.Data.SqlDbType.Bit) { Value = chkActive.Checked },
                            new SqlParameter("@MaDanhMuc", System.Data.SqlDbType.Int) { Value = Convert.ToInt32(row.Cells["MaDanhMuc"].Value) }
                        };
                        DatabaseHelper.ExecuteNonQuery(updateSql, p);
                    }
                    LoadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi lưu danh mục: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BtnDelete_Click(object? sender, EventArgs e)
        {
            if (dgv.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn danh mục để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var result = MessageBox.Show("Bạn có chắc chắn muốn xóa danh mục này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result != DialogResult.Yes) return;

            try
            {
                int id = Convert.ToInt32(dgv.CurrentRow.Cells["MaDanhMuc"].Value);
                string delSql = "DELETE FROM dbo.DanhMuc WHERE MaDanhMuc = @MaDanhMuc";
                DatabaseHelper.ExecuteNonQuery(delSql, new SqlParameter[] { new SqlParameter("@MaDanhMuc", System.Data.SqlDbType.Int) { Value = id } });
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xóa danh mục: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
