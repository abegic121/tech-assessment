using CSharp.Controllers;
using CSharp.Models;
using CSharp.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace CSharp.UnitTest.Controllers
{
    [TestClass]
    public class OrdersControllerTests
    {

        private Mock<IOrderService> mockOrderService;

        /*
        * Create throws conflit when Id > 0.
        */
        [TestMethod]
        public async Task Create_Should_Return_Conflict_Async()
        {
            var newOrder = new Order
            {
                Id = 1,
                CustomerId = 1,
                CreatedOn = DateTime.Now,
            };
            var controller = getController();

            var actionResult = await controller.Create(newOrder);

            this.mockOrderService.Verify(s => s.Create(It.IsAny<Order>()), Times.Never);
            
            Assert.IsInstanceOfType(actionResult.Result, typeof(ConflictResult));
        }

        /*
        * Successful Create
        */
        [TestMethod]
        public async Task Create_Async()
        {
            //Arrange
            this.mockOrderService.Setup(s => s.Create(It.IsAny<Order>())).ReturnsAsync(1);

            var newOrder = new Order
            {
                Id = 0,
                CustomerId = 1,
                CreatedOn = DateTime.Now,
            };
            var controller = getController();

            //Act
            var actionResult = await controller.Create(newOrder);
            
            ////Assert
            this.mockOrderService.Verify(s => s.Create(newOrder), Times.Once);

            var value = actionResult.Value;
            Assert.IsTrue(value == 1);
        }

        /*
         * Throws BadRequest when OrderId is 0.
         */
        [TestMethod]
        public async Task Update_Should_Return_BadRequest_Async()
        {
            var orderToUpdate = new Order
            {
                Id = 1,
                CustomerId = 1,
                CreatedOn = DateTime.Now,
            };
            var controller = getController();

            var action = await controller.Update(0, orderToUpdate);

            this.mockOrderService.Verify(s => s.Update(It.IsAny<Order>()), Times.Never);

            Assert.IsInstanceOfType(action.Result, typeof(BadRequestResult));
        }

        /*
         * Throws BadRequest when OrderId does not match Object Id.
         */
        [TestMethod]
        public async Task Update_Should_Return_BadRequest_V2_Async()
        {
            var orderToUpdate = new Order
            {
                Id = 1,
                CustomerId = 1,
                CreatedOn = DateTime.Now,
            };
            var controller = getController();

            var action = await controller.Update(2, orderToUpdate);

            this.mockOrderService.Verify(s => s.Update(It.IsAny<Order>()), Times.Never);

            Assert.IsInstanceOfType(action.Result, typeof(BadRequestResult));
        }

        /*
         * Successful Update
         */
        [TestMethod]
        public async Task Update_Async()
        {
            var orderToUpdate = new Order
            {
                Id = 1,
                CustomerId = 1,
                CreatedOn = DateTime.Now,
            };

            this.mockOrderService.Setup(s => s.Update(orderToUpdate)).ReturnsAsync(true);
            var controller = getController();

            var actionResult = await controller.Update(orderToUpdate.Id, orderToUpdate);

            this.mockOrderService.Verify(s => s.Update(orderToUpdate), Times.Once);
            
            var value = actionResult.Value;
            Assert.IsTrue(value);
        }

        /*
        * Throws BadRequest when OrderId <= 0
        */
        [TestMethod]
        public async Task Delete_Should_Return_BadRequest_Async()
        {
            var toDeleteOrderId = 0;
            var controller = getController();

            var action = await controller.Delete(toDeleteOrderId);

            this.mockOrderService.Verify(s => s.Delete(toDeleteOrderId), Times.Never);

            Assert.IsInstanceOfType(action, typeof(BadRequestResult));
        }

        /*
        * Successful delete
        */
        [TestMethod]
        public async Task Delete_Async()
        {
            var toDeleteOrderId = 1;
            var controller = getController();

            var action = await controller.Delete(toDeleteOrderId);

            this.mockOrderService.Verify(s => s.Delete(toDeleteOrderId), Times.Once);

            Assert.IsInstanceOfType(action, typeof(OkResult));
        }


        [TestInitialize]
        public void TestInitialize()
        {
            this.mockOrderService = new Mock<IOrderService>();
        }

        private OrdersController getController()
        {
            return new OrdersController(this.mockOrderService.Object);
        }
    }
}