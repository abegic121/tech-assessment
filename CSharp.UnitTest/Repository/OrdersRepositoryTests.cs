using CSharp.Controllers;
using CSharp.Models;
using CSharp.Repository;
using CSharp.Services;
using CSharp.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace CSharp.UnitTest.Repository
{
    [TestClass]
    public class OrdersRepositoryTests
    {

        private List<Order> orders = new List<Order> { new Order { Id = 1, CustomerId = 1 },
        new Order { Id = 2, CustomerId = 1 },
        new Order { Id = 3, CustomerId = 2 },
        new Order { Id = 4, CustomerId = 3 },
        new Order { Id = 5, CustomerId = 4 }};
    

        [TestMethod]
        public async Task Create_Order_ReturnsSameId_Async()
        {
            var newOrder = new Order { Id=1, CustomerId = 1 };
            var repository = getRepository();

            var id = await repository.Create(newOrder);

            //did not create
            Assert.AreEqual(id, newOrder.Id);
        }

        [TestMethod]
        public async Task Create_Order_ReturnsNewId_Async()
        {
            var newOrder = new Order { Id = 0, CustomerId = 1 };
            var repository = getRepository();

            var id = await repository.Create(newOrder);

            Assert.AreEqual(id, this.orders.Max(s => s.Id) + 1);
        }

        [TestMethod]
        public async Task Get_Async()
        {
            var repository = getRepository();

            var data = await repository.Get();

            Assert.AreEqual(data.Count, this.orders.Count);
        }

        [TestMethod]
        public async Task Delete_Order_ReturnsFalse_Async()
        {
            //get item that does not exist
            var toDeleteId = this.orders.Max(s => s.Id) + 1; 
            var repository = getRepository();

            var isDeleted = await repository.Delete(toDeleteId);

            Assert.IsFalse(isDeleted);
        }

        [TestMethod]
        public async Task Delete_Order_ReturnsTrue_Async()
        {
            //get last item
            var toDeleteId = this.orders.Max(s => s.Id);
            var repository = getRepository();

            var isDeleted = await repository.Delete(toDeleteId);

            Assert.IsTrue(isDeleted);
        }

        [TestMethod]
        public async Task Update_Order_ReturnsTrue_Async()
        {
            var repository = getRepository();

            //get last item
            var toUpdate = this.orders.MaxBy(s => s.Id);
            toUpdate!.Id += 1;

            var isUpdated = await repository.Update(toUpdate);

            Assert.IsFalse(isUpdated);
        }

        [TestMethod]
        public async Task Update_Order_ReturnsFalse_Async()
        {
            
            var repository = getRepository();
            
            //get last item
            var toUpdate = this.orders.MaxBy(s => s.Id);
            toUpdate!.CustomerId += 1;

            var isUpdated = await repository.Update(toUpdate);

            Assert.IsTrue(isUpdated);
        }

        private IOrderRepository getRepository()
        {
            return new OrderRepository(orders);
        }
    }
}