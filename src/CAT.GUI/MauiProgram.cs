using CAT.GUI.Functionalities.Analyzers.Pages;
using CAT.GUI.Functionalities.Analyzers.ViewModels;
using Microsoft.Extensions.Logging;

namespace CAT.GUI
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

            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddSingleton<MainVM>();

            builder.Services.AddSingleton<AnalyzersMainPage>();
            builder.Services.AddSingleton<AnalyzersMainVM>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}