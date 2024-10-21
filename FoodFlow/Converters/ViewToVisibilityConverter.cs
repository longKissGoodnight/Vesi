using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using FoodFlow.ViewModels.Layout;

namespace FoodFlow.Converters
{
    public class ViewToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // Если текущее представление - WellcomeLayoutViewModel, скрываем TextBlock
            if (value is WellcomeLayoutViewModel or HistoryOrderLayoutViewModel)
            {
                return Visibility.Collapsed;
            }
            return Visibility.Visible; // В остальных случаях показываем
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
