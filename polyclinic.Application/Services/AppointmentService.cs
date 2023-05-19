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
    public class AppointmentService : IAppointmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        public AppointmentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public Task AddAsync(Appointment item)
        {
            _unitOfWork.AppointmentRepository.AddAsync(item);
            return _unitOfWork.SaveAllAsync();
        }

        public Task AddWithTalonAsync(Appointment item, Talon talon)
        {
            if (talon == null)
                throw new Exceptions.TalonException("Talon is null");
            if (talon.IsBooked)
                throw new Exceptions.TalonException("Talon already booked!");
            item.AppointmentDate = item.AppointmentDate.Add(talon.AppointmentTime.ToTimeSpan());
            _unitOfWork.AppointmentRepository.AddAsync(item);
            talon.IsBooked = true;
            _unitOfWork.TalonRepository.UpdateAsync(talon);
            return _unitOfWork.SaveAllAsync();
        }

        public Task DeleteAsync(Appointment item)
        {
            _unitOfWork.AppointmentRepository.DeleteAsync(item);
            return _unitOfWork.SaveAllAsync();
        }

        public Task<IReadOnlyList<Appointment>> GetAllAsync()
        {
            return _unitOfWork.AppointmentRepository.ListAllAsync();
        }

        public Task<Appointment?> GetByIdAsync(int id)
        {
            return _unitOfWork.AppointmentRepository.GetByIdAsync(id);
        }

        public Task<IReadOnlyList<Appointment>> GetAllByClientId(int? clientId)
        {
            return _unitOfWork.AppointmentRepository.ListAsync(x => x.ClientId == clientId);
        }

        public Task<IReadOnlyList<Appointment>> GetOnDateAsync(DateTime date, Appointment.Status? status = null)
        {
            if (status != null)
                return _unitOfWork.AppointmentRepository.ListAsync(x => x.AppointmentDate.Date == date.Date && x.AppointmentStatus == status);
            else
                return _unitOfWork.AppointmentRepository.ListAsync(x => x.AppointmentDate.Date == date.Date);
        }

        public Task UpdateAsync(Appointment item)
        {
            _unitOfWork.AppointmentRepository.UpdateAsync(item);
            return _unitOfWork.SaveAllAsync();
        }

        public Task GetConnections()
        {
            _unitOfWork.DoctorRepository.ListAllAsync();
            _unitOfWork.ClientRepository.ListAllAsync();
            return Task.CompletedTask;
        }
    }
}
