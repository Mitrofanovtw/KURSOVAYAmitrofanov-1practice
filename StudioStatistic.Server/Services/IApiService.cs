using Refit;
using StudioStatistic.Web.Models.DTO;

namespace StudioStatistic.Web.Services
{
    public interface IApiService
    {
        [Post("/api/auth/login")]
        Task<AuthResponseDto> LoginAsync([Body] LoginRequestDto request);

        [Get("/api/clients")]
        Task<List<ClientDto>> GetClientsAsync([Header("Authorization")] string token);
    }
}