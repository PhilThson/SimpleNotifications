using Manager.Interfaces.Commands;

namespace Manager.Commands;

public class BaseCommand : IBaseCommand
{
    private readonly Action<object> _command;
    private readonly Func<bool> _canExecute;

    public BaseCommand(Action command, Func<bool> canExecute = null)
        : this((o) => command(), canExecute)
    {

    }

    public BaseCommand(Action<object> command, Func<bool> canExecute = null)
        : this(command)
    {
        _canExecute = canExecute ?? (() => true);
    }

    private BaseCommand(Action<object> command)
    {
        _command = command ?? throw new ArgumentNullException(nameof(command));
    }

    public event EventHandler CanExecuteChanged;

    public bool CanExecute(object parameter = null) =>
        _canExecute.Invoke();

    public void Execute(object parameter) =>
        _command(parameter);

    public void RaiseCanExecuteChanged() =>
        CanExecuteChanged?.Invoke(this, EventArgs.Empty);
}

