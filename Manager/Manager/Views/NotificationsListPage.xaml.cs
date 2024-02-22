using Manager.ViewModels;

namespace Manager.Views;

public partial class NotificationsListPage : ContentPage
{
	public NotificationsListPage(NotificationsListViewModel vm)
	{
        InitializeComponent();
        BindingContext = vm;
	}
}
