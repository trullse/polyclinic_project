using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using polyclinic.Application.Abstractions;
using polyclinic.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace polyclinic.UI.ViewModels
{
    public partial class AddDoctorViewModel : ObservableObject
    {
        private readonly IDoctorService _doctorService;

        public AddDoctorViewModel(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        [ObservableProperty]
        string name;
        [ObservableProperty]
        string surname;
        [ObservableProperty]
        string patronymic;
        [ObservableProperty]
        string specialization;
        [ObservableProperty]
        string qualification;

        [ObservableProperty]
        string warning;
        [ObservableProperty]
        bool warningVisible = false;

        [RelayCommand]
        async void AddDoctor() => await AddDoctorDb();

        public async Task AddDoctorDb()
        {
            if (Name == null || Name.Length == 0)
            {
                ShowWarning("Enter name");
                return;
            }
            if (Surname == null || Surname.Length == 0)
            {
                ShowWarning("Enter surname");
                return;
            }
            if (Specialization == null || Specialization.Length == 0)
            {
                ShowWarning("Enter specialization");
                return;
            }
            if (Qualification == null || Qualification.Length == 0)
            {
                ShowWarning("Enter qualification");
                return;
            }
            if (Patronymic != null && Patronymic.Length != 0)
            {
                await _doctorService.AddAsync(new Doctor()
                {
                    Name = this.Name,
                    Surname = this.Surname,
                    Patronymic = this.Patronymic,
                    Specialization = this.Specialization,
                    Qualification = this.Qualification
                });
            }
            else
            {
                await _doctorService.AddAsync(new Doctor()
                {
                    Name = this.Name,
                    Surname = this.Surname,
                    Specialization = this.Specialization,
                    Qualification = this.Qualification
                });
            }
            var toast = Toast.Make("Doctor successfully added!");
            await toast.Show();
            await Shell.Current.GoToAsync("..");
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
