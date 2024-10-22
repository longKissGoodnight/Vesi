using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.Encodings.Web;
using FoodFlow.Models;
using FoodFlow.ViewModels;
using System.Text.Json.Serialization;

namespace FoodFlow.Services
{
    public class OrderHistoryService
    {
        private readonly string _filePath;
        private static int _lastOrderNumber = 0;

        public OrderHistoryService()
        {
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string projectPath = Path.GetFullPath(Path.Combine(baseDirectory, @"..\..\..\Menu"));
            _filePath = Path.Combine(projectPath, "Orders.json");
            
            if (File.Exists(_filePath))
            {
                var orders = LoadOrders();
                if (orders.Count > 0)
                {
                    _lastOrderNumber = orders[^1].OrderNumber;
                }
            }
        }

        public void SaveOrder(OrderViewModel currentOrder, decimal totalPrice)
        {
            var orders = LoadOrders();
            var order = new Order
            {
                OrderNumber = ++_lastOrderNumber,
                CreateTime = DateTime.Now,
                TotalPrice = totalPrice,
                Items = currentOrder.Items.Select(item => new OrderItem
                {
                    Dish = item.Dish,
                    Amount = item.Amount
                }).ToList()
            };
            orders.Add(order);




            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull,
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping // Это позволит сохранять русские символы без экранирования
            };
            options.Converters.Add(new JsonStringEnumConverter());

            using (var writer = new StreamWriter(_filePath, false, new UTF8Encoding(false))) // false означает не использовать BOM
            {
                writer.Write(JsonSerializer.Serialize(orders, options));
            }
        }

        private List<Order> LoadOrders()
        {
            if (!File.Exists(_filePath))
            {
                return new List<Order>();
            }

            var json = File.ReadAllText(_filePath, new UTF8Encoding(false));
            try
            {
                var options = new JsonSerializerOptions
                {
                    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                };
                return JsonSerializer.Deserialize<List<Order>>(json, options) ?? new List<Order>();
            }
            catch
            {
                return new List<Order>();
            }
        }

        public List<Order> GetAllOrders()
        {
            return LoadOrders();
        }
    }
}