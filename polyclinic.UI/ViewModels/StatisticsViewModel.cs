using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using polyclinic.Application.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace polyclinic.UI.ViewModels
{
    public partial class StatisticsViewModel : ObservableObject
    {
        private readonly IStatisticsService _statisticsService;

        [ObservableProperty]
        DateTime date = DateTime.Now;

        [ObservableProperty]
        int appointments_count = 0;
        [ObservableProperty]
        int appointments_over = 0;
        [ObservableProperty]
        double current_income = 0;

        [ObservableProperty]
        int appointments_count_month = 0;
        [ObservableProperty]
        int appointments_over_month = 0;
        [ObservableProperty]
        double current_income_month = 0;

        public StatisticsViewModel(IStatisticsService statisticsService)
        {
            _statisticsService = statisticsService;
        }

        [RelayCommand]
        public async Task GetStatistics()
        {
            var day_statistics = await _statisticsService.GetDayStatistics(Date);
            Appointments_count = (int)day_statistics[0];
            Appointments_over = (int)day_statistics[1];
            Current_income = day_statistics[2];

            var month_statistics = await _statisticsService.GetMonthStatistics(Date, true);
            Appointments_count_month = (int)month_statistics[0];
            Appointments_over_month = (int)month_statistics[1];
            Current_income_month = month_statistics[2];
        }

    }
}
