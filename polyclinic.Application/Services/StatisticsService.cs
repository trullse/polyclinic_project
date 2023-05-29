using polyclinic.Application.Abstractions;
using polyclinic.Domain.Abstractions;
using polyclinic.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace polyclinic.Application.Services
{
	public class StatisticsService : IStatisticsService
	{
		private readonly IUnitOfWork _unitOfWork;
		public StatisticsService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public async Task<List<double>> GetDayStatistics(DateTime date)
		{
			List<double> results = new List<double>();
			int appointment_count;
			int appointments_over = 0;
			double current_income = 0;


			IReadOnlyList<Appointment> appointments = await _unitOfWork.AppointmentRepository.ListAsync(a => a.AppointmentDate.Date == date.Date);
			foreach (Appointment appointment in appointments)
			{
				if (appointment.AppointmentStatus == Appointment.Status.Ended)
				{
					appointments_over++;
					if (appointment.TreatmentCost != null)
						current_income += (double)appointment.TreatmentCost;
				}
			}
			appointment_count = appointments.Count();

			results.Add(appointment_count);
			results.Add(appointments_over);
			results.Add(current_income);

			return results;
		}

		public async Task<List<double>> GetMonthStatistics(DateTime date, bool tillTheDay = false)
		{
			List<double> results = new List<double>();
			int appointment_count;
			int appointments_over = 0;
			double current_income = 0;

			IReadOnlyList<Appointment> appointments;
			if (tillTheDay)
				appointments = await _unitOfWork.AppointmentRepository.ListAsync(a => (a.AppointmentDate.Month == date.Month && a.AppointmentDate.Day <= date.Day));
			else
				appointments = await _unitOfWork.AppointmentRepository.ListAsync(a => a.AppointmentDate.Month == date.Month);
			foreach (Appointment appointment in appointments)
			{
				if (appointment.AppointmentStatus == Appointment.Status.Ended)
				{
					appointments_over++;
					if (appointment.TreatmentCost != null)
						current_income += (double)appointment.TreatmentCost;
				}
			}
			appointment_count = appointments.Count();

			results.Add(appointment_count);
			results.Add(appointments_over);
			results.Add(current_income);

			return results;
		}
	}
}
