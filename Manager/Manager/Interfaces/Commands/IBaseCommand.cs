using System.Windows.Input;

namespace Manager.Interfaces.Commands;

public interface IBaseCommand : ICommand
{
    void RaiseCanExecuteChanged();
}

