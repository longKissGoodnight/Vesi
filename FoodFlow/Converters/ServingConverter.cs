using System;
using System.Globalization;
using System.Windows.Data;
using FoodFlow.Models;

namespace FoodFlow.Converters
{
    public class ServingConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {


            return value switch
            {
                ServingMethod.Unit => "Шт.",
                ServingMethod.Weight => "Вес.",
                _ => value?.ToString() ?? string.Empty// Это вернет строковое представление для других значений
            };
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
