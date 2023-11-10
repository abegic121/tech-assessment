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
        public Task Create();

        public Task<Customer> OrdersByCustomer(int customerId);

        public Task<Customer> Update(int customerId);

        public Task<Customer> Delete(int customerId);
    }
}
