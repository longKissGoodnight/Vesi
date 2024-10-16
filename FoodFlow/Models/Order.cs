using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace FoodFlow.Models
{
    public class Order : INotifyPropertyChanged
    {
        public int Id { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime CloseTime { get; set; }
        public ObservableCollection<OrderItem> Items { get; set; } = new();

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
