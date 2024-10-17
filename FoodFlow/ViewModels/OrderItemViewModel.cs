using System.ComponentModel;
using FoodFlow.Models;

namespace FoodFlow.ViewModels
{
    public class OrderItemViewModel : INotifyPropertyChanged
    {
        private int _amount;
        public Dish Dish { get; set; } = Dish.Empty;

        public int Amount
        {
            get => _amount;
            set
            {
                if (_amount != value)
                {
                    _amount = value;
                    OnPropertyChanged(nameof(Amount));
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}