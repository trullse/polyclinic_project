using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore;
using polyclinic.Application.Abstractions;
using polyclinic.UI.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;

namespace polyclinic.UI.ViewModels
{
	public partial class StatisticsViewModel : ObservableObject
	{
		private readonly IStatisticsService _statisticsService;

		[ObservableProperty]
		DateTime date = DateTime.Now;

		public ObservableCollection<IncomeCell> IncomeCells { get; set; } = new();
		public ObservableCollection<IncomeCell> IncomeCells_month { get; set; } = new();

		[ObservableProperty]
		int appointments_count = 0;
		[ObservableProperty]
		int appointments_over = 0;
		[ObservableProperty]
		double current_income = 0;

		[ObservableProperty]
		int difference_appointments = 0;
		[ObservableProperty]
		double difference_income = 0;

		[ObservableProperty]
		int appointments_count_month = 0;
		[ObservableProperty]
		int appointments_over_month = 0;
		[ObservableProperty]
		double current_income_month = 0;

        [ObservableProperty]
        int difference_appointments_month = 0;
        [ObservableProperty]
        double difference_income_month = 0;

        public ObservableCollection<ISeries> Series { get; set; } = new ObservableCollection<ISeries>();

        public ObservableCollection<ISeries> SeriesMonth { get; set; } = new ObservableCollection<ISeries>();

        public ObservableCollection<Axis> XAxes { get; set; } = new ObservableCollection<Axis>
		{
			new Axis
			{
				IsVisible = false
			}
		};

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

			var day_before_statistics = await _statisticsService.GetDayStatistics(Date.AddDays(-1));
			Difference_appointments = Appointments_over - (int)day_before_statistics[1];
			Difference_income = Current_income - (int)day_before_statistics[2];

            var month_statistics = await _statisticsService.GetMonthStatistics(Date);
			Appointments_count_month = (int)month_statistics[0];
			Appointments_over_month = (int)month_statistics[1];
			Current_income_month = month_statistics[2];

            var month_before_statistics = await _statisticsService.GetMonthStatistics(Date.AddMonths(-1), true);
            Difference_appointments_month = Appointments_over_month - (int)month_before_statistics[1];
            Difference_income_month = Current_income_month - (int)month_before_statistics[2];

            IncomeCells.Clear();
			IncomeCells_month.Clear();
			for (int i = -2; i < 0; i++)
			{
				var statistics = await _statisticsService.GetDayStatistics(Date.AddDays(i));
				IncomeCells.Add(new IncomeCell(Date.AddDays(i).ToShortDateString(), (int)statistics[2]));
			}
			IncomeCells.Add(new IncomeCell(Date.ToShortDateString(), Current_income));
			for (int i = -2; i < 0; i++)
			{
				var statistics = await _statisticsService.GetMonthStatistics(Date.AddMonths(i));
				IncomeCells_month.Add(new IncomeCell(Date.AddMonths(i).ToString("MMMM"), (int)statistics[2]));
			}
			IncomeCells_month.Add(new IncomeCell(Date.ToString("MMMM"), Current_income_month));

			Series.Clear();
			Series.Add(new ColumnSeries<IncomeCell>
			{
				Values = IncomeCells,
				Mapping = (cell, point) =>
				{
					point.PrimaryValue = (double)cell.Income;
					point.SecondaryValue = point.Context.Index;
				},
				Fill = new SolidColorPaint(SKColors.MediumPurple),
				TooltipLabelFormatter = point => $"{point.PrimaryValue:C2}",
				DataLabelsPaint = new SolidColorPaint(SKColors.MediumPurple),
				DataLabelsFormatter = (point) => point.Model.Date,
				DataLabelsPosition = LiveChartsCore.Measure.DataLabelsPosition.Bottom
            });

            SeriesMonth.Clear();
            SeriesMonth.Add(new ColumnSeries<IncomeCell>
            {
                Values = IncomeCells_month,
                Mapping = (cell, point) =>
                {
                    point.PrimaryValue = (double)cell.Income;
                    point.SecondaryValue = point.Context.Index;
                },
                Fill = new SolidColorPaint(SKColors.MediumPurple),
                TooltipLabelFormatter = point => $"{point.PrimaryValue:C2}",
                DataLabelsPaint = new SolidColorPaint(SKColors.MediumPurple),
                DataLabelsFormatter = (point) => point.Model.Date,
                DataLabelsPosition = LiveChartsCore.Measure.DataLabelsPosition.Bottom
            });
        }
	}
}
