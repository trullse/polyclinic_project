using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace polyclinic.Domain.Entities
{
    public class Entity
    {
        [PrimaryKey, AutoIncrement, Indexed]
        public int Id { get; set; }
    }
}
