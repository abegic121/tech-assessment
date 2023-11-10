using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Channels;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CSharp.Controllers
{
	[ApiController]
	[Route("/orders")]
	public class OrderController : ControllerBase
	{
		//Create order endpoint
		//List all orders by customer endpoint
		//Update order endpoint
		//Cancel order endpoint

        [HttpPost]
		public async Task<string> Create()
		{
            //create resource
			return await "Success!";
		}

        [HttpGet]
        [Route("customers/{customerId}")]
        public async Task<List<>> GetByCustomer(int customerId)
        {
            //retrieve by customerid
            return await "Success!";
        }

		[HttpPatch]
        [Route("{orderId}")]
        public async Task<string> Put(int orderId)
        {
            //update resource
            return await "Success!";
        }

        [HttpDelete]
        [Route("{orderId}")]
        public async Task<string> Update(int orderId)
        {
            //cancel a resource
            return await "Success!";
        }
    }
}
