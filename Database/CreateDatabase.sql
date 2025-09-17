-- Tạo database cho Quản lý Quán Net
-- Chạy script này trong SQL Server Management Studio (SSMS)

USE master;
GO

-- Tạo database nếu chưa tồn tại
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = N'QuanLyQuanNet')
BEGIN
    CREATE DATABASE [QuanLyQuanNet]
    ON (
        NAME = 'QuanLyQuanNet',
        FILENAME = 'C:\Database\QuanLyQuanNet.mdf',
        SIZE = 100MB,
        MAXSIZE = 1GB,
        FILEGROWTH = 10MB
    )
    LOG ON (
        NAME = 'QuanLyQuanNet_Log',
        FILENAME = 'C:\Database\QuanLyQuanNet_Log.ldf',
        SIZE = 10MB,
        MAXSIZE = 100MB,
        FILEGROWTH = 1MB
    );
    
    PRINT 'Database QuanLyQuanNet đã được tạo thành công!';
END
ELSE
BEGIN
    PRINT 'Database QuanLyQuanNet đã tồn tại.';
END
GO

-- Sử dụng database vừa tạo
USE [QuanLyQuanNet];
GO

-- Kiểm tra kết nối
SELECT 
    'Database QuanLyQuanNet đã sẵn sàng!' AS Message,
    DB_NAME() AS CurrentDatabase,
    GETDATE() AS CurrentDateTime;

PRINT 'Script hoàn thành! Bây giờ bạn có thể chạy ứng dụng .NET';