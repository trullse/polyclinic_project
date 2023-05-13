using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using polyclinic.Domain.Entities;
using polyclinic.UI.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace polyclinic.UI.ViewModels
{
    [QueryProperty(nameof(SelectedAppointment), "Appointment")]
    public partial class AppointmentDetailsViewModel : ObservableObject
    {
        [ObservableProperty]
        Appointment selectedAppointment;

        [RelayCommand]
        public async Task EditAppointment()
        {
            IDictionary<string, object> parameters = new Dictionary<string, object>()
            {
               { "Appointment", SelectedAppointment }
            };
            await Shell.Current.GoToAsync(nameof(EditAppointmentView), parameters);
        }
    }
}
