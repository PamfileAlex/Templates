using System.Linq;
using System.Windows;

namespace Core.WPF.Utils;
public static class WindowUtils
{
	public static bool IsWindowOpen<T>(string name = "") where T : Window
	{
		return string.IsNullOrEmpty(name)
		   ? Application.Current.Windows.OfType<T>().Any()
		   : Application.Current.Windows.OfType<T>().Any(w => w.Name.Equals(name));
	}

	public static bool IsOpen(this Window window)
	{
		return Application.Current.Windows.Cast<Window>().Any(x => x == window);
	}

	public static Window GetActiveWindow()
	{
		return Application.Current.Windows.OfType<Window>().FirstOrDefault(x => x.IsActive);
	}

	public static T GetActiveWindow<T>() where T : Window
	{
		return Application.Current.Windows.OfType<T>().FirstOrDefault();
	}

	public static void CloseAllExceptActive()
	{
		CloseAllExceptProvided(GetActiveWindow());
	}

	public static void CloseAllExceptProvided(Window source)
	{
		Application.Current.Windows
			.Cast<Window>()
			.Where(x => x != source)
			.ToList()
			.ForEach(x => x.Close());
	}

	public static void CenterWindow(Window window)
	{
		if (window is null) { return; }

		double screenWidth = SystemParameters.PrimaryScreenWidth;
		double screenHeight = SystemParameters.PrimaryScreenHeight;

		window.Left = screenWidth / 2 - window.Width / 2;
		window.Top = screenHeight / 2 - window.Height / 2;
	}

	public static void CenterActiveWindow()
	{
		CenterWindow(Application.Current.MainWindow);
	}
}
