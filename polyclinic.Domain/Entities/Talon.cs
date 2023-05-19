using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace polyclinic.Domain.Entities
{
    public class Talon : Entity
    {
        public TimeOnly AppointmentTime { get; set; }
        public bool IsBooked { get; set; } = false;
        public int ShiftId { get; set; }
        public Shift? ShiftSource { get; set; }
    }
}
