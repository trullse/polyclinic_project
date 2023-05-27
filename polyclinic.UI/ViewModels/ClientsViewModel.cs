using CommunityToolkit.Mvvm.ComponentModel;
using polyclinic.Application.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace polyclinic.UI.ViewModels
{
    public class ClientsViewModel : ObservableObject
    {
        private readonly IClientService _clientService;
        public ClientsViewModel(IClientService clientService)
        {
            _clientService = clientService;
        }
    }
}
