using System.Text.Json;
using FoodFlow.Models;

namespace FoodFlow.Repos
{
    internal class DishesRepository
    {
        public IEnumerable<Dish> GetAll()
        {
            return JsonSerializer.Deserialize<List<Dish>>(System.IO.File.ReadAllText("Menu\\Menu.json"))!;
        }
    }
}
