﻿using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace polyclinic.Domain.Entities
{
    public class Client : Entity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string? Patronymic { get; set; }
        public DateTime BirthDate { get; set; }
        public List<Appointment>? Appointments { get; set; }
        public string FullName
        {
            get => string.Join(" ", Surname, Name, Patronymic);
        }
    }
}
