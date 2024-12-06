using Microsoft.Extensions.Logging;
using Microsoft.FluentUI.AspNetCore.Components;
using Microsoft.FluentUI.AspNetCore.Components.Components.Tooltip;
using System.Text.Json;
using TrainTrackChecker.Models;
using TrainTrackChecker.Services;

namespace TrainTrackChecker
{

    public static class MauiProgram {
        public static MauiApp CreateMauiApp() {

            var builder = MauiApp.CreateBuilder();

            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts => {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            builder.Services.AddMauiBlazorWebView();

#if DEBUG
    		builder.Services.AddBlazorWebViewDeveloperTools();
    		builder.Logging.AddDebug();
#endif

            builder.Services.AddBlazorBootstrap();

            builder.Services.AddSingleton(new HttpClient { BaseAddress = new Uri("http://10.0.2.2:5285") });
            
            builder.Services.AddSingleton<IApiService<Hardware>, ApiService<Hardware>>();
            builder.Services.AddSingleton<IApiService<Trecho>, ApiService<Trecho>>();
            builder.Services.AddSingleton<IApiService<Trecho_Registro>, ApiService<Trecho_Registro>>();
            builder.Services.AddSingleton<IApiService<OrdemManutencao>, ApiService<OrdemManutencao>>();
            builder.Services.AddSingleton<IApiService<OrdemManutencao_Local>, ApiService<OrdemManutencao_Local>>();

            builder.Services.AddScoped<ITooltipService, TooltipService>();

            builder.Services.AddFluentUIComponents();


            return builder.Build();
        }
    }
}
