using polyclinic.UI.ViewModels;

namespace polyclinic.UI.Views;

public partial class EditAppointmentView : ContentPage
{
	public EditAppointmentView(EditAppointmentViewModel editAppointmentViewModel)
	{
		InitializeComponent();
		BindingContext = editAppointmentViewModel;
	}
}