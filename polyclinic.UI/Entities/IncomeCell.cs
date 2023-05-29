using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace polyclinic.UI.Entities
{
    public class IncomeCell
    {
        public string Date { get; set; }

        public double Income { get; set; }

        public IncomeCell(string xValue, double yValue)
        {
            Date = xValue;
            Income = yValue;
        }
    }
}
