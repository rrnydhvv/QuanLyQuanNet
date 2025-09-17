-- Script để reset password cho các tài khoản mặc định
-- Password hash cho "admin123" được tạo bằng BCrypt với cost factor 11

USE QuanLyQuanNet;
GO

-- Update password cho admin (BCrypt hash của "admin123")
UPDATE NhanVien 
SET PasswordHash = '$2a$11$vFtXQKLdWLzj7NwzO1dFr.mH2HWNZWUJkV5Y1MwjEJhvPKGnF6f3K'
WHERE Username = 'admin';

-- Update password cho quanly1
UPDATE NhanVien 
SET PasswordHash = '$2a$11$vFtXQKLdWLzj7NwzO1dFr.mH2HWNZWUJkV5Y1MwjEJhvPKGnF6f3K'
WHERE Username = 'quanly1';

-- Update password cho nhanvien1
UPDATE NhanVien 
SET PasswordHash = '$2a$11$vFtXQKLdWLzj7NwzO1dFr.mH2HWNZWUJkV5Y1MwjEJhvPKGnF6f3K'
WHERE Username = 'nhanvien1';

-- Update password cho các user mẫu
UPDATE NguoiDung 
SET PasswordHash = '$2a$11$vFtXQKLdWLzj7NwzO1dFr.mH2HWNZWUJkV5Y1MwjEJhvPKGnF6f3K'
WHERE Username LIKE 'user%';

PRINT N'Đã cập nhật password thành công!';
PRINT N'Password cho tất cả tài khoản là: admin123';

GO