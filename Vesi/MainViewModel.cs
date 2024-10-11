using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace Vesi
{
    public class MainViewModel : INotifyPropertyChanged
    {
        // Коллекция для хранения текущего заказа
        public ObservableCollection<string> CurrentOrder { get; set; }

        // Команды
        public ICommand AddItemCommand { get; }
        public ICommand RemoveItemCommand { get; }
        public ICommand ClearOrderCommand { get; }

        public MainViewModel()
        {
            CurrentOrder = new ObservableCollection<string>();

            // Инициализация команд с привязкой к методам
            AddItemCommand = new RelayCommand(AddItem);
            RemoveItemCommand = new RelayCommand(RemoveItem);
            ClearOrderCommand = new RelayCommand(_ => ClearOrder());
        }

        // Метод для добавления блюда в заказ
        private void AddItem(object item)
        {
            if (item is string dishName)
            {
                CurrentOrder.Add(dishName);
            }
        }

        // Метод для удаления блюда из заказа
        private void RemoveItem(object item)
        {
            if (item is string dishName)
            {
                CurrentOrder.Remove(dishName);
            }
        }

        // Метод для очистки заказа
        private void ClearOrder()
        {
            CurrentOrder.Clear();
        }

        // Обработка изменений свойств (если понадобится в будущем)
        public event PropertyChangedEventHandler PropertyChanged;
    }
}

