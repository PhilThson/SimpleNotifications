using System.Collections.ObjectModel;
using Manager.Commands;
using Manager.Domain.DTOs;
using Manager.Domain.ViewModels;
using Manager.Helpers;
using Manager.Interfaces;
using Manager.Interfaces.Commands;
using Manager.ViewModels.Abstract;

namespace Manager.ViewModels
{
    public class NotificationsListViewModel : BaseViewModel
	{
		private readonly IHttpClientService _httpClientService;

		public NotificationsListViewModel(IHttpClientService httpClientService)
		{
			_httpClientService = httpClientService;
			LoadCommand.IsExecutingChanged += OnIsExecutingChanged;
		}

		private bool _IsLoading;
		public bool IsLoading
		{
			get => _IsLoading;
			set
			{
				_IsLoading = value;
				RaisePropertyChanged(nameof(IsLoading));
			}
		}

		private ObservableCollection<NotificationViewModel> _Notifications;
		public ObservableCollection<NotificationViewModel> Notifications
		{
			get => _Notifications;
			set
			{
				_Notifications = value;
				RaisePropertyChanged(nameof(Notifications));
			}
		}

		private IAsyncCommand _LoadCommand;
		public IAsyncCommand LoadCommand =>
			_LoadCommand ??= new AsyncCommand(Load, onException: OnException);

		private async Task Load()
		{
			var notificationDtos = await _httpClientService.GetAllNotificationsAsync<NotificationDto>();
			var notificationVMs = notificationDtos.MapToListVM();
			Notifications = new ObservableCollection<NotificationViewModel>(notificationVMs);
		}

		private async void OnException(Exception e)
		{
            await Application.Current.MainPage.DisplayAlert("Błąd", e.Message, "OK");
        }

		private void OnIsExecutingChanged()
		{
			IsLoading = LoadCommand.IsExecuting;
		}
	}
}

