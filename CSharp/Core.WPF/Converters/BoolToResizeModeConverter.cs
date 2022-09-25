using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows;

namespace Core.WPF.Converters;

[ValueConversion(typeof(bool), typeof(ResizeMode))]
public sealed class BoolToResizeModeConverter : IValueConverter
{
	public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
	{
		return (bool)value ? ResizeMode.CanResize : ResizeMode.CanMinimize;
	}

	public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
	{
		throw new NotImplementedException();
	}
}
