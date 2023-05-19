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
    public class ShiftService : IShiftService
    {
        private readonly IUnitOfWork _unitOfWork;
        public ShiftService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task AddAsync(Shift item)
        {
            await _unitOfWork.ShiftRepository.AddAsync(item);
            await _unitOfWork.SaveAllAsync();
            item = await GetByDoctorAndDayAsync(item.DoctorId, item.Date);
            List<Talon> talons = new List<Talon>();
            if (item.Type == Shift.ShiftType.First)
            {
                for (var time = new TimeOnly(9, 30); time < new TimeOnly(13, 30); time = time.AddMinutes(15))
                {
                    talons.Add(new Talon
                    {
                        ShiftId = item.Id,
                        AppointmentTime = time
                    });
                }
            }
            else
            {
                for (var time = new TimeOnly(15, 30); time < new TimeOnly(19, 30); time = time.AddMinutes(15))
                {
                    talons.Add(new Talon
                    {
                        ShiftId = item.Id,
                        AppointmentTime = time
                    });
                }
            }
            foreach (var talon in talons)
            {
                await _unitOfWork.TalonRepository.AddAsync(talon);
            }
        }

        public Task DeleteAsync(Shift item)
        {
            _unitOfWork.ShiftRepository.DeleteAsync(item);
            return _unitOfWork.SaveAllAsync();
        }

        public Task<IReadOnlyList<Shift>> GetAllAsync()
        {
            return _unitOfWork.ShiftRepository.ListAllAsync();
        }

        public async Task<Shift?> GetByDoctorAndDayAsync(int doctorId, DateOnly date)
        {
            var list = await _unitOfWork.ShiftRepository.ListAsync(s => s.DoctorId == doctorId && s.Date == date);
            if (list.Count == 0)
                return null;
            return list.FirstOrDefault();
        }

        public Task<Shift?> GetByIdAsync(int id)
        {
            return _unitOfWork.ShiftRepository.GetByIdAsync(id);
        }

        public Task UpdateAsync(Shift item)
        {
            _unitOfWork.ShiftRepository.UpdateAsync(item);
            return _unitOfWork.SaveAllAsync();
        }
    }
}
