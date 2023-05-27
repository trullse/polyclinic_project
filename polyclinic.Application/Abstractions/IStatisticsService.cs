using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace polyclinic.Application.Abstractions
{
    public interface IStatisticsService
    {
        Task<List<double>> GetDayStatistics(DateTime date);
        /// <summary>
        /// Returns List that contains following info: appointments count, appointments over, current income
        /// </summary>
        Task<List<double>> GetMonthStatistics(DateTime date, bool tillTheDay = false);
        /// <summary>
        /// Returns List that contains following info: appointments count, appointments over, current income
        /// </summary>
    }
}
