using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using FoodFlow.Models;
using FoodFlow.Repos;
using FoodFlow.Services;
using FoodFlow.ViewModels;
using FoodFlow.ViewModels.Layout;

namespace FoodFlow
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private OrderViewModel? _currentOrder { get; set; }
        private ObservableCollection<Dish> _dishes;

        private DishesRepository _dishesRepository = new DishesRepository();

        private decimal _totalPrice;


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

        private readonly TimerService _timerService;
        private string _currentTime;
        public string CurrentTime
        {
            get => _currentTime;
            set
            {
                _currentTime = value;
                OnPropertyChanged(nameof(CurrentTime));
            }
        }

        public decimal TotalPrice
        {
            get => _totalPrice;
            set
            {
                _totalPrice = value;
                OnPropertyChanged(nameof(TotalPrice));
            }
        }

        private void UpdateTotalPrice()
        {
            TotalPrice = CurrentOrder.Items.Sum(item => item.Dish.Price * item.Amount);
        }

        public ICommand AddItemCommand { get; }
        public ICommand NewOrderCommand { get; }
        public ICommand CancelOrderCommand { get; }
        public ICommand RemoveItemCommand { get; }
        public ICommand ClearOrderCommand { get; }
        public ICommand IncreaseAmountCommand { get; }
        public ICommand DecreaseAmountCommand { get; }
        public ICommand SelectDishCommand { get; }
        public ICommand CompleteOrderCommand { get; }

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
            CompleteOrderCommand = new RelayCommand(OnCompleteOrder);

            //new
            Dishes = new ObservableCollection<Dish>(_dishesRepository.GetAll());


            _currentView = new ViewModels.Layout.WellcomeLayoutViewModel();

            _timerService = new TimerService();
            _timerService.TimeUpdated += UpdateTime;
            _currentTime = DateTime.Now.ToString("HH:mm:ss");

        }

        private void AddItem(Dish dish)
        {
            CurrentView = new ViewModels.Layout.AddDishLayoutViewModel();

            if (dish != null && CurrentOrder != null)
            {
                // Добавляем новое блюдо в текущий заказ
                CurrentOrder.Items.Add(new OrderItemViewModel
                {
                    Dish = dish,
                    Amount = 1
                });

                UpdateTotalPrice(); // Обновляем общую стоимость

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
            UpdateTotalPrice(); // Обновляем общую стоимость
        }

        // Метод для очистки заказа
        private void ClearOrder()
        {
            CurrentOrder?.Items.Clear();
            TotalPrice = 0; // Обнуляем общую стоимость
            OnPropertyChanged(nameof(CurrentOrder));
        }

        // Метод для отмены заказа
        private void CancelOrder()
        {
            //CurrentOrder.Clear();

            CurrentOrder = null;
            TotalPrice = 0; // Обнуляем общую стоимость
            CurrentView = new WellcomeLayoutViewModel();
        }

        private void IncreaseAmount(OrderItemViewModel item)
        {
            if (item != null)
            {
                item.Amount++;
                UpdateTotalPrice(); // Обновляем общую стоимость
            }
        }

        private void DecreaseAmount(OrderItemViewModel item)
        {
            if (item != null && item.Amount > 1)
            {
                item.Amount--;
                UpdateTotalPrice(); // Обновляем общую стоимость
            }
        }

        private void UpdateTime(string newTime)
        {
            CurrentTime = newTime;
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

        private void OnCompleteOrder()
        {
            // Логика завершения заказа, например, отправка на сервер или очистка заказа
           
        }
    }
}

