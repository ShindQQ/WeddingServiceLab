using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
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
    public class CaremonyServiceTests
    {
        private readonly ICeremoniesService _ceremoniesService;

        public static IConfiguration InitConfiguration()
        {
            var config = new ConfigurationBuilder()
               .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();

            return config;
        }

        public CaremonyServiceTests()
        {
            var services = new ServiceCollection();
            services.AddScoped<ICeremoniesService, CeremoniesService>();
            services.AddDbContext<WeddingServiceContext>(options =>
                    options.UseSqlServer(InitConfiguration()["ConnectionStrings:DefaultConnection"]));

            var serviceProvider = services.BuildServiceProvider();

            _ceremoniesService = serviceProvider.GetService<ICeremoniesService>(); ;
        }

        [Fact]
        public async void AddAsync_AddedCeremony_ReturnedSameCeremony()
        {
            var ceremony = new Ceremony() { Name = "Ceremony1", Price = 100 };

            var AddedCeremony = await _ceremoniesService.AddAsync(ceremony);

            Assert.Equal(ceremony, AddedCeremony);
        }

        [Fact]
        public async void UpdateAsync_UpdateCeremony_UpdatedCeremonyInDB()
        {
            var ceremony = await _ceremoniesService.FindAsync(new CeremonyDto { Id = 2 });

            ceremony.Name = "UpdatedCeremony";

            await _ceremoniesService.UpdateAsync(ceremony);

            var newCeremony = await _ceremoniesService.FindAsync(new CeremonyDto { Id = 2 });

            Assert.Equal(ceremony, newCeremony);
        }

        [Fact]
        public async void DeleteAsync_DeleteCeremony_DeletedCeremonyInDB()
        {
            var ceremony = await _ceremoniesService.FindAsync(new CeremonyDto { Id = 2 });

            await _ceremoniesService.DeleteAsync(ceremony);

            var notDeleted = await _ceremoniesService.IsExistAsync(new CeremonyDto { Id = 2 });
            Assert.False(notDeleted);
        }

        [Fact]
        public async void FindAsync_FindCeremony_CeremonyHasId2()
        {
            int ID = 2;

            var ceremony = await _ceremoniesService.FindAsync(new CeremonyDto { Id = 2 });

            Assert.Equal(ceremony.Id, ID);
        }

        [Fact]
        public async void IsExistAsync_CheckCeremony_ExistCeremonyId2()
        {
            var exist = await _ceremoniesService.IsExistAsync(new CeremonyDto { Id = 2 });

            Assert.True(exist);
        }

        [Fact]
        public async void GetAsync_GetList_NotEmptyList()
        {
            var list = await _ceremoniesService.GetAsync();

            Assert.NotEmpty(list);
        }
    }
}
