using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace polyclinic.Domain.Entities
{
    public class Appointment : Entity
    {
        public int? ClientId { get; set; }
        public int? DoctorId { get; set; }
        public string? Diagnosis { get; set; }
        public double? TreatmentCost { get; set; }
        public DateTime? AppointmentDate { get; set; }
        public Appointment(int id, int clientId, int doctorId, string diagnosis, double treatmentCost, DateTime appointmentDate)
        {
            Id = id;
            ClientId = clientId;
            DoctorId = doctorId;
            Diagnosis = diagnosis;
            TreatmentCost = treatmentCost;
            AppointmentDate = appointmentDate;
        }

        public Appointment()
        {
        }
    }
}
