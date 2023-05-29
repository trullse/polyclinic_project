using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using polyclinic.Application.Abstractions;
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
		private readonly IAppointmentService _appointmentService;

		[ObservableProperty]
		Appointment selectedAppointment;
		[ObservableProperty]
		string appointmentDiagnosis = string.Empty;
		[ObservableProperty]
		double? appointmentCost = -1;
		[ObservableProperty]
		Appointment.Status appointmentStatus;

		[ObservableProperty]
		string diagnosisEntryText = string.Empty;
		[ObservableProperty]
		string costEntryText = string.Empty;

		[ObservableProperty]
		bool bookedVisible = false;
		[ObservableProperty]
		bool approvedVisible = false;
		[ObservableProperty]
		bool toPayVisible = false;
		[ObservableProperty]
		bool appointmentInfoVisible = false;
		[ObservableProperty]
		bool setInfoVisible = false;

		[ObservableProperty]
		bool warningVisible = false;
		[ObservableProperty]
		string warningMessage = string.Empty;

		public AppointmentDetailsViewModel(IAppointmentService appointmentService)
		{
			_appointmentService = appointmentService;
		}

		[RelayCommand]
		async Task ShowStatus()
		{
			await ShowAccordingStatus(SelectedAppointment.AppointmentStatus);
			AppointmentDiagnosis = SelectedAppointment.Diagnosis;
			AppointmentCost = SelectedAppointment.TreatmentCost;
			AppointmentStatus = SelectedAppointment.AppointmentStatus;
		}

		[RelayCommand]
		public async Task EditAppointment()
		{
			IDictionary<string, object> parameters = new Dictionary<string, object>()
			{
			   { "Appointment", SelectedAppointment }
			};
			await Shell.Current.GoToAsync(nameof(EditAppointmentView), parameters);
		}

		[RelayCommand]
		public async void ChangeStatus(Appointment.Status status)
		{
			if (WarningVisible)
				WarningVisible = false;
			try
			{
				SelectedAppointment.AppointmentStatus = status;
				await _appointmentService.UpdateAsync(SelectedAppointment);
				await ShowAccordingStatus(SelectedAppointment.AppointmentStatus);
                var toast = Toast.Make($"Appointment status was successfully changed!");
                await toast.Show();
            }
			catch(Exception ex)
			{
				await MainThread.InvokeOnMainThreadAsync(() =>
				{
					WarningMessage = "Failed changing status: " + ex.Message;
					WarningVisible = true;
				});
			}
		}

		[RelayCommand]
		public async void ShowEntries()
		{
			await MainThread.InvokeOnMainThreadAsync(() =>
			{
				ApprovedVisible = false;
				SetInfoVisible = true;
			});
		}

		[RelayCommand]
		public async void AddAppointmentInfo()
		{
			if (DiagnosisEntryText == string.Empty || CostEntryText == string.Empty)
			{
				await MainThread.InvokeOnMainThreadAsync(() =>
				{
					WarningMessage = "Enter information";
					WarningVisible = true;
				});
				return;
			}
			double treatmentCost;
			if (!Double.TryParse(CostEntryText, out treatmentCost))
			{
				await MainThread.InvokeOnMainThreadAsync(() =>
				{
					WarningMessage = "Wrong cost input";
					WarningVisible = true;
				});
				return;
			}
			try
			{
				SelectedAppointment.TreatmentCost = treatmentCost;
				SelectedAppointment.Diagnosis = DiagnosisEntryText;
				await _appointmentService.UpdateAsync(SelectedAppointment);
			}
			catch (Exception ex)
			{
				await MainThread.InvokeOnMainThreadAsync(() =>
				{
					WarningMessage = "Failed adding diagnosis and treatment cost: " + ex.Message;
					WarningVisible = true;
				});
				return;
			}
			ChangeStatus(Appointment.Status.ToPay);
		}

		public async Task ShowAccordingStatus(Appointment.Status status)
		{
			await MainThread.InvokeOnMainThreadAsync(() =>
			{
				WarningVisible = false;
				switch (status)
				{
					case Appointment.Status.Booked:
						BookedVisible = true;
						break;
					case Appointment.Status.Approved:
						if (BookedVisible)
							BookedVisible = false;
						ApprovedVisible = true;
						break;
					case Appointment.Status.ToPay:
						if (SetInfoVisible)
							SetInfoVisible = false;
						ToPayVisible = true;
						break;
					case Appointment.Status.Ended:
						if (ToPayVisible)
							ToPayVisible = false;
						break;
					case Appointment.Status.Missed:
						if (BookedVisible)
							BookedVisible = false;
						break;
				}
				if (status == Appointment.Status.ToPay || status == Appointment.Status.Ended)
				{
					AppointmentInfoVisible = true;
                    AppointmentDiagnosis = SelectedAppointment.Diagnosis;
                    AppointmentCost = SelectedAppointment.TreatmentCost;
					AppointmentStatus = SelectedAppointment.AppointmentStatus;
                }
			});
		}

		[RelayCommand]
		public async Task DeleteAppointment()
		{
			try
			{
				await _appointmentService.DeleteAsync(SelectedAppointment);
			}
			catch (Exception ex)
			{
                await MainThread.InvokeOnMainThreadAsync(() =>
                {
                    WarningMessage = "Failed deleting the appointment: " + ex.Message;
                    WarningVisible = true;
                });
                return;
            }
            var toast = Toast.Make($"Appointment was successfully deleted!");
            await toast.Show();
            await Shell.Current.Navigation.PopAsync();
        }
	}
}
