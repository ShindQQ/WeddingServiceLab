using Microsoft.EntityFrameworkCore;
using WeddingService.Bll.Models;
using WeddingService.Bll.Models.Base;
using WeddingService.Bll.Services;
using WeddingService.Bll.Services.Interfaces;
using WeddingService.Dal.Contexts;
using WeddingService.Dal.Entities;
using Xunit;

namespace WeddingServiceLab.Tests
{
    public class OrdersServiceTests
    {
        private readonly IOrdersService _ordersService;
        private readonly ServiceDto _serviceEntity;

        public OrdersServiceTests(IOrdersService ordersService, ServiceDto serviceEntity)
        {
            _ordersService = ordersService;
            _serviceEntity = serviceEntity;
        }

        [Fact]
        public void AddAsync_AddedOrder_ReturnedSameOrder()
        {
            var order = new Orders { TotalPrice = 0 };
            var AddedOrder = _ordersService.AddAsync(order).Result;

            Assert.Equal(order, AddedOrder);
        }

        [Fact]
        public void AddServiceToOrderAsync_AddService_ReturnedOrder()
        {
            var order = _ordersService.FindAsync(new OrdersDto { Id = 1}).Result;

            var newOrder = _ordersService.AddServiceToOrderAsync(1, _serviceEntity).Result;

            Assert.Equal(order, newOrder);
        }

        [Fact]
        public void DeleteAsync_DeleteOrder_DeletedOrderInDB()
        {
            var order = new Orders() { Id = 1 };
            _ordersService.DeleteAsync(order);
            var exist = _ordersService.IsExistAsync(new OrdersDto { Id = 1 }).Result;
            Assert.False(exist);
        }

        [Fact]
        public void DeleteServiceFromOrderAsync_DeleteServiceFromOrder_DeletedServiceInDB()
        {
            var order = _ordersService.FindAsync(new OrdersDto { Id = 1 }).Result;
            var orderDeleted = _ordersService.DeleteServiceFromOrderAsync(1, 1).Result;

            Assert.NotEqual(order.Services, orderDeleted.Services);
        }

        [Fact]
        public void FindAsync_FindOrder_FoundOrderId1()
        {
            int ID = 1;
            var order = _ordersService.FindAsync(new OrdersDto { Id = 1 }).Result;

            long orderID = order.Id;
            Assert.Equal(orderID, ID);
        }

        [Fact]
        public void GetAsync_GetCollection_NotEmptyCollection()
        {
            var list = _ordersService.GetAsync(true).Result;

            Assert.NotEmpty(list);
        }

        [Fact]
        public void IsExistAsync_CheckOrder_ExistOrderId1()
        {
            var exist = _ordersService.IsExistAsync(new OrdersDto { Id = 1 }).Result;

            Assert.True(exist);
        }

        [Fact]
        public void UpdateAsync_UpdateOrder_UpdatedOrderInDB()
        {
            var order = _ordersService.FindAsync(new OrdersDto { Id = 1 }).Result;
            order.TotalPrice = 10;

            _ordersService.UpdateAsync(order);

            var newOrder = _ordersService.FindAsync(new OrdersDto { Id = 1 }).Result;

            Assert.Equal(order, newOrder);
        }
    }
}
