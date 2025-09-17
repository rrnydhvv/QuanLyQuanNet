using QuanLyQuanNet.Data.Repositories;
using QuanLyQuanNet.Models;

namespace QuanLyQuanNet.Services
{
    public class DichVuService : IDichVuService
    {
        private readonly IDichVuRepository _dichVuRepository;

        public DichVuService(IDichVuRepository dichVuRepository)
        {
            _dichVuRepository = dichVuRepository;
        }

        public async Task<IEnumerable<DichVu>> GetAllDichVuAsync()
        {
            return await _dichVuRepository.GetAllAsync();
        }

        public async Task<DichVu?> GetDichVuByIdAsync(int id)
        {
            return await _dichVuRepository.GetByIdAsync(id);
        }

        public async Task<bool> AddDichVuAsync(DichVu dichVu)
        {
            try
            {
                await _dichVuRepository.AddAsync(dichVu);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdateDichVuAsync(DichVu dichVu)
        {
            try
            {
                await _dichVuRepository.UpdateAsync(dichVu);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> DeleteDichVuAsync(int id)
        {
            try
            {
                return await _dichVuRepository.DeleteAsync(id);
            }
            catch
            {
                return false;
            }
        }

        public async Task<IEnumerable<DichVu>> GetDichVuConHangAsync()
        {
            return await _dichVuRepository.GetDichVuConHangAsync();
        }

        public async Task<bool> CapNhatSoLuongTonAsync(int dichVuId, int soLuongMoi)
        {
            return await _dichVuRepository.CapNhatSoLuongTonAsync(dichVuId, soLuongMoi);
        }

        public async Task<bool> NhapHangAsync(int dichVuId, int soLuongNhap)
        {
            return await _dichVuRepository.TangSoLuongTonAsync(dichVuId, soLuongNhap);
        }

        public async Task<bool> XuatHangAsync(int dichVuId, int soLuongXuat)
        {
            return await _dichVuRepository.GiamSoLuongTonAsync(dichVuId, soLuongXuat);
        }

        public async Task<IEnumerable<DichVu>> TimKiemDichVuAsync(string keyword)
        {
            return await _dichVuRepository.TimKiemDichVuAsync(keyword);
        }

        public async Task<bool> DoiTrangThaiDichVuAsync(int dichVuId, bool trangThai)
        {
            try
            {
                var dichVu = await _dichVuRepository.GetByIdAsync(dichVuId);
                if (dichVu == null) return false;

                dichVu.TrangThai = trangThai;
                await _dichVuRepository.UpdateAsync(dichVu);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}