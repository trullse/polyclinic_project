using polyclinic.UI.ViewModels;

namespace polyclinic.UI.Views;

public partial class AddShiftView : ContentPage
{
	public AddShiftView(AddShiftViewModel addShiftViewModel)
	{
		InitializeComponent();
		BindingContext = addShiftViewModel;
	}
}