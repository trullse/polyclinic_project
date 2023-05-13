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
	public partial class AppointmentsViewModel : ObservableObject
	{
		private readonly IAppointmentService _appointmentsService;

		public AppointmentsViewModel(IClientService clientService, IAppointmentService appointmentsService)
		{
			_appointmentsService = appointmentsService;
		}

		public ObservableCollection<Appointment> Appointments { get; set; } = new();

        public enum Filter
        {
            All,
            Upcoming,
            Approved,
            Payment,
            History
        }

        [ObservableProperty]
		Filter selectedFilter = Filter.All;

		[RelayCommand]
		async void UpdateAppointmentsList() => await GetAppointments();
		[RelayCommand]
		async void ShowAppointmentDetails(Appointment appointment) => await GotoAppointmentDetailsPage(appointment);
		[RelayCommand]
		async void AddAppointment(Client client) => await GotoAddAppointmentPage(client);

		public async Task GetAppointments()
		{
			await _appointmentsService.GetConnections();
			IReadOnlyList<Appointment> appointments;
			switch (SelectedFilter)
			{
				case Filter.All:
					appointments = await _appointmentsService.GetOnDateAsync(DateTime.Today);
					break;
				case Filter.Upcoming:
					appointments = await _appointmentsService.GetOnDateAsync(DateTime.Today, Appointment.Status.Booked);
					break;
				case Filter.Approved:
                    appointments = await _appointmentsService.GetOnDateAsync(DateTime.Today, Appointment.Status.Approved);
                    break;
				case Filter.Payment:
                    appointments = await _appointmentsService.GetOnDateAsync(DateTime.Today, Appointment.Status.ToPay);
                    break;
                case Filter.History:
                    appointments = await _appointmentsService.GetAllAsync();
					break;
				default:
					throw new NotImplementedException();
            }
			await MainThread.InvokeOnMainThreadAsync(() =>
			{
				Appointments.Clear();
				foreach (var appointment in appointments)
				{
					Appointments.Add(appointment);
				}
			});
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
