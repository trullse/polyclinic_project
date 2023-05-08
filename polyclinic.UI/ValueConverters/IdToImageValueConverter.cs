using polyclinic.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace polyclinic.UI.ValueConverters
{
    class IdToImageValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                int id = (int)value;
                string imagePath = Path.Combine(FileSystem.Current.AppDataDirectory, @"/AppointmentImages/", $"appointment{id}.png");
                if (File.Exists(imagePath))
                    return ImageSource.FromFile(imagePath);
            }
            return ImageSource.FromFile("noimage.png");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
