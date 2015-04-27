using System;
using System.Windows.Input;

namespace EpubReaderDemo.Utils
{
    public class Command : ICommand
    {
        Predicate<object> canExecute = null;
        Action<Object> executeAction = null;

        public Command(Action executeAction)
            : this(param => true, param => executeAction())
        {
        }
        
        public Command(Action<object> executeAction)
            : this(param => true, executeAction)
        {
        }

        public Command(Predicate<object> canExecute, Action<object> executeAction)
        {
            this.canExecute = canExecute;
            this.executeAction = executeAction;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            if (canExecute != null)
                return canExecute(parameter);
            return true;
        }

        public void Execute(object parameter)
        {
            if (executeAction != null)
                executeAction(parameter);
            UpdateCanExecuteState();
        }

        public void UpdateCanExecuteState()
        {
            if (CanExecuteChanged != null)
                CanExecuteChanged(this, new EventArgs());
        }
    }
}
