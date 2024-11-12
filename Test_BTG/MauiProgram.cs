using Microsoft.Extensions.Logging;
using Test_BTG.Services;
using Test_BTG.ViewModel;

namespace Test_BTG
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

            builder.Services.AddSingleton<ClienteService>()
                   .AddSingleton<ViewModelCliente>();

            return builder.Build();
        }
    }
}
