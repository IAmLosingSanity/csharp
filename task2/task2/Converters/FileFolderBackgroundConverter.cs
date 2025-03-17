using Avalonia.Data.Converters;
using System;
using System.Globalization;
using task2.Models;
using Avalonia.Media;
using Avalonia;

namespace task2.Converters
{
    public class FileFolderBackgroundConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is ElementType type)
            {
                // Пастельные оттенки Solarized:
                // Для папок – светло-зелёный (#b5e1a4),
                // для файлов – светло-голубой (#a3d0f7)
                return type == ElementType.Folder 
                    ? new SolidColorBrush(Color.Parse("#b5e1a4"))
                    : new SolidColorBrush(Color.Parse("#a3d0f7"));
            }
            return new SolidColorBrush(Colors.Transparent);
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
