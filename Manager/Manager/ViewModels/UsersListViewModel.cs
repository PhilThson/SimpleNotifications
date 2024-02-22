using System.Collections.ObjectModel;
using Manager.Commands;
using Manager.Domain.DTOs;
using Manager.Domain.ViewModels;
using Manager.Helpers;
using Manager.Interfaces;
using Manager.Interfaces.Commands;
using Manager.ViewModels.Abstract;

namespace Manager.ViewModels;

public class UsersListViewModel : BaseViewModel
{
    private readonly IHttpClientService _httpClientService;

    public UsersListViewModel(IHttpClientService httpClientService)
    {
        LoadCommand.IsExecutingChanged += () => IsLoading = LoadCommand.IsExecuting;
        _httpClientService = httpClientService;
    }

    private ObservableCollection<UserViewModel> _Users;
    public ObservableCollection<UserViewModel> Users
    {
        get => _Users;
        set
        {
            _Users = value;
            RaisePropertyChanged(nameof(Users));
        }
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

    private IAsyncCommand _LoadCommand;
	public IAsyncCommand LoadCommand =>
		_LoadCommand ??= new AsyncCommand(LoadAsync, onException: OnException);

	private async Task LoadAsync()
	{
        var users = await _httpClientService.GetConnectedUsersAsync<UserDto>();
        Users = new ObservableCollection<UserViewModel>(users.MapToListVM());
    }

    private async void OnException(Exception e)
    {
        await Application.Current.MainPage.DisplayAlert("Błąd", e.Message, "OK");
    } 
}