using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace polyclinic.Domain.Entities
{
    public class Doctor : Entity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string? Patronymic { get; set; }
        public string Qualification { get; set; }
        public string Specialization { get; set; }
        public List<Appointment>? Appointments { get; set; }
    }
}
