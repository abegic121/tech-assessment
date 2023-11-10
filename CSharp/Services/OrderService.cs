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

        public async Task<Customer> Delete(int customerId)
        {
            return await this.orderRepository.Delete(customerId);
        }

        public async Task<Customer> OrdersByCustomer(int customerId)
        {
            return await this.orderRepository.Delete(customerId);
        }

        public async Task Create()
        {
            return await this.orderRepository.Create();
        }

        public async Task<Customer> Update(int customerId)
        {
            return await this.orderRepository.Update(customerId);
        }
    }
}
