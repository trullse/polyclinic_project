using polyclinic.UI.ViewModels;

namespace polyclinic.UI.Views;

public partial class ClientSelectView : ContentPage
{
	public ClientSelectView(ClientSelectViewModel clientSelectViewModel)
	{
		InitializeComponent();
		BindingContext = clientSelectViewModel;
	}
}