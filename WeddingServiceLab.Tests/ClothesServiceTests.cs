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
    public class ClothesServiceTests
    {
        private readonly IClothesService _clothesService;

        public static IConfiguration InitConfiguration()
        {
            var config = new ConfigurationBuilder()
               .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();

            return config;
        }

        public ClothesServiceTests()
        {
            var services = new ServiceCollection();
            services.AddScoped<IClothesService, ClothesService>();
            services.AddDbContext<WeddingServiceContext>(options =>
                    options.UseSqlServer(InitConfiguration()["ConnectionStrings:DefaultConnection"]));

            var serviceProvider = services.BuildServiceProvider();

            _clothesService = serviceProvider.GetService<IClothesService>(); ;
        }

        [Fact]
        public async void AddAsync_AddedClothes_ReturnedSameCloth()
        {
            var cloth = new Cloth() { Name = "Cloth1", Price = 200 };
            var AddedCloth = await _clothesService.AddAsync(cloth);

            Assert.Equal(cloth, AddedCloth);
        }

        [Fact]
        public async void UpdateAsync_UpdateClothes_UpdatedClothesInDB()
        {
            var clothes = await _clothesService.FindAsync(new ClothDto { Id = 3 });

            clothes.Name = "UpdatedClothes";

            await _clothesService.UpdateAsync(clothes);

            var newClothes = await _clothesService.FindAsync(new ClothDto { Id = 3 });

            Assert.Equal(clothes, newClothes);
        }

        [Fact]
        public async void DeleteAsync_DeleteClothes_DeletedClothesInDB()
        {
            var clothes = await _clothesService.FindAsync(new ClothDto { Id = 3 });

            await _clothesService.DeleteAsync(clothes);

            var notDeleted = await _clothesService.IsExistAsync(new ClothDto { Id = 3 });
            Assert.False(notDeleted);
        }

        [Fact]
        public async void FindAsync_FindClothes_ClothesHasId3()
        {
            int ID = 3;
            var clothes = await _clothesService.FindAsync(new ClothDto { Id = 3 });

            Assert.Equal(clothes.Id, ID);
        }

        [Fact]
        public async void IsExistAsync_CheckClothes_ExistClothesId3()
        {
            var exist = await _clothesService.IsExistAsync(new ClothDto { Id = 3 });

            Assert.True(exist);
        }

        [Fact]
        public async void GetAsync_GetList_NotEmptyList()
        {
            var list = await _clothesService.GetAsync();

            Assert.NotEmpty(list);
        }
    }
}
