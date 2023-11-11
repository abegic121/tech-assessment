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
    [Route("/orders")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService orderService;

        public OrdersController(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create(Order newOrder)
        {
            //resource exists, return conflict
            if (newOrder.Id > 0)
            {
                return Conflict();
            } 

            return await this.orderService.Create(newOrder);
        }
        
        [HttpGet]
        public async Task<ActionResult<List<Order>>> Get()
        {
            return await this.orderService.Get();
        }

        [HttpPut]
        [Route("{orderId}")]
        public async Task<ActionResult<bool>> Update(int orderId, Order orderToUpdate)
        {
            if (orderId <= 0 || orderId != orderToUpdate.Id)
            {
                return BadRequest();
            }

            //update resource
            //Depends on what client wants as return
            //Example as boolean
            return await this.orderService.Update(orderToUpdate);
        }

        [HttpDelete]
        [Route("{orderId}")]
        public async Task<ActionResult> Delete(int orderId)
        {
            //cancel a resource

            if (orderId <= 0)
            {
                return BadRequest();
            }

            //Depends on what client wants as return
            //Example as nothing
            await this.orderService.Delete(orderId);
            return Ok();
        }
    }
}
