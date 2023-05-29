using polyclinic.UI.ViewModels;

namespace polyclinic.UI.Views;

public partial class DoctorDetailsView : ContentPage
{
	public DoctorDetailsView(DoctorDetailsViewModel doctorDetailsViewModel)
	{
		InitializeComponent();
		BindingContext = doctorDetailsViewModel;
	}
}