using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.Xml;
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
            try
            {
                if (!File.Exists(OrdersJsonFile))
                {
                    using var file = File.Create(this.OrdersJsonFile);
                }
            }
            catch (Exception e) { }
        }

        //Primarily for testing purposes
        public OrderRepository(List<Order> ordersToWrite) : this()
        {
            WriteOrdersFile(ordersToWrite);
        }

        public async Task<int> Create(Order newOrder)
        {
            var existingOrders = await ReadOrdersFile();

            if (existingOrders.Exists(s => s.Id == newOrder.Id))
            {
                return newOrder.Id;
            }
            else
            {
                newOrder.Id = existingOrders.Count > 0 ? existingOrders.Max(s => s.Id) + 1 : 1;
                existingOrders.Add(newOrder);
            }

            WriteOrdersFile(existingOrders);

            return newOrder.Id;
        }

        public async Task<List<Order>> Get()
        {
            return await ReadOrdersFile();
        }

        public async Task<bool> Delete(int orderId)
        {
            var existingOrders = await ReadOrdersFile();
            Order toRemove = existingOrders.FirstOrDefault(s => s.Id == orderId);

            //if we found an orderId to remove, then update the .json file
            if (toRemove != null)
            {
                existingOrders.Remove(toRemove);
                WriteOrdersFile(existingOrders);
                return true;
            }

            return false;
        }

        public async Task<bool> Update(Order orderToUpdate)
        {
            var existingOrders = await ReadOrdersFile();
            var objToUpdateIndex = existingOrders.FindIndex(s => s.Id == orderToUpdate.Id);

            if (objToUpdateIndex != -1)
            {
                existingOrders[objToUpdateIndex] = orderToUpdate;
                WriteOrdersFile(existingOrders);
                return true;
            }

            return false;
        }

        private async Task<List<Order>> ReadOrdersFile()
        {
            using (FileStream jsonStream = new FileStream(this.OrdersJsonFile, FileMode.Open, FileAccess.Read))
            {
                if (jsonStream.Length > 0)
                {
                    return await JsonSerializer.DeserializeAsync<List<Order>>(jsonStream);
                }
            }

            return new List<Order>();
        }

        private void WriteOrdersFile(List<Order> orders)
        {
            string jsonToWrite = JsonSerializer.Serialize(orders);
            File.WriteAllText(this.OrdersJsonFile, jsonToWrite);
        }
    }
}
