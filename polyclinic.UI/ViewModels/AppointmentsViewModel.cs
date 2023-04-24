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

namespace polyclinic.UI.ViewModels
{
    public partial class AppointmentsViewModel : ObservableObject
    {
        private readonly IClientService _clientService;
        private readonly IAppointmentService _appointmentsService;

        public AppointmentsViewModel(IClientService clientService, IAppointmentService appointmentsService)
        {
            _clientService = clientService;
            _appointmentsService = appointmentsService;
        }

        public ObservableCollection<Client> Clients { get; set; } = new();
        public ObservableCollection<Appointment> Appointments { get; set; } = new();

        [ObservableProperty]
        Client selectedClient;

        [RelayCommand]
        async void UpdateClientsList() => await GetClients();
        [RelayCommand]
        async void UpdateAppointmentsList() => await GetAppointments();

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
            var appointments = await _appointmentsService.GetAllByClientId(SelectedClient.Id);
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
