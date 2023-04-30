using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Maui.Alerts;
using polyclinic.Application.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using polyclinic.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace polyclinic.UI.ViewModels
{
    public partial class AddClientViewModel : ObservableObject
    {
        private readonly IClientService _clientService;

        public AddClientViewModel(IClientService clientService)
        {
            _clientService = clientService;
        }

        [ObservableProperty]
        string name;
        [ObservableProperty]
        string surname;
        [ObservableProperty]
        string patronymic;
        [ObservableProperty]
        DateTime birthDate;

        [ObservableProperty]
        string warning;
        [ObservableProperty]
        bool warningVisible = false;

        [RelayCommand]
        async void AddClient() => await AddClientDb();

        public async Task AddClientDb()
        {
            if (Name == null || Name.Length == 0)
            {
                ShowWarning("Enter name");
                return;
            }
            if (Surname == null || Name.Length == 0)
            {
                ShowWarning("Enter surname");
                return;
            }
            if (Patronymic != null && Patronymic.Length != 0)
            {
                await _clientService.AddAsync(new Client()
                {
                    Name = this.Name,
                    Surname = this.Surname,
                    Patronymic = this.Patronymic,
                    BirthDate = this.BirthDate,
                });
            }
            else
            {
                await _clientService.AddAsync(new Client()
                {
                    Name = this.Name,
                    Surname = this.Surname,
                    BirthDate = this.BirthDate,
                });
            }
            var toast = Toast.Make("Client successfully added!");
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
