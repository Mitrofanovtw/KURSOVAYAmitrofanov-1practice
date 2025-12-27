using Microsoft.Extensions.Logging;
using StudioStatistic.Client.Services;
using StudioStatistic.Client.ViewModels;
using StudioStatistic.Client.Views;

namespace StudioStatistic.Client
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
            builder.Logging.AddDebug();
#endif

            builder.Services.AddHttpClient("Api", client =>
            {
                client.BaseAddress = new Uri("http://10.0.2.2:5085/");
            });
            builder.Services.AddSingleton<AuthService>();
            builder.Services.AddTransient<ApiService>();

            builder.Services.AddSingleton<AppShell>();

            builder.Services.AddTransient<LoginPage>();
            builder.Services.AddTransient<LoginViewModel>();

            builder.Services.AddTransient<MyRequestsPage>();
            builder.Services.AddTransient<MyRequestsViewModel>();

            builder.Services.AddTransient<CreateRequestPage>();
            builder.Services.AddTransient<CreateRequestViewModel>();

            builder.Services.AddTransient<AllRequestsPage>();
            builder.Services.AddTransient<AllRequestsViewModel>();

            builder.Services.AddTransient<ClientsPage>();
            builder.Services.AddTransient<ClientsViewModel>();

            builder.Services.AddTransient<EngineersPage>();
            builder.Services.AddTransient<EngineersViewModel>();

            builder.Services.AddTransient<ServicesPage>();
            builder.Services.AddTransient<ServicesViewModel>();

            return builder.Build();
        }
    }
}