using System.Reflection;
using System.Windows.Input;

namespace FoodFlow
{
    public class RelayCommand<T> : ICommand
    {
        private readonly Action<T> _execute;

        public event EventHandler? CanExecuteChanged;

        public RelayCommand(Action<T> execute)
        {
            _execute = execute ?? throw new ArgumentNullException("execute");
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        public bool CanExecute(object? parameter) => true;

        public virtual void Execute(object? parameter)
        {
            if (!CanExecute(parameter) || _execute == null)
            {
                return;
            }

            if (parameter == null)
            {
                if (typeof(T).GetTypeInfo().IsValueType)
                {
                    _execute(default);
                }
                else
                {
                    _execute((T)parameter);
                }
            }
            else
            {
                _execute((T)parameter);
            }
        }
    }


    public class RelayCommand : ICommand
    {
        private readonly Action _execute;

        public event EventHandler? CanExecuteChanged;

        public RelayCommand(Action execute)
        {
            _execute = execute ?? throw new ArgumentNullException("execute");
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        public bool CanExecute(object? parameter) => true;

        public virtual void Execute(object? parameter)
        {
            if (!CanExecute(parameter) || _execute == null)
            {
                return;
            }

            _execute();
        }
    }
}