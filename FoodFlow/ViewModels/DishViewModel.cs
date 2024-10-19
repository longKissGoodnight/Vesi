using FoodFlow.Models;

namespace FoodFlow.ViewModels
{
    public class DishViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public ServingMethod Serving { get; set; }
        public DishType Type { get; set; }
        public static Dish Empty { get; private set; } = new Dish();
        public decimal Price { get; set; } // Обязательно проверить тип

        // Добавляем свойство для сокращенного типа
        // Добавляем свойство для сокращенного типа

        //попробовать конвертер
        /*        public string ServingShort
                {
                    get
                    {
                        return Serving == ServingMethod.Weight ? "Вес" : "Шт";
                    }
                }*/

        //public string ServingString => Serving.ToString();
    }
}