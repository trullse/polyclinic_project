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
        private readonly Lazy<IRepository<Shift>> _shiftRepository;
        private readonly Lazy<IRepository<Talon>> _talonRepository;
        public EfUnitOfWork(AppDbContext context)
        {
            _context = context;
            _appointmentRepository = new Lazy<IRepository<Appointment>>(() => new EfRepository<Appointment>(context));
            _clientRepository = new Lazy<IRepository<Client>>(() => new EfRepository<Client>(context));
            _doctorRepository = new Lazy<IRepository<Doctor>>(() => new EfRepository<Doctor>(context));
            _shiftRepository = new Lazy<IRepository<Shift>>(() => new EfRepository<Shift>(context));
            _talonRepository = new Lazy<IRepository<Talon>>(() => new EfRepository<Talon>(context));
        }
        IRepository<Appointment> IUnitOfWork.AppointmentRepository => _appointmentRepository.Value;
        IRepository<Client> IUnitOfWork.ClientRepository => _clientRepository.Value;
        IRepository<Doctor> IUnitOfWork.DoctorRepository => _doctorRepository.Value;
        IRepository<Shift> IUnitOfWork.ShiftRepository => _shiftRepository.Value;
        IRepository<Talon> IUnitOfWork.TalonRepository => _talonRepository.Value;
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
