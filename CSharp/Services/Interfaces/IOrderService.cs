using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSharp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CSharp.Services.Interfaces
{
    public interface IOrderService
    {
        public Task<List<Order>> GetOrdersByCustomer(int customerId);

        public Task<int> CreateOrder(Order newOrder);

        public Task<bool> Update(Order orderToUpdate);

        public Task Delete(int orderId);
    }
}
