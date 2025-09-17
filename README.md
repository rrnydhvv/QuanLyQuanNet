# Hệ thống Quản lý Quán Net

## Mô tả
Ứng dụng WinForms quản lý quán net được xây dựng bằng C# .NET 9.0 với Entity Framework Core và SQL Server.

## Tính năng chính

### ✅ Đã hoàn thành:
1. **Hệ thống đăng nhập và phân quyền**
   - Nhân viên, Quản lý, Quản trị viên
   - Mã hóa mật khẩu bằng BCrypt

2. **Kiến trúc phần mềm**
   - Model-Repository-Service pattern
   - Entity Framework Core với Code First
   - Dependency Injection

3. **Giao diện cơ bản**
   - Form đăng nhập
   - Form chính với menu điều hướng
   - Phân quyền theo chức vụ

### 🚧 Đang phát triển:
- Form quản lý máy trạm
- Form quản lý khách hàng  
- Form quản lý dịch vụ
- Form tạo hóa đơn
- Form thống kê báo cáo
- Form quản lý nhân viên

## Cấu trúc Database

### Bảng chính:
- **NhanVien**: Quản lý tài khoản nhân viên
- **NguoiDung**: Quản lý tài khoản khách hàng
- **MayTram**: Thông tin các máy trạm
- **DichVu**: Danh sách dịch vụ
- **HoaDon**: Hóa đơn thanh toán
- **ChiTietHoaDon**: Chi tiết dịch vụ trong hóa đơn

## Hướng dẫn cài đặt

### 1. Yêu cầu hệ thống:
- .NET 9.0 SDK
- SQL Server (LocalDB hoặc SQL Server Express)
- Visual Studio 2022 hoặc Visual Studio Code

### 2. Cài đặt Database:
1. Mở SQL Server Management Studio
2. Chạy script trong file `Database/CreateDatabase.sql`
3. Kiểm tra connection string trong `QuanNetDbContext.cs`

### 3. Chạy ứng dụng:
```bash
cd QuanLyQuanNet
dotnet run
```

## Thông tin đăng nhập mặc định

### Nhân viên:
- **Quản trị viên**: `admin` / `admin123`
- **Quản lý**: `quanly1` / `admin123`  
- **Nhân viên**: `nhanvien1` / `admin123`

### Khách hàng mẫu:
- `user001` đến `user005` / `admin123`

## Công nghệ sử dụng

- **Framework**: .NET 9.0
- **UI**: Windows Forms
- **Database**: SQL Server
- **ORM**: Entity Framework Core 8.0
- **Security**: BCrypt.Net-Next
- **Architecture**: Repository Pattern + Service Layer

## Cấu trúc thư mục

```
QuanLyQuanNet/
├── Models/              # Entity models
├── Data/               
│   ├── QuanNetDbContext.cs
│   └── Repositories/    # Repository classes
├── Services/           # Business logic services
├── Forms/              # WinForms UI
├── Database/           # SQL scripts
└── Program.cs          # Entry point
```

## Quy trình phát triển tiếp theo

1. **Form Quản lý Máy trạm**:
   - Hiển thị danh sách máy với trạng thái
   - Bật/tắt máy
   - Theo dõi thời gian sử dụng

2. **Form Quản lý Khách hàng**:
   - CRUD khách hàng
   - Nạp tiền tài khoản
   - Xem lịch sử sử dụng

3. **Form Hóa đơn**:
   - Tính tiền giờ chơi
   - Thêm dịch vụ
   - Xuất hóa đơn

4. **Form Thống kê**:
   - Doanh thu theo thời gian
   - Báo cáo sử dụng máy
   - Thống kê khách hàng

## Liên hệ
Dự án được phát triển bởi GitHub Copilot cho mục đích học tập và demo.