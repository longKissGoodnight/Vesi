namespace FoodFlow.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime CloseTime { get; set; }
        public List<OrderItem> Items { get; set; } = new();
    }
}