using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using polyclinic.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace polyclinic.UI.ViewModels
{
    [QueryProperty(nameof(CurrentClient), "CurrentClient")]
    public partial class AddAppointmentViewModel : ObservableObject
    {
        [ObservableProperty]
        Client currentClient;
    }
}
