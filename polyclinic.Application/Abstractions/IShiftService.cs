using polyclinic.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace polyclinic.Application.Abstractions
{
    public interface IShiftService : IBaseService<Shift>
    {
        Task<Shift?> GetByDoctorAndDayAsync(int doctorId, DateOnly date);
    }
}
