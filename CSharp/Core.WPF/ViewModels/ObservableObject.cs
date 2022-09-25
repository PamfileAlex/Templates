using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Core.WPF.ViewModels;
public class ObservableObject : INotifyPropertyChanged
{
	/// <inheritdoc/>
	public event PropertyChangedEventHandler? PropertyChanged;

	/// <summary>
	/// Raises the <see cref="PropertyChanged"/> event.
	/// </summary>
	/// <param name="propertyName">(optional) The name of the property that changed.</param>
	protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
	{
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
	}

	/// <summary>
	/// Compares the current and new values for a given property. If the value has changed,
	/// updates the property with the new value, then raises the <see cref="PropertyChanged"/> event.
	/// </summary>
	/// <typeparam name="T">The type of the property that changed.</typeparam>
	/// <param name="field">The field storing the property's value.</param>
	/// <param name="newValue">The property's value after the change occurred.</param>
	/// <param name="propertyName">(optional) The name of the property that changed.</param>
	/// <returns><see langword="true"/> if the property was changed, <see langword="false"/> otherwise.</returns>
	/// <remarks>
	/// The <see cref="PropertyChanged"/> event is not raised
	/// if the current and new value for the target property are the same.
	/// </remarks>
	protected bool SetAndNotifyIfChanged<T>(ref T field, T newValue, [CallerMemberName] string? propertyName = null)
	{
		if (EqualityComparer<T>.Default.Equals(field, newValue))
		{
			return false;
		}

		field = newValue;
		OnPropertyChanged(propertyName);
		return true;
	}

	/// <summary>
	/// Compares the current and new values for a given property. If the value has changed,
	/// updates the property with the new value, then raises the <see cref="PropertyChanged"/> event.
	/// </summary>
	/// <typeparam name="T">The type of the property that changed.</typeparam>
	/// <param name="field">The field storing the property's value.</param>
	/// <param name="newValue">The property's value after the change occurred.</param>
	/// <param name="onChanged">A callback to invoke after the <see cref="PropertyChanged"/> is raised.</param>
	/// <param name="propertyName">(optional) The name of the property that changed.</param>
	/// <returns><see langword="true"/> if the property was changed, <see langword="false"/> otherwise.</returns>
	/// <remarks>
	/// The <see cref="PropertyChanged"/> event is not raised
	/// if the current and new value for the target property are the same.
	/// </remarks>
	protected bool SetAndNotifyIfChanged<T>(ref T field, T newValue, Action onChanged, [CallerMemberName] string? propertyName = null)
	{
		bool result = SetAndNotifyIfChanged(ref field, newValue, propertyName);
		onChanged?.Invoke();
		return result;
	}

	/// <summary>
	/// Compares the current and new values for a given property. If the value has changed,
	/// updates the property with the new value, then raises the <see cref="PropertyChanged"/> event.
	/// </summary>
	/// <typeparam name="T">The type of the property that changed.</typeparam>
	/// <param name="field">The field storing the property's value.</param>
	/// <param name="newValue">The property's value after the change occurred.</param>
	/// /// <param name="onChanged">A callback to invoke after the <see cref="PropertyChanged"/> is raised.</param>
	/// <param name="propertyName">(optional) The name of the property that changed.</param>
	/// <returns><see langword="true"/> if the property was changed, <see langword="false"/> otherwise.</returns>
	/// <remarks>
	/// The <see cref="PropertyChanged"/> event is not raised
	/// if the current and new value for the target property are the same.
	/// </remarks>
	protected bool SetAndNotifyIfChanged<T>(ref T field, T newValue, Action<T> onChanged, [CallerMemberName] string? propertyName = null)
	{
		bool result = SetAndNotifyIfChanged(ref field, newValue, propertyName);
		onChanged?.Invoke(newValue);
		return result;
	}
}
