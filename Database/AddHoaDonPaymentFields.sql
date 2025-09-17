-- Script to add missing payment fields to HoaDon table
-- Execute this script in SQL Server Management Studio or Azure Data Studio

USE QuanNetDB;  -- Change to your database name if different
GO

-- Check if columns exist before adding them
IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'HoaDon' AND COLUMN_NAME = 'NgayThanhToan')
BEGIN
    ALTER TABLE HoaDon ADD NgayThanhToan DATETIME2 NULL;
    PRINT 'Added NgayThanhToan column';
END
ELSE
    PRINT 'NgayThanhToan column already exists';

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'HoaDon' AND COLUMN_NAME = 'TienKhachDua')
BEGIN
    ALTER TABLE HoaDon ADD TienKhachDua DECIMAL(18,2) NOT NULL DEFAULT 0;
    PRINT 'Added TienKhachDua column';
END
ELSE
    PRINT 'TienKhachDua column already exists';

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'HoaDon' AND COLUMN_NAME = 'TienThua')
BEGIN
    ALTER TABLE HoaDon ADD TienThua DECIMAL(18,2) NOT NULL DEFAULT 0;
    PRINT 'Added TienThua column';
END
ELSE
    PRINT 'TienThua column already exists';

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'HoaDon' AND COLUMN_NAME = 'PhuongThucThanhToan')
BEGIN
    ALTER TABLE HoaDon ADD PhuongThucThanhToan NVARCHAR(50) NOT NULL DEFAULT N'Tiền mặt';
    PRINT 'Added PhuongThucThanhToan column';
END
ELSE
    PRINT 'PhuongThucThanhToan column already exists';

-- Verify the changes
SELECT COLUMN_NAME, DATA_TYPE, IS_NULLABLE, COLUMN_DEFAULT
FROM INFORMATION_SCHEMA.COLUMNS 
WHERE TABLE_NAME = 'HoaDon' 
  AND COLUMN_NAME IN ('NgayThanhToan', 'TienKhachDua', 'TienThua', 'PhuongThucThanhToan')
ORDER BY COLUMN_NAME;

PRINT 'Script execution completed successfully!';