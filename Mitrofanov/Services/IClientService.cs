using StudioStatistic.DTO;

namespace StudioStatistic.Services
{
    public interface IClientService
    {
        Task<IEnumerable<ClientDto>> GetAllAsync();
        Task<ClientDto?> GetByIdAsync(int id);
        Task<ClientDto> CreateAsync(ClientDto dto);
        Task<ClientDto> UpdateAsync(int id, ClientDto dto);
        Task<bool> DeleteAsync(int id);
    }
}