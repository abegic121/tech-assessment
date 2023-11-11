using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSharp.Models;
using CSharp.Repository;
using CSharp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CSharp.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            this.orderRepository = orderRepository;
        }

        public async Task Delete(int orderId)
        {
            //any business logic
            await this.orderRepository.Delete(orderId);
        }

        public async Task<List<Order>> Get()
        {
            //any business logic
            return await this.orderRepository.Get();
        }

        public async Task<List<Order>> GetOrdersByCustomer(int customerId)
        {
            //any business logic
            //want to point out that if this was a db retrieval, this is not ok as all results would be pulled then filter would apply. 
            return (await this.orderRepository.Get()).Where(s => s.CustomerId == customerId).ToList();
        }

        public async Task<int> Create(Order newOrder)
        {
            //any business logic
            return await this.orderRepository.Create(newOrder);
        }

        public async Task<bool> Update(Order orderToUpdate)
        {
            //any business logic
            return await this.orderRepository.Update(orderToUpdate);
        }

    }
}
