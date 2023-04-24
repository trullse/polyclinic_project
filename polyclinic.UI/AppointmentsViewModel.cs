using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using polyclinic.Application.Abstractions;
using polyclinic.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace polyclinic.UI
{
    public partial class AppointmentsViewModel : ObservableObject
    {
        IClientService _clientService;
        IAppointmentService _appointmentService;

        public AppointmentsViewModel(IClientService clientService, IAppointmentService appointmentService)
        {
            _clientService = clientService;
            _appointmentService = appointmentService;
        }

        public ObservableCollection<Client> Clients { get; set; } = new();
        public ObservableCollection<Appointment> Appointments { get; set; } = new();

        [ObservableProperty]
        Client selectedClient;

        [RelayCommand]
        async void UpdateClientsList() => await GetClients();
        [RelayCommand]
        async void UpdateAppoitmentsList() => await GetAppointments();

        public async Task GetClients()
        {
            var clients = await _clientService.GetAllAsync();
            await MainThread.InvokeOnMainThreadAsync(() =>
            {
                Clients.Clear();
                foreach (var client in clients)
                {
                    Clients.Add(client);
                }
            });
        }

        public async Task GetAppointments()
        {
            var appointments = await _appointmentService.GetAllByClientId(SelectedClient.Id);
            await MainThread.InvokeOnMainThreadAsync(() =>
            {
                Appointments.Clear();
                foreach (var appointment in appointments)
                {
                    Appointments.Add(appointment);
                }
            });
        }
    }
}
