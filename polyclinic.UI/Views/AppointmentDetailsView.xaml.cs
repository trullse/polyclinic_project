using polyclinic.UI.ViewModels;

namespace polyclinic.UI.Views;

public partial class AppointmentDetailsView : ContentPage
{
	public AppointmentDetailsView(AppointmentDetailsViewModel appointmentDetailsViewModel)
	{
		InitializeComponent();
		BindingContext = appointmentDetailsViewModel;
	}
}