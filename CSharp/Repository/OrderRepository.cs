using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSharp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CSharp.Repository
{
    public class OrderRepository : IOrderRepository
    {        
        public OrderRepository() {
                          
        }

        public Task<Customer> Delete(int customerId)
        {
            throw new NotImplementedException();
        }

        public Task<Customer> OrdersByCustomer(int customerId)
        {
            throw new NotImplementedException();
        }

        public Task Create()
        {
            throw new NotImplementedException();
        }

        public Task<Customer> Update(int customerId)
        {
            throw new NotImplementedException();
        }
    }
}
