using StudioStatistic.Models.DTO;

namespace StudioStatistic.Services
{
    public interface IAdminService
    {
        Task<IEnumerable<AdminDto>> GetAllAsync();
        Task<AdminDto?> GetByIdAsync(int id);
        Task<AdminDto> CreateAsync(CreateAdminDto dto);
    }
}