using CommunityToolkit.Mvvm.ComponentModel;
using polyclinic.Application.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace polyclinic.UI.ViewModels
{
    public class DoctorsViewModel : ObservableObject
    {
        private readonly IDoctorService _doctorService;
        public DoctorsViewModel(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }
    }
}
