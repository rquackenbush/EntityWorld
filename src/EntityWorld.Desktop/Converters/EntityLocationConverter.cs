using System;
using System.Globalization;
using Avalonia;
using Avalonia.Data.Converters;
using Point = System.Drawing.Point;
using Rectangle = Avalonia.Controls.Shapes.Rectangle;

namespace EntityWorld.Desktop.Converters
{
    public class EntityLocationConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Point location = (Point) value;

            return new Thickness(location.X, location.Y, 0, 0);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}