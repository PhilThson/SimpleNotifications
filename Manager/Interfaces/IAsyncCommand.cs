using System.Windows.Input;

namespace Manager.Interfaces;

public interface IAsyncCommand : ICommand
{
    void RaiseCanExecuteChanged();
}

