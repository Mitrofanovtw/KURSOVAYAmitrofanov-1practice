using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using StudioStatistic.Client.Models.DTO;
using StudioStatistic.Client.Services;

namespace StudioStatistic.Client.ViewModels
{
    public partial class LoginViewModel : ObservableObject
    {
        [ObservableProperty]
        private string email = string.Empty;

        [ObservableProperty]
        private string password = string.Empty;

        [ObservableProperty]
        private string errorMessage = string.Empty;

        private readonly IApiService _api;
        private readonly AuthService _auth;

        public LoginViewModel(IApiService api, AuthService auth)
        {
            _api = api;
            _auth = auth;
        }

        [RelayCommand]
        private async Task Login()
        {
            try
            {
                ErrorMessage = string.Empty;

                var response = await _api.LoginAsync(new LoginRequestDto
                {
                    Email = Email,
                    Password = Password
                });

                // ← ВОТ ЭТО ИСПРАВЛЕНИЕ!
                _auth.SaveToken(response);

                await Shell.Current.GoToAsync("//main");
            }
            catch (Exception ex)
            {
                ErrorMessage = "Ошибка входа: " + ex.Message;
            }
        }
    }
}