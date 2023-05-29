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
    [QueryProperty(nameof(SelectedDoctor), "SelectedDoctor")]
    public partial class AddShiftViewModel : ObservableObject
    {
        [ObservableProperty]
        DateTime dateStart = DateTime.Today;
        [ObservableProperty]
        DateTime dateEnd = DateTime.Today;
        [ObservableProperty]
        Doctor selectedDoctor;
        [ObservableProperty]
        bool startWithFirst = true;

        [ObservableProperty]
        string warningMessage = "";
        [ObservableProperty]
        bool warningVisible = false;

        private readonly IShiftService _shiftService;

        public AddShiftViewModel(IShiftService shiftService)
        {
            _shiftService = shiftService;
        }

        [RelayCommand]
        public async void AddShifts()
        {
            WarningVisible = false;
            if ((DateEnd - DateStart).Days > 60)
            {
                WarningMessage = "Date interval is too big (<= 60 is allowed)";
                WarningVisible = true;
                return;
            }
            if ((DateEnd - DateStart).Days < 0)
            {
                WarningMessage = "Date interval is incorrect";
                WarningVisible = true;
                return;
            }
            await _shiftService.AddOnInterval(SelectedDoctor, DateStart, DateEnd, StartWithFirst);
            var toast = Toast.Make("Shift successfully added!");
            await toast.Show();
            await Shell.Current.GoToAsync("..");
        }
    }
}
