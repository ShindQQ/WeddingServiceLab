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
                    options.UseSqlServer(InitConfiguration()["ConnectionStrings:SalivonConnection"]));

            var serviceProvider = services.BuildServiceProvider();

            _carsService = serviceProvider.GetService<ICarsService>(); ;
        }

        [Fact]
        public void AddAsync_AddedCar_ReturnedSameCar()
        {
            var car = new Cars() { Name = "Car1", Price = 10000 };
            var AddedCar = _carsService.AddAsync(car).Result;

            Assert.Equal(car, AddedCar);
        }

        [Fact]
        public void UpdateAsync_UpdateCar_UpdatedCarInDB()
        {
            var car = _carsService.FindAsync(new CarsDto { Id = 1 }).Result;

            car.Name = "UpdatedCar";

            _carsService.UpdateAsync(car);

            var newCar = _carsService.FindAsync(new CarsDto { Id = 1 }).Result;

            Assert.Equal(car, newCar);
        }

        [Fact]
        public void DeleteAsync_DeleteCar_DeletedCarInDB()
        {
            var car = _carsService.FindAsync(new CarsDto { Id = 1 }).Result;

            _carsService.DeleteAsync(car);

            var notDeleted = _carsService.IsExistAsync(new CarsDto { Id = 1 }).Result;
            Assert.False(notDeleted);
        }

        [Fact]
        public void FindAsync_FindCar_CarHasId1()
        {
            int ID = 1;
            var car = _carsService.FindAsync(new CarsDto { Id = 1 }).Result;

            Assert.Equal(car.Id, ID);
        }

        [Fact]
        public void IsExistAsync_CheckCar_ExistCarId1()
        {
            var exist = _carsService.IsExistAsync(new CarsDto { Id = 1 }).Result;

            Assert.True(exist);
        }

        [Fact]
        public void GetAsync_GetList_NotEmptyList()
        {
            var list = _carsService.GetAsync().Result;

            Assert.NotEmpty(list);
        }
    }
}
