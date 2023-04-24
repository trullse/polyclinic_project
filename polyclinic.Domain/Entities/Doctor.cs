using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace polyclinic.Domain.Entities
{
    public class Doctor : Entity
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Patronymic { get; set; }
        public string? Qualification { get; set; }
        public string? Specialization { get; set; }
        public Doctor(int id, string name, string surname, string? patronymic, string qualification, string specialization)
        {
            Id = id;
            Name = name;
            Surname = surname;
            Patronymic = patronymic;
            Qualification = qualification;
            Specialization = specialization;
        }
    }
}
