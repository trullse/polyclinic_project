using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace polyclinic.Domain.Entities
{
    public class Appointment : Entity, IComparable<Appointment>
    {
        [Indexed]
        public int ClientId { get; set; }
        [Indexed]
        public int DoctorId { get; set; }
        public string? Diagnosis { get; set; }
        public double? TreatmentCost { get; set; }
        public DateTime AppointmentDate { get; set; }

        public int CompareTo(Appointment? other)
        {
            return -DateTime.Compare(AppointmentDate, other.AppointmentDate);
        }
    }
}
