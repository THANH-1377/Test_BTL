using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;
using BaiTap_WinForms.Data;

namespace BaiTap_WinForms.Forms
{
    public class FrmSanPham : Form
    {
        private DataGridView dgv = null!;
        private Button btnAdd = null!;
        private Button btnEdit = null!;
        private Button btnDelete = null!;

        public FrmSanPham()
        {
            InitializeComponent();
            LoadData();
        }

        private void InitializeComponent()
        {
            this.Text = "Quản lý Sản phẩm";
            this.StartPosition = FormStartPosition.CenterParent;
            this.ClientSize = new Size(1000, 500);

            dgv = new DataGridView { Dock = DockStyle.Top, Height = 380, ReadOnly = true, SelectionMode = DataGridViewSelectionMode.FullRowSelect, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill };
            dgv.AutoGenerateColumns = false;
            dgv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "MaSanPham", HeaderText = "ID", Visible = false });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "TenSanPham", HeaderText = "Tên" });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "MaSKU", HeaderText = "SKU" });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "TenDanhMuc", HeaderText = "Danh mục" });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "TenDonVi", HeaderText = "ĐVT" });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "LoaiSanPham", HeaderText = "Loại" });
            dgv.Columns.Add(new DataGridViewCheckBoxColumn { DataPropertyName = "DangHoatDong", HeaderText = "Hoạt động" });

            btnAdd = new Button { Text = "Thêm", Left = 20, Top = 400, Width = 120 };
            btnEdit = new Button { Text = "Sửa", Left = 160, Top = 400, Width = 120 };
            btnDelete = new Button { Text = "Xóa", Left = 300, Top = 400, Width = 120 };

            btnAdd.Click += (s, e) => EditDialog(null);
            btnEdit.Click += (s, e) => { if (dgv.CurrentRow != null) EditDialog(dgv.CurrentRow); };
            btnDelete.Click += BtnDelete_Click;

            this.Controls.AddRange(new Control[] { dgv, btnAdd, btnEdit, btnDelete });
        }

        private void LoadData()
        {
            try
            {
                string sql = @"SELECT sp.MaSanPham, sp.TenSanPham, sp.MaSKU, sp.LoaiSanPham, sp.DangHoatDong, dm.TenDanhMuc, dv.TenDonVi
                                FROM dbo.SanPham sp
                                JOIN dbo.DanhMuc dm ON sp.MaDanhMuc = dm.MaDanhMuc
                                JOIN dbo.DonVi dv ON sp.MaDonViBan = dv.MaDonVi
                                ORDER BY sp.MaSanPham";
                DataTable dt = DatabaseHelper.ExecuteQuery(sql);
                dgv.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải sản phẩm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void EditDialog(DataGridViewRow? row)
        {
            using var dlg = new Form { Text = row == null ? "Thêm sản phẩm" : "Sửa sản phẩm", StartPosition = FormStartPosition.CenterParent, Size = new Size(520, 380), FormBorderStyle = FormBorderStyle.FixedDialog };

            var lblName = new Label { Text = "Tên sản phẩm:", Left = 10, Top = 20, AutoSize = true };
            var txtName = new TextBox { Left = 140, Top = 16, Width = 340 };
            var lblSKU = new Label { Text = "SKU:", Left = 10, Top = 60, AutoSize = true };
            var txtSKU = new TextBox { Left = 140, Top = 56, Width = 340 };
            var lblCat = new Label { Text = "Danh mục:", Left = 10, Top = 100, AutoSize = true };
            var cboCat = new ComboBox { Left = 140, Top = 96, Width = 340, DropDownStyle = ComboBoxStyle.DropDownList };
            var lblDonVi = new Label { Text = "Đơn vị bán:", Left = 10, Top = 140, AutoSize = true };
            var cboDonVi = new ComboBox { Left = 140, Top = 136, Width = 340, DropDownStyle = ComboBoxStyle.DropDownList };
            var lblLoai = new Label { Text = "Loại (ThanhPham/KhongTruKho/Combo):", Left = 10, Top = 180, AutoSize = true };
            var txtLoai = new TextBox { Left = 300, Top = 176, Width = 180, Text = "ThanhPham" };
            var chkActive = new CheckBox { Text = "Đang hoạt động", Left = 140, Top = 216, AutoSize = true, Checked = true };

            var btnOk = new Button { Text = "Lưu", Left = 140, Top = 260, Width = 120 }; btnOk.DialogResult = DialogResult.OK;
            var btnCancel = new Button { Text = "Hủy", Left = 280, Top = 260, Width = 120 }; btnCancel.DialogResult = DialogResult.Cancel;

            try
            {
                var dtCat = DatabaseHelper.ExecuteQuery("SELECT MaDanhMuc, TenDanhMuc FROM dbo.DanhMuc WHERE DangHoatDong = 1 ORDER BY TenDanhMuc");
                cboCat.DisplayMember = "TenDanhMuc"; cboCat.ValueMember = "MaDanhMuc"; cboCat.DataSource = dtCat;
                var dtDV = DatabaseHelper.ExecuteQuery("SELECT MaDonVi, TenDonVi FROM dbo.DonVi ORDER BY TenDonVi");
                cboDonVi.DisplayMember = "TenDonVi"; cboDonVi.ValueMember = "MaDonVi"; cboDonVi.DataSource = dtDV;
            }
            catch { }

            if (row != null)
            {
                txtName.Text = row.Cells["TenSanPham"].Value?.ToString() ?? string.Empty;
                txtSKU.Text = row.Cells["MaSKU"].Value?.ToString() ?? string.Empty;
                chkActive.Checked = Convert.ToBoolean(row.Cells["DangHoatDong"].Value ?? true);
                // try set category and donvi by names
                var catName = row.Cells["TenDanhMuc"].Value?.ToString();
                var dvName = row.Cells["TenDonVi"].Value?.ToString();
                if (catName != null) cboCat.SelectedIndex = Math.Max(0, cboCat.FindStringExact(catName));
                if (dvName != null) cboDonVi.SelectedIndex = Math.Max(0, cboDonVi.FindStringExact(dvName));
                txtLoai.Text = row.Cells["LoaiSanPham"].Value?.ToString() ?? "ThanhPham";
            }

            dlg.Controls.AddRange(new Control[] { lblName, txtName, lblSKU, txtSKU, lblCat, cboCat, lblDonVi, cboDonVi, lblLoai, txtLoai, chkActive, btnOk, btnCancel });

            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                try
                {
                    if (row == null)
                    {
                        string ins = @"INSERT INTO dbo.SanPham (MaDanhMuc, MaDonViBan, TenSanPham, MaSKU, LoaiSanPham, BatBuocCongThuc, DangHoatDong)
                                      VALUES (@MaDanhMuc, @MaDonViBan, @TenSanPham, @MaSKU, @LoaiSanPham, @BatBuoc, @DangHoatDong)";
                        var p = new SqlParameter[] {
                            new SqlParameter("@MaDanhMuc", System.Data.SqlDbType.Int) { Value = Convert.ToInt32(cboCat.SelectedValue) },
                            new SqlParameter("@MaDonViBan", System.Data.SqlDbType.Int) { Value = Convert.ToInt32(cboDonVi.SelectedValue) },
                            new SqlParameter("@TenSanPham", System.Data.SqlDbType.NVarChar, 200) { Value = txtName.Text.Trim() },
                            new SqlParameter("@MaSKU", System.Data.SqlDbType.NVarChar, 50) { Value = string.IsNullOrEmpty(txtSKU.Text) ? (object)DBNull.Value : txtSKU.Text.Trim() },
                            new SqlParameter("@LoaiSanPham", System.Data.SqlDbType.NVarChar, 30) { Value = txtLoai.Text.Trim() },
                            new SqlParameter("@BatBuoc", System.Data.SqlDbType.Bit) { Value = true },
                            new SqlParameter("@DangHoatDong", System.Data.SqlDbType.Bit) { Value = chkActive.Checked }
                        };
                        DatabaseHelper.ExecuteNonQuery(ins, p);
                    }
                    else
                    {
                        string upd = @"UPDATE dbo.SanPham SET MaDanhMuc=@MaDanhMuc, MaDonViBan=@MaDonViBan, TenSanPham=@TenSanPham, MaSKU=@MaSKU, LoaiSanPham=@LoaiSanPham, DangHoatDong=@DangHoatDong WHERE MaSanPham = @MaSanPham";
                        var p = new SqlParameter[] {
                            new SqlParameter("@MaDanhMuc", System.Data.SqlDbType.Int) { Value = Convert.ToInt32(cboCat.SelectedValue) },
                            new SqlParameter("@MaDonViBan", System.Data.SqlDbType.Int) { Value = Convert.ToInt32(cboDonVi.SelectedValue) },
                            new SqlParameter("@TenSanPham", System.Data.SqlDbType.NVarChar, 200) { Value = txtName.Text.Trim() },
                            new SqlParameter("@MaSKU", System.Data.SqlDbType.NVarChar, 50) { Value = string.IsNullOrEmpty(txtSKU.Text) ? (object)DBNull.Value : txtSKU.Text.Trim() },
                            new SqlParameter("@LoaiSanPham", System.Data.SqlDbType.NVarChar, 30) { Value = txtLoai.Text.Trim() },
                            new SqlParameter("@DangHoatDong", System.Data.SqlDbType.Bit) { Value = chkActive.Checked },
                            new SqlParameter("@MaSanPham", System.Data.SqlDbType.Int) { Value = Convert.ToInt32(row.Cells["MaSanPham"].Value) }
                        };
                        DatabaseHelper.ExecuteNonQuery(upd, p);
                    }
                    LoadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi lưu sản phẩm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BtnDelete_Click(object? sender, EventArgs e)
        {
            if (dgv.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn sản phẩm để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var result = MessageBox.Show("Bạn có chắc chắn muốn xóa sản phẩm này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result != DialogResult.Yes) return;

            try
            {
                int id = Convert.ToInt32(dgv.CurrentRow.Cells["MaSanPham"].Value);
                string delSql = "DELETE FROM dbo.SanPham WHERE MaSanPham = @MaSanPham";
                DatabaseHelper.ExecuteNonQuery(delSql, new SqlParameter[] { new SqlParameter("@MaSanPham", System.Data.SqlDbType.Int) { Value = id } });
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xóa sản phẩm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
