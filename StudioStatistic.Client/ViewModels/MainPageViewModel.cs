using StudioStatistic.Client.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using StudioStatistic.Client.Views;
using CommunityToolkit.Mvvm.Input;

namespace StudioStatistic.Client.ViewModels
{
    public partial class MainPageViewModel : ObservableObject
    {
        private readonly AuthService _authService;

        [ObservableProperty]
        private string welcomeText;

        [ObservableProperty]
        private List<MenuItem> menuItems = new();

        public MainPageViewModel(AuthService authService)
        {
            _authService = authService;
            _authService.LoadAuth();

            WelcomeText = $"Добро пожаловать, {_authService.FullName} ({_authService.Role})";

            BuildMenu();
        }

        private void BuildMenu()
        {
            MenuItems.Clear();

            MenuItems.Add(new MenuItem { Title = "Мои записи", Icon = "my_requests.png", PageType = typeof(MyRequestsPage) });

            if (_authService.Role == "Client")
            {
                MenuItems.Add(new MenuItem { Title = "Новая запись", Icon = "new_request.png", PageType = typeof(CreateRequestPage) });
            }

            if (_authService.Role == "Engineer" || _authService.Role == "Admin")
            {
                MenuItems.Add(new MenuItem { Title = "Все записи студии", Icon = "all_requests.png", PageType = typeof(AllRequestsPage) });
            }

            if (_authService.Role == "Admin")
            {
                MenuItems.Add(new MenuItem { Title = "Клиенты", Icon = "clients.png", PageType = typeof(ClientsPage) });
                MenuItems.Add(new MenuItem { Title = "Инженеры", Icon = "engineers.png", PageType = typeof(EngineersPage) });
                MenuItems.Add(new MenuItem { Title = "Услуги", Icon = "services.png", PageType = typeof(ServicesPage) });
            }
        }

        [RelayCommand]
        private async Task Navigate(MenuItem item)
        {
            await Shell.Current.GoToAsync(item.PageType.FullName);
        }
        [RelayCommand]
        private async Task Logout()
        {
            _authService.Logout();

            await Shell.Current.GoToAsync("///login");
        }
    }

    public class MenuItem
    {
        public string Title { get; set; } = "";
        public string Icon { get; set; } = "";
        public Type PageType { get; set; }
    }
}