using System;
using System.Windows.Input;

namespace VersOne.Epub.WpfDemo.Utils
{
    /// <summary>
    /// A custom command which uses an action and an optional predicate supplied through one of its constructors.
    /// The predicate is used to determine if the command can be executed and the action is called with the command is executed.
    /// </summary>
    public class Command : ICommand
    {
        private readonly Predicate<object> canExecute = null;
        private readonly Action<object> executeAction = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="Command" /> class with a specified execute action.
        /// </summary>
        /// <param name="executeAction">An action that needs to be executed when the <see cref="Execute" /> method is called.</param>
        public Command(Action executeAction)
            : this(param => true, param => executeAction())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Command" /> class with a specified execute action.
        /// </summary>
        /// <param name="executeAction">An action that needs to be executed when the <see cref="Execute" /> method is called.</param>
        public Command(Action<object> executeAction)
            : this(param => true, executeAction)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Command" /> class with a specified predicate that determines whether the action can be executed or not
        /// and a specified execute action.
        /// </summary>
        /// <param name="canExecute">A predicate that is executed when the <see cref="CanExecute" /> method is called.</param>
        /// <param name="executeAction">An action that needs to be executed when the <see cref="Execute" /> method is called.</param>
        public Command(Predicate<object> canExecute, Action<object> executeAction)
        {
            this.canExecute = canExecute;
            this.executeAction = executeAction;
        }

        /// <summary>
        /// Occurs after the execution of the <see cref="Execute" /> method, so the consumer of this command can check whether the command can still be executed or not.
        /// </summary>
        public event EventHandler CanExecuteChanged;

        /// <summary>
        /// Determines whether the command can execute in its current state.
        /// </summary>
        /// <param name="parameter">
        /// Data used by the predicate supplied through the constructor of this command.
        /// If the predicate does not require data to be passed, this object can be set to <c>null</c>.
        /// </param>
        /// <returns><c>true</c> if this command can be executed; otherwise, <c>false</c>.</returns>
        public bool CanExecute(object parameter)
        {
            if (canExecute != null)
            {
                return canExecute(parameter);
            }
            return true;
        }

        /// <summary>
        /// Method called when the command is invoked.
        /// </summary>
        /// <param name="parameter">
        /// Data used by the action supplied through the constructor of this command.
        /// If the action does not require data to be passed, this object can be set to <c>null</c>.
        /// </param>
        public void Execute(object parameter)
        {
            executeAction?.Invoke(parameter);
            UpdateCanExecuteState();
        }

        /// <summary>
        /// Triggers the <see cref="CanExecuteChanged" /> event.
        /// </summary>
        public void UpdateCanExecuteState()
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }
    }
}
