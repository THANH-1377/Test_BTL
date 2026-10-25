using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;
using BaiTap_WinForms.Data;

namespace BaiTap_WinForms.Forms
{
    public class FrmPhanQuyen : Form
    {
        private ComboBox cboRole = null!;
        private CheckedListBox chkMenus = null!;
        private Button btnLoad = null!;
        private Button btnSave = null!;

        public FrmPhanQuyen()
        {
            InitializeComponent();
            LoadRoles();
        }

        private void InitializeComponent()
        {
            this.Text = "Phân quyền";
            this.StartPosition = FormStartPosition.CenterParent;
            this.ClientSize = new Size(700, 500);

            var lblRole = new Label { Text = "Vai trò:", Left = 10, Top = 15, AutoSize = true };
            cboRole = new ComboBox { Left = 80, Top = 10, Width = 300, DropDownStyle = ComboBoxStyle.DropDownList };
            btnLoad = new Button { Text = "Tải", Left = 400, Top = 8, Width = 80 };
            btnLoad.Click += (s, e) => LoadMenusForRole();

            chkMenus = new CheckedListBox { Left = 10, Top = 50, Width = 660, Height = 380 }; chkMenus.CheckOnClick = true;
            btnSave = new Button { Text = "Lưu phân quyền", Left = 10, Top = 440, Width = 140 };
            btnSave.Click += (s, e) => SavePermissions();

            this.Controls.AddRange(new Control[] { lblRole, cboRole, btnLoad, chkMenus, btnSave });
        }

        private void LoadRoles()
        {
            try
            {
                var dt = DatabaseHelper.ExecuteQuery("SELECT MaVaiTro, MaCode, TenVaiTro FROM dbo.VaiTro ORDER BY MaVaiTro");
                cboRole.DisplayMember = "TenVaiTro"; cboRole.ValueMember = "MaVaiTro"; cboRole.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải vai trò: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadMenusForRole()
        {
            if (cboRole.SelectedValue == null) return;
            int maVaiTro = Convert.ToInt32(cboRole.SelectedValue);
            try
            {
                // Load all menus
                var dtMenus = DatabaseHelper.ExecuteQuery("SELECT MaMenu, MaCode, TenHienThi, MaForm, MaMenuCha, Loai, ThuTu FROM dbo.MenuChucNang ORDER BY ThuTu");
                chkMenus.Items.Clear();
                foreach (DataRow r in dtMenus.Rows)
                {
                    string text = r["TenHienThi"].ToString() ?? "";
                    var item = new MenuItemTag { MaMenu = Convert.ToInt32(r["MaMenu"]), Text = text };
                    chkMenus.Items.Add(item, false);
                }

                // Load existing assignments
                var dtAssign = DatabaseHelper.ExecuteQuery("SELECT MaMenu FROM dbo.VaiTro_Menu WHERE MaVaiTro = @MaVaiTro", new SqlParameter[] { new SqlParameter("@MaVaiTro", System.Data.SqlDbType.Int) { Value = maVaiTro } });
                var assigned = dtAssign.AsEnumerable().Select(r => Convert.ToInt32(r["MaMenu"])).ToHashSet();
                for (int i = 0; i < chkMenus.Items.Count; i++)
                {
                    if (chkMenus.Items[i] is MenuItemTag mit && assigned.Contains(mit.MaMenu)) chkMenus.SetItemChecked(i, true);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải menu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SavePermissions()
        {
            if (cboRole.SelectedValue == null) return;
            int maVaiTro = Convert.ToInt32(cboRole.SelectedValue);
            try
            {
                using var conn = DatabaseHelper.GetConnection(); conn.Open(); using var tran = conn.BeginTransaction();
                // delete existing
                DatabaseHelper.ExecuteNonQuery("DELETE FROM dbo.VaiTro_Menu WHERE MaVaiTro = @MaVaiTro", new SqlParameter[] { new SqlParameter("@MaVaiTro", System.Data.SqlDbType.Int) { Value = maVaiTro } }, conn, tran);
                // insert checked
                for (int i = 0; i < chkMenus.Items.Count; i++)
                {
                    if (chkMenus.GetItemChecked(i) && chkMenus.Items[i] is MenuItemTag mit)
                    {
                        DatabaseHelper.ExecuteNonQuery("INSERT INTO dbo.VaiTro_Menu (MaVaiTro, MaMenu) VALUES (@MaVaiTro, @MaMenu)", new SqlParameter[] {
                            new SqlParameter("@MaVaiTro", System.Data.SqlDbType.Int) { Value = maVaiTro },
                            new SqlParameter("@MaMenu", System.Data.SqlDbType.Int) { Value = mit.MaMenu }
                        }, conn, tran);
                    }
                }
                tran.Commit();
                MessageBox.Show("Lưu phân quyền thành công.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi lưu phân quyền: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private class MenuItemTag
        {
            public int MaMenu { get; set; }
            public string Text { get; set; } = string.Empty;
            public override string ToString() => Text;
        }
    }
}
