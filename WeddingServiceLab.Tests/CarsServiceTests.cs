using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WeddingService.Bll.Models;
using WeddingService.Bll.Services;
using WeddingService.Bll.Services.Interfaces;
using WeddingService.Dal.Contexts;
using WeddingService.Dal.Entities;
using Xunit;

namespace WeddingServiceLab.Tests
{
    public class CarsServiceTests
    {
        private readonly ICarsService _carsService;

        public static IConfiguration InitConfiguration()
        {
            var config = new ConfigurationBuilder()
               .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();

            return config;
        }

        public CarsServiceTests()
        {
            var services = new ServiceCollection();
            services.AddScoped<ICarsService, CarsService>();
            services.AddDbContext<WeddingServiceContext>(options =>
                    options.UseSqlServer(InitConfiguration()["ConnectionStrings:DefaultConnection"]));

            var serviceProvider = services.BuildServiceProvider();

            _carsService = serviceProvider.GetService<ICarsService>(); ;
        }

        [Fact]
        public async void AddAsync_AddedCar_ReturnedSameCar()
        {
            var car = new Car() { Name = "Car1", Price = 10000 };
            var AddedCar = await _carsService.AddAsync(car);

            Assert.Equal(car, AddedCar);
        }

        [Fact]
        public async void UpdateAsync_UpdateCar_UpdatedCarInDB()
        {
            var car = await _carsService.FindAsync(new CarDto { Id = 1 });

            car.Name = "UpdatedCar";

            await _carsService.UpdateAsync(car);

            var newCar = await _carsService.FindAsync(new CarDto { Id = 1 });

            Assert.Equal(car, newCar);
        }

        [Fact]
        public async void DeleteAsync_DeleteCar_DeletedCarInDB()
        {
            var car = await _carsService.FindAsync(new CarDto { Id = 1 });

            await _carsService.DeleteAsync(car);

            var notDeleted = await _carsService.IsExistAsync(new CarDto { Id = 1 });
            Assert.False(notDeleted);
        }

        [Fact]
        public async void FindAsync_FindCar_CarHasId1()
        {
            int ID = 1;
            var car = await _carsService.FindAsync(new CarDto { Id = 1 });

            Assert.Equal(car.Id, ID);
        }

        [Fact]
        public async void IsExistAsync_CheckCar_ExistCarId1()
        {
            var exist = await _carsService.IsExistAsync(new CarDto { Id = 1 });

            Assert.True(exist);
        }

        [Fact]
        public async void GetAsync_GetList_NotEmptyList()
        {
            var list = await _carsService.GetAsync();

            Assert.NotEmpty(list);
        }
    }
}
