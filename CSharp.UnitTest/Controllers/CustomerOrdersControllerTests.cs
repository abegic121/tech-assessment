using CSharp.Controllers;
using CSharp.Models;
using CSharp.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;

namespace CSharp.UnitTest.Controllers
{
    [TestClass]
    public class CustomerOrdersControllerTests
    {

        private Mock<IOrderService> mockOrderService;

        /*
        * Get customer orders throws conflit when Id <= 0.
        */
        [TestMethod]
        public async Task Get_By_CustomerId_Should_Return_BadRequest_Async()
        {
            var customerId = 0;
            var controller = getController();

            var actionResult = await controller.Get(customerId);

            this.mockOrderService.Verify(s => s.GetOrdersByCustomer(It.IsAny<int>()), Times.Never);

            Assert.IsInstanceOfType(actionResult.Result, typeof(BadRequestResult));
        }

        /*
        * Successful Get by CustomerId
        */
        [TestMethod]
        public async Task Get_By_CustomerId_Async()
        {
            var customerId = 1;
            var ordersToReturnMock = new List<Order> { new Order { Id = 1, CustomerId = 1 } };

            this.mockOrderService.Setup(s => s.GetOrdersByCustomer(customerId)).ReturnsAsync(ordersToReturnMock);

            var controller = getController();

            var actionResult = await controller.Get(customerId);

            this.mockOrderService.Verify(s => s.GetOrdersByCustomer(customerId), Times.Once);

            var value = actionResult.Value;
            Assert.IsTrue(value?.Count == 1);
        }

        [TestInitialize]
        public void TestInitialize()
        {
            this.mockOrderService = new Mock<IOrderService>();
        }

        private CustomerOrdersController getController()
        {
            return new CustomerOrdersController(this.mockOrderService.Object);
        }
    }
}