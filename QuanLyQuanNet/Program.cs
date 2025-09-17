using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using QuanLyQuanNet.Data;
using QuanLyQuanNet.Data.Repositories;
using QuanLyQuanNet.Forms;
using QuanLyQuanNet.Services;
using QuanLyQuanNet.Models;

namespace QuanLyQuanNet
{
    internal static class Program
    {
        public static IServiceProvider? ServiceProvider { get; private set; }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static async Task Main()
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                
                Console.WriteLine("Starting application...");
                
                // Setup dependency injection
                var services = new ServiceCollection();
                ConfigureServices(services);
                ServiceProvider = services.BuildServiceProvider();
                
                Console.WriteLine("Services configured successfully");
                
                // Initialize database
                await InitializeDatabase();
                
                var loginForm = new FormLogin();
                Console.WriteLine("Login form created successfully");
                
                Application.Run(loginForm);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Application error: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
                MessageBox.Show($"Lỗi khởi tạo ứng dụng: {ex.Message}\n\nVui lòng kiểm tra:\n1. SQL Server LocalDB đã được cài đặt\n2. SQL Server Express đã được cài đặt\n3. Kết nối mạng", 
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
            }
        }

        private static async Task InitializeDatabase()
        {
            try
            {
                using var scope = ServiceProvider?.CreateScope();
                var contextFactory = scope?.ServiceProvider.GetRequiredService<IDbContextFactory<QuanNetDbContext>>();
                
                if (contextFactory != null)
                {
                    using var context = await contextFactory.CreateDbContextAsync();
                    Console.WriteLine("Checking database...");
                    await context.Database.EnsureCreatedAsync();
                    Console.WriteLine("Database initialized successfully");
                    
                    // Update database schema for missing columns
                    UpdateDatabaseSchema(context);
                    
                    // Seed default data if needed
                    await SeedDefaultData(context);
                    
                    // Create some test data for invoice demo
                    await CreateTestDataForInvoice(context);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Database initialization error: {ex.Message}");
                throw new Exception($"Không thể khởi tạo database: {ex.Message}", ex);
            }
        }

        private static void UpdateDatabaseSchema(QuanNetDbContext context)
        {
            try
            {
                Console.WriteLine("Updating database schema...");
                
                // Execute SQL commands to add missing columns
                var updateScript = @"
                    IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'HoaDon' AND COLUMN_NAME = 'NgayThanhToan')
                    BEGIN
                        ALTER TABLE HoaDon ADD NgayThanhToan DATETIME2 NULL;
                    END

                    IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'HoaDon' AND COLUMN_NAME = 'TienKhachDua')
                    BEGIN
                        ALTER TABLE HoaDon ADD TienKhachDua DECIMAL(18,2) NOT NULL DEFAULT 0;
                    END

                    IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'HoaDon' AND COLUMN_NAME = 'TienThua')
                    BEGIN
                        ALTER TABLE HoaDon ADD TienThua DECIMAL(18,2) NOT NULL DEFAULT 0;
                    END

                    IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'HoaDon' AND COLUMN_NAME = 'PhuongThucThanhToan')
                    BEGIN
                        ALTER TABLE HoaDon ADD PhuongThucThanhToan NVARCHAR(50) NOT NULL DEFAULT N'Tiền mặt';
                    END
                ";

                context.Database.ExecuteSqlRaw(updateScript);
                Console.WriteLine("Database schema updated successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Warning: Could not update database schema: {ex.Message}");
                // Don't throw - this is not critical if the columns already exist
            }
        }

        private static async Task SeedDefaultData(QuanNetDbContext context)
        {
            try
            {
                // Check if we have any users
                if (!context.NguoiDungs.Any())
                {
                    Console.WriteLine("Seeding default users...");
                    
                    // Add default admin account
                    var admin = new NguoiDung
                    {
                        HoTen = "Quản trị viên",
                        Username = "admin",
                        PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin123"),
                        SoDu = 0,
                        NgayDangKy = DateTime.Now
                    };
                    context.NguoiDungs.Add(admin);
                    
                    // Add sample customers
                    var khach1 = new NguoiDung
                    {
                        HoTen = "Nguyễn Văn A",
                        Username = "khach001",
                        PasswordHash = BCrypt.Net.BCrypt.HashPassword("123456"),
                        SoDu = 50000,
                        NgayDangKy = DateTime.Now,
                        SoDienThoai = "0901234567"
                    };
                    
                    var khach2 = new NguoiDung
                    {
                        HoTen = "Trần Thị B",
                        Username = "khach002",
                        PasswordHash = BCrypt.Net.BCrypt.HashPassword("123456"),
                        SoDu = 30000,
                        NgayDangKy = DateTime.Now,
                        SoDienThoai = "0907654321"
                    };
                    
                    context.NguoiDungs.AddRange(khach1, khach2);
                }

                // Check if we have any machines
                if (!context.MayTrams.Any())
                {
                    Console.WriteLine("Seeding default machines...");
                    
                    for (int i = 1; i <= 10; i++)
                    {
                        var may = new MayTram
                        {
                            TenMay = $"PC{i:D2}",
                            TrangThai = TrangThaiMay.Trong,
                            GiaTheoGio = 15000, // 15,000 VND per hour
                            ViTri = $"Vị trí {i}"
                        };
                        context.MayTrams.Add(may);
                    }
                }

                // Check if we have any services
                if (!context.DichVus.Any())
                {
                    Console.WriteLine("Seeding default services...");
                    
                    var dichVus = new[]
                    {
                        new DichVu { TenDichVu = "Nước ngọt", DonGia = 10000, MoTa = "Các loại nước ngọt", DonViTinh = "Chai", SoLuongTon = 50 },
                        new DichVu { TenDichVu = "Snack", DonGia = 15000, MoTa = "Bánh kẹo các loại", DonViTinh = "Gói", SoLuongTon = 30 },
                        new DichVu { TenDichVu = "Mì tôm", DonGia = 20000, MoTa = "Mì tôm ăn liền", DonViTinh = "Ly", SoLuongTon = 20 },
                        new DichVu { TenDichVu = "Game card", DonGia = 50000, MoTa = "Thẻ game online", DonViTinh = "Thẻ", SoLuongTon = 100 },
                        new DichVu { TenDichVu = "In ấn", DonGia = 2000, MoTa = "Dịch vụ in ấn", DonViTinh = "Trang", SoLuongTon = 1000 }
                    };
                    
                    context.DichVus.AddRange(dichVus);
                }

                // Check if we have any employees
                if (!context.NhanViens.Any())
                {
                    Console.WriteLine("Seeding default employees...");
                    
                    var nhanViens = new[]
                    {
                        new NhanVien 
                        { 
                            HoTen = "Admin System", 
                            Username = "admin", 
                            PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin123"), 
                            ChucVu = ChucVu.QuanTriVien,
                            Luong = 10000000,
                            NgayVaoLam = DateTime.Now
                        },
                        new NhanVien 
                        { 
                            HoTen = "Nguyễn Quản Lý", 
                            Username = "quanly", 
                            PasswordHash = BCrypt.Net.BCrypt.HashPassword("ql123"), 
                            ChucVu = ChucVu.QuanLy,
                            Luong = 8000000,
                            NgayVaoLam = DateTime.Now
                        },
                        new NhanVien 
                        { 
                            HoTen = "Trần Nhân Viên", 
                            Username = "nhanvien", 
                            PasswordHash = BCrypt.Net.BCrypt.HashPassword("nv123"), 
                            ChucVu = ChucVu.NhanVien,
                            Luong = 5000000,
                            NgayVaoLam = DateTime.Now
                        }
                    };
                    
                    context.NhanViens.AddRange(nhanViens);
                }

                await context.SaveChangesAsync();
                Console.WriteLine("Default data seeded successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error seeding data: {ex.Message}");
            }
        }

        private static async Task CreateTestDataForInvoice(QuanNetDbContext context)
        {
            try
            {
                Console.WriteLine("Creating test data for invoice demo...");
                
                // Check if we already have machines in use
                var machinesInUse = context.MayTrams.Where(m => m.TrangThai == TrangThaiMay.DangSuDung).ToList();
                
                if (!machinesInUse.Any())
                {
                    // Get some users and machines for test
                    var testUser = context.NguoiDungs.FirstOrDefault(u => u.Username == "khach001");
                    var availableMachines = context.MayTrams
                        .Where(m => m.TrangThai == TrangThaiMay.Trong)
                        .Take(2)
                        .ToList();
                    
                    if (testUser != null && availableMachines.Any())
                    {
                        foreach (var machine in availableMachines)
                        {
                            machine.TrangThai = TrangThaiMay.DangSuDung;
                            machine.UserIDHienTai = testUser.UserID;
                            machine.ThoiGianBatDau = DateTime.Now.AddHours(-1); // Started 1 hour ago
                        }
                        
                        await context.SaveChangesAsync();
                        Console.WriteLine($"Set {availableMachines.Count} machines as in-use for testing");
                    }
                }
                else
                {
                    Console.WriteLine($"Already have {machinesInUse.Count} machines in use");
                }
                
                // Verify services exist
                var servicesCount = context.DichVus.Count();
                Console.WriteLine($"Available services: {servicesCount}");
                
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Warning: Could not create test data: {ex.Message}");
            }
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            // Database - Try multiple connection strings with better error handling
            try
            {
                string connectionString = GetConnectionString();
                Console.WriteLine($"Using connection string: {connectionString}");
                
                // Use DbContextFactory instead of direct DbContext registration
                services.AddDbContextFactory<QuanNetDbContext>(options =>
                {
                    options.UseSqlServer(connectionString);
                    options.EnableSensitiveDataLogging();
                    options.EnableDetailedErrors();
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Database configuration error: {ex.Message}");
                throw;
            }

            // Repositories
            services.AddScoped<IRepository<NguoiDung>, Repository<NguoiDung>>();
            services.AddScoped<IRepository<MayTram>, Repository<MayTram>>();
            services.AddScoped<IRepository<DichVu>, Repository<DichVu>>();
            services.AddScoped<IRepository<HoaDon>, Repository<HoaDon>>();
            services.AddScoped<IRepository<ChiTietHoaDon>, Repository<ChiTietHoaDon>>();
            services.AddScoped<IRepository<NhanVien>, Repository<NhanVien>>();
            
            services.AddScoped<INguoiDungRepository, NguoiDungRepository>();
            services.AddScoped<IMayTramRepository, MayTramRepository>();
            services.AddScoped<INhanVienRepository, NhanVienRepository>();
            services.AddScoped<IDichVuRepository, DichVuRepository>();

            // Services
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IQuanLyMayService, QuanLyMayService>();
            services.AddScoped<IHoaDonService, HoaDonService>();
            services.AddScoped<IDichVuService, DichVuService>();
        }

        private static string GetConnectionString()
        {
            // Connection strings in order of preference for SSMS/SQL Server
            var connectionConfigs = new[]
            {
                new { 
                    Name = "SQL Server LAPTOP-GE6ISH50",
                    ConnectionString = @"Server=LAPTOP-GE6ISH50;Database=QuanLyQuanNet;Trusted_Connection=true;MultipleActiveResultSets=true;ConnectRetryCount=0;TrustServerCertificate=true;"
                },
                new { 
                    Name = "SQL Server Default Instance",
                    ConnectionString = @"Server=.;Database=QuanLyQuanNet;Trusted_Connection=true;MultipleActiveResultSets=true;ConnectRetryCount=0;TrustServerCertificate=true;"
                },
                new { 
                    Name = "SQL Server localhost",
                    ConnectionString = @"Server=localhost;Database=QuanLyQuanNet;Trusted_Connection=true;MultipleActiveResultSets=true;ConnectRetryCount=0;TrustServerCertificate=true;"
                },
                new { 
                    Name = "SQL Server (local)",
                    ConnectionString = @"Server=(local);Database=QuanLyQuanNet;Trusted_Connection=true;MultipleActiveResultSets=true;ConnectRetryCount=0;TrustServerCertificate=true;"
                },
                new { 
                    Name = "SQL Server Express (SQLEXPRESS)",
                    ConnectionString = @"Server=.\SQLEXPRESS;Database=QuanLyQuanNet;Trusted_Connection=true;MultipleActiveResultSets=true;ConnectRetryCount=0;TrustServerCertificate=true;"
                },
                new { 
                    Name = "SQL Server Express (localhost)",
                    ConnectionString = @"Server=localhost\SQLEXPRESS;Database=QuanLyQuanNet;Trusted_Connection=true;MultipleActiveResultSets=true;ConnectRetryCount=0;TrustServerCertificate=true;"
                },
                new { 
                    Name = "SQL Server LocalDB (MSSQLLocalDB)",
                    ConnectionString = @"Server=(localdb)\MSSQLLocalDB;Database=QuanLyQuanNet;Trusted_Connection=true;MultipleActiveResultSets=true;ConnectRetryCount=0;"
                }
            };

            Console.WriteLine("Testing database connections...");
            
            foreach (var config in connectionConfigs)
            {
                try
                {
                    Console.WriteLine($"Trying: {config.Name}");
                    using var connection = new Microsoft.Data.SqlClient.SqlConnection(config.ConnectionString);
                    connection.Open();
                    
                    // Test if we can create a simple table (database exists check)
                    using var command = connection.CreateCommand();
                    command.CommandText = "SELECT 1";
                    command.ExecuteScalar();
                    
                    connection.Close();
                    Console.WriteLine($"✓ Successfully connected with: {config.Name}");
                    return config.ConnectionString;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"✗ Failed: {config.Name} - {ex.Message}");
                }
            }

            // If all fail, show helpful error message
            var errorMessage = @"
❌ Không thể kết nối đến SQL Server. Vui lòng kiểm tra:

1. SQL Server service đang chạy:
   - Mở Services (services.msc)
   - Tìm 'SQL Server (MSSQLSERVER)' và khởi động
   - Hoặc 'SQL Server (SQLEXPRESS)' nếu dùng Express

2. Trong SQL Server Management Studio:
   - Kết nối đến server
   - Tạo database 'QuanLyQuanNet' (nếu chưa có)
   - Đảm bảo Windows Authentication được bật

3. Hoặc chạy lệnh SQL:
   CREATE DATABASE QuanLyQuanNet;

Xem thêm hướng dẫn trong file SETUP_DATABASE.md
";
            
            Console.WriteLine(errorMessage);
            throw new Exception("Không thể kết nối đến SQL Server. Vui lòng kiểm tra SQL Server service và database.");
        }
    }
}
