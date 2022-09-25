using System;
using System.Globalization;
using System.Windows.Data;

namespace Core.WPF.Converters;

[ValueConversion(typeof(object), typeof(bool))]
public sealed class IsOfTypeConverter : IValueConverter
{
	public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
	{
		return value.GetType().Equals(parameter);
	}

	public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
	{
		throw new NotImplementedException();
	}
}
