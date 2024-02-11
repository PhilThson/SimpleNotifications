using System.Collections.ObjectModel;
using Manager.Commands;
using Manager.Interfaces;
using Manager.ViewModels.Abstract;
using Manager.Views;

namespace Manager.ViewModels;

public class MainWindowViewModel : BaseViewModel
{
	public MainWindowViewModel()
	{

	}

	string _Text;
	public string Text
	{
		get => _Text;
		set
		{
			_Text = value;
			RaisePropertyChanged(nameof(Text));
            AddCommand.RaiseCanExecuteChanged();
        }
	}

	private ObservableCollection<string> _Items;
	public ObservableCollection<string> Items =>
		_Items ??= new ObservableCollection<string>();

	private IBaseCommand _AddCommand;
	public IBaseCommand AddCommand =>
		_AddCommand ??= new BaseCommand(
			() => Items.Add(_Text),
			() => !string.IsNullOrWhiteSpace(_Text));

	private IBaseCommand _DeleteCommand;
	public IBaseCommand DeleteCommand =>
		_DeleteCommand ??= new BaseCommand(
			(object item) =>
			{
				if (Items.Contains(item.ToString()))
					Items.Remove(item.ToString());
			});

	private IAsyncCommand _TapCommand;
	public IAsyncCommand TapCommand =>
		_TapCommand ??= new AsyncCommand(
			async (object item) =>
				await Shell.Current.GoToAsync($"{nameof(DetailPage)}?Text={item}"
					//,new Dictionary<string, object>
					//{

					//}
					));

	public bool IsAddEnabled => AddCommand.CanExecute(null);
}