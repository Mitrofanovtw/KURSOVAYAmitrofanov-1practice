using StudioStatistic.Models;
using StudioStatistic.Models.DTO;

namespace StudioStatistic.Services
{
    public interface IAdminService
    {
        Task<IEnumerable<AdminDto>> GetAllAsync();
        Task<AdminDto?> GetByIdAsync(int id);
        Task<AdminDto> CreateAsync(CreateAdminDto dto);
        Task<UserDto> ChangeUserRoleAsync(int userId, UserRole newRole);
    }
}