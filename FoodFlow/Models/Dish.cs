using System.Text.Json.Serialization;

namespace FoodFlow.Models
{
    public class Dish
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

       
        public ServingMethod Serving { get;set;}

       
        public DishType Type { get; set; }
        public static Dish Empty { get; private set; } = new Dish();
        public decimal Price { get; set; }
    }
}