using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using polyclinic.Domain.Entities;
using polyclinic.UI.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace polyclinic.UI.ViewModels
{
    [QueryProperty(nameof(SelectedDoctor), "SelectedDoctor")]
    public partial class DoctorDetailsViewModel : ObservableObject
    {
        [ObservableProperty]
        Doctor selectedDoctor;

        [RelayCommand]
        private async Task GotoShiftsManager()
        {
            IDictionary<string, object> parameters = new Dictionary<string, object>()
            {
               { "SelectedDoctor", SelectedDoctor }
            };
            await Shell.Current.GoToAsync(nameof(AddShiftView), parameters);
        }
    }
}
