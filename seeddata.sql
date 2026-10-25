-- ========================================
-- SEED DATA - FACILITY MANAGEMENT
-- ========================================
-- Chạy file này sau khi đã CREATE DATABASE và TABLES

USE facility_test;

-- ========================================
-- 1. USERS (NguoiDung)
-- ========================================
INSERT INTO nguoi_dung 
(username, password, email, ho_ten, so_dien_thoai, vai_tro, trang_thai, ghi_chu, ngay_tao, ngay_cap_nhat) VALUES
('admin', 'admin@123', 'admin@cateriv.vn', 'Quản trị viên hệ thống', '0912345678', 'ADMIN', 1, 'Admin chính', NOW(), NOW()),
('manager1', 'manager@123', 'manager1@cateriv.vn', 'Trần Văn B', '0912345679', 'QUAN_LY', 1, 'Quản lý CSVC', NOW(), NOW()),
('manager2', 'manager@123', 'manager2@cateriv.vn', 'Lê Thị C', '0912345680', 'QUAN_LY', 1, 'Quản lý kho', NOW(), NOW()),
('tech1', 'tech@123', 'tech1@cateriv.vn', 'Nguyễn Văn D', '0912345681', 'KY_THUAT', 1, 'Kỹ thuật viên bảo trì', NOW(), NOW()),
('tech2', 'tech@123', 'tech2@cateriv.vn', 'Phạm Văn E', '0912345682', 'KY_THUAT', 1, 'Kỹ thuật viên sửa chữa', NOW(), NOW()),
('staff1', 'staff@123', 'staff1@cateriv.vn', 'Lê Thị F', '0912345683', 'NHAN_VIEN', 1, 'Nhân viên kho', NOW(), NOW()),
('staff2', 'staff@123', 'staff2@cateriv.vn', 'Trương Văn G', '0912345684', 'NHAN_VIEN', 1, 'Nhân viên hỗ trợ', NOW(), NOW());

-- ========================================
-- 2. KHU NHÀ (KhuNha)
-- ========================================
INSERT INTO khu_nha (ten_khu_nha, vi_tri, mo_ta) VALUES
('Khu A - Tòa nhà A', 'Phía Bắc', 'Khu dạy học lý thuyết, tầng 1-3'),
('Khu B - Tòa nhà B', 'Phía Nam', 'Khu dạy học lý thuyết, tầng 1-4'),
('Khu C - Tòa nhà C', 'Phía Tây', 'Khu thực hành, tầng 1-2'),
('Khu D - Tòa nhà D', 'Phía Đông', 'Khu văn phòng, tầng 1-3');

-- ========================================
-- 3. PHÒNG HỌC (PhongHoc)
-- ========================================
INSERT INTO phong_hoc (ten_phong, toa_nha, tang, suc_chua, loai_phong, trang_thai, khu_nha_id) VALUES
('A101', 'Tòa A', 1, 30, 'Lý thuyết', 'DANG_SU_DUNG', 1),
('A102', 'Tòa A', 1, 30, 'Lý thuyết', 'DANG_SU_DUNG', 1),
('A201', 'Tòa A', 2, 25, 'Lý thuyết', 'DANG_SU_DUNG', 1),
('A202', 'Tòa A', 2, 25, 'Lý thuyết', 'DANG_SU_DUNG', 1),
('A301', 'Tòa A', 3, 20, 'Lý thuyết', 'DANG_SU_DUNG', 1),
('B101', 'Tòa B', 1, 35, 'Lý thuyết', 'DANG_SU_DUNG', 2),
('B102', 'Tòa B', 1, 35, 'Lý thuyết', 'DANG_SU_DUNG', 2),
('B201', 'Tòa B', 2, 30, 'Thực hành', 'DANG_SU_DUNG', 2),
('C101', 'Tòa C', 1, 25, 'Lab', 'DANG_SU_DUNG', 3),
('C102', 'Tòa C', 1, 25, 'Lab', 'DANG_SU_DUNG', 3),
('C201', 'Tòa C', 2, 20, 'Lab', 'DANG_SU_DUNG', 3),
('D101', 'Tòa D', 1, 10, 'Văn phòng', 'DANG_SU_DUNG', 4);

-- ========================================
-- 4. LOẠI CSVC (LoaiCSVC)
-- ========================================
INSERT INTO loai_csvc (ten_loai, mo_ta) VALUES
('Nội thất', 'Bàn, ghế, tủ, kệ học tập'),
('Thiết bị điện', 'Máy lạnh, quạt, đèn, ổ cắm'),
('Thiết bị CNTT', 'Máy tính, máy chiếu, laptop'),
('Thiết bị HVQT', 'Bảng, bút, phấn, dụng cụ dạy học'),
('Thiết bị điều hòa', 'Máy lạnh, máy sưởi, quạt'),
('Thiết bị an toàn', 'Bình chữa cháy, biển chỉ dẫn');

-- ========================================
-- 5. NHÀ CUNG CẤP (NhaCungCap)
-- ========================================
INSERT INTO nha_cung_cap (tenncc, nguoi_lien_he, so_dien_thoai, email, dia_chi) VALUES
('Công ty TNHH A - Nội thất', 'Nguyễn Văn H', '0901111111', 'contact@a-furniture.com', '123 Trần Hưng Đạo, TP HCM'),
('Công ty TNHH B - Thiết bị điện', 'Trần Thị I', '0902222222', 'contact@b-electric.com', '456 Nguyễn Huệ, TP HCM'),
('Công ty TNHH C - Công nghệ thông tin', 'Lê Văn J', '0903333333', 'contact@c-it.com', '789 Lê Lợi, TP HCM'),
('Công ty TNHH D - Thiết bị học tập', 'Phạm Thị K', '0904444444', 'contact@d-education.com', '101 Hai Bà Trưng, Hà Nội'),
('Công ty TNHH E - Đồ dùng văn phòng', 'Hoàng Văn L', '0905555555', 'contact@e-office.com', '202 Đống Đa, Hà Nội');

-- ========================================
-- 6. KHO VẬT TƯ (KhoVatTu)
-- ========================================
INSERT INTO kho_vat_tu (ten_kho, vi_tri, nguoi_phu_trach_id) VALUES
('Kho chính', 'Tầng 1 - Tòa D', 6),
('Kho phụ - Tòa A', 'Tầng 1 - Tòa A', 7),
('Kho dụng cụ', 'Phòng D101', 6);

-- ========================================
-- 7. VẬT TƯ LINH KIỆN (VatTuLinhKien)
-- ========================================
INSERT INTO vat_tu_linh_kien (ten_vat_tu, don_vi_tinh, so_luong_ton, muc_canh_bao_toi_thieu, don_gia, kho_ma_kho) VALUES
('Bút bi xanh', 'Cái', 150, 30, 5000, 1),
('Bút bi đỏ', 'Cái', 120, 25, 5000, 1),
('Phấn trắng', 'Hộp', 50, 10, 15000, 1),
('Phấn màu', 'Hộp', 30, 5, 20000, 1),
('Giấy A4', 'Ream (500 tờ)', 20, 5, 85000, 1),
('Dây điện 2.5mm', 'Mét', 200, 50, 20000, 1),
('Bóng đèn LED 10W', 'Cái', 80, 20, 45000, 1),
('Ổ cắm điện 3 chấu', 'Cái', 60, 15, 35000, 1),
('Dây cáp HDMI', 'Cái', 25, 5, 150000, 2),
('Bóng đèn tuýp T8 20W', 'Cái', 40, 10, 55000, 3);

-- ========================================
-- 8. CƠ SỞ VẬT CHẤT (CoSoVatChat)
-- ========================================
INSERT INTO co_so_vat_chat (ma_hieu, tencsvc, nha_san_xuat, ngay_mua, ngay_het_bao_hanh, nguyen_gia, so_luong, tinh_trang, phong_id, nha_cung_cap_id) VALUES
('BG-001', 'Ghế học tập màu xanh', 'Công ty A', '2023-01-15', '2026-01-15', 350000, 30, 'DANG_SU_DUNG', 1, 1),
('BG-002', 'Ghế học tập màu xanh', 'Công ty A', '2023-01-15', '2026-01-15', 350000, 30, 'DANG_SU_DUNG', 2, 1),
('BN-001', 'Bàn học tập 1.2m', 'Công ty A', '2023-02-01', '2026-02-01', 450000, 15, 'DANG_SU_DUNG', 1, 1),
('BN-002', 'Bàn học tập 1.2m', 'Công ty A', '2023-02-01', '2026-02-01', 450000, 15, 'DANG_SU_DUNG', 2, 1),
('DC-001', 'Máy lạnh 1 HP', 'Daikin', '2022-06-01', '2025-06-01', 8500000, 1, 'DANG_SU_DUNG', 3, 2),
('DC-002', 'Quạt treo tường', 'Panasonic', '2023-03-10', '2026-03-10', 890000, 4, 'DANG_SU_DUNG', 4, 2),
('MC-001', 'Máy chiếu 3LCD', 'Epson', '2023-04-01', '2026-04-01', 12500000, 1, 'DANG_SU_DUNG', 5, 3),
('TK-001', 'Laptop Lenovo', 'Lenovo', '2023-05-15', '2026-05-15', 15000000, 5, 'DANG_SU_DUNG', 6, 3),
('BN-003', 'Bàn giáo viên', 'Công ty A', '2023-06-01', '2026-06-01', 500000, 3, 'DANG_SU_DUNG', 9, 1),
('GH-001', 'Ghế giáo viên', 'Công ty A', '2023-06-01', '2026-06-01', 400000, 3, 'DANG_SU_DUNG', 9, 1),
('BB-001', 'Bảng trắng 1.2m x 0.9m', 'Công ty D', '2023-07-01', '2026-07-01', 1200000, 10, 'DANG_SU_DUNG', 3, 4),
('DC-003', 'Ổ cắm điện 6 vị trí', 'Panasonic', '2023-08-01', '2026-08-01', 250000, 20, 'DANG_SU_DUNG', 10, 2),
('TK-002', 'Máy tính để bàn', 'Dell', '2023-09-01', '2026-09-01', 12000000, 8, 'DANG_SU_DUNG', 8, 3),
('BB-002', 'Bảng xanh 1.5m x 1m', 'Công ty D', '2023-10-01', '2026-10-01', 800000, 5, 'DANG_SU_DUNG', 7, 4);

-- ========================================
-- 9. KẾ HOẠCH BẢO TRỊ ĐỊNH KỲ (KeHoachBaoTriDinhKy)
-- ========================================
INSERT INTO ke_hoach_bao_tri_dinh_ky (hinh_thuc_bao_tri, chu_ky_ngay, ngay_bao_tri_ke_tiep, csvc_id) VALUES
('BAO_TRI_DINH_KY', 90, DATE_ADD(CURDATE(), INTERVAL 30 DAY), 5),
('BAO_TRI_DINH_KY', 180, DATE_ADD(CURDATE(), INTERVAL 60 DAY), 6),
('KIEM_DINH_AN_TOAN', 365, DATE_ADD(CURDATE(), INTERVAL 120 DAY), 5),
('VE_SINH_DINH_KY', 30, DATE_ADD(CURDATE(), INTERVAL 15 DAY), 7),
('BAO_TRI_DINH_KY', 60, DATE_ADD(CURDATE(), INTERVAL 45 DAY), 8);

-- ========================================
-- 10. PHIẾU BẢO TRỊ SỬA CHỮA (PhieuBaoTriSuaChua)
-- ========================================
INSERT INTO phieu_bao_tri_sua_chua (noi_dung, muc_do_uu_tien, trang_thai, ngay_yeu_cau, ngay_bat_dau, ngay_hoan_thanh, tong_chi_phi) VALUES
('Sửa máy lạnh bị rò nước', 'CAO', 'HOAN_THANH', '2024-01-10 08:00:00', '2024-01-11 09:00:00', '2024-01-11 11:30:00', 350000),
('Thay dây cáp HDMI máy chiếu', 'TRUNG_BINH', 'HOAN_THANH', '2024-01-12 10:00:00', '2024-01-13 14:00:00', '2024-01-13 15:00:00', 150000),
('Sạch bảng ghế bàn phòng học', 'THAP', 'HOAN_THANH', '2024-01-15 07:00:00', '2024-01-16 08:00:00', '2024-01-16 10:00:00', 200000),
('Kiểm tra và bảo dưỡng máy tính', 'TRUNG_BINH', 'DANG_XU_LY', '2024-01-18 09:00:00', '2024-01-19 10:00:00', NULL, NULL),
('Sửa quạt treo bị rung', 'CAO', 'MOI_TAO', '2024-01-20 14:00:00', NULL, NULL, NULL);

-- ========================================
-- 11. NHẬT KỲ SỰ KIỆN (NhatKySuKien)
-- ========================================
INSERT INTO nhat_ky_su_kien (hanh_dong, doi_tuong, ma_doi_tuong, ketqua, thoi_gian) VALUES
('TẠO', 'User', 1, 'THANH_CONG', '2024-01-10 08:00:00'),
('TẠO', 'User', 7, 'THANH_CONG', '2024-01-10 08:15:00'),
('TẠO', 'CoSoVatChat', 5, 'THANH_CONG', '2024-01-10 09:00:00'),
('CẬP NHẬT', 'CoSoVatChat', 5, 'THANH_CONG', '2024-01-11 10:00:00'),
('TẠO', 'KeHoachBaoTri', 1, 'THANH_CONG', '2024-01-12 11:00:00'),
('TẠO', 'PhieuBaoTri', 1, 'THANH_CONG', '2024-01-15 14:00:00'),
('CẬP NHẬT', 'PhieuBaoTri', 1, 'THANH_CONG', '2024-01-15 15:00:00'),
('XÓA', 'PhieuBaoTri', 5, 'THAT_BAI', '2024-01-16 08:00:00');

-- ========================================
-- 12. THÔNG BÁO HỆ THỐNG (ThongBaoHeThong)
-- ========================================
INSERT INTO thong_bao_he_thong (loai_thong_bao, noi_dung, da_doc, thoi_gian_gui) VALUES
('BAO_TRI_DEN_HAN', 'Máy lạnh phòng A101 cần bảo trì', 0, '2024-01-10 08:00:00'),
('THONG_BAO_CHUNG', 'Tạo tài khoản mới cho nhân viên', 1, '2024-01-10 09:00:00'),
('CANH_BAO_TON_KHO', 'Vật tư bút bi xanh sắp hết (tồn 25)', 0, '2024-01-11 10:00:00'),
('KIEM_KE_DINH_KY', 'Cập nhật CSVC phòng B101', 1, '2024-01-12 11:00:00'),
('BAO_TRI_DEN_HAN', 'Phiếu bảo trị #5 quá hạn', 0, '2024-01-18 14:00:00');

-- ========================================
-- VERIFY DATA
-- ========================================

SELECT 'USERS' as 'Table', COUNT(*) as 'Count' FROM nguoi_dung
UNION ALL
SELECT 'KHU NHÀ', COUNT(*) FROM khu_nha
UNION ALL
SELECT 'PHÒNG HỌC', COUNT(*) FROM phong_hoc
UNION ALL
SELECT 'LOẠI CSVC', COUNT(*) FROM loai_csvc
UNION ALL
SELECT 'NHÀ CUNG CẤP', COUNT(*) FROM nha_cung_cap
UNION ALL
SELECT 'KHO VẬT TƯ', COUNT(*) FROM kho_vat_tu
UNION ALL
SELECT 'VẬT TƯ', COUNT(*) FROM vat_tu_linh_kien
UNION ALL
SELECT 'CSVC', COUNT(*) FROM co_so_vat_chat
UNION ALL
SELECT 'KỀ HOẠCH BẢO TRỊ', COUNT(*) FROM ke_hoach_bao_tri_dinh_ky
UNION ALL
SELECT 'PHIẾU BẢO TRỊ', COUNT(*) FROM phieu_bao_tri_sua_chua
UNION ALL
SELECT 'NHẬT KỲ', COUNT(*) FROM nhat_ky_su_kien
UNION ALL
SELECT 'THÔNG BÁO', COUNT(*) FROM thong_bao_he_thong;

-- ========================================
-- SEED DATA COMPLETE
-- ========================================