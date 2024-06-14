using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WpfApp.Commands
{
    internal class RelayCommand : ICommand
    {
        private readonly Func<object?, bool>? _canCommand;
        private readonly Action<object?> _command;

        public RelayCommand(Action<object?> command) : this(null, command)
        {
        }
        public RelayCommand(Func<object?, bool>? canCommand, Action<object?> command)
        {
            _canCommand = canCommand;
            _command = command;
        }

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return _canCommand?.Invoke(parameter) ?? true;
        }

        public void Execute(object? parameter)
        {
            _command.Invoke(parameter);
        }
    }
}
