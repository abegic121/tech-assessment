using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Channels;
using System.Threading.Tasks;
using CSharp.Models;
using CSharp.Services;
using CSharp.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CSharp.Controllers
{
    [ApiController]
    [AllowAnonymous]
    [Route("/customers/{customerId}/orders")]
    public class CustomerOrdersController : ControllerBase
    {
        private readonly IOrderService orderService;

        public CustomerOrdersController(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        //Delete customer order
        //Update customer order
        //Put/patch customer order

        [HttpGet]
        public async Task<ActionResult<List<Order>>> Get(int customerId)
        {
            if(customerId <= 0)
            {
                return BadRequest();
            }

            //retrieve by customerid
            return await this.orderService.GetOrdersByCustomer(customerId);
        }
    }
}
