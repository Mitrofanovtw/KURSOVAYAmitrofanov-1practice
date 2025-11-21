using StudioStatistic.Models.DTO;

namespace StudioStatistic.Services
{
    public interface IEngineerService
    {
        Task<IEnumerable<EngineerDto>> GetAllAsync();
        Task<EngineerDto?> GetByIdAsync(int id);
        Task<EngineerDto> CreateAsync(CreateEngineerDto dto);
        Task<EngineerDto> UpdateAsync(int id, UpdateEngineerDto dto);
        Task<bool> DeleteAsync(int id);
    }
}