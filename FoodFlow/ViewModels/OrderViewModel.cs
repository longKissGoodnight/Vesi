using System.Collections.ObjectModel;

namespace FoodFlow.ViewModels
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime CloseTime { get; set; }
        public ObservableCollection<OrderItemViewModel> Items { get; set; } = new();
    }
}