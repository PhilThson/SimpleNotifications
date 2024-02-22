using System.Windows.Input;

namespace Manager.Interfaces.Commands;

public interface IAsyncCommand : ICommand
{
    bool IsExecuting { get; }

    event Action IsExecutingChanged;

    void RaiseCanExecuteChanged();
}

