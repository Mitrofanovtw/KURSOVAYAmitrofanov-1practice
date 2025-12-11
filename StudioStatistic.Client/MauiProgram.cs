using Microsoft.Extensions.Logging;
using Refit;
using StudioStatistic.Client.Views;
using StudioStatistic.Client.ViewModels; // ← ДОБАВИЛ ЭТО!
using StudioStatistic.Client.Services;

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

            builder.Services.AddSingleton<IApiService>(sp =>
            {
                var handler = new HttpClientHandler();
                if (DeviceInfo.Platform == DevicePlatform.Android)
                    handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;

                var client = new HttpClient(handler)
                {
                    BaseAddress = new Uri("https://localhost:7146")
                };
                return RestService.For<IApiService>(client);
            });

            builder.Services.AddSingleton<AuthService>();
            builder.Services.AddTransient<LoginPage>();
            builder.Services.AddTransient<LoginViewModel>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}