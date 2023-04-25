using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace polyclinic.UI.ValueConverters
{
    internal class DateToColorValueConverter : IValueConverter
    {
        private readonly Color passed = Color.FromArgb("218359");
        private readonly Color future = Colors.Transparent;
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            
            if ((DateTime)value <= DateTime.Now)
                return passed;
            return future;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
