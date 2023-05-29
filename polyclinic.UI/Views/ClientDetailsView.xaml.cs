using polyclinic.UI.ViewModels;

namespace polyclinic.UI.Views;

public partial class ClientDetailsView : ContentPage
{
	public ClientDetailsView(ClientDetailsViewModel clientDetailsViewModel)
	{
		InitializeComponent();
		BindingContext = clientDetailsViewModel;
	}
}