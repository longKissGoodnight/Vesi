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
                _currentOrder = value;
                OnPropertyChanged(nameof(CurrentOrder));
            }
        }

        private object _currentView;
        public object CurrentView
        {
            get => _currentView;
            set
            {
                _currentView = value;
                OnPropertyChanged(nameof(CurrentView));
            }
        }

        public ICommand AddItemCommand { get; }
        public ICommand NewOrderCommand { get; }
        public ICommand CancelOrderCommand { get; }
        public ICommand RemoveItemCommand { get; }
        public ICommand ClearOrderCommand { get; }

        public MainViewModel()
        {
            AddItemCommand = new RelayCommand<Dish>(AddItem);
            RemoveItemCommand = new RelayCommand<OrderItem>(RemoveItem);
            ClearOrderCommand = new RelayCommand(ClearOrder);
            NewOrderCommand = new RelayCommand(NewOrder);
            CancelOrderCommand = new RelayCommand(CancelOrder);

            _currentView = new WellcomeViewModel();
        }

        private void AddItem(Dish dish)
        {
            CurrentOrder!.Items.Add(new OrderItem 
            { 
                Dish = _dishesRepository.GetAll().Skip(Random.Shared.Next(10)).First(), 
                Amount = 1
            });

            // хак с занулением переделать на норльманое нотифай проперти ченджед 
            var co = CurrentOrder;
            CurrentOrder = null;
            CurrentOrder = co;
        }

        private void RemoveItem(OrderItem item)
        {
            CurrentOrder!.Items.Remove(item);

            var co = CurrentOrder;
            CurrentOrder = null;
            CurrentOrder = co;
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

