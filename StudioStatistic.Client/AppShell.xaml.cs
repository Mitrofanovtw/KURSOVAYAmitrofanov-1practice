using Microsoft.Maui.Controls;
using StudioStatistic.Client.Views;
namespace StudioStatistic.Client
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            UpdateButtonsVisibility();

            this.Navigated += OnNavigated;
        }

        private void OnNavigated(object sender, ShellNavigatedEventArgs e)
        {
            UpdateButtonsVisibility();
            UpdateUserInfo();
        }

        private void UpdateButtonsVisibility()
        {
            var hasToken = !string.IsNullOrEmpty(Preferences.Get("AccessToken", null));
            LogoutButton.IsVisible = hasToken;

            UpdateFlyoutItemsVisibility(hasToken);
        }

        private void UpdateFlyoutItemsVisibility(bool isLoggedIn)
        {
            var flyoutItems = Items.OfType<FlyoutItem>().ToList();
            foreach (var item in flyoutItems)
            {
                item.IsVisible = isLoggedIn;
            }

            FlyoutBehavior = isLoggedIn ? FlyoutBehavior.Flyout : FlyoutBehavior.Disabled;

            if (!isLoggedIn && CurrentState?.Location?.OriginalString != "//login")
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await GoToAsync("///login");
                });
            }
        }

        private void UpdateUserInfo()
        {
            var hasToken = !string.IsNullOrEmpty(Preferences.Get("AccessToken", null));

            if (hasToken)
            {
                var username = Preferences.Get("Username", "Пользователь");
                var role = Preferences.Get("Role", "User");
                UserNameLabel.Text = username + " (" + role + ")";
            }
            else
            {
                UserNameLabel.Text = "Гость";
            }
        }

        private async void OnLogoutClicked(object sender, EventArgs e)
        {
            bool confirm = await DisplayAlert("Выход",
                "Вы уверены, что хотите выйти из аккаунта?",
                "Да", "Нет");

            if (confirm)
            {
                Preferences.Remove("AccessToken");
                Preferences.Remove("Username");
                Preferences.Remove("Role");
                Preferences.Remove("FullName");

                UpdateButtonsVisibility();
                UpdateUserInfo();

                var loginPage = Handler.MauiContext.Services.GetRequiredService<LoginPage>();

                Application.Current.MainPage = new NavigationPage(loginPage);
            }
        }

        private async void OnExitAppClicked(object sender, EventArgs e)
        {
            bool confirm = await DisplayAlert("Выход из приложения",
                "Вы уверены, что хотите закрыть приложение?",
                "Да", "Нет");

            if (confirm)
            {
#if ANDROID
                Android.OS.Process.KillProcess(Android.OS.Process.MyPid());
#endif
            }
        }

        protected override void OnNavigating(ShellNavigatingEventArgs args)
        {
            base.OnNavigating(args);

            var hasToken = !string.IsNullOrEmpty(Preferences.Get("AccessToken", null));
            var target = args.Target.Location.OriginalString;

            if (!hasToken && !target.Contains("login"))
            {
                args.Cancel();
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await GoToAsync("///login");
                });
            }
        }
    }
}