using polyclinic.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace polyclinic.Application.Abstractions
{
    public interface IAppointmentService : IBaseService<Appointment>
    {
        Task AddWithTalonAsync(Appointment item, Talon talon);

        Task<IReadOnlyList<Appointment>> GetAllByClientId(int? clientId);

        Task<IReadOnlyList<Appointment>> GetOnDateAsync(DateTime date, Appointment.Status? status = null);

        Task GetConnections();
    }
}
