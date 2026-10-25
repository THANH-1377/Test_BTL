using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;
using BaiTap_WinForms.Data;

namespace BaiTap_WinForms.Forms
{
    public class FrmChiNhanh : Form
    {
        private DataGridView dgv = null!;
        private Button btnAdd = null!;
        private Button btnEdit = null!;
        private Button btnDelete = null!;

        public FrmChiNhanh()
        {
            InitializeComponent();
            LoadData();
        }

        private void InitializeComponent()
        {
            this.Text = "Quản lý chi nhánh";
            this.StartPosition = FormStartPosition.CenterParent;
            this.ClientSize = new Size(800, 450);

            dgv = new DataGridView { Dock = DockStyle.Top, Height = 360, ReadOnly = true, SelectionMode = DataGridViewSelectionMode.FullRowSelect, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill };
            dgv.AutoGenerateColumns = false;
            dgv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "MaChiNhanh", HeaderText = "ID", Visible = false });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "MaCode", HeaderText = "Mã" });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "TenChiNhanh", HeaderText = "Tên chi nhánh" });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "LoaiChiNhanh", HeaderText = "Loại" });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "DiaChi", HeaderText = "Địa chỉ" });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "SoDienThoai", HeaderText = "SĐT" });
            dgv.Columns.Add(new DataGridViewCheckBoxColumn { DataPropertyName = "DangHoatDong", HeaderText = "Hoạt động" });

            btnAdd = new Button { Text = "Thêm", Left = 20, Top = 370, Width = 100 };
            btnEdit = new Button { Text = "Sửa", Left = 140, Top = 370, Width = 100 };
            btnDelete = new Button { Text = "Xóa", Left = 260, Top = 370, Width = 100 };

            btnAdd.Click += (s, e) => EditDialog(null);
            btnEdit.Click += (s, e) => { if (dgv.CurrentRow != null) EditDialog(dgv.CurrentRow); };
            btnDelete.Click += BtnDelete_Click;

            this.Controls.AddRange(new Control[] { dgv, btnAdd, btnEdit, btnDelete });
        }

        private void LoadData()
        {
            try
            {
                string sql = "SELECT MaChiNhanh, MaCode, TenChiNhanh, LoaiChiNhanh, DiaChi, SoDienThoai, DangHoatDong FROM dbo.ChiNhanh ORDER BY MaChiNhanh";
                DataTable dt = DatabaseHelper.ExecuteQuery(sql);
                dgv.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải chi nhánh: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void EditDialog(DataGridViewRow? row)
        {
            using var dlg = new Form { Text = row == null ? "Thêm chi nhánh" : "Sửa chi nhánh", StartPosition = FormStartPosition.CenterParent, Size = new Size(420, 300), FormBorderStyle = FormBorderStyle.FixedDialog };

                var lblCode = new Label { Text = "Mã:", Left = 10, Top = 20, AutoSize = true };
                var txtCode = new TextBox { Left = 120, Top = 16, Width = 260 };
                var lblName = new Label { Text = "Tên:", Left = 10, Top = 60, AutoSize = true };
                var txtName = new TextBox { Left = 120, Top = 56, Width = 260 };
                var lblLoai = new Label { Text = "Loại (1=CuaHang,2=LoKhoTong):", Left = 10, Top = 100, AutoSize = true };
                var numLoai = new NumericUpDown { Left = 250, Top = 96, Width = 130, Minimum = 1, Maximum = 2 };
                var lblDiaChi = new Label { Text = "Địa chỉ:", Left = 10, Top = 140, AutoSize = true };
                var txtDiaChi = new TextBox { Left = 120, Top = 136, Width = 260 };
                var lblPhone = new Label { Text = "SĐT:", Left = 10, Top = 180, AutoSize = true };
                var txtPhone = new TextBox { Left = 120, Top = 176, Width = 260 };
                var chkActive = new CheckBox { Text = "Đang hoạt động", Left = 120, Top = 210, AutoSize = true, Checked = true };
                var btnOk = new Button { Text = "Lưu", Left = 120, Top = 240, Width = 100 }; btnOk.DialogResult = DialogResult.OK;
                var btnCancel = new Button { Text = "Hủy", Left = 240, Top = 240, Width = 100 }; btnCancel.DialogResult = DialogResult.Cancel;

                dlg.Controls.AddRange(new Control[] { lblCode, txtCode, lblName, txtName, lblLoai, numLoai, lblDiaChi, txtDiaChi, lblPhone, txtPhone, chkActive, btnOk, btnCancel });

                if (row != null)
                {
                    txtCode.Text = row.Cells["MaCode"].Value?.ToString() ?? string.Empty;
                    txtName.Text = row.Cells["TenChiNhanh"].Value?.ToString() ?? string.Empty;
                    numLoai.Value = Convert.ToDecimal(row.Cells["LoaiChiNhanh"].Value ?? 1);
                    txtDiaChi.Text = row.Cells["DiaChi"].Value?.ToString() ?? string.Empty;
                    txtPhone.Text = row.Cells["SoDienThoai"].Value?.ToString() ?? string.Empty;
                    chkActive.Checked = Convert.ToBoolean(row.Cells["DangHoatDong"].Value ?? true);
                }

                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    try
                    {
                        if (row == null)
                        {
                            string insertSql = @"INSERT INTO dbo.ChiNhanh (MaCode, TenChiNhanh, LoaiChiNhanh, DiaChi, SoDienThoai, DangHoatDong)
                                                 VALUES (@MaCode, @Ten, @Loai, @DiaChi, @Phone, @Active)";
                            var p = new SqlParameter[] {
                                new SqlParameter("@MaCode", System.Data.SqlDbType.NVarChar, 20) { Value = txtCode.Text.Trim() },
                                new SqlParameter("@Ten", System.Data.SqlDbType.NVarChar, 200) { Value = txtName.Text.Trim() },
                                new SqlParameter("@Loai", System.Data.SqlDbType.TinyInt) { Value = Convert.ToByte(numLoai.Value) },
                                new SqlParameter("@DiaChi", System.Data.SqlDbType.NVarChar, 255) { Value = string.IsNullOrEmpty(txtDiaChi.Text) ? (object)DBNull.Value : txtDiaChi.Text.Trim() },
                                new SqlParameter("@Phone", System.Data.SqlDbType.NVarChar, 20) { Value = string.IsNullOrEmpty(txtPhone.Text) ? (object)DBNull.Value : txtPhone.Text.Trim() },
                                new SqlParameter("@Active", System.Data.SqlDbType.Bit) { Value = chkActive.Checked }
                            };
                            DatabaseHelper.ExecuteNonQuery(insertSql, p);
                        }
                        else
                        {
                            string updateSql = @"UPDATE dbo.ChiNhanh SET MaCode=@MaCode, TenChiNhanh=@Ten, LoaiChiNhanh=@Loai, DiaChi=@DiaChi, SoDienThoai=@Phone, DangHoatDong=@Active WHERE MaChiNhanh = @MaChiNhanh";
                            var p = new SqlParameter[] {
                                new SqlParameter("@MaCode", System.Data.SqlDbType.NVarChar, 20) { Value = txtCode.Text.Trim() },
                                new SqlParameter("@Ten", System.Data.SqlDbType.NVarChar, 200) { Value = txtName.Text.Trim() },
                                new SqlParameter("@Loai", System.Data.SqlDbType.TinyInt) { Value = Convert.ToByte(numLoai.Value) },
                                new SqlParameter("@DiaChi", System.Data.SqlDbType.NVarChar, 255) { Value = string.IsNullOrEmpty(txtDiaChi.Text) ? (object)DBNull.Value : txtDiaChi.Text.Trim() },
                                new SqlParameter("@Phone", System.Data.SqlDbType.NVarChar, 20) { Value = string.IsNullOrEmpty(txtPhone.Text) ? (object)DBNull.Value : txtPhone.Text.Trim() },
                                new SqlParameter("@Active", System.Data.SqlDbType.Bit) { Value = chkActive.Checked },
                                new SqlParameter("@MaChiNhanh", System.Data.SqlDbType.Int) { Value = Convert.ToInt32(row.Cells["MaChiNhanh"].Value) }
                            };
                            DatabaseHelper.ExecuteNonQuery(updateSql, p);
                        }

                        LoadData();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi lưu chi nhánh: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
        }

        private void BtnDelete_Click(object? sender, EventArgs e)
        {
            if (dgv.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn chi nhánh để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var result = MessageBox.Show("Bạn có chắc chắn muốn xóa chi nhánh này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result != DialogResult.Yes) return;

            try
            {
                int id = Convert.ToInt32(dgv.CurrentRow.Cells["MaChiNhanh"].Value);
                string delSql = "DELETE FROM dbo.ChiNhanh WHERE MaChiNhanh = @MaChiNhanh";
                DatabaseHelper.ExecuteNonQuery(delSql, new SqlParameter[] { new SqlParameter("@MaChiNhanh", System.Data.SqlDbType.Int) { Value = id } });
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xóa chi nhánh: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
