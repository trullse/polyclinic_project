using polyclinic.UI.ViewModels;

namespace polyclinic.UI.Views;

public partial class DoctorsView : ContentPage
{
	public DoctorsView(DoctorsViewModel doctorsViewModel)
	{
		InitializeComponent();
		BindingContext = doctorsViewModel;
	}
}