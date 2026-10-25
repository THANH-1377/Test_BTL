using BaiTap_WinForms.Models;

namespace BaiTap_WinForms.UI
{
    public static class Session
    {
        public static int MaNhanVien => UserSession.MaNhanVien;
        public static int MaChiNhanh => UserSession.MaChiNhanh;
        public static string TenNhanVien => UserSession.HoTen;
        public static string TenChiNhanh => UserSession.TenChiNhanh;
        public static bool LaQuanLy => UserSession.LaQuanLy;   // duyệt hủy, hoàn thành kiểm kho
        public static bool LaAdmin => UserSession.IsAdmin;     // đổi chi nhánh
    }
}