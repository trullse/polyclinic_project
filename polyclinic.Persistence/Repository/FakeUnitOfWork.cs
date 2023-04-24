using Microsoft.EntityFrameworkCore;
using polyclinic.Domain.Abstractions;
using polyclinic.Domain.Entities;
using polyclinic.Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace polyclinic.Persistence.Repository
{
    public class FakeUnitOfWork : IUnitOfWork
    {
        private readonly Lazy<IRepository<Appointment>> _appointmentRepository;
        private readonly Lazy<IRepository<Client>> _clientRepository;
        //private readonly Lazy<IRepository<Doctor>> _doctorRepository;

        public FakeUnitOfWork()
        {
            _appointmentRepository = new Lazy<IRepository<Appointment>>(() => new FakeAppointmentRepository());
            _clientRepository = new Lazy<IRepository<Client>>(() => new FakeClientRepository());
            //_doctorRepository = new Lazy<IRepository<Doctor>>(() => new EfRepository<Doctor>(context));
        }
        IRepository<Appointment> IUnitOfWork.AppointmentRepository => _appointmentRepository.Value;
        IRepository<Client> IUnitOfWork.ClientRepository => _clientRepository.Value;

        public IRepository<Doctor> DoctorRepository => throw new NotImplementedException();

        public Task CreateDatabaseAsync()
        {
            //throw new NotImplementedException();
            return Task.CompletedTask;
        }

        public Task RemoveDatbaseAsync()
        {
            //throw new NotImplementedException();
            return Task.CompletedTask;
        }

        public Task SaveAllAsync()
        {
            //throw new NotImplementedException();
            return Task.CompletedTask;
        }
    }
}
