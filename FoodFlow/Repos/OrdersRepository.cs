using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using FoodFlow.Models;

namespace FoodFlow.Repos
{
    internal class OrderRepository
    {
        public IEnumerable<Order> GetAll()
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                WriteIndented = true
            };

            //var json = File.ReadAllText("Menu\\Order.json");
            //return JsonSerializer.Deserialize<List<Order>>(json, options);
            return JsonSerializer.Deserialize<List<Order>>(System.IO.File.ReadAllText("Menu\\Order.json"), options);
        }
    }
}
