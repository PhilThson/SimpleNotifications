using Manager.ViewModels;

namespace Manager.Views;

public partial class UsersListPage : ContentPage
{
	public UsersListPage(UsersListViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}
