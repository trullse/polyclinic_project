using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using polyclinic.Application.Abstractions;
using polyclinic.Application.Services;
using polyclinic.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace polyclinic.UI.ViewModels
{
    [QueryProperty(nameof(Appointment), "Appointment")]
    public partial class EditAppointmentViewModel : ObservableObject
    {
        private readonly IAppointmentService _appointmentService;
        private readonly IClientService _clientService;
        private readonly IDoctorService _doctorService;

        [ObservableProperty]
        Appointment appointment;

        [ObservableProperty]
        Client selectedClient;
        [ObservableProperty]
        Doctor selectedDoctor;
        [ObservableProperty]
        DateTime selectedDate;
        [ObservableProperty]
        string selectedDiagnosis;
        [ObservableProperty]
        double? selectedTreatmentCost;

        public ObservableCollection<Client> Clients { get; set; } = new();
        public ObservableCollection<Doctor> Doctors { get; set; } = new();

        [RelayCommand]
        async void GetInfo() => await SetInfoAsync();
        [RelayCommand]
        async void ApplyChanges() => await ApplyChangesAsync();
        [RelayCommand]
        async void GetImage() => await SetImageAsync();

        public EditAppointmentViewModel(IAppointmentService appointmentService, IClientService clientService, IDoctorService doctorService)
        {
            _appointmentService = appointmentService;
            _clientService = clientService;
            _doctorService = doctorService;
        }

        public async Task SetInfoAsync()
        {
            var clients = await _clientService.GetAllAsync();
            var doctors = await _doctorService.GetAllAsync();

            await MainThread.InvokeOnMainThreadAsync(() =>
            {
                Clients.Clear();
                foreach (var client in clients)
                {
                    Clients.Add(client);
                }

                Doctors.Clear();
                foreach (var doctor in doctors)
                {
                    Doctors.Add(doctor);
                }

                SelectedClient = Appointment.Client;
                SelectedDoctor = Appointment.Doctor;
                SelectedDate = Appointment.AppointmentDate;
                SelectedDiagnosis = Appointment.Diagnosis;
                SelectedTreatmentCost = Appointment.TreatmentCost;
            });
        }

        public async Task ApplyChangesAsync()
        {
            try
            {
                if (Appointment.ClientId != SelectedClient.Id)
                    Appointment.ClientId = SelectedClient.Id;
                if (Appointment.DoctorId != SelectedDoctor.Id)
                    Appointment.DoctorId = SelectedDoctor.Id;
                if (Appointment.AppointmentDate != SelectedDate)
                    Appointment.AppointmentDate = SelectedDate;
                if (Appointment.Diagnosis != SelectedDiagnosis)
                    Appointment.Diagnosis = SelectedDiagnosis;
                if (Appointment.TreatmentCost != SelectedTreatmentCost)
                    Appointment.TreatmentCost = SelectedTreatmentCost;
                await _appointmentService.UpdateAsync(Appointment);
                var toast = Toast.Make("Appointment successfully changed!");
                await toast.Show();
                await Shell.Current.GoToAsync("../..");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        public async Task SetImageAsync()
        {
            FileResult result = await FilePicker.PickAsync(new PickOptions
            {
                FileTypes = FilePickerFileType.Images
            });
            if (result != null)
            {
                var stream = await result.OpenReadAsync();
                string imagesPath = Path.Combine(FileSystem.AppDataDirectory, @"/AppointmentImages/");
                if (!Path.Exists(imagesPath))
                    Directory.CreateDirectory(imagesPath);
                string imagePath = Path.Combine(imagesPath, $"appointment{Appointment.Id}.png");
                using FileStream fstream = new FileStream(imagePath, FileMode.OpenOrCreate);
                await stream.CopyToAsync(fstream);
            }
        }
    }
}
