using polyclinic.UI.ViewModels;

namespace polyclinic.UI.Views;

public partial class StatisticsView : ContentPage
{
	public StatisticsView(StatisticsViewModel statisticsViewModel)
	{
		InitializeComponent();
		BindingContext = statisticsViewModel;
	}
}