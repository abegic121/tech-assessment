using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using CSharp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CSharp.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly string OrdersJsonFile = "orders.json";

        public OrderRepository()
        {
            //noop            
        }

        public async Task<int> Create(Order newOrder)
        {
            var json = await File.ReadAllTextAsync(this.OrdersJsonFile);
            var listOfOrders = JsonSerializer.Deserialize<List<Order>>(json);

            if (listOfOrders.Exists(s => s.Id == newOrder.Id))
            {
                return newOrder.Id;
            }

            newOrder.Id = listOfOrders.Max(s => s.Id) + 1;
            listOfOrders.Add(newOrder);

            string jsonToWrite = JsonSerializer.Serialize(newOrder);
            File.WriteAllText(this.OrdersJsonFile, jsonToWrite);

            return newOrder.Id;
        }

        public async Task<List<Order>> GetOrdersByCustomer(int customerId)
        {
            var json = await File.ReadAllTextAsync(this.OrdersJsonFile);
            var listOfOrders = JsonSerializer.Deserialize<List<Order>>(json);
            return listOfOrders.Where(s => s.CustomerId == customerId).ToList();
        }

        public async Task<bool> Delete(int orderId)
        {
            var json = await File.ReadAllTextAsync(this.OrdersJsonFile);
            var listOfOrders = JsonSerializer.Deserialize<List<Order>>(json);
            Order toRemove = listOfOrders.FirstOrDefault(s => s.Id == orderId);            

            //if we found an orderId to remove, then update the .json file
            if (toRemove != null)
            {
                listOfOrders.Remove(listOfOrders.FirstOrDefault(s => s.Id == orderId));
                string jsonToWrite = JsonSerializer.Serialize(listOfOrders);
                File.WriteAllText(this.OrdersJsonFile, jsonToWrite);
                return true;
            }

            return false;
        }

        public async Task<bool> Update(Order orderToUpdate)
        {
            var json = await File.ReadAllTextAsync(this.OrdersJsonFile);
            var listOfOrders = JsonSerializer.Deserialize<List<Order>>(json);
            var toUpdate = listOfOrders.FirstOrDefault(s => s.Id == orderToUpdate.Id);

            if (toUpdate != null)
            {
                toUpdate = orderToUpdate;
                string jsonToWrite = JsonSerializer.Serialize(listOfOrders);
                File.WriteAllText(this.OrdersJsonFile, jsonToWrite);
                return true;
            }

            return false;
        }
    }
}
