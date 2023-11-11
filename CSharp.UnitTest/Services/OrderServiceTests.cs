using CSharp.Controllers;
using CSharp.Models;
using CSharp.Repository;
using CSharp.Services;
using CSharp.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace CSharp.UnitTest.Services
{
    [TestClass]
    public class OrdersServiceTests
    {

        private Mock<IOrderRepository> mockOrderRepository;


        /*
         * Unit test any business logic goes here
         */

        [TestMethod]
        public async Task DeleteAsync()
        {
            var orderId = 1;
            var service = getService();

            await service.Delete(orderId);

            this.mockOrderRepository.Verify(s => s.Delete(orderId), Times.Once);
        }

        [TestMethod]
        public async Task GetAsync()
        {
            var ordersToReturnMock = new List<Order> { new Order { Id = 1, CustomerId = 1 } };

            this.mockOrderRepository.Setup(s => s.Get()).ReturnsAsync(ordersToReturnMock);

            var service = getService();

            var data = await service.Get();

            this.mockOrderRepository.Verify(s => s.Get(), Times.Once);

            Assert.IsTrue(data.Count == ordersToReturnMock.Count);
        }

        [TestMethod]
        public async Task GetOrdersByCustomer()
        {
            var ordersToReturnMock = new List<Order> { new Order { Id = 1, CustomerId = 1 }, new Order { Id = 1, CustomerId = 1 }, new Order { Id = 1, CustomerId = 2 }, };

            this.mockOrderRepository.Setup(s => s.Get()).ReturnsAsync(ordersToReturnMock);

            var customerId = 1;
            var service = getService();

            var data = await service.GetOrdersByCustomer(customerId);

            this.mockOrderRepository.Verify(s => s.Get(), Times.Once);

            Assert.IsTrue(data.Count == 2);
        }

        [TestMethod]
        public async Task CreateAsync()
        {
            var orderToCreate = new Order { Id = 0, CustomerId = 1 };

            this.mockOrderRepository.Setup(s => s.Create(orderToCreate)).ReturnsAsync(1);

            var service = getService();

            var createdId = await service.Create(orderToCreate);

            this.mockOrderRepository.Verify(s => s.Create(orderToCreate), Times.Once);

            Assert.IsTrue(createdId == 1);
        }

        [TestMethod]
        public async Task UpdateAsync()
        {
            var orderToUpdate = new Order { Id = 1, CustomerId = 3 };

            this.mockOrderRepository.Setup(s => s.Update(orderToUpdate)).ReturnsAsync(true);

            var service = getService();

            var isUpdateSuccess = await service.Update(orderToUpdate);

            this.mockOrderRepository.Verify(s => s.Update(orderToUpdate), Times.Once);

            Assert.IsTrue(isUpdateSuccess);
        }


        [TestInitialize]
        public void TestInitialize()
        {
            this.mockOrderRepository = new Mock<IOrderRepository>();
        }

        private IOrderService getService()
        {
            return new OrderService(this.mockOrderRepository.Object);
        }
    }
}