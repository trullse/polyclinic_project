using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace polyclinic.Domain.Entities
{
	public class Shift : Entity
	{
		public enum ShiftType
		{
			First,
			Second
		}
		public int DoctorId { get; set; }
		public DateOnly Date { get; set; }
        public ShiftType Type { get; set; }
		public List<Talon> Talons { get; set; } = new List<Talon>();
	}
}
