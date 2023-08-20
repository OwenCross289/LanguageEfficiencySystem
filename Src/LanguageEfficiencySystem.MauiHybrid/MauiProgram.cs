using Microsoft.Extensions.Logging;
using LanguageEfficiencySystem.MauiHybrid.Data;
using LanguageEfficiencySystem.Models;
using LanguageEfficiencySystem.Repositories;
using LanguageEfficiencySystem.Services;

namespace LanguageEfficiencySystem.MauiHybrid;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts => { fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular"); });

        builder.Services.AddMauiBlazorWebView();

#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
        builder.Logging.AddDebug();
#endif
        builder.Services.AddTransient<IDeveloperRepository, DeveloperRepository>();
        builder.Services.AddSingleton<WeatherForecastService>();

        return builder.Build();
    }
}