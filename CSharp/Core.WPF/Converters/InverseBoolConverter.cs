using System;
using System.Globalization;
using System.Windows.Data;

namespace Core.WPF.Converters;

[ValueConversion(typeof(bool), typeof(bool))]
public sealed class InverseBoolConverter : IValueConverter
{
	public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
	{
		return Inverse(value);
	}

	public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
	{
		return Inverse(value);
	}

	private static object Inverse(object value)
	{
		return value is bool boolVal ? !boolVal : value;
	}
}
