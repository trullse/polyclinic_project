using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace polyclinic.Domain.Entities
{
    public class Appointment : Entity, IComparable<Appointment>
    {
        public enum Status
        { 
            Booked,
            Missed,
            Approved,
            ToPay,
            Ended
        }

        public int ClientId { get; set; }
        public Client? Client { get; set; }
        public int DoctorId { get; set; }
        public Doctor? Doctor { get; set; }
        public string? Diagnosis { get; set; }
        public double? TreatmentCost { get; set; }
        public DateTime AppointmentDate { get; set; }
        public Status AppointmentStatus { get; set; }

        public int CompareTo(Appointment? other)
        {
            return -DateTime.Compare(AppointmentDate, other.AppointmentDate);
        }
    }
}
