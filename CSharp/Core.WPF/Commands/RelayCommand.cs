using System;
using System.Windows.Input;

namespace Core.WPF.Commands;

/// <summary>
/// A command whose sole purpose is to relay its functionality to other
/// objects by invoking delegates. The default return value for the <see cref="CanExecute"/>
/// method is <see langword="true"/>. This type does not allow you to accept command parameters
/// in the <see cref="Execute"/> and <see cref="CanExecute"/> callback methods.
/// </summary>
public sealed class RelayCommand : ICommand
{
	#region Private members
	/// <summary>
	/// The <see cref="Action"/> to invoke when <see cref="Execute"/> is used.
	/// </summary>
	private readonly Action _execute;

	/// <summary>
	/// The optional action to invoke when <see cref="CanExecute"/> is used.
	/// </summary>
	private readonly Func<bool>? _canExecute;
	#endregion

	#region Constructors
	/// <summary>
	/// Initializes a new instance of the <see cref="RelayCommand"/> class.
	/// </summary>
	/// <param name="execute">The execution logic.</param>
	/// <param name="canExecute">The execution status logic.</param>
	/// <exception cref="System.ArgumentNullException">Thrown if <paramref name="execute"/> or <paramref name="canExecute"/> are <see langword="null"/>.</exception>
	public RelayCommand(Action execute, Func<bool>? canExecute = null)
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
		return _canExecute is null || _canExecute();
	}

	/// <inheritdoc/>
	public void Execute(object? parameter)
	{
		_execute();
	}
	#endregion
}
