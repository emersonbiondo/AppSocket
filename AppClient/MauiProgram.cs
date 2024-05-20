using Microsoft.Extensions.Logging;
using Util;
using AppClient.Presentations.ViewModels;
using AppClient.Presentations.Views;
using CommunityToolkit.Maui;
using AppServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
#if WINDOWS
using Microsoft.Maui.LifecycleEvents;
using Microsoft.UI;
using Microsoft.UI.Windowing;
#endif

namespace AppClient
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

#if WINDOWS
            builder.ConfigureLifecycleEvents(events =>
            {
                events.AddWindows(wndLifeCycleBuilder =>
                {
                    wndLifeCycleBuilder.OnWindowCreated(window =>
                    {
                        IntPtr nativeWindowHandle = WinRT.Interop.WindowNative.GetWindowHandle(window);
                        WindowId win32WindowsId = Win32Interop.GetWindowIdFromWindow(nativeWindowHandle);
                        AppWindow winuiAppWindow = AppWindow.GetFromWindowId(win32WindowsId);
                        if (winuiAppWindow.Presenter is OverlappedPresenter p)
                        {
                            p.Maximize();
                        }
                    });
                });
            });
#endif
            var environmentName = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT_DEVELOPMENT");

            var configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile($"appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environmentName}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();

            var configuration = configurationBuilder.Build();

            var clientConfiguration = configuration.GetSection(nameof(ClientConfiguration)).Get<ClientConfiguration>();

            builder.Services.AddSingleton(clientConfiguration);

            builder.Services.AddSingleton<IAlertService, AlertService>();

            builder.Services.AddTransient<ClientViewModel>();

            builder.Services.AddTransient<ClientView>();

            return builder.Build();
        }
    }
}
