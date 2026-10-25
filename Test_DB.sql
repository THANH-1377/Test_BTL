USE QuanLyBanhMi;
SELECT COUNT(*) AS SoBang FROM sys.tables WHERE schema_id = SCHEMA_ID('dbo');   -- kỳ vọng 35

SELECT 'ChiNhanh' AS Bang, COUNT(*) AS SoDong FROM dbo.ChiNhanh
UNION ALL SELECT 'NhanVien',   COUNT(*) FROM dbo.NhanVien
UNION ALL SELECT 'VaiTro',     COUNT(*) FROM dbo.VaiTro
UNION ALL SELECT 'DonVi',      COUNT(*) FROM dbo.DonVi
UNION ALL SELECT 'NguyenLieu', COUNT(*) FROM dbo.NguyenLieu
UNION ALL SELECT 'TonKho',     COUNT(*) FROM dbo.TonKho_ChiNhanh
UNION ALL SELECT 'SanPham',    COUNT(*) FROM dbo.SanPham;

SELECT MaVaiTro, MaCode FROM dbo.VaiTro;     -- phải thấy ADMIN / MANAGER / STAFF