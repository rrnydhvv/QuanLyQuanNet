# Hướng Dẫn Khắc Phục Sự Cố - QuanLyQuanNet

## 🔧 Các Vấn Đề Thường Gặp và Cách Khắc Phục

### 1. Lỗi Kết Nối Database
**Triệu chứng:**
```
A network-related or instance-specific error occurred while establishing a connection to SQL Server
```

**Cách khắc phục:**

#### Bước 1: Kiểm tra SQL Server đang chạy
```powershell
# Kiểm tra service
Get-Service | Where-Object {$_.Name -like "*SQL*"}

# Hoặc dùng Command Prompt
sc query MSSQLSERVER
```

#### Bước 2: Khởi động SQL Server nếu cần
```powershell
# PowerShell (chạy as Administrator)
Start-Service MSSQLSERVER
Start-Service SQLBrowser

# Hoặc Command Prompt (chạy as Administrator)
net start MSSQLSERVER
net start SQLBrowser
```

#### Bước 3: Kiểm tra kết nối
```cmd
sqlcmd -S LAPTOP-GE6ISH50 -E -Q "SELECT @@SERVERNAME"
sqlcmd -S . -E -Q "SELECT @@SERVERNAME"
sqlcmd -S localhost -E -Q "SELECT @@SERVERNAME"
```

### 2. Lỗi "App bị treo ngoài login"
**Nguyên nhân:** Thường do connection timeout hoặc database chưa được khởi tạo

**Cách khắc phục:**
1. **Đảm bảo SQL Server đang chạy**
2. **Kiểm tra database tồn tại:**
   ```sql
   sqlcmd -S LAPTOP-GE6ISH50 -E -Q "SELECT name FROM sys.databases WHERE name = 'QuanLyQuanNet'"
   ```
3. **Nếu database chưa tồn tại, chạy script tạo database:**
   ```sql
   sqlcmd -S LAPTOP-GE6ISH50 -E -i "Database\CreateDatabaseSimple.sql"
   ```

### 3. Lỗi DbContext Threading
**Triệu chứng:**
```
A second operation was started on this context instance before a previous operation completed. 
This is usually caused by different threads concurrently using the same instance of DbContext.
```

**Nguyên nhân:** Nhiều async operations đang sử dụng cùng một DbContext instance đồng thời

**Cách khắc phục đã thực hiện:**
- ✅ Thêm SemaphoreSlim để đồng bộ hóa database operations
- ✅ Bảo vệ tất cả async database calls với semaphore
- ✅ Đảm bảo proper disposal của semaphore

### 4. Lỗi Authentication Failed
**Nguyên nhân:** Username/password không đúng hoặc hash password bị lỗi

**Cách khắc phục:**
1. **Reset password cho admin:**
   ```sql
   -- Kết nối vào database
   sqlcmd -S LAPTOP-GE6ISH50 -E -d QuanLyQuanNet
   
   -- Tạo lại admin user với password hash mới
   UPDATE NhanVien 
   SET PasswordHash = '$2a$11$example_hash_here'
   WHERE Username = 'admin';
   ```

2. **Hoặc xóa và tạo lại admin:**
   ```sql
   DELETE FROM NhanVien WHERE Username = 'admin';
   -- Khởi động lại app để tự động tạo admin mới
   ```

## 🚀 Cách Khởi Động Ứng Dụng

### Cách 1: Sử dụng Batch File (Khuyến nghị)
```cmd
# Chạy file batch để tự động kiểm tra và khởi động
start-app.bat
```

### Cách 2: Thủ công
```powershell
# 1. Mở PowerShell as Administrator
# 2. Khởi động SQL Server
Start-Service MSSQLSERVER

# 3. Chuyển đến thư mục project
cd "C:\Users\phucb\Documents\Code\vs-dotNET\QuanLyQuanNet"

# 4. Chạy ứng dụng
dotnet run --project QuanLyQuanNet
```

## 🔍 Kiểm Tra Hệ Thống

### Kiểm tra các service SQL Server
```powershell
Get-Service | Where-Object {$_.Name -like "*SQL*"} | Select-Object Name, Status
```

### Kiểm tra connection strings
App sẽ thử các connection string theo thứ tự:
1. `Server=LAPTOP-GE6ISH50` (Máy cụ thể)
2. `Server=.` (Default instance)
3. `Server=localhost`
4. `Server=(local)`
5. `Server=.\SQLEXPRESS`
6. `Server=localhost\SQLEXPRESS`
7. `Server=(localdb)\MSSQLLocalDB`

### Test connection trực tiếp
```cmd
# Test với tên máy cụ thể
sqlcmd -S LAPTOP-GE6ISH50 -E -Q "SELECT 'Connection OK' as Status"

# Test với default instance
sqlcmd -S . -E -Q "SELECT 'Connection OK' as Status"
```

## 📝 Thông Tin Login Mặc Định
- **Username:** admin
- **Password:** admin123

## 🆘 Nếu Vẫn Gặp Lỗi
1. Kiểm tra Windows Firewall
2. Kiểm tra SQL Server Configuration Manager
3. Đảm bảo SQL Server Authentication mode = Mixed Mode
4. Restart SQL Server service
5. Restart máy tính

## 📞 Debug Mode
Để xem chi tiết lỗi, mở Command Prompt và chạy:
```cmd
cd "C:\Users\phucb\Documents\Code\vs-dotNET\QuanLyQuanNet"
dotnet run --project QuanLyQuanNet
```
Xem output console để biết chi tiết lỗi.