using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Windows.Input;

namespace Vesi
{
    public class RelayCommand : ICommand
    {
        private readonly Action<object> _execute; // Действие, которое выполняется командой
        private readonly Func<object, bool> _canExecute; // Условие, при котором команда может быть выполнена

        // Конструктор команды, который принимает действие и условие
        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        // Определяет, может ли команда быть выполнена
        public bool CanExecute(object parameter) => _canExecute == null || _canExecute(parameter);

        // Выполняет действие команды
        public void Execute(object parameter) => _execute(parameter);

        // Событие, которое обновляет состояние команды (нужно для CanExecute)
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}

