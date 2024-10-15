using System.Text.Json;
using System.Text.Json.Serialization;
using FoodFlow.Models;

namespace FoodFlow.Repos
{
    internal class DishesRepository
    {
        public IEnumerable<Dish> GetAll()
        {
            var options = new JsonSerializerOptions
            {
                Converters = { new JsonStringEnumConverter() },
                WriteIndented = true // Для удобства чтения результата
            };

            return JsonSerializer.Deserialize<List<Dish>>(System.IO.File.ReadAllText("Menu\\Menu.json"), options);
        }
    }
}
