using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using StudioStatistic.Client.Models.DTO;
using StudioStatistic.Client.Services;

namespace StudioStatistic.Client.ViewModels
{
    public partial class LoginViewModel : ObservableObject
    {
        private readonly ApiService _apiService;

        [ObservableProperty]
        private string email = string.Empty;

        [ObservableProperty]
        private string password = string.Empty;

        [ObservableProperty]
        private bool isBusy;

        [ObservableProperty]
        private string errorMessage = string.Empty;

        [ObservableProperty]
        private bool hasError;

        public LoginViewModel(ApiService apiService)
        {
            _apiService = apiService;
        }

        [RelayCommand]
        private async Task Login()
        {
            if (string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(Password))
            {
                ErrorMessage = "Заполните все поля";
                HasError = true;
                return;
            }

            IsBusy = true;
            HasError = false;

            try
            {
                var request = new LoginRequestDto
                {
                    Email = Email,
                    Password = Password
                };

                var response = await _apiService.LoginAsync(request);

                if (response.Token != string.Empty)
                {
                    await Shell.Current.GoToAsync("///main");
                }
                else
                {
                    ErrorMessage = "Ошибка входа";
                    HasError = true;
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = "Нет соединения";
                HasError = true;
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}