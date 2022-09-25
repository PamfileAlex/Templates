using System;
using System.Windows.Input;

namespace Core.WPF.Commands;

/// <summary>
/// A generic command whose sole purpose is to relay its functionality to other
/// objects by invoking delegates. The default return value for the CanExecute
/// method is <see langword="true"/>. This class allows you to accept command parameters
/// in the <see cref="Execute(T)"/> and <see cref="CanExecute(T)"/> callback methods.
/// </summary>
/// <typeparam name="T">The type of parameter being passed as input to the callbacks.</typeparam>
public sealed class RelayCommand<T> : ICommand
{
	#region Private members
	/// <summary>
	/// The <see cref="Action"/> to invoke when <see cref="Execute(T)"/> is used.
	/// </summary>
	private readonly Action<T?> _execute;

	/// <summary>
	/// The optional action to invoke when <see cref="CanExecute(T)"/> is used.
	/// </summary>
	private readonly Predicate<T?>? _canExecute;
	#endregion

	#region Constructors

	/// <summary>
	/// Initializes a new instance of the <see cref="RelayCommand{T}"/> class.
	/// </summary>
	/// <param name="execute">The execution logic.</param>
	/// <param name="canExecute">The execution status logic.</param>
	/// <remarks>
	/// Due to the fact that the <see cref="ICommand"/> interface exposes methods that accept a
	/// nullable <see cref="object"/> parameter, it is recommended that if <typeparamref name="T"/> is a reference type,
	/// you should always declare it as nullable, and to always perform checks within <paramref name="execute"/>.
	/// </remarks>
	/// <exception cref="ArgumentNullException">Thrown if <paramref name="execute"/> is <see langword="null"/>.</exception>
	public RelayCommand(Action<T?> execute, Predicate<T?>? canExecute = null)
	{
		_execute = execute ?? throw new ArgumentNullException(nameof(execute));
		_canExecute = canExecute;
	}
	#endregion

	#region Public methods
	/// <summary>
	/// Notifies that the <see cref="ICommand.CanExecute"/> property has changed.
	/// </summary>
	public void NotifyCanExecuteChanged()
	{
		CanExecuteChanged?.Invoke(this, EventArgs.Empty);
	}
	#endregion

	#region ICommand
	/// <inheritdoc/>
	public event EventHandler? CanExecuteChanged;

	/// <inheritdoc/>
	public bool CanExecute(object? parameter)
	{
		// Special case a null value for a value type argument type.
		// This ensures that no exceptions are thrown during initialization.
		if (parameter is null && default(T) is not null)
		{
			return false;
		}

		return _canExecute == null || _canExecute((T)parameter);
	}

	/// <inheritdoc/>
	public void Execute(object? parameter)
	{
		_execute((T)parameter);
	}
	#endregion
}
