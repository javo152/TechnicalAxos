using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;
using TechnicalAxos_JavierUezen.Services;
using TechnicalAxos_JavierUezen.ViewModels;

namespace TechnicalAxos_JavierUezen
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
                })
                .UseMauiCommunityToolkit();

#if DEBUG
            builder.Logging.AddDebug();
#endif
            builder.Services.AddSingleton<ICountryService, CountryService>();
            builder.Services.AddSingleton<MainPageViewModel>();
            builder.Services.AddSingleton<MainPage>();

            return builder.Build();
        }
    }
}