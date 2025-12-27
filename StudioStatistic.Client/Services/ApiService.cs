using System.Text;
using System.Text.Json;
using StudioStatistic.Client.Models.DTO;

namespace StudioStatistic.Client.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;
        private readonly AuthService _authService;

        public ApiService(HttpClient httpClient, AuthService authService)
        {
            _httpClient = httpClient;
            _authService = authService;
            _authService.LoadAuth();
        }

        private void AddBearer()
        {
            if (!string.IsNullOrEmpty(_authService.Token))
            {
                _httpClient.DefaultRequestHeaders.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _authService.Token);
            }
        }

        public async Task<T> GetAsync<T>(string endpoint)
        {
            AddBearer();
            var response = await _httpClient.GetAsync(endpoint);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;
        }

        public async Task<TResponse> PostAsync<TRequest, TResponse>(string endpoint, TRequest data)
        {
            AddBearer();
            var json = JsonSerializer.Serialize(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(endpoint, content);
            response.EnsureSuccessStatusCode();
            var responseJson = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<TResponse>(responseJson, new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;
        }

        public async Task PutAsync<TRequest>(string endpoint, TRequest data)
        {
            AddBearer();
            var json = JsonSerializer.Serialize(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync(endpoint, content);
            response.EnsureSuccessStatusCode();
        }

        public async Task<AuthResponseDto> LoginAsync(LoginRequestDto dto)
        {
            var response = await PostAsync<LoginRequestDto, AuthResponseDto>("api/auth/login", dto);
            _authService.SaveAuth(response);
            return response;
        }

        public async Task<List<RequestDto>> GetMyRequestsAsync()
            => await GetAsync<List<RequestDto>>("api/requests/my");

        public async Task<RequestDto> CreateRequestAsync(CreateRequestDto dto)
    => await PostAsync<CreateRequestDto, RequestDto>("api/requests", dto);

        public async Task<List<RequestDto>> GetAllRequestsAsync()
            => await GetAsync<List<RequestDto>>("api/requests/all");

        public async Task UpdateStatusAsync(int id, UpdateStatusDto dto)
    => await PutAsync<UpdateStatusDto>($"api/requests/{id}/status", dto);

        public async Task<List<ClientDto>> GetClientsAsync()
            => await GetAsync<List<ClientDto>>("api/clients");

        public async Task<List<EngineerDto>> GetEngineersAsync()
            => await GetAsync<List<EngineerDto>>("api/engineers");

        public async Task<List<ServiceDto>> GetServicesAsync()
            => await GetAsync<List<ServiceDto>>("api/services");
    }
}