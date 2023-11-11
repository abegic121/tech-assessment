using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSharp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CSharp.Repository
{
    public interface IOrderRepository
    {

        public Task<int> Create(Order newOrder);

        public Task<List<Order>> Get();     

        public Task<bool> Update(Order orderToUpdate);

        public Task<bool> Delete(int orderId);
    }
}
