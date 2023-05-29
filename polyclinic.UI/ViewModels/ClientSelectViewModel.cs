using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using polyclinic.Application.Abstractions;
using polyclinic.Application.Services;
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
	[QueryProperty(nameof(AddDoctor), "AddDoctor")]
	[QueryProperty(nameof(AddDate), "AddDate")]
	[QueryProperty(nameof(AddTalon), "AddTalon")]
	public partial class ClientSelectViewModel : ObservableObject
	{
		private readonly IClientService _clientService;
		public ClientSelectViewModel(IClientService clientService)
		{
			_clientService= clientService;
		}

		public Doctor AddDoctor { get; set; }
		public DateTime AddDate { get; set; }
		public Talon AddTalon { get; set; }

        // Warning

        [ObservableProperty]
        string warning;

        [ObservableProperty]
        bool warningVisible;

        // Client picker

        public ObservableCollection<Client> Clients { get; set; } = new();

		[ObservableProperty]
		bool clientsVisible = true;

		[ObservableProperty]
		string clientSearchText = "";

		[ObservableProperty]
		bool selectedClientVisible = false;

		[ObservableProperty]
		Client selectedClient;

		[RelayCommand]
		async void ToConfirmation() => await SubmitClientAsync();
		[RelayCommand]
		async void ShowClients(string searchText) => await GetClientsAsync(searchText);
		[RelayCommand]
		async void SelectClient(Client client) => await SelectClientAsync(client);

		public async Task SelectClientAsync(Client client)
		{
			await MainThread.InvokeOnMainThreadAsync(() =>
			{
				ClientsVisible = false;
				SelectedClient = client;
				SelectedClientVisible = true;
			});
		}

		public async Task GetClientsAsync(string searchText = "")
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
					if (!ClientsVisible)
						ClientsVisible = true;
				});
			}
		}

		public async Task SubmitClientAsync()
		{
			if (SelectedClient == null)
			{
				ShowWarning("Choose client");
				return;
			}
			IDictionary<string, object> parameters = new Dictionary<string, object>()
				{
				   { "AddDoctor", AddDoctor },
				   { "AddDate", AddDate },
				   { "AddTalon", AddTalon },
				   { "AddClient", SelectedClient }
				};
			await Shell.Current.GoToAsync(nameof(SubmitAppointmentView), parameters);
		}

		private void ShowWarning(string message)
		{
			MainThread.InvokeOnMainThreadAsync(() =>
			{
                Warning = message;
                if (!WarningVisible)
                    WarningVisible = true;
            });
		}

		private void CloseWarning()
		{
			if (WarningVisible)
				WarningVisible = false;
		}
	}
}
