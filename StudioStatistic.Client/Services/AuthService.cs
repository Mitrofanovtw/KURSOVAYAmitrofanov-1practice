using StudioStatistic.Client.Models.DTO;

namespace StudioStatistic.Client.Services
{
    public class AuthService
    {
        public string? Token { get; private set; }
        public string? Username { get; private set; }
        public string? Role { get; private set; }

        public void SaveToken(AuthResponseDto response)
        {
            Token = response.Token;
            Username = response.Username;
            Role = response.Role;

            Preferences.Default.Set("jwt_token", Token ?? string.Empty);
            Preferences.Default.Set("user_name", Username ?? string.Empty);
            Preferences.Default.Set("user_role", Role ?? string.Empty);
        }

        public void LoadToken()
        {
            Token = Preferences.Default.Get<string>("jwt_token", string.Empty);
            Username = Preferences.Default.Get<string>("user_name", string.Empty);
            Role = Preferences.Default.Get<string>("user_role", string.Empty);

            if (string.IsNullOrWhiteSpace(Token))
                Token = null;
        }

        public void Logout()
        {
            Token = null;
            Username = null;
            Role = null;
            Preferences.Default.Remove("jwt_token");
            Preferences.Default.Remove("user_name");
            Preferences.Default.Remove("user_role");
        }

        public bool IsLoggedIn => !string.IsNullOrEmpty(Token);
    }
}