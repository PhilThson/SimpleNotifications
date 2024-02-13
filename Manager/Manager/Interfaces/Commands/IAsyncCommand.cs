using System.Windows.Input;

namespace Manager.Interfaces.Commands;

public interface IAsyncCommand : ICommand
{
    void RaiseCanExecuteChanged();
}

