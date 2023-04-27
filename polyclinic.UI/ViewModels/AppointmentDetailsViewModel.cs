﻿using CommunityToolkit.Mvvm.ComponentModel;
using polyclinic.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace polyclinic.UI.ViewModels
{
    [QueryProperty(nameof(Appointment), "Appointment")]
    public partial class AppointmentDetailsViewModel : ObservableObject
    {
        [ObservableProperty]
        Appointment appointment;
    }
}
