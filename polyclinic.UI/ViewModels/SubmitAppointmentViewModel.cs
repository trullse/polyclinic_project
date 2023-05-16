using CommunityToolkit.Mvvm.ComponentModel;
using polyclinic.Application.Abstractions;
using polyclinic.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace polyclinic.UI.ViewModels
{
    [QueryProperty(nameof(AddDoctor), "AddDoctor")]
    [QueryProperty(nameof(AddDate), "AddDate")]
    [QueryProperty(nameof(AddClient), "AddClient")]
    public partial class SubmitAppointmentViewModel : ObservableObject
    {
        IAppointmentService _appointmentService;
        public SubmitAppointmentViewModel(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        [ObservableProperty]
        Doctor addDoctor;
        [ObservableProperty]
        Client addClient;
        [ObservableProperty]
        DateTime addDate;
    }
}
