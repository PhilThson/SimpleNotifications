using Manager.Interfaces;
using Manager.Services;
using Manager.ViewModels;
using Manager.Views;

namespace Manager;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder.Services.AddSingleton<MainPage>();
		builder.Services.AddSingleton<MainPageViewModel>();
		builder.Services.AddTransient<DetailPage>();
		builder.Services.AddTransient<DetailViewModel>();
		builder.Services.AddSingleton<NotificationsListPage>();
		builder.Services.AddSingleton<NotificationsListViewModel>();
		builder.Services.AddTransient<IHttpClientService, HttpClientService>();

		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		return builder.Build();
	}
}
