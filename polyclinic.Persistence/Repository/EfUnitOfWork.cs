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
    public class EfUnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private readonly Lazy<IRepository<Appointment>> _appointmentRepository;
        private readonly Lazy<IRepository<Client>> _clientRepository;
        private readonly Lazy<IRepository<Doctor>> _doctorRepository;
        public EfUnitOfWork(AppDbContext context)
        {
            _context = context;
            _appointmentRepository = new Lazy<IRepository<Appointment>>(() => new EfRepository<Appointment>(context));
            _clientRepository = new Lazy<IRepository<Client>>(() => new EfRepository<Client>(context));
            _doctorRepository = new Lazy<IRepository<Doctor>>(() => new EfRepository<Doctor>(context));
        }
        IRepository<Appointment> IUnitOfWork.AppointmentRepository => _appointmentRepository.Value;
        IRepository<Client> IUnitOfWork.ClientRepository => _clientRepository.Value;
        IRepository<Doctor> IUnitOfWork.DoctorRepository => _doctorRepository.Value;
        public async Task CreateDatabaseAsync()
        {
            await _context.Database.EnsureCreatedAsync();
        }
        public async Task RemoveDatbaseAsync()
        {
            await _context.Database.EnsureDeletedAsync();
        }
        public async Task SaveAllAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
