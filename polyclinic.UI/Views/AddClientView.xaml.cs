using polyclinic.UI.ViewModels;

namespace polyclinic.UI.Views;

public partial class AddClientView : ContentPage
{
	public AddClientView(AddClientViewModel addClientViewModel)
	{
		InitializeComponent();
		BindingContext = addClientViewModel;
	}
}