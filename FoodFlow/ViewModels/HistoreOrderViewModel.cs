using FoodFlow.Models;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace FoodFlow.ViewModels.Layout
{
    public class HistoryOrderViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Order> _orders;

        public ObservableCollection<Order> Orders
        {
            get => _orders;
            set
            {
                if (_orders != value)
                {
                    _orders = value;
                    OnPropertyChanged(nameof(Orders));
                }
            }
        }

        public HistoryOrderViewModel()
        {
            LoadOrders();
        }

        private void LoadOrders()
        {
            // Пример загрузки данных из JSON-файла
            string json = File.ReadAllText("Menu\\Order.json");
            var orders = JsonSerializer.Deserialize<ObservableCollection<Order>>(json);
            Orders = orders ?? new ObservableCollection<Order>();
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
