using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using FoodFlow.Models;
using FoodFlow.Repos;
using FoodFlow.ViewModels;

namespace FoodFlow
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private OrderViewModel? _currentOrder { get; set; }
        private ObservableCollection<Dish> _dishes;

        private DishesRepository _dishesRepository = new DishesRepository();

        //New
        public ObservableCollection<Dish> Dishes
        {
            get => _dishes;
            set
            {
                if (_dishes != value)
                {
                    _dishes = value;
                    OnPropertyChanged(nameof(Dishes));
                }
            }
        }


        public OrderViewModel? CurrentOrder
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
        public ICommand SelectDishCommand { get; }


        public MainViewModel()
        {
            AddItemCommand = new RelayCommand<Dish>(AddItem);
            RemoveItemCommand = new RelayCommand<OrderItemViewModel>(RemoveItem);
            ClearOrderCommand = new RelayCommand(ClearOrder);
            NewOrderCommand = new RelayCommand(NewOrder);
            CancelOrderCommand = new RelayCommand(CancelOrder);
            IncreaseAmountCommand = new RelayCommand<OrderItemViewModel>(IncreaseAmount);
            DecreaseAmountCommand = new RelayCommand<OrderItemViewModel>(DecreaseAmount);
            SelectDishCommand = new RelayCommand<Dish>(AddItem); // Здесь вы используете AddItem

            //new
            Dishes = new ObservableCollection<Dish>(_dishesRepository.GetAll());


            _currentView = new ViewModels.Layout.WellcomeLayoutViewModel();

        }

        private void AddItem(Dish dish)
        {
            /*            // Добавляем новый элемент в коллекцию
                        CurrentOrder!.Items.Add(new OrderItem
                        {
                            Dish = _dishesRepository.GetAll().Skip(Random.Shared.Next(10)).First(),
                            Amount = 1
                        });

                        // Уведомляем об изменении свойства CurrentOrder
                        OnPropertyChanged(nameof(CurrentOrder));*/

            CurrentView = new ViewModels.Layout.AddDishLayoutViewModel();

            if (dish != null && CurrentOrder != null)
            {
                // Добавляем новое блюдо в текущий заказ
                CurrentOrder.Items.Add(new OrderItemViewModel
                {
                    Dish = dish,
                    Amount = 1
                });

                // Уведомляем об изменении свойства CurrentOrder
                OnPropertyChanged(nameof(CurrentOrder));

                // Вернуться к OrderView
                CurrentView = new ViewModels.Layout.OrderLayoutViewModel(); // Здесь вы можете использовать OrderViewModel
            }
        }


        private void RemoveItem(OrderItemViewModel item)
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

        private void IncreaseAmount(OrderItemViewModel item)
        {
            if (item != null)
            {
                item.Amount++;
            }
        }

        private void DecreaseAmount(OrderItemViewModel item)
        {
            if (item != null && item.Amount > 1)
            {
                item.Amount--;
            }
        }

        private void NewOrder()
        {
            CurrentOrder = new OrderViewModel();
            CurrentView = new ViewModels.Layout.OrderLayoutViewModel();
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

