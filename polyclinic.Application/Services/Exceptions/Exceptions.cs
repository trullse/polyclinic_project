using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace polyclinic.Application.Services.Exceptions
{
    public class TalonException : Exception
    {
        public TalonException(string? message)
        : base(message) { }
    }
}
