﻿using System;
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
    [Route("[controller]")]
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

            return Ok(await this.orderService.CreateOrder(newOrder));
        }

        [HttpGet]
        [Route("customers/{customerId}")]
        public async Task<ActionResult<List<Order>>> GetByCustomer(int customerId)
        {
            //retrieve by customerid
            return Ok(await this.orderService.GetOrdersByCustomer(customerId));
        }

        [HttpPut]
        [Route("{orderId}")]
        public async Task<ActionResult> Update(int orderId, Order orderToUpdate)
        {
            //update resource
            if (orderId <= 0 || orderId != orderToUpdate.Id)
            {
                return BadRequest();
            }

            return Ok(await this.orderService.Update(orderToUpdate));
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
            
            await this.orderService.Delete(orderId);
            return Ok();
        }
    }
}
