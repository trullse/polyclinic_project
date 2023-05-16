using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using polyclinic.Application.Abstractions;
using polyclinic.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace polyclinic.UI.ViewModels
{
    [QueryProperty(nameof(AddDoctor), "AddDoctor")]
    [QueryProperty(nameof(AddDate), "AddDate")]
    [QueryProperty(nameof(AddClient), "AddClient")]
    public partial class SubmitAppointmentViewModel : ObservableObject
    {
        IAppointmentService _appointmentService;
        public SubmitAppointmentViewModel(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        [ObservableProperty]
        Doctor addDoctor;
        [ObservableProperty]
        Client addClient;
        [ObservableProperty]
        DateTime addDate;

        [RelayCommand]
        async void Submit() => await SubmitAsync();

        public async Task SubmitAsync()
        {
            await _appointmentService.AddAsync(new Appointment()
            {
                ClientId = AddClient.Id,
                DoctorId = AddDoctor.Id,
                AppointmentDate = AddDate
            });
            var toast = Toast.Make("Appointment successfully added!");
            await toast.Show();
            //await Shell.Current.GoToAsync("..");
            await Shell.Current.Navigation.PopToRootAsync();
        }
    }
}
