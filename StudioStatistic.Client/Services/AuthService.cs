using StudioStatistic.Client.Models.DTO;

namespace StudioStatistic.Client.Services
{
    public class AuthService
    {
        public string? Token { get; private set; }
        public string? Role { get; private set; }
        public string? FullName { get; private set; }

        public void SaveAuth(AuthResponseDto response)
        {
            Token = response.Token;
            Role = response.Role;
            FullName = response.FullName;

            Preferences.Set("Token", Token ?? "");
            Preferences.Set("Role", Role ?? "");
            Preferences.Set("FullName", FullName ?? "");
        }

        public void LoadAuth()
        {
            Token = Preferences.Get("Token", null);
            Role = Preferences.Get("Role", null);
            FullName = Preferences.Get("FullName", null);
        }

        public void Logout()
        {
            Token = null;
            Role = null;
            FullName = null;
            Preferences.Clear();
        }
    }
}