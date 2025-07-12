using DexMAUI.Services;
using DexMAUI.ViewModels;
using DexMAUI.Views;
using Microsoft.Extensions.Logging;

namespace DexMAUI
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
            ConfigureServices(builder.Services);
            return builder.Build();
        }
        #region Dependency Injection
        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<MainPage>();
            services.AddTransient<MainPageViewModel>();
            services.AddSingleton<DexUploadService>();
        }
        #endregion
    }
}
