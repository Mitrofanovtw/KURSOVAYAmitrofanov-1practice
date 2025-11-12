using StudioStatistic.Models.DTO;

namespace StudioStatistic.Services
{
    public interface IRequestService
    {
        Task<IEnumerable<RequestDto>> GetAllAsync();
        Task<RequestDto?> GetByIdAsync(int id);
        Task<RequestDto> CreateAsync(CreateRequestDto dto);
    }
}