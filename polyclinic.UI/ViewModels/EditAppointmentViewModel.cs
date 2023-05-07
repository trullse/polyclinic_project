using CommunityToolkit.Mvvm.ComponentModel;
using polyclinic.Application.Abstractions;
using polyclinic.Application.Services;
using polyclinic.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace polyclinic.UI.ViewModels
{
    [QueryProperty(nameof(Appointment), "Appointment")]
    public partial class EditAppointmentViewModel : ObservableObject
    {
        private readonly IAppointmentService _appointmentsService;

        [ObservableProperty]
        Appointment appointment;

        public EditAppointmentViewModel(IAppointmentService appointmentService)
        {
            _appointmentsService = appointmentService;
        }
    }
}
