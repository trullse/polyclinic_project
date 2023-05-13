using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static polyclinic.Domain.Entities.Appointment;

namespace polyclinic.UI.ValueConverters
{
    public class StatusValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var status = (Status)value;
            switch (status)
            {
                case Status.Approved:
                    return "Approved";
                case Status.Ended:
                    return "Ended";
                case Status.ToPay:
                    return "Payment required";
                case Status.Missed:
                    return "Missed";
                case Status.Booked:
                    return "Booked";
                default:
                    return string.Empty;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
