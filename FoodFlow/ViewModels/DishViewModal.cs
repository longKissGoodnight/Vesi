using FoodFlow.Models;

namespace FoodFlow.ViewModels
{
    public class DishViewModal
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public ServingMethod Serving { get; set; }
        public DishType Type { get; set; }
        public static Dish Empty { get; private set; } = new Dish();
        public float Price { get; set; } // Обязательно проверить тип

        // Добавляем свойство для сокращенного типа
        // Добавляем свойство для сокращенного типа

        //попробовать конвертер
        public string ServingShort
        {
            get
            {
                return Serving == ServingMethod.Weight ? "Вес" : "Шт";
            }
        }
    }
}