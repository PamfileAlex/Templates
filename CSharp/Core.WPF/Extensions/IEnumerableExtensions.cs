using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Core.WPF.Extensions;
public static class IEnumerableExtensions
{
	public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> list)
	{
		return new ObservableCollection<T>(list);
	}
}
