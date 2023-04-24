using polyclinic.UI.ViewModels;

namespace polyclinic.UI.Views;

public partial class AppointmentsView : ContentPage
{
	public AppointmentsView(AppointmentsViewModel appointmentsViewModel)
	{
		InitializeComponent();
		BindingContext = appointmentsViewModel;
	}
}