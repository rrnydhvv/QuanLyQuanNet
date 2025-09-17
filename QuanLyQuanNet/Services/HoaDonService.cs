using Microsoft.EntityFrameworkCore;
using QuanLyQuanNet.Data;
using QuanLyQuanNet.Data.Repositories;
using QuanLyQuanNet.Models;

namespace QuanLyQuanNet.Services
{
    public class HoaDonService : IHoaDonService
    {
        private readonly QuanNetDbContext _context;
        private readonly IRepository<HoaDon> _hoaDonRepository;
        private readonly IRepository<ChiTietHoaDon> _chiTietHoaDonRepository;
        private readonly IRepository<DichVu> _dichVuRepository;
        private readonly IMayTramRepository _mayTramRepository;

        public HoaDonService(QuanNetDbContext context, 
            IRepository<HoaDon> hoaDonRepository,
            IRepository<ChiTietHoaDon> chiTietHoaDonRepository,
            IRepository<DichVu> dichVuRepository,
            IMayTramRepository mayTramRepository)
        {
            _context = context;
            _hoaDonRepository = hoaDonRepository;
            _chiTietHoaDonRepository = chiTietHoaDonRepository;
            _dichVuRepository = dichVuRepository;
            _mayTramRepository = mayTramRepository;
        }

        public async Task<HoaDon> TaoHoaDonAsync(int userId, int mayId, int nhanVienId)
        {
            var mayTram = await _mayTramRepository.GetByIdAsync(mayId);
            if (mayTram?.ThoiGianBatDau == null)
                throw new InvalidOperationException("Máy chưa được bật hoặc không tồn tại");

            var hoaDon = new HoaDon
            {
                UserID = userId,
                MayID = mayId,
                GioBatDau = mayTram.ThoiGianBatDau.Value,
                GioKetThuc = DateTime.Now,
                NhanVienID = nhanVienId,
                NgayTao = DateTime.Now
            };

            return await _hoaDonRepository.AddAsync(hoaDon);
        }

        public async Task<bool> ThemDichVuAsync(int hoaDonId, int dichVuId, int soLuong)
        {
            var dichVu = await _dichVuRepository.GetByIdAsync(dichVuId);
            if (dichVu == null || !dichVu.TrangThai || dichVu.SoLuongTon < soLuong)
                return false;

            // Kiểm tra xem dịch vụ đã tồn tại trong hóa đơn chưa
            var chiTietExisting = await _context.ChiTietHoaDons
                .FirstOrDefaultAsync(ct => ct.HoaDonID == hoaDonId && ct.DichVuID == dichVuId);

            if (chiTietExisting != null)
            {
                // Cập nhật số lượng
                if (dichVu.SoLuongTon < soLuong)
                    return false;

                chiTietExisting.SoLuong += soLuong;
                chiTietExisting.ThanhTien = chiTietExisting.SoLuong * chiTietExisting.DonGia;
                await _chiTietHoaDonRepository.UpdateAsync(chiTietExisting);
            }
            else
            {
                // Thêm mới
                var chiTiet = new ChiTietHoaDon
                {
                    HoaDonID = hoaDonId,
                    DichVuID = dichVuId,
                    SoLuong = soLuong,
                    DonGia = dichVu.DonGia,
                    ThanhTien = dichVu.DonGia * soLuong
                };

                await _chiTietHoaDonRepository.AddAsync(chiTiet);
            }

            // Cập nhật số lượng tồn
            dichVu.SoLuongTon -= soLuong;
            await _dichVuRepository.UpdateAsync(dichVu);

            return true;
        }

        public async Task<bool> XoaDichVuAsync(int hoaDonId, int dichVuId)
        {
            var chiTiet = await _context.ChiTietHoaDons
                .FirstOrDefaultAsync(ct => ct.HoaDonID == hoaDonId && ct.DichVuID == dichVuId);

            if (chiTiet == null) return false;

            // Hoàn lại số lượng tồn
            var dichVu = await _dichVuRepository.GetByIdAsync(dichVuId);
            if (dichVu != null)
            {
                dichVu.SoLuongTon += chiTiet.SoLuong;
                await _dichVuRepository.UpdateAsync(dichVu);
            }

            await _chiTietHoaDonRepository.DeleteAsync(chiTiet.CTID);
            return true;
        }

        public async Task<bool> CapNhatSoLuongDichVuAsync(int hoaDonId, int dichVuId, int soLuongMoi)
        {
            var chiTiet = await _context.ChiTietHoaDons
                .FirstOrDefaultAsync(ct => ct.HoaDonID == hoaDonId && ct.DichVuID == dichVuId);

            if (chiTiet == null) return false;

            var dichVu = await _dichVuRepository.GetByIdAsync(dichVuId);
            if (dichVu == null) return false;

            // Tính chênh lệch
            int chenhLech = soLuongMoi - chiTiet.SoLuong;

            // Kiểm tra tồn kho
            if (chenhLech > 0 && dichVu.SoLuongTon < chenhLech)
                return false;

            // Cập nhật
            chiTiet.SoLuong = soLuongMoi;
            chiTiet.ThanhTien = soLuongMoi * chiTiet.DonGia;
            await _chiTietHoaDonRepository.UpdateAsync(chiTiet);

            // Cập nhật tồn kho
            dichVu.SoLuongTon -= chenhLech;
            await _dichVuRepository.UpdateAsync(dichVu);

            return true;
        }

        public async Task<decimal> TinhTienMayAsync(int hoaDonId)
        {
            var hoaDon = await _context.HoaDons
                .Include(h => h.MayTram)
                .FirstOrDefaultAsync(h => h.HoaDonID == hoaDonId);

            if (hoaDon == null || hoaDon.MayTram == null) return 0;

            var thoiGianSuDung = (hoaDon.GioKetThuc ?? DateTime.Now) - hoaDon.GioBatDau;
            var soGio = (decimal)thoiGianSuDung.TotalHours;
            
            // Làm tròn lên 15 phút
            soGio = Math.Ceiling(soGio * 4) / 4;
            
            return soGio * hoaDon.MayTram.GiaTheoGio;
        }

        public async Task<decimal> TinhTienDichVuAsync(int hoaDonId)
        {
            return await _context.ChiTietHoaDons
                .Where(ct => ct.HoaDonID == hoaDonId)
                .SumAsync(ct => ct.ThanhTien);
        }

        public async Task<decimal> TinhTongTienAsync(int hoaDonId)
        {
            var tienMay = await TinhTienMayAsync(hoaDonId);
            var tienDichVu = await TinhTienDichVuAsync(hoaDonId);

            var hoaDon = await _hoaDonRepository.GetByIdAsync(hoaDonId);
            if (hoaDon != null)
            {
                hoaDon.TienGio = tienMay;
                hoaDon.TienDichVu = tienDichVu;
                hoaDon.TongTien = tienMay + tienDichVu;
                await _hoaDonRepository.UpdateAsync(hoaDon);
            }

            return tienMay + tienDichVu;
        }

        public async Task<TimeSpan> TinhThoiGianSuDungAsync(int hoaDonId)
        {
            var hoaDon = await _hoaDonRepository.GetByIdAsync(hoaDonId);
            if (hoaDon == null) return TimeSpan.Zero;

            return (hoaDon.GioKetThuc ?? DateTime.Now) - hoaDon.GioBatDau;
        }

        public async Task<bool> ThanhToanAsync(int hoaDonId, decimal tienKhachDua, string phuongThucThanhToan = "Tiền mặt")
        {
            var hoaDon = await _hoaDonRepository.GetByIdAsync(hoaDonId);
            if (hoaDon == null) return false;

            // Cập nhật thông tin thanh toán
            hoaDon.GioKetThuc = DateTime.Now;
            await TinhTongTienAsync(hoaDonId); // Cập nhật tổng tiền
            
            hoaDon.DaThanhToan = true;
            hoaDon.NgayThanhToan = DateTime.Now;
            hoaDon.TienKhachDua = tienKhachDua;
            hoaDon.TienThua = tienKhachDua - hoaDon.TongTien;
            hoaDon.PhuongThucThanhToan = phuongThucThanhToan;

            await _hoaDonRepository.UpdateAsync(hoaDon);

            // Tắt máy
            await _mayTramRepository.TatMayAsync(hoaDon.MayID);

            return true;
        }

        public async Task<bool> HuyHoaDonAsync(int hoaDonId)
        {
            var hoaDon = await _context.HoaDons
                .Include(h => h.ChiTietHoaDons)
                .FirstOrDefaultAsync(h => h.HoaDonID == hoaDonId);

            if (hoaDon == null || hoaDon.DaThanhToan) return false;

            // Hoàn lại số lượng dịch vụ
            foreach (var chiTiet in hoaDon.ChiTietHoaDons)
            {
                var dichVu = await _dichVuRepository.GetByIdAsync(chiTiet.DichVuID);
                if (dichVu != null)
                {
                    dichVu.SoLuongTon += chiTiet.SoLuong;
                    await _dichVuRepository.UpdateAsync(dichVu);
                }
            }

            // Xóa chi tiết hóa đơn
            foreach (var chiTiet in hoaDon.ChiTietHoaDons)
            {
                await _chiTietHoaDonRepository.DeleteAsync(chiTiet.CTID);
            }

            // Xóa hóa đơn
            await _hoaDonRepository.DeleteAsync(hoaDonId);

            // Tắt máy
            await _mayTramRepository.TatMayAsync(hoaDon.MayID);

            return true;
        }

        public async Task<IEnumerable<HoaDon>> GetHoaDonChuaThanhToanAsync()
        {
            return await _context.HoaDons
                .Include(h => h.NguoiDung)
                .Include(h => h.MayTram)
                .Include(h => h.ChiTietHoaDons)
                .ThenInclude(ct => ct.DichVu)
                .Where(h => !h.DaThanhToan)
                .OrderBy(h => h.GioBatDau)
                .ToListAsync();
        }

        public async Task<IEnumerable<HoaDon>> GetHoaDonTheoNgayAsync(DateTime ngay)
        {
            var startDate = ngay.Date;
            var endDate = startDate.AddDays(1);

            return await _context.HoaDons
                .Include(h => h.NguoiDung)
                .Include(h => h.MayTram)
                .Include(h => h.ChiTietHoaDons)
                .ThenInclude(ct => ct.DichVu)
                .Where(h => h.NgayTao >= startDate && h.NgayTao < endDate)
                .OrderByDescending(h => h.NgayTao)
                .ToListAsync();
        }

        public async Task<IEnumerable<HoaDon>> GetHoaDonTheoKhachHangAsync(int userId)
        {
            return await _context.HoaDons
                .Include(h => h.MayTram)
                .Include(h => h.ChiTietHoaDons)
                .ThenInclude(ct => ct.DichVu)
                .Where(h => h.UserID == userId)
                .OrderByDescending(h => h.NgayTao)
                .ToListAsync();
        }

        public async Task<HoaDon?> GetHoaDonByIdAsync(int hoaDonId)
        {
            return await _context.HoaDons
                .Include(h => h.NguoiDung)
                .Include(h => h.MayTram)
                .Include(h => h.NhanVien)
                .Include(h => h.ChiTietHoaDons)
                .ThenInclude(ct => ct.DichVu)
                .FirstOrDefaultAsync(h => h.HoaDonID == hoaDonId);
        }

        public async Task<HoaDon?> GetHoaDonDangMoTheoMayAsync(int mayId)
        {
            return await _context.HoaDons
                .Include(h => h.NguoiDung)
                .Include(h => h.MayTram)
                .Include(h => h.ChiTietHoaDons)
                .ThenInclude(ct => ct.DichVu)
                .FirstOrDefaultAsync(h => h.MayID == mayId && !h.DaThanhToan);
        }

        public async Task<decimal> TinhDoanhThuTheoNgayAsync(DateTime ngay)
        {
            var startDate = ngay.Date;
            var endDate = startDate.AddDays(1);

            return await _context.HoaDons
                .Where(h => h.DaThanhToan && h.NgayThanhToan >= startDate && h.NgayThanhToan < endDate)
                .SumAsync(h => h.TongTien);
        }

        public async Task<int> DemSoHoaDonTheoNgayAsync(DateTime ngay)
        {
            var startDate = ngay.Date;
            var endDate = startDate.AddDays(1);

            return await _context.HoaDons
                .CountAsync(h => h.NgayTao >= startDate && h.NgayTao < endDate);
        }
    }
}