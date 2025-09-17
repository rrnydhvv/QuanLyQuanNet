@echo off
echo Kiem tra va khoi dong SQL Server...

echo.
echo 1. Kiem tra trang thai SQL Server...
sc query MSSQLSERVER | findstr "STATE"

echo.
echo 2. Neu SQL Server chua chay, se thu khoi dong...
net start MSSQLSERVER 2>nul
if %errorlevel%==0 (
    echo SQL Server da duoc khoi dong thanh cong!
) else (
    echo SQL Server da dang chay hoac khong the khoi dong.
    echo Thu khoi dong SQL Server Browser...
    net start SQLBrowser 2>nul
)

echo.
echo 3. Kiem tra ket noi database...
sqlcmd -S LAPTOP-GE6ISH50 -E -Q "SELECT @@SERVERNAME as ServerName, DB_NAME() as CurrentDatabase" -d QuanLyQuanNet

echo.
echo 4. Khoi dong ung dung...
dotnet run --project QuanLyQuanNet

pause