﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using polyclinic.Application.Abstractions;
using polyclinic.Domain.Entities;
using polyclinic.UI.Views;
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
        [ObservableProperty]
        bool addAppointmentVisible = false;

        [RelayCommand]
        async void UpdateClientsList() => await GetClients();
        [RelayCommand]
        async void UpdateAppointmentsList() => await GetAppointments();
        [RelayCommand]
        async void ShowAppointmentDetails(Appointment appointment) => await GotoAppointmentDetailsPage(appointment);
        [RelayCommand]
        async void AddClient() => await GotoAddClientPage();
        [RelayCommand]
        async void AddAppointment(Client client) => await GotoAddAppointmentPage(client);

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
            if (SelectedClient != null)
            {
                await _appointmentsService.GetConnections();
                var appointments = await _appointmentsService.GetAllByClientId(SelectedClient.Id);
                await MainThread.InvokeOnMainThreadAsync(() =>
                {
                    Appointments.Clear();
                    foreach (var appointment in appointments)
                    {
                        Appointments.Add(appointment);
                    }
                    if (!AddAppointmentVisible)
                        AddAppointmentVisible = true;
                });
            }
            else
            {
                await MainThread.InvokeOnMainThreadAsync(() => Appointments.Clear());
                AddAppointmentVisible = false;
            }
        }

        private async Task GotoAppointmentDetailsPage(Appointment appointment)
        {
            IDictionary<string, object> parameters = new Dictionary<string, object>()
            {
               { "Appointment", appointment }
            };
            await Shell.Current.GoToAsync(nameof(AppointmentDetailsView), parameters);
        }

        private async Task GotoAddClientPage()
        {
            await Shell.Current.GoToAsync(nameof(AddClientView));
        }

        private async Task GotoAddAppointmentPage(Client client)
        {
            IDictionary<string, object> parameters = new Dictionary<string, object>()
            {
               { "CurrentClient", client }
            };
            await Shell.Current.GoToAsync(nameof(AddAppointmentView), parameters);
        }
    }
}
