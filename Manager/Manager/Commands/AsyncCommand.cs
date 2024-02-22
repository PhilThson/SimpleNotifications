using Manager.Interfaces.Commands;

namespace Manager.Commands;

public class AsyncCommand : IAsyncCommand
{
    private readonly Func<object, Task> _command;
    private readonly Func<bool> _canExecute;
    private readonly Action<Exception> _onException;
    private bool isExecuting;

	public AsyncCommand(Func<Task> command,
        Func<bool> canExecute = null,
        Action<Exception> onException = null)
        : this((o) => command(), canExecute, onException)
	{

	}

    public AsyncCommand(Func<object, Task> command,
        Func<bool> canExecute = null,
        Action<Exception> onException = null)
    {
        _command = command ?? throw new ArgumentNullException(nameof(command));
        _canExecute = canExecute ?? (() => true);
        _onException = onException;
    }

    public event EventHandler CanExecuteChanged;
    public event Action IsExecutingChanged;

    public bool IsExecuting
    {
        get => isExecuting;
        set
        {
            isExecuting = value;
            RaiseCanExecuteChanged();
            RaiseIsExecutingChanged();
        }
    }

    public bool CanExecute(object parameter) =>
        _canExecute() && !IsExecuting;

    public async void Execute(object parameter)
    {
        IsExecuting = true;
        try
        {
            await ExecuteAsync(parameter);
        }
        catch (Exception e)
        {
            //Log exception
            _onException?.Invoke(e);
        }
        IsExecuting = false;
    }

    protected async Task ExecuteAsync(object parameter) =>
        await _command(parameter);

    public void RaiseCanExecuteChanged() =>
        CanExecuteChanged?.Invoke(this, EventArgs.Empty);

    protected void RaiseIsExecutingChanged() =>
        IsExecutingChanged?.Invoke();
}

