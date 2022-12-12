using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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

        public static IConfiguration InitConfiguration()
        {
            var config = new ConfigurationBuilder()
               .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();

            return config;
        }

        public OrdersServiceTests()
        {
            var services = new ServiceCollection();
            services.AddScoped<IOrdersService, OrdersService>();
            services.AddDbContext<WeddingServiceContext>(options =>
                    options.UseSqlServer(InitConfiguration()["ConnectionStrings:DefaultConnection"]));

            var serviceProvider = services.BuildServiceProvider();

            _ordersService = serviceProvider.GetService<IOrdersService>(); ;
        }

        [Fact]
        public async  void AddAsync_AddedOrder_ReturnedSameOrder()
        {
            var order = new Order { TotalPrice = 0 };
            var AddedOrder = await _ordersService.AddAsync(order);

            Assert.Equal(order, AddedOrder);
        }

        [Fact]
        public async void AddServiceToOrderAsync_AddService_ReturnedOrder()
        {
            var order = new Order { Id = 1, TotalPrice = 10000 };
            order.Services.Add(new Car { Id = 1, Name = "Car1", Price = 10000 });

            var newOrder = await _ordersService.AddServiceToOrderAsync(1, new BaseServiceDto { Id = 1, Name = "Car1", Price = 10000 });

            Assert.Equal(order, newOrder);
        }

        [Fact]
        public async void DeleteAsync_DeleteOrder_DeletedOrderInDB()
        {
            await _ordersService.DeleteAsync(new Order() { Id = 1 });

            var exist = await _ordersService.IsExistAsync(new OrderDto { Id = 1 });

            Assert.False(exist);
        }

        [Fact]
        public async void DeleteServiceFromOrderAsync_DeleteServiceFromOrder_DeletedServiceInDB()
        {
            var order = await _ordersService.FindAsync(new OrderDto { Id = 1 });

            var orderDeleted = await _ordersService.DeleteServiceFromOrderAsync(1, 1);

            Assert.NotEqual(order.Services, orderDeleted.Services);
        }

        [Fact]
        public async void FindAsync_FindOrder_FoundOrderId1()
        {
            int ID = 1;
            var order = await _ordersService.FindAsync(new OrderDto { Id = 1 });

            long orderID = order.Id;
            Assert.Equal(orderID, ID);
        }

        [Fact]
        public async void GetAsync_GetCollection_NotEmptyCollection()
        {
            var list = await _ordersService.GetAsync(true);

            Assert.NotEmpty(list);
        }

        [Fact]
        public async void IsExistAsync_CheckOrder_ExistOrderId1()
        {
            var exist = await _ordersService.IsExistAsync(new OrderDto { Id = 1 });

            Assert.True(exist);
        }

        [Fact]
        public async void UpdateAsync_UpdateOrder_UpdatedOrderInDB()
        {
            var order = await _ordersService.FindAsync(new OrderDto { Id = 2 });
            order.TotalPrice = 10;

            await _ordersService.UpdateAsync(order);

            var newOrder = await _ordersService.FindAsync(new OrderDto { Id = 2 });

            Assert.Equal(order, newOrder);
        }
    }
}
