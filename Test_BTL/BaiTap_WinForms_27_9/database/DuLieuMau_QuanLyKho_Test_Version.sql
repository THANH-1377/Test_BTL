/*
Du lieu mau GON cho chuc nang QUAN LY KHO.
Chay SAU 01_TaoBang_Khoa.sql tren database QuanLyBanhMi.
Chi tao it du lieu de test WinForms; khong xoa database.
Chay 1 lan tren database sach.
*/
USE QuanLyBanhMi_Demo;
GO
SET NOCOUNT ON;
SET XACT_ABORT ON;
BEGIN TRANSACTION;

SET QUOTED_IDENTIFIER ON;   -- bắt buộc khi ghi bảng có cột tính toán PERSISTED (ChiTiet_KiemKho)
SET ANSI_NULLS ON;
GO

USE QuanLyBanhMi;
GO

SET NOCOUNT ON;
SET XACT_ABORT ON;   -- lỗi ở bất kỳ câu nào → rollback toàn bộ
BEGIN TRANSACTION;

/*------------------------------------------------------------------------------
  1. VAI TRÒ
------------------------------------------------------------------------------*/
INSERT INTO dbo.VaiTro (MaCode, TenVaiTro, MoTa) VALUES
(N'ADMIN',   N'Quản trị hệ thống',       N'Toàn quyền truy cập và cấu hình toàn bộ hệ thống'),
(N'MANAGER', N'Quản lý cửa hàng',        N'Quản lý nhân sự, kho, thực đơn, bán hàng và xem báo cáo'),
(N'STAFF',   N'Nhân viên bán hàng (POS)', N'Lập đơn, thanh toán, in hóa đơn, tích điểm và kiểm kho cơ bản');

/*------------------------------------------------------------------------------
  2. MENU CHỨC NĂNG (nhóm menu trước, trang con sau)
------------------------------------------------------------------------------*/
INSERT INTO dbo.MenuChucNang (MaCode, MaMenuCha, TenHienThi, MaForm, Loai, ThuTu) VALUES
(N'SYS',  NULL, N'Hệ thống',  NULL, N'NhomMenu', 1),
(N'MENU', NULL, N'Thực đơn',  NULL, N'NhomMenu', 2),
(N'POS',  NULL, N'Bán hàng',  NULL, N'NhomMenu', 3),
(N'INV',  NULL, N'Kho',       NULL, N'NhomMenu', 4),
(N'RPT',  NULL, N'Báo cáo',   NULL, N'NhomMenu', 5),
(N'UTIL', NULL, N'Tiện ích',  NULL, N'NhomMenu', 6);

INSERT INTO dbo.MenuChucNang (MaCode, MaMenuCha, TenHienThi, MaForm, Loai, ThuTu)
SELECT v.MaCode, cha.MaMenu, v.TenHienThi, v.MaForm, N'Trang', v.ThuTu
FROM (VALUES
    -- Hệ thống
    (N'SYS_AUTH',     N'SYS',  N'Đổi mật khẩu',             N'FrmDoiMatKhau',     1),
    (N'SYS_ACCOUNT',  N'SYS',  N'Quản lý tài khoản',        N'FrmTaiKhoan',       2),
    (N'SYS_ROLE',     N'SYS',  N'Phân quyền',               N'FrmPhanQuyen',      3),
    (N'SYS_BRANCH',   N'SYS',  N'Quản lý chi nhánh',        N'FrmChiNhanh',       4),
    (N'SYS_SHIFT',    N'SYS',  N'Ca làm & giao ca',         N'FrmCaLam',          5),
    -- Thực đơn
    (N'MENU_CAT',     N'MENU', N'Danh mục món',             N'FrmDanhMuc',        1),
    (N'MENU_DISH',    N'MENU', N'Món ăn / đồ uống',         N'FrmSanPham',        2),
    (N'MENU_UNIT',    N'MENU', N'Đơn vị tính',              N'FrmDonVi',          3),
    (N'MENU_PRICE',   N'MENU', N'Giá bán theo chi nhánh',   N'FrmGiaChiNhanh',    4),
    (N'MENU_COMBO',   N'MENU', N'Combo',                    N'FrmCombo',          5),
    (N'MENU_PROMO',   N'MENU', N'Khuyến mãi',               N'FrmKhuyenMai',      6),
    -- Bán hàng
    (N'POS_ORDER',    N'POS',  N'Lập đơn hàng',             N'FrmBanHang',        1),
    (N'POS_PAYMENT',  N'POS',  N'Thanh toán',               N'FrmThanhToan',      2),
    (N'POS_PRINT',    N'POS',  N'In hóa đơn',               N'FrmInHoaDon',       3),
    (N'POS_SEARCH',   N'POS',  N'Tra cứu hóa đơn',          N'FrmTraCuuHoaDon',   4),
    (N'POS_CUST',     N'POS',  N'Khách hàng thân thiết',    N'FrmKhachHang',      5),
    -- Kho
    (N'INV_MAT',      N'INV',  N'Nguyên liệu & công thức',  N'FrmNguyenLieu',     1),
    (N'INV_IO',       N'INV',  N'Nhập / xuất kho',          N'FrmNhapXuatKho',    2),
    (N'INV_TRANSFER', N'INV',  N'Điều chuyển kho',          N'FrmDieuChuyen',     3),
    (N'INV_CHECK',    N'INV',  N'Kiểm kho',                 N'FrmKiemKho',        4),
    (N'INV_WARN',     N'INV',  N'Cảnh báo tồn thấp',        N'FrmCanhBaoTon',     5),
    (N'INV_SUPPLIER', N'INV',  N'Nhà cung cấp',             N'FrmNhaCungCap',     6),
    (N'INV_EQUIP',    N'INV',  N'Thiết bị',                 N'FrmThietBi',        7),
    -- Báo cáo
    (N'RPT_REV',      N'RPT',  N'Doanh thu',                N'FrmBaoCaoDoanhThu', 1),
    (N'RPT_TOP',      N'RPT',  N'Món bán chạy',             N'FrmMonBanChay',     2),
    (N'RPT_STOCK',    N'RPT',  N'Xuất nhập tồn',            N'FrmBaoCaoTon',      3),
    (N'RPT_CHART',    N'RPT',  N'Biểu đồ',                  N'FrmBieuDo',         4),
    (N'RPT_EXPORT',   N'RPT',  N'Xuất Excel / PDF',         N'FrmXuatBaoCao',     5),
    -- Tiện ích
    (N'UTIL_RAND',    N'UTIL', N'Tiện ích ngẫu nhiên',      N'FrmNgauNhien',      1)
) AS v (MaCode, MaCha, TenHienThi, MaForm, ThuTu)
JOIN dbo.MenuChucNang cha ON cha.MaCode = v.MaCha;

/*------------------------------------------------------------------------------
  3. PHÂN QUYỀN VAI TRÒ – MENU (RBAC0)
------------------------------------------------------------------------------*/

-- 3.1 ADMIN: toàn bộ menu
INSERT INTO dbo.VaiTro_Menu (MaVaiTro, MaMenu)
SELECT vt.MaVaiTro, m.MaMenu
FROM dbo.VaiTro vt
CROSS JOIN dbo.MenuChucNang m
WHERE vt.MaCode = N'ADMIN';

-- 3.2 MANAGER + STAFF: chỉ gán trang con, nhóm menu cha được bổ sung ở 3.3
INSERT INTO dbo.VaiTro_Menu (MaVaiTro, MaMenu)
SELECT vt.MaVaiTro, m.MaMenu
FROM (VALUES
    -- MANAGER: Bán hàng, Thực đơn, Kho, Báo cáo
    (N'MANAGER', N'SYS_AUTH'), (N'MANAGER', N'SYS_ACCOUNT'), (N'MANAGER', N'SYS_BRANCH'), (N'MANAGER', N'SYS_SHIFT'),
    (N'MANAGER', N'MENU_CAT'), (N'MANAGER', N'MENU_DISH'), (N'MANAGER', N'MENU_UNIT'), (N'MANAGER', N'MENU_PRICE'), (N'MANAGER', N'MENU_COMBO'), (N'MANAGER', N'MENU_PROMO'),
    (N'MANAGER', N'POS_ORDER'), (N'MANAGER', N'POS_PAYMENT'), (N'MANAGER', N'POS_PRINT'), (N'MANAGER', N'POS_SEARCH'), (N'MANAGER', N'POS_CUST'),
    (N'MANAGER', N'INV_MAT'), (N'MANAGER', N'INV_IO'), (N'MANAGER', N'INV_TRANSFER'), (N'MANAGER', N'INV_CHECK'), (N'MANAGER', N'INV_WARN'), (N'MANAGER', N'INV_SUPPLIER'), (N'MANAGER', N'INV_EQUIP'),
    (N'MANAGER', N'RPT_REV'), (N'MANAGER', N'RPT_TOP'), (N'MANAGER', N'RPT_STOCK'), (N'MANAGER', N'RPT_CHART'), (N'MANAGER', N'RPT_EXPORT'),
    (N'MANAGER', N'UTIL_RAND'),

    -- STAFF: Bán hàng + nhập/xuất kho & kiểm kê tại cửa hàng
    (N'STAFF', N'SYS_AUTH'), (N'STAFF', N'SYS_SHIFT'),
    (N'STAFF', N'POS_ORDER'), (N'STAFF', N'POS_PAYMENT'), (N'STAFF', N'POS_PRINT'), (N'STAFF', N'POS_SEARCH'), (N'STAFF', N'POS_CUST'),
    (N'STAFF', N'INV_IO'), (N'STAFF', N'INV_CHECK'), (N'STAFF', N'INV_WARN'),
    (N'STAFF', N'UTIL_RAND')
) AS v (MaVaiTroCode, MaMenuCode)
JOIN dbo.VaiTro vt       ON vt.MaCode = v.MaVaiTroCode
JOIN dbo.MenuChucNang m  ON m.MaCode  = v.MaMenuCode;

-- 3.3 Có trang con thì phải có nhóm menu cha để MenuStrip hiển thị được
INSERT INTO dbo.VaiTro_Menu (MaVaiTro, MaMenu)
SELECT DISTINCT vm.MaVaiTro, m.MaMenuCha
FROM dbo.VaiTro_Menu vm
JOIN dbo.MenuChucNang m ON m.MaMenu = vm.MaMenu
WHERE m.MaMenuCha IS NOT NULL
  AND NOT EXISTS (
        SELECT 1 FROM dbo.VaiTro_Menu x
        WHERE x.MaVaiTro = vm.MaVaiTro AND x.MaMenu = m.MaMenuCha
  );



/*------------------------------------------------------------------------------
  6. TÀI KHOẢN
  Mật khẩu mặc định: 123456, lưu dạng SHA-256 (hex in hoa) của chuỗi UTF-8.
  C#: Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(matKhau)))
------------------------------------------------------------------------------*/
DECLARE @MatKhauMacDinh NVARCHAR(255) =
    CONVERT(NVARCHAR(255), HASHBYTES('SHA2_256', '123456'), 2);

INSERT INTO dbo.TaiKhoan (MaNhanVien, MaVaiTro, TenDangNhap, MatKhauBam, DangHoatDong)
SELECT nv.MaNhanVien, vt.MaVaiTro, v.TenDangNhap, @MatKhauMacDinh, 1
FROM (VALUES
-- Admin hệ thống
    (N'admin',     N'NV000', N'ADMIN'),

    -- Quản lý chi nhánh
    (N'ql_q1',     N'NV001', N'MANAGER'),
    (N'ql_thuduc', N'NV009', N'MANAGER'),
    (N'ql_govap',  N'NV017', N'MANAGER'),

    -- Trưởng ca & nhân viên Quận 1
    (N'tc1_q1', N'NV002', N'STAFF'), (N'tc2_q1', N'NV003', N'STAFF'), (N'tc3_q1', N'NV004', N'STAFF'),
    (N'nv1_q1', N'NV005', N'STAFF'), (N'nv2_q1', N'NV006', N'STAFF'), (N'nv3_q1', N'NV007', N'STAFF'), (N'nv4_q1', N'NV008', N'STAFF'),

    -- Trưởng ca & nhân viên Thủ Đức
    (N'tc1_td', N'NV010', N'STAFF'), (N'tc2_td', N'NV011', N'STAFF'), (N'tc3_td', N'NV012', N'STAFF'),
    (N'nv1_td', N'NV013', N'STAFF'), (N'nv2_td', N'NV014', N'STAFF'), (N'nv3_td', N'NV015', N'STAFF'), (N'nv4_td', N'NV016', N'STAFF'),

    -- Trưởng ca & nhân viên Gò Vấp
    (N'tc1_gv', N'NV018', N'STAFF'), (N'tc2_gv', N'NV019', N'STAFF'), (N'tc3_gv', N'NV020', N'STAFF'),
    (N'nv1_gv', N'NV021', N'STAFF'), (N'nv2_gv', N'NV022', N'STAFF'), (N'nv3_gv', N'NV023', N'STAFF'), (N'nv4_gv', N'NV024', N'STAFF')
) AS v (TenDangNhap, MaNhanVienCode, MaVaiTroCode)
JOIN dbo.NhanVien nv ON nv.MaCode = v.MaNhanVienCode
JOIN dbo.VaiTro   vt ON vt.MaCode = v.MaVaiTroCode;

------------------------------------------------------------------------------------------------------------------------------------------

-- 1. Chi nhanh
INSERT dbo.ChiNhanh (MaCode, TenChiNhanh, LoaiChiNhanh, DiaChi, SoDienThoai, DangHoatDong)
VALUES
(N'CN01', N'Banh Mi Ngon - Quan 1', 1, N'Quan 1, TP.HCM', '0901111001', 1),
(N'CN02', N'Banh Mi Ngon - Thu Duc', 1, N'TP. Thu Duc, TP.HCM', '0901111002', 1);

-- 2. Nhan vien kho (moi chi nhanh 1 nguoi)
INSERT dbo.NhanVien (MaCode, MaChiNhanh, HoTen, ChucVu, SoDienThoai, DangHoatDong)
SELECT v.MaCode, cn.MaChiNhanh, v.HoTen, N'Nhan vien kho', v.SDT, 1
FROM (VALUES
 (N'NVK01', N'Nguyen Van An', '0913000001', N'CN01'),
 (N'NVK02', N'Tran Thi Binh', '0913000002', N'CN02')
) v(MaCode, HoTen, SDT, MaChiNhanhCode)
JOIN dbo.ChiNhanh cn ON cn.MaCode = v.MaChiNhanhCode;

-- 3. Don vi tinh
INSERT dbo.DonVi (TenDonVi, LoaiDonVi)
VALUES (N'kg', N'KhoiLuong'), (N'lit', N'TheTich'), (N'cai', N'Dem');

-- 4. Nha cung cap
INSERT dbo.NhaCungCap (TenNCC, SoDienThoai, DiaChi, DangHoatDong)
VALUES
(N'NCC Thuc pham An Tam', '0281111222', N'Quan 1, TP.HCM', 1),
(N'NCC Nguyen lieu Minh Phat', '0283333444', N'TP. Thu Duc, TP.HCM', 1);

-- 5. Nguyen lieu
INSERT dbo.NguyenLieu (TenNguyenLieu, MaDonViGoc, TonToiThieuCanhBao, DangHoatDong)
SELECT v.Ten, d.MaDonVi, v.TonToiThieu, 1
FROM (VALUES
 (N'Bot mi', N'kg', CAST(10 AS DECIMAL(18,3))),
 (N'Thịt nguoi', N'kg', CAST(5 AS DECIMAL(18,3))),
 (N'Rau dua leo', N'kg', CAST(3 AS DECIMAL(18,3))),
 (N'Sot banh mi', N'lit', CAST(2 AS DECIMAL(18,3))),
 (N'Tui giay', N'cai', CAST(50 AS DECIMAL(18,3)))
) v(Ten, TenDonVi, TonToiThieu)
JOIN dbo.DonVi d ON d.TenDonVi = v.TenDonVi;

-- 6. Ton dau ky: 2 chi nhanh x 5 nguyen lieu
INSERT dbo.TonKho_ChiNhanh (MaChiNhanh, MaNguyenLieu, SoLuongTon)
SELECT cn.MaChiNhanh, nl.MaNguyenLieu, v.SoLuong
FROM (VALUES
 (N'CN01', N'Bot mi', 40.000), (N'CN01', N'Thịt nguoi', 17.000),
 (N'CN01', N'Rau dua leo', 8.000), (N'CN01', N'Sot banh mi', 6.000),
 (N'CN01', N'Tui giay', 200.000),
 (N'CN02', N'Bot mi', 18.000), (N'CN02', N'Thịt nguoi', 8.000),
 (N'CN02', N'Rau dua leo', 5.000), (N'CN02', N'Sot banh mi', 4.000),
 (N'CN02', N'Tui giay', 120.000)
) v(MaChiNhanhCode, TenNguyenLieu, SoLuong)
JOIN dbo.ChiNhanh cn ON cn.MaCode = v.MaChiNhanhCode
JOIN dbo.NguyenLieu nl ON nl.TenNguyenLieu = v.TenNguyenLieu;

-- 7. Phieu nhap da nhan: 1 phieu, 2 dong chi tiet (ton dau ky khong tinh phieu nay)
INSERT dbo.Phieu_NhapHang (MaChiNhanh, MaNCC, NgayDat, NgayNhan, TongTien, DaThanhToan, TrangThai, TrangThaiCongNo)
SELECT cn.MaChiNhanh, ncc.MaNCC, '2026-10-01', '2026-10-01', 650000, 650000, N'DaNhap', N'DaTT'
FROM dbo.ChiNhanh cn
CROSS JOIN dbo.NhaCungCap ncc
WHERE cn.MaCode = N'CN01' AND ncc.TenNCC = N'NCC Thuc pham An Tam';

DECLARE @PhieuNhap INT = SCOPE_IDENTITY();

INSERT dbo.ChiTiet_NhapHang (MaPhieuNhap, MaNguyenLieu, SoLuong, DonGiaNhap, ThanhTien)
SELECT @PhieuNhap, nl.MaNguyenLieu, v.SoLuong, v.DonGia, v.SoLuong * v.DonGia
FROM (VALUES
 (N'Bot mi', CAST(10 AS DECIMAL(18,3)), CAST(25000 AS DECIMAL(18,2))),
 (N'Thịt nguoi', CAST(5 AS DECIMAL(18,3)), CAST(80000 AS DECIMAL(18,2)))
) v(TenNguyenLieu, SoLuong, DonGia)
JOIN dbo.NguyenLieu nl ON nl.TenNguyenLieu = v.TenNguyenLieu;

-- 8. Phieu dieu chuyen mau: CN01 gui 2 kg bot mi sang CN02 (cho xu ly)
INSERT dbo.Phieu_DieuChuyen (MaKhoGui, MaKhoNhan, NgayGui, TrangThai, NguoiGui)
SELECT gui.MaChiNhanh, nhan.MaChiNhanh, '2026-10-02 09:00:00', N'ChoGui', nv.MaNhanVien
FROM dbo.ChiNhanh gui
JOIN dbo.ChiNhanh nhan ON nhan.MaCode = N'CN02'
JOIN dbo.NhanVien nv ON nv.MaCode = N'NVK01'
WHERE gui.MaCode = N'CN01';
DECLARE @PhieuDC INT = SCOPE_IDENTITY();

INSERT dbo.ChiTiet_DieuChuyen (MaPhieu, MaNguyenLieu, SoLuongGui, SoLuongNhanThucTe)
SELECT @PhieuDC, MaNguyenLieu, 2.000, NULL
FROM dbo.NguyenLieu WHERE TenNguyenLieu = N'Bot mi';

-- 9. Phieu huy hao hut mau
INSERT dbo.Phieu_HuyHaoHut (MaChiNhanh, NguoiLap, NgayLap, LyDo, TrangThai)
SELECT cn.MaChiNhanh, nv.MaNhanVien, '2026-10-03 10:00:00', N'Rau bi hu', N'Draft'
FROM dbo.ChiNhanh cn CROSS JOIN dbo.NhanVien nv
WHERE cn.MaCode = N'CN01' AND nv.MaCode = N'NVK01';
DECLARE @PhieuHuy INT = SCOPE_IDENTITY();

INSERT dbo.ChiTiet_HuyHaoHut (MaPhieu, MaNguyenLieu, SoLuong, GhiChu)
SELECT @PhieuHuy, MaNguyenLieu, 0.500, N'Mau kiem thu'
FROM dbo.NguyenLieu WHERE TenNguyenLieu = N'Rau dua leo';

-- 10. Phieu kiem kho mau
INSERT dbo.Phieu_KiemKho (MaChiNhanh, NgayKiem, NguoiKiem, TrangThai, GhiChu)
SELECT cn.MaChiNhanh, '2026-10-04 08:00:00', nv.MaNhanVien, N'DangKiem', N'Kiem kho mau'
FROM dbo.ChiNhanh cn CROSS JOIN dbo.NhanVien nv
WHERE cn.MaCode = N'CN01' AND nv.MaCode = N'NVK01';
DECLARE @PhieuKiem INT = SCOPE_IDENTITY();

INSERT dbo.ChiTiet_KiemKho (MaPhieu, MaNguyenLieu, SoLuongHeThong, SoLuongThucTe)
SELECT @PhieuKiem, nl.MaNguyenLieu, tk.SoLuongTon,
       CASE WHEN nl.TenNguyenLieu = N'Bot mi' THEN tk.SoLuongTon - 1 ELSE tk.SoLuongTon END
FROM dbo.TonKho_ChiNhanh tk
JOIN dbo.ChiNhanh cn ON cn.MaChiNhanh = tk.MaChiNhanh
JOIN dbo.NguyenLieu nl ON nl.MaNguyenLieu = tk.MaNguyenLieu
WHERE cn.MaCode = N'CN01';

-- 11. Nhat ky kho mau (ghi nhan ton dau ky de test bang lich su)
INSERT dbo.NhatKy_Kho
 (MaChiNhanh, MaNguyenLieu, LoaiGiaoDich, SoLuongThayDoi, TonSau, MaNhanVien, NgayTao, LoaiThamChieu, MaThamChieu, GhiChu)
SELECT cn.MaChiNhanh, nl.MaNguyenLieu, N'KIEM_KE', tk.SoLuongTon, tk.SoLuongTon,
       nv.MaNhanVien, '2026-10-01 08:00:00', N'KIEM_KHO', NULL, N'Ton dau ky mau'
FROM dbo.TonKho_ChiNhanh tk
JOIN dbo.ChiNhanh cn ON cn.MaChiNhanh = tk.MaChiNhanh
JOIN dbo.NguyenLieu nl ON nl.MaNguyenLieu = tk.MaNguyenLieu
JOIN dbo.NhanVien nv ON nv.MaCode = CASE cn.MaCode WHEN N'CN01' THEN N'NVK01' ELSE N'NVK02' END;

COMMIT TRANSACTION;
PRINT N'Da nap du lieu mau gon cho chuc nang quan ly kho.';
GO

-- Kiem tra nhanh
SELECT cn.MaCode AS ChiNhanh, nl.TenNguyenLieu, d.TenDonVi,
       tk.SoLuongTon, nl.TonToiThieuCanhBao
FROM dbo.TonKho_ChiNhanh tk
JOIN dbo.ChiNhanh cn ON cn.MaChiNhanh = tk.MaChiNhanh
JOIN dbo.NguyenLieu nl ON nl.MaNguyenLieu = tk.MaNguyenLieu
JOIN dbo.DonVi d ON d.MaDonVi = nl.MaDonViGoc
ORDER BY cn.MaCode, nl.TenNguyenLieu;
GO
