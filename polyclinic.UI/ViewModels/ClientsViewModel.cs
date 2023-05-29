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
using System.Text.RegularExpressions;
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

        [ObservableProperty]
        string searchText = "";    

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

        [RelayCommand]
        public async Task GetClients(string searchText = "")
        {
            var clients = await _clientService.GetAllAsync();
            IEnumerable<Client> filtredClients = clients;

            if (!string.IsNullOrEmpty(searchText))
            {
                filtredClients = clients.Where(client => client.FullName.Contains(searchText));
            }
            if (!filtredClients.SequenceEqual(Clients))
            {
                await MainThread.InvokeOnMainThreadAsync(() =>
                {
                    Clients.Clear();
                    foreach (var doctor in filtredClients)
                    {
                        Clients.Add(doctor);
                    }
                });
            }
        }

        [RelayCommand]
        public void ClearSearch()
        {
            SearchText = string.Empty;
        }
    }
}
