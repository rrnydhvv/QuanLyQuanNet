-- Script tạo database QuanLyQuanNet
-- Chạy script này trên SQL Server để tạo database và dữ liệu mẫu

USE master;
GO

-- Xóa database nếu đã tồn tại
IF EXISTS(SELECT * FROM sys.databases WHERE name = 'QuanLyQuanNet')
BEGIN
    ALTER DATABASE QuanLyQuanNet SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE QuanLyQuanNet;
END
GO

-- Tạo database mới
CREATE DATABASE QuanLyQuanNet;
GO

USE QuanLyQuanNet;
GO

-- Tạo bảng NhanVien
CREATE TABLE NhanVien (
    NhanVienID int IDENTITY(1,1) PRIMARY KEY,
    HoTen nvarchar(100) NOT NULL,
    ChucVu int NOT NULL DEFAULT 0, -- 0: NhanVien, 1: QuanLy, 2: QuanTriVien
    Username nvarchar(50) NOT NULL UNIQUE,
    PasswordHash nvarchar(255) NOT NULL,
    CaLamViec nvarchar(100),
    SoDienThoai nvarchar(15),
    DiaChi nvarchar(200),
    NgayVaoLam datetime2 NOT NULL DEFAULT GETDATE(),
    TrangThai bit NOT NULL DEFAULT 1,
    Luong decimal(18,2) NOT NULL DEFAULT 0
);

-- Tạo bảng NguoiDung
CREATE TABLE NguoiDung (
    UserID int IDENTITY(1,1) PRIMARY KEY,
    HoTen nvarchar(100) NOT NULL,
    Username nvarchar(50) NOT NULL UNIQUE,
    PasswordHash nvarchar(255) NOT NULL,
    SoDu decimal(18,2) NOT NULL DEFAULT 0,
    NgayDangKy datetime2 NOT NULL DEFAULT GETDATE(),
    SoDienThoai nvarchar(15),
    DiaChi nvarchar(200)
);

-- Tạo bảng MayTram
CREATE TABLE MayTram (
    MayID int IDENTITY(1,1) PRIMARY KEY,
    TenMay nvarchar(50) NOT NULL,
    TrangThai int NOT NULL DEFAULT 0, -- 0: Trong, 1: DangSuDung, 2: Hong, 3: BaoTri
    ViTri nvarchar(100),
    GiaTheoGio decimal(18,2) NOT NULL DEFAULT 0,
    ThoiGianBatDau datetime2,
    UserIDHienTai int,
    FOREIGN KEY (UserIDHienTai) REFERENCES NguoiDung(UserID)
);

-- Tạo bảng DichVu
CREATE TABLE DichVu (
    DichVuID int IDENTITY(1,1) PRIMARY KEY,
    TenDichVu nvarchar(100) NOT NULL,
    DonGia decimal(18,2) NOT NULL DEFAULT 0,
    MoTa nvarchar(500),
    DonViTinh nvarchar(20),
    TrangThai bit NOT NULL DEFAULT 1,
    SoLuongTon int NOT NULL DEFAULT 0
);

-- Tạo bảng HoaDon
CREATE TABLE HoaDon (
    HoaDonID int IDENTITY(1,1) PRIMARY KEY,
    UserID int NOT NULL,
    MayID int NOT NULL,
    GioBatDau datetime2 NOT NULL,
    GioKetThuc datetime2,
    TienGio decimal(18,2) NOT NULL DEFAULT 0,
    TienDichVu decimal(18,2) NOT NULL DEFAULT 0,
    TongTien decimal(18,2) NOT NULL DEFAULT 0,
    NgayTao datetime2 NOT NULL DEFAULT GETDATE(),
    DaThanhToan bit NOT NULL DEFAULT 0,
    NhanVienID int,
    GhiChu nvarchar(500),
    FOREIGN KEY (UserID) REFERENCES NguoiDung(UserID),
    FOREIGN KEY (MayID) REFERENCES MayTram(MayID),
    FOREIGN KEY (NhanVienID) REFERENCES NhanVien(NhanVienID)
);

-- Tạo bảng ChiTietHoaDon
CREATE TABLE ChiTietHoaDon (
    CTID int IDENTITY(1,1) PRIMARY KEY,
    HoaDonID int NOT NULL,
    DichVuID int NOT NULL,
    SoLuong int NOT NULL DEFAULT 1,
    DonGia decimal(18,2) NOT NULL DEFAULT 0,
    ThanhTien decimal(18,2) NOT NULL DEFAULT 0,
    ThoiGianDat datetime2 NOT NULL DEFAULT GETDATE(),
    GhiChu nvarchar(200),
    FOREIGN KEY (HoaDonID) REFERENCES HoaDon(HoaDonID) ON DELETE CASCADE,
    FOREIGN KEY (DichVuID) REFERENCES DichVu(DichVuID)
);

-- Insert dữ liệu mẫu

-- Thêm nhân viên admin
INSERT INTO NhanVien (HoTen, ChucVu, Username, PasswordHash, CaLamViec, NgayVaoLam, TrangThai, Luong)
VALUES 
    (N'Administrator', 2, 'admin', '$2a$11$rJ8qLZKvGJy8P5UMlI4Zv.uV3PVHWWZoYqD3nW4WYGfhSqOJJj1a.', N'Toàn thời gian', GETDATE(), 1, 0),
    (N'Nguyễn Văn Nam', 1, 'quanly1', '$2a$11$rJ8qLZKvGJy8P5UMlI4Zv.uV3PVHWWZoYqD3nW4WYGfhSqOJJj1a.', N'Ca sáng', GETDATE(), 1, 8000000),
    (N'Trần Thị Lan', 0, 'nhanvien1', '$2a$11$rJ8qLZKvGJy8P5UMlI4Zv.uV3PVHWWZoYqD3nW4WYGfhSqOJJj1a.', N'Ca chiều', GETDATE(), 1, 5000000);

-- Thêm máy trạm
INSERT INTO MayTram (TenMay, TrangThai, ViTri, GiaTheoGio)
VALUES 
    (N'PC01', 0, N'Tầng 1 - Góc A', 10000),
    (N'PC02', 0, N'Tầng 1 - Góc A', 10000),
    (N'PC03', 0, N'Tầng 1 - Góc B', 12000),
    (N'PC04', 0, N'Tầng 1 - Góc B', 12000),
    (N'VIP01', 0, N'Phòng VIP', 20000),
    (N'VIP02', 0, N'Phòng VIP', 20000),
    (N'PC05', 0, N'Tầng 2 - Góc A', 10000),
    (N'PC06', 0, N'Tầng 2 - Góc A', 10000),
    (N'PC07', 0, N'Tầng 2 - Góc B', 12000),
    (N'PC08', 0, N'Tầng 2 - Góc B', 12000);

-- Thêm dịch vụ
INSERT INTO DichVu (TenDichVu, DonGia, MoTa, DonViTinh, TrangThai, SoLuongTon)
VALUES 
    (N'Nước ngọt Coca', 15000, N'Coca Cola 330ml', N'Lon', 1, 100),
    (N'Nước ngọt Pepsi', 15000, N'Pepsi 330ml', N'Lon', 1, 100),
    (N'Nước suối', 8000, N'Nước suối Aquafina 500ml', N'Chai', 1, 150),
    (N'Mì tôm', 8000, N'Mì tôm cung đình', N'Ly', 1, 50),
    (N'Bánh mì', 25000, N'Bánh mì thịt nướng', N'Ổ', 1, 20),
    (N'Trà đá', 5000, N'Trà đá tươi', N'Ly', 1, 200),
    (N'Cà phê', 12000, N'Cà phê sữa đá', N'Ly', 1, 80),
    (N'Kẹo', 5000, N'Kẹo mút các loại', N'Cái', 1, 200),
    (N'In tài liệu', 500, N'In đen trắng A4', N'Trang', 1, 1000),
    (N'In màu', 2000, N'In màu A4', N'Trang', 1, 500),
    (N'Photocopy', 300, N'Photocopy A4', N'Trang', 1, 2000),
    (N'Thuê tai nghe', 10000, N'Thuê tai nghe gaming', N'Cái', 1, 30);

-- Thêm một số người dùng mẫu
INSERT INTO NguoiDung (HoTen, Username, PasswordHash, SoDu, NgayDangKy, SoDienThoai)
VALUES 
    (N'Lê Văn An', 'user001', '$2a$11$rJ8qLZKvGJy8P5UMlI4Zv.uV3PVHWWZoYqD3nW4WYGfhSqOJJj1a.', 100000, GETDATE(), '0901234567'),
    (N'Phạm Thị Bình', 'user002', '$2a$11$rJ8qLZKvGJy8P5UMlI4Zv.uV3PVHWWZoYqD3nW4WYGfhSqOJJj1a.', 150000, GETDATE(), '0902345678'),
    (N'Hoàng Văn Cường', 'user003', '$2a$11$rJ8qLZKvGJy8P5UMlI4Zv.uV3PVHWWZoYqD3nW4WYGfhSqOJJj1a.', 80000, GETDATE(), '0903456789'),
    (N'Đặng Thị Dung', 'user004', '$2a$11$rJ8qLZKvGJy8P5UMlI4Zv.uV3PVHWWZoYqD3nW4WYGfhSqOJJj1a.', 200000, GETDATE(), '0904567890'),
    (N'Vũ Văn Em', 'user005', '$2a$11$rJ8qLZKvGJy8P5UMlI4Zv.uV3PVHWWZoYqD3nW4WYGfhSqOJJj1a.', 50000, GETDATE(), '0905678901');

PRINT N'Tạo database QuanLyQuanNet thành công!';
PRINT N'';
PRINT N'Thông tin đăng nhập mặc định:';
PRINT N'- Username: admin, Password: admin123 (Quản trị viên)';
PRINT N'- Username: quanly1, Password: admin123 (Quản lý)';
PRINT N'- Username: nhanvien1, Password: admin123 (Nhân viên)';
PRINT N'';
PRINT N'Người dùng mẫu:';
PRINT N'- Username: user001-user005, Password: admin123';

GO