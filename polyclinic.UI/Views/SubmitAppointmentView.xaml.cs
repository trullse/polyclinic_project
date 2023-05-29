using polyclinic.UI.ViewModels;

namespace polyclinic.UI.Views;

public partial class SubmitAppointmentView : ContentPage
{
	public SubmitAppointmentView(SubmitAppointmentViewModel submitAppointmentViewModel)
	{
		InitializeComponent();
		BindingContext = submitAppointmentViewModel;
	}
}