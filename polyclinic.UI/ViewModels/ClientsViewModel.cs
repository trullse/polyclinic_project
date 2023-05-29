using CommunityToolkit.Mvvm.ComponentModel;
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
    public partial class ClientsViewModel : ObservableObject
    {
        private readonly IClientService _clientService;
        public ClientsViewModel(IClientService clientService)
        {
            _clientService = clientService;
        }

        public ObservableCollection<Client> Clients { get; set; } = new();

        [RelayCommand]
        public async void GetClients()
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

        [RelayCommand]
        public async Task ShowClientDetails(Client client)
        {
            return;
        }

        [RelayCommand]
        public async Task AddClient()
        {
            await Shell.Current.GoToAsync(nameof(AddClientView));
        }
    }
}
