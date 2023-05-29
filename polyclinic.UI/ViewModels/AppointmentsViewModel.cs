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
		[ObservableProperty]
		DateTime selectedDate = DateTime.Today;
		[ObservableProperty]
		bool dateVisible = false;

		[RelayCommand]
		async void UpdateAppointmentsList() => await GetAppointments();
		[RelayCommand]
		async void ShowAppointmentDetails(Appointment appointment) => await GotoAppointmentDetailsPage(appointment);
		[RelayCommand]
		async void AddAppointment(Client client) => await GotoAddAppointmentPage(client);

		public async Task GetAppointments()
		{
			await _appointmentsService.GetConnections();
			DateVisible = false;
			IReadOnlyList<Appointment> appointments;
			switch (SelectedFilter)
			{
				case Filter.All:
					appointments = await _appointmentsService.GetOnDateAsync(DateTime.Today);
					appointments = appointments.Where(a => a.AppointmentStatus != Appointment.Status.Missed 
					&& a.AppointmentStatus != Appointment.Status.Ended).ToList();
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
					DateVisible = true;
                    appointments = await _appointmentsService.GetOnDateAsync(SelectedDate);
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
