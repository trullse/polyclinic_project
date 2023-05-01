using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using polyclinic.Application.Abstractions;
using polyclinic.Domain.Entities;
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

        [ObservableProperty]
        Client currentClient;

        [ObservableProperty]
        DateTime appointmentDate;
        [ObservableProperty]
        bool doctorsVisible;
        [ObservableProperty]
        string doctorSearchText;
        [ObservableProperty]
        bool selectedDoctorVisible;
        [ObservableProperty]
        Doctor selectedDoctor;
        [ObservableProperty]
        string warning;
        [ObservableProperty]
        bool warningVisible;

        public ObservableCollection<Doctor> Doctors { get; set; } = new();

        [RelayCommand]
        async void AddAppointment() => await AddAppointmentAsync();
        [RelayCommand]
        async void ShowDoctors() => await SearchDoctorAsync();
        [RelayCommand]
        async void SelectDoctor(Doctor doctor) => await SelectDoctorAsync(doctor);

        public AddAppointmentViewModel(IAppointmentService appointmentService, IDoctorService doctorService)
        {
            _appointmentService = appointmentService;
            _doctorService = doctorService;
            AppointmentDate = DateTime.Now;
            doctorsVisible = false;
            selectedDoctorVisible = false;
            warningVisible = false;
        }

        public async Task AddAppointmentAsync()
        {
            if (SelectedDoctor == null)
            {
                ShowWarning("Choose doctor");
                return;
            }
            if (AppointmentDate < DateTime.Now)
            {
                ShowWarning("Incorrect date");
                return;
            }
            await _appointmentService.AddAsync(new Appointment()
            {
                ClientId = CurrentClient.Id,
                DoctorId = SelectedDoctor.Id,
                AppointmentDate = this.AppointmentDate
            });
            var toast = Toast.Make("Appointment successfully added!");
            await toast.Show();
            await Shell.Current.GoToAsync("..");
        }

        public async Task GetDoctorsAsync(Regex regex = null)
        {
            var doctors = await _doctorService.GetAllAsync();
            IEnumerable<Doctor> filtredDoctors = doctors;
            if (regex != null)
            {
                filtredDoctors = doctors.Where(doctor => regex.IsMatch(doctor.Surname));
            }
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

        public async Task SearchDoctorAsync()
        {
            if (DoctorSearchText != null && DoctorSearchText.Length >= 1)
            {
                await GetDoctorsAsync(new Regex(DoctorSearchText));
            }
        }

        public async Task SelectDoctorAsync(Doctor doctor)
        {
            await MainThread.InvokeOnMainThreadAsync(() =>
            {
                DoctorsVisible = false;
                SelectedDoctor = doctor;
                SelectedDoctorVisible = true;
            });
        }

        private void ShowWarning(string message)
        {
            Warning = message;
            if (!WarningVisible)
                WarningVisible = true;
        }

        private void CloseWarning()
        {
            if (WarningVisible)
                WarningVisible = false;
        }
    }
}
