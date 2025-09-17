using Microsoft.EntityFrameworkCore;
using QuanLyQuanNet.Data;
using QuanLyQuanNet.Data.Repositories;
using QuanLyQuanNet.Models;

namespace QuanLyQuanNet.Services
{
    public class HoaDonService : IHoaDonService
    {
        private readonly QuanNetDbContext _context;
        private readonly Repository<HoaDon> _hoaDonRepository;
        private readonly Repository<ChiTietHoaDon> _chiTietHoaDonRepository;
        private readonly Repository<DichVu> _dichVuRepository;
        private readonly MayTramRepository _mayTramRepository;

        public HoaDonService(QuanNetDbContext context, 
            Repository<HoaDon> hoaDonRepository,
            Repository<ChiTietHoaDon> chiTietHoaDonRepository,
            Repository<DichVu> dichVuRepository,
            MayTramRepository mayTramRepository)
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
                NhanVienID = nhanVienId
            };

            // Tính tiền giờ
            var thoiGianSuDung = hoaDon.GioKetThuc.Value - hoaDon.GioBatDau;
            var soGio = (decimal)thoiGianSuDung.TotalHours;
            soGio = Math.Ceiling(soGio * 4) / 4; // Làm tròn lên 15 phút
            hoaDon.TienGio = soGio * mayTram.GiaTheoGio;

            return await _hoaDonRepository.AddAsync(hoaDon);
        }

        public async Task<bool> ThemDichVuAsync(int hoaDonId, int dichVuId, int soLuong)
        {
            var dichVu = await _dichVuRepository.GetByIdAsync(dichVuId);
            if (dichVu == null || !dichVu.TrangThai || dichVu.SoLuongTon < soLuong)
                return false;

            var chiTiet = new ChiTietHoaDon
            {
                HoaDonID = hoaDonId,
                DichVuID = dichVuId,
                SoLuong = soLuong,
                DonGia = dichVu.DonGia,
                ThanhTien = dichVu.DonGia * soLuong
            };

            await _chiTietHoaDonRepository.AddAsync(chiTiet);

            // Cập nhật số lượng tồn
            dichVu.SoLuongTon -= soLuong;
            await _dichVuRepository.UpdateAsync(dichVu);

            return true;
        }

        public async Task<decimal> TinhTongTienAsync(int hoaDonId)
        {
            var hoaDon = await _context.HoaDons
                .Include(h => h.ChiTietHoaDons)
                .FirstOrDefaultAsync(h => h.HoaDonID == hoaDonId);

            if (hoaDon == null)
                return 0;

            hoaDon.TienDichVu = hoaDon.ChiTietHoaDons.Sum(ct => ct.ThanhTien);
            hoaDon.TongTien = hoaDon.TienGio + hoaDon.TienDichVu;

            await _hoaDonRepository.UpdateAsync(hoaDon);
            return hoaDon.TongTien;
        }

        public async Task<bool> ThanhToanAsync(int hoaDonId)
        {
            var hoaDon = await _hoaDonRepository.GetByIdAsync(hoaDonId);
            if (hoaDon == null)
                return false;

            hoaDon.DaThanhToan = true;
            await _hoaDonRepository.UpdateAsync(hoaDon);

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
                .ToListAsync();
        }
    }
}