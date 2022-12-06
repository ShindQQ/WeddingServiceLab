using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WeddingService.Bll.Models;
using WeddingService.Bll.Services;
using WeddingService.Bll.Services.Interfaces;
using WeddingService.Dal.Contexts;
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
        public void UpdateAsync_UpdateClothes_UpdatedClothesInDB()
        {
            var clothes = _clothesService.FindAsync(new ClothesDto { Id = 1 }).Result;

            clothes.Name = "UpdatedClothes";

            _clothesService.UpdateAsync(clothes);

            var newClothes = _clothesService.FindAsync(new ClothesDto { Id = 1 }).Result;

            Assert.Equal(clothes, newClothes);
        }

        [Fact]
        public void DeleteAsync_DeleteClothes_DeletedClothesInDB()
        {
            var clothes = _clothesService.FindAsync(new ClothesDto { Id = 1 }).Result;

            _clothesService.DeleteAsync(clothes);

            var notDeleted = _clothesService.IsExistAsync(new ClothesDto { Id = 1 }).Result;
            Assert.False(notDeleted);
        }

        [Fact]
        public void FindAsync_FindClothes_ClothesHasId1()
        {
            int ID = 1;
            var clothes = _clothesService.FindAsync(new ClothesDto { Id = 1 }).Result;

            Assert.Equal(clothes.Id, ID);
        }

        [Fact]
        public void IsExistAsync_CheckClothes_ExistClothesId1()
        {
            var exist = _clothesService.IsExistAsync(new ClothesDto { Id = 1 }).Result;

            Assert.True(exist);
        }

        [Fact]
        public void GetAsync_GetList_NotEmprtyList()
        {
            var list = _clothesService.GetAsync().Result;

            Assert.NotEmpty(list);
        }
    }
}
