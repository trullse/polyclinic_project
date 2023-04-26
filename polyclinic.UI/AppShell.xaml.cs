using polyclinic.UI.Views;

namespace polyclinic.UI
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(AppointmentDetailsView), typeof(AppointmentDetailsView));
        }
    }
}