using StudioStatistic.Client.Views;

namespace StudioStatistic.Client
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            var loginPage = Handler.MauiContext.Services.GetRequiredService<LoginPage>();

            MainPage = new NavigationPage(loginPage);
        }
    }
}