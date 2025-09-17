-- Script đơn giản để tạo database QuanLyQuanNet
-- Chạy trong SQL Server Management Studio (SSMS)

-- Tạo database với cài đặt mặc định
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = N'QuanLyQuanNet')
BEGIN
    CREATE DATABASE [QuanLyQuanNet];
    PRINT 'Database QuanLyQuanNet đã được tạo thành công!';
END
ELSE
BEGIN
    PRINT 'Database QuanLyQuanNet đã tồn tại.';
END
GO

-- Sử dụng database
USE [QuanLyQuanNet];
GO

-- Test kết nối
SELECT 'Database sẵn sàng!' AS Status, DB_NAME() AS DatabaseName;