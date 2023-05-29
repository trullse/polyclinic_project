using polyclinic.UI.ViewModels;

namespace polyclinic.UI.Views;

public partial class AddAppointmentView : ContentPage
{
	public AddAppointmentView(AddAppointmentViewModel addAppointmentViewModel)
	{
		InitializeComponent();
		BindingContext = addAppointmentViewModel;
	}
}