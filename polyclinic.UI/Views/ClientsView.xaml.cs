using polyclinic.UI.ViewModels;

namespace polyclinic.UI.Views;

public partial class ClientsView : ContentPage
{
	public ClientsView(ClientsViewModel clientsViewModel)
	{
		InitializeComponent();
		BindingContext = clientsViewModel;
	}
}