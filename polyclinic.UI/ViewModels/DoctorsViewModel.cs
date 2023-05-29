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
    public partial class DoctorsViewModel : ObservableObject
    {
        private readonly IDoctorService _doctorService;
        public DoctorsViewModel(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        public ObservableCollection<Doctor> Doctors { get; set; } = new();

        [ObservableProperty]
        string searchText = "";

        [RelayCommand]
        public async Task ShowDoctorDetails(Doctor doctor)
        {
            IDictionary<string, object> parameters = new Dictionary<string, object>()
            {
               { "SelectedDoctor", doctor }
            };
            await Shell.Current.GoToAsync(nameof(DoctorDetailsView), parameters);
        }

        [RelayCommand]
        public async Task AddDoctor()
        {
            //await Shell.Current.GoToAsync(nameof(AddDoctorView));
        }

        [RelayCommand]
        public async Task GetDoctors(string searchText = "")
        {
            var doctors = await _doctorService.GetAllAsync();
            IEnumerable<Doctor> filtredDoctors = doctors;

            if (!string.IsNullOrEmpty(searchText))
            {
                filtredDoctors = doctors.Where(doctor => doctor.FullName.Contains(searchText));
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
