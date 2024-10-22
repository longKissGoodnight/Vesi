namespace FoodFlow.Models
{
    public class Order
    {
/*      public int Id { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime CloseTime { get; set; }
        public List<OrderItem> Items { get; set; } = new();*/

        public int OrderNumber { get; set; }  // Изменено с Id для большей ясности
        public DateTime CreateTime { get; set; }
        public decimal TotalPrice { get; set; }  // Добавлено поле для общей стоимости
        public List<OrderItem> Items { get; set; } = new();
    }
}
