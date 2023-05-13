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
    public partial class ClientSelectViewModel : ObservableObject
    {
        private readonly IClientService _clientService;
        public ClientSelectViewModel(IClientService clientService)
        {
            _clientService= clientService;
        }

        public Doctor AddDoctor { get; set; }
        public DateTime AddDate { get; set; }

    }
}
