namespace FoodFlow.Models
{
    public class OrderItem
    {
        public Dish Dish { get; set; } = Dish.Empty;
        public int Amount { get;set; } // граммы для весового, штуки для штучного 
    }
}