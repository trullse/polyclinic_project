using CommunityToolkit.Maui.Alerts;
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
	[QueryProperty(nameof(CurrentClient), "CurrentClient")]
	public partial class AddAppointmentViewModel : ObservableObject
	{
		private readonly IAppointmentService _appointmentService;
		private readonly IDoctorService _doctorService;
		private readonly IShiftService _shiftService;

		[ObservableProperty]
		Client currentClient;

		[ObservableProperty]
		DateTime appointmentDate;
		[ObservableProperty]
		bool doctorsVisible;
		[ObservableProperty]
		string doctorSearchText = "";
		[ObservableProperty]
		bool selectedDoctorVisible;
		[ObservableProperty]
		Doctor selectedDoctor;
		[ObservableProperty]
		bool talonsVisible;
		[ObservableProperty]
		Talon selectedTalon;
		[ObservableProperty]
		bool selectedTalonVisible;
		[ObservableProperty]
		string warning;
		[ObservableProperty]
		bool warningVisible;

		public ObservableCollection<Doctor> Doctors { get; set; } = new();

		public ObservableCollection<Talon> Talons { get; set; } = new();

		[RelayCommand]
		async void ContinueAdding() => await ContinueAddingAsync();
		[RelayCommand]
		async void ShowDoctors() => await SearchDoctorAsync();
		[RelayCommand]
		async void SelectDoctor(Doctor doctor) => await SelectDoctorAsync(doctor);
		[RelayCommand]
		async void ShowTalons() => await GetFreeTalonsAsync();
		[RelayCommand]
		async void SelectTalon(Talon talon) => await SelectTalonAsync(talon);

		public AddAppointmentViewModel(IAppointmentService appointmentService, IDoctorService doctorService, IShiftService shiftService)
		{
			_appointmentService = appointmentService;
			_doctorService = doctorService;
			_shiftService = shiftService;

			AppointmentDate = DateTime.Now;
			doctorsVisible = false;
			selectedDoctorVisible = false;
			selectedTalonVisible = false;
			talonsVisible = false;
			warningVisible = false;
		}

		public async Task ContinueAddingAsync()
		{
			if (SelectedDoctor == null)
			{
				ShowWarning("Choose the doctor");
				return;
			}
			if (AppointmentDate.Date < DateTime.Today.Date)
			{
				ShowWarning("Incorrect date");
				return;
			}
			if (SelectedTalon == null)
			{
				ShowWarning("Time is not selected");
				return;
			}
			if (CurrentClient == null)
			{
				IDictionary<string, object> parameters = new Dictionary<string, object>()
				{
					{ "AddDoctor", SelectedDoctor },
					{ "AddDate", AppointmentDate },
					{ "AddTalon", SelectedTalon }
				};
				await Shell.Current.GoToAsync(nameof(ClientSelectView), parameters);
			}
			else
			{
				throw new NotImplementedException();
			}
		}

		public async Task GetDoctorsAsync(Regex regex = null)
		{
			var doctors = await _doctorService.GetAllAsync();
			IEnumerable<Doctor> filtredDoctors = doctors;
			if (regex != null)
			{
				filtredDoctors = doctors.Where(doctor => regex.IsMatch(doctor.FullName));
			}
			if (!filtredDoctors.SequenceEqual(Doctors))
			{
				await MainThread.InvokeOnMainThreadAsync(() =>
				{
					Doctors.Clear();
					foreach (var doctor in filtredDoctors)
					{
						Doctors.Add(doctor);
					}
					if (!DoctorsVisible)
						DoctorsVisible = true;
				});
			}
		}

		public async Task GetFreeTalonsAsync()
		{
			await MainThread.InvokeOnMainThreadAsync(() =>
			{
				Talons.Clear();
				SelectedTalonVisible = false;
			});
			SelectedTalon = null;
			if (SelectedDoctor != null && AppointmentDate.Date >= DateTime.Today.Date)
			{
				var shift = await _shiftService.GetByDoctorAndDayAsync(SelectedDoctor.Id, DateOnly.FromDateTime(AppointmentDate));
				if (shift != null)
				{
					var freeTalons = shift.Talons.Where(t => t.IsBooked == false);
					await MainThread.InvokeOnMainThreadAsync(() =>
					{
						foreach (var talon in freeTalons)
						{
							Talons.Add(talon);
						}
						if (!TalonsVisible)
							TalonsVisible = true;
					});
				}
			}
			if (Talons.Count == 0)
			{
				ShowWarning("No talons available for this date");
			}
        }

		public async Task SearchDoctorAsync()
		{
			await GetDoctorsAsync(new Regex(DoctorSearchText));
		}

		public async Task SelectDoctorAsync(Doctor doctor)
		{
			await MainThread.InvokeOnMainThreadAsync(() =>
			{
				DoctorsVisible = false;
				SelectedDoctor = doctor;
				SelectedDoctorVisible = true;
			});

			await GetFreeTalonsAsync();
		}

		public async Task SelectTalonAsync(Talon talon)
		{
			await MainThread.InvokeOnMainThreadAsync(() =>
			{
				SelectedTalon = talon;
				SelectedTalonVisible = true;
			});
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
            MainThread.InvokeOnMainThreadAsync(() =>
            {
                if (WarningVisible)
                    WarningVisible = false;
            });
		}
	}
}
