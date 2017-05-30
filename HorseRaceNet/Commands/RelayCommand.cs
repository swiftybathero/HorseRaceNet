using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HorseRaceNet.Commands
{
    public class RelayCommand<T> : ICommand
    {
        private Action<T> _executeCommand;
        private Predicate<T> _canExecuteCommand;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; } 
            remove { CommandManager.RequerySuggested += value; }
        }

        public RelayCommand(Action<T> executeAction) : this(executeAction, null)
        {
        }

        public RelayCommand(Action<T> executeAction, Predicate<T> canExecuteAction)
        {
            _executeCommand = executeAction ?? throw new ArgumentNullException("Execute action cannot be null!");
            _canExecuteCommand = canExecuteAction;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecuteCommand != null ? _canExecuteCommand((T)parameter) : true;
        }

        public void Execute(object parameter)
        {
            _executeCommand((T)parameter);
        }
    }
}
