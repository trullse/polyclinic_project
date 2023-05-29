using polyclinic.UI.Views;

namespace polyclinic.UI
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(AppointmentDetailsView), typeof(AppointmentDetailsView));
            Routing.RegisterRoute(nameof(AddClientView), typeof(AddClientView));
            Routing.RegisterRoute(nameof(AddAppointmentView), typeof(AddAppointmentView));
            Routing.RegisterRoute(nameof(EditAppointmentView), typeof(EditAppointmentView));
            Routing.RegisterRoute(nameof(ClientSelectView), typeof(ClientSelectView));
            Routing.RegisterRoute(nameof(SubmitAppointmentView), typeof(SubmitAppointmentView));
            Routing.RegisterRoute(nameof(ClientDetailsView), typeof(ClientDetailsView));
            Routing.RegisterRoute(nameof(DoctorDetailsView), typeof(DoctorDetailsView));
            Routing.RegisterRoute(nameof(AddDoctorView), typeof(AddDoctorView));
        }
    }
}