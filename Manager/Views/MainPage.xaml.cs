using Manager.ViewModels;

namespace Manager.Views;

public partial class MainPage : ContentPage
{
	public MainPage(MainWindowViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}

