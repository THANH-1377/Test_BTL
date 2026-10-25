using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;
using BaiTap_WinForms.Data;
using BaiTap_WinForms.Models;

namespace BaiTap_WinForms.Forms
{
    public class FrmCaLam : Form
    {
        private DataGridView dgv = null!;
        private Button btnOpen = null!;
        private Button btnClose = null!;

        public FrmCaLam()
        {
            InitializeComponent();
            LoadData();
        }

        private void InitializeComponent()
        {
            this.Text = "Ca làm";
            this.StartPosition = FormStartPosition.CenterParent;
            this.ClientSize = new Size(800, 450);

            dgv = new DataGridView { Dock = DockStyle.Top, Height = 360, ReadOnly = true, SelectionMode = DataGridViewSelectionMode.FullRowSelect, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill };
            dgv.AutoGenerateColumns = false;
            dgv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "MaCa", HeaderText = "ID", Visible = false });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "MaNhanVien", HeaderText = "MaNV", Visible = false });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "HoTen", HeaderText = "Nhân viên" });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "GioMo", HeaderText = "Giờ mở" });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "GioDong", HeaderText = "Giờ đóng" });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "TrangThai", HeaderText = "Trạng thái" });

            btnOpen = new Button { Text = "Mở ca", Left = 20, Top = 370, Width = 120 };
            btnClose = new Button { Text = "Đóng ca", Left = 160, Top = 370, Width = 120 };
            btnOpen.Click += BtnOpen_Click;
            btnClose.Click += BtnClose_Click;

            this.Controls.AddRange(new Control[] { dgv, btnOpen, btnClose });
        }

        private void LoadData()
        {
            try
            {
                string sql = @"SELECT c.MaCa, c.MaNhanVien, nv.HoTen, c.GioMo, c.GioDong, c.TrangThai
                                FROM dbo.NhatKy_CaLam c
                                JOIN dbo.NhanVien nv ON c.MaNhanVien = nv.MaNhanVien
                                WHERE c.MaChiNhanh = @MaChiNhanh
                                ORDER BY c.MaCa DESC";
                var p = new SqlParameter[] { new SqlParameter("@MaChiNhanh", System.Data.SqlDbType.Int) { Value = UserSession.MaChiNhanh } };
                DataTable dt = DatabaseHelper.ExecuteQuery(sql, p);
                dgv.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải ca làm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnOpen_Click(object? sender, EventArgs e)
        {
            try
            {
                string sql = @"INSERT INTO dbo.NhatKy_CaLam (MaChiNhanh, MaNhanVien, GioMo, TrangThai) VALUES (@MaChiNhanh, @MaNhanVien, SYSUTCDATETIME(), @TrangThai)";
                DatabaseHelper.ExecuteNonQuery(sql, new SqlParameter[] {
                    new SqlParameter("@MaChiNhanh", System.Data.SqlDbType.Int) { Value = UserSession.MaChiNhanh },
                    new SqlParameter("@MaNhanVien", System.Data.SqlDbType.Int) { Value = UserSession.MaNhanVien },
                    new SqlParameter("@TrangThai", System.Data.SqlDbType.NVarChar, 20) { Value = "DangMo" }
                });
                LoadData();
                MessageBox.Show("Mở ca thành công.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi mở ca: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnClose_Click(object? sender, EventArgs e)
        {
            if (dgv.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn ca để đóng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int maCa = Convert.ToInt32(dgv.CurrentRow.Cells["MaCa"].Value);
            try
            {
                string sql = @"UPDATE dbo.NhatKy_CaLam SET GioDong = SYSUTCDATETIME(), TrangThai = @TrangThai WHERE MaCa = @MaCa";
                DatabaseHelper.ExecuteNonQuery(sql, new SqlParameter[] {
                    new SqlParameter("@TrangThai", System.Data.SqlDbType.NVarChar, 20) { Value = "DaDong" },
                    new SqlParameter("@MaCa", System.Data.SqlDbType.Int) { Value = maCa }
                });
                LoadData();
                MessageBox.Show("Đóng ca thành công.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi đóng ca: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
