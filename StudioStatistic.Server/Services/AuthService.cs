using Microsoft.JSInterop;
using StudioStatistic.Web.Models.DTO;

namespace StudioStatistic.Web.Services
{
    public class AuthService
    {
        private readonly IJSRuntime _js;

        public AuthService(IJSRuntime js)
        {
            _js = js;
        }

        public string? Token { get; private set; }

        public async Task SaveTokenAsync(AuthResponseDto response)
        {
            Token = response.Token;
            await _js.InvokeVoidAsync("localStorage.setItem", "jwt_token", Token);
        }

        public async Task LoadTokenAsync()
        {
            Token = await _js.InvokeAsync<string>("localStorage.getItem", "jwt_token");
        }

        public async Task LogoutAsync()
        {
            Token = null;
            await _js.InvokeVoidAsync("localStorage.removeItem", "jwt_token");
        }

        public string? GetBearerToken() => Token is not null ? $"Bearer {Token}" : null;
    }
}