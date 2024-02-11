using System.Windows.Input;

namespace Manager.Interfaces;

public interface IBaseCommand : ICommand
{
    void RaiseCanExecuteChanged();
}

