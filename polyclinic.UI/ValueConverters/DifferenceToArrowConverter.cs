using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace polyclinic.UI.ValueConverters
{
    public class DifferenceToArrowConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double)
            {
                double dvalue = (double)value;
                if (dvalue < 0)
                {
                    return ImageSource.FromFile("down.png");
                }
                else if (dvalue > 0)
                {
                    return ImageSource.FromFile("up.png");
                }
                return null;
            }
            else
            {
                int dvalue = (int)value;
                if (dvalue < 0)
                {
                    return ImageSource.FromFile("down.png");
                }
                else if (dvalue > 0)
                {
                    return ImageSource.FromFile("up.png");
                }
                return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
