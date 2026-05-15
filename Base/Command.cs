using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace spa.Base
{
    public class Command : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            _execute?.Invoke(parameter);
        }

        private Action<object?> _execute;
        public Command(Action<object?> action)
        {
            _execute = action;
        }

        public Command()
        {
        }
    }
}
