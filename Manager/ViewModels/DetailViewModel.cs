using Manager.Commands;
using Manager.Interfaces;
using Manager.ViewModels.Abstract;

namespace Manager.ViewModels;

[QueryProperty("Text", "Text")]
public class DetailViewModel : BaseViewModel
{
	public DetailViewModel()
	{
	}

	private string _Text;
	public string Text
	{
		get => _Text;
		set
		{
			_Text = value;
			RaisePropertyChanged(nameof(Text));
        }
	}

	private async Task GoBack() => await Shell.Current.GoToAsync("..");

	private IAsyncCommand _GoBackCommand;
	public IAsyncCommand GoBackCommand =>
		_GoBackCommand ??= new AsyncCommand(GoBack);
}

