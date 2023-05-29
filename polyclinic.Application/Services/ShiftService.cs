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

            // talons autofill
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

        public async Task AddOnInterval(Doctor doctor, DateTime start, DateTime end, bool start_with_first)
        {
            Shift.ShiftType shiftType;
            if (start_with_first)
                shiftType = Shift.ShiftType.First;
            else
                shiftType = Shift.ShiftType.Second;
            for (var date = start.Date; date <= end.Date; date = date.AddDays(1))
            {
                var found = await GetByDoctorAndDayAsync(doctor.Id, DateOnly.FromDateTime(date));
                if (found == null)
                {
                    Shift shift = new Shift()
                    {
                        DoctorId = doctor.Id,
                        Date = DateOnly.FromDateTime(date),
                        Type = shiftType
                    };
                    await AddAsync(shift);

                    // Change shift
                    if (shiftType == Shift.ShiftType.First)
                        shiftType = Shift.ShiftType.Second;
                    else
                        shiftType = Shift.ShiftType.First;
                }
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
            var list = await _unitOfWork.ShiftRepository.ListAsync(s => s.DoctorId == doctorId && s.Date == date, CancellationToken.None, x => x.Talons);
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
