using System.ComponentModel;
using System.Windows.Input;
using FoodFlow.Models;
using FoodFlow.Repos;
using FoodFlow.ViewModels;

namespace FoodFlow
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private Order? _currentOrder { get; set; }
        private DishesRepository _dishesRepository = new DishesRepository();
        public Order? CurrentOrder
        {
            get => _currentOrder;
            set
            {
                if (_currentOrder != value)
                {
                    _currentOrder = value;
                    OnPropertyChanged(nameof(CurrentOrder));
                }
            }
        }

        private object _currentView;
        public object CurrentView
        {
            get => _currentView;
            set
            {
                if (_currentView != value)
                {
                    _currentView = value;
                    OnPropertyChanged(nameof(CurrentView));
                }
            }
        }

        public ICommand AddItemCommand { get; }
        public ICommand NewOrderCommand { get; }
        public ICommand CancelOrderCommand { get; }
        public ICommand RemoveItemCommand { get; }
        public ICommand ClearOrderCommand { get; }
        public ICommand IncreaseAmountCommand { get; }
        public ICommand DecreaseAmountCommand { get; }

        public MainViewModel()
        {
            AddItemCommand = new RelayCommand<Dish>(AddItem);
            RemoveItemCommand = new RelayCommand<OrderItem>(RemoveItem);
            ClearOrderCommand = new RelayCommand(ClearOrder);
            NewOrderCommand = new RelayCommand(NewOrder);
            CancelOrderCommand = new RelayCommand(CancelOrder);
            IncreaseAmountCommand = new RelayCommand<OrderItem>(IncreaseAmount);
            DecreaseAmountCommand = new RelayCommand<OrderItem>(DecreaseAmount);

            _currentView = new WellcomeViewModel();
        }

        private void AddItem(Dish dish)
        {
            // Добавляем новый элемент в коллекцию
            CurrentOrder!.Items.Add(new OrderItem
            {
                Dish = _dishesRepository.GetAll().Skip(Random.Shared.Next(10)).First(),
                Amount = 1
            });

            // Уведомляем об изменении свойства CurrentOrder
            OnPropertyChanged(nameof(CurrentOrder));
        }


        private void RemoveItem(OrderItem item)
        {
            CurrentOrder!.Items.Remove(item);
            OnPropertyChanged(nameof(CurrentOrder));
        }

        // Метод для очистки заказа
        private void ClearOrder()
        {
            //CurrentOrder.Clear();
        }

        private void CancelOrder()
        {
            //CurrentOrder.Clear();
        }

        private void IncreaseAmount(OrderItem item)
        {
            if (item != null)
            {
                item.Amount++;
            }
        }

        private void DecreaseAmount(OrderItem item)
        {
            if (item != null && item.Amount > 1)
            {
                item.Amount--;
            }
        }

        private void NewOrder()
        {
            CurrentOrder = new Order();
            CurrentView = new OrderViewModel();
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

