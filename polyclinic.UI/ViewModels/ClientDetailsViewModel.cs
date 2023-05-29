using CommunityToolkit.Mvvm.ComponentModel;
using polyclinic.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace polyclinic.UI.ViewModels
{
    [QueryProperty(nameof(SelectedClient), "SelectedClient")]
    public partial class ClientDetailsViewModel : ObservableObject
    {
        [ObservableProperty]
        Client selectedClient;
    }
}
