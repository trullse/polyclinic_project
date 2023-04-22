using polyclinic.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace polyclinic.Domain.Abstractions
{
    public interface IUnitOfWork
    {
        IRepository<Appointment> AppointmentRepository { get; }
        IRepository<Client> ClientRepository { get; }
        IRepository<Doctor> DoctorRepository { get; }
        public Task RemoveDatbaseAsync();
        public Task CreateDatabaseAsync();
        public Task SaveAllAsync();
    }
}
