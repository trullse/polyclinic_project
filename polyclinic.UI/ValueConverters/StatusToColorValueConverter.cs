using polyclinic.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace polyclinic.UI.ValueConverters
{
    public class StatusToColorValueConverter : IValueConverter
    {
        private readonly Color missed = Color.FromArgb("A10702");
        private readonly Color approved = Color.FromArgb("FAA613");
        private readonly Color toPay = Color.FromArgb("8E7DBE");
        private readonly Color ended = Color.FromArgb("688E26");
        private readonly Color booked = Colors.Transparent;
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var status = (Appointment.Status)value;
            switch (status)
            {
                case Appointment.Status.Missed:
                    return missed;
                case Appointment.Status.Approved:
                    return approved;
                case Appointment.Status.ToPay:
                    return toPay;
                case Appointment.Status.Ended:
                    return ended;
                default:
                    return booked;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
