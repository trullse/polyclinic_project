using polyclinic.UI.ViewModels;

namespace polyclinic.UI.Views;

public partial class AddDoctorView : ContentPage
{
	public AddDoctorView(AddDoctorViewModel addDoctorViewModel)
	{
		InitializeComponent();
		BindingContext = addDoctorViewModel;
	}
}