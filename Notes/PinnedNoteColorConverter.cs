using System;
using Xamarin.Forms;

namespace Notes
{
    public class PinnedNoteColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value is bool isPinned)
            {
                return isPinned ? Color.LightBlue : Color.Transparent;
            }
            return Color.Transparent; // Default color if value is not a boolean
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
