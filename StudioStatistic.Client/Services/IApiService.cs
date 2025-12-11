using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Refit;
using StudioStatistic.Client.Models.DTO;

namespace StudioStatistic.Client.Services
{
    public interface IApiService
    {
        [Post("/api/auth/login")]
        Task<AuthResponseDto> LoginAsync([Body] LoginRequestDto request);

        [Post("/api/auth/register")]
        Task<AuthResponseDto> RegisterAsync([Body] RegisterRequestDto request);

        [Get("/api/clients")]
        Task<List<ClientDto>> GetClientsAsync([Header("Authorization")] string token);

        [Get("/api/engineers")]
        Task<List<EngineerDto>> GetEngineersAsync([Header("Authorization")] string token);

        [Post("/api/requests")]
        Task<RequestDto> CreateRequestAsync([Header("Authorization")] string token, [Body] CreateRequestDto request);
    }
}
