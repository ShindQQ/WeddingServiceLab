using Microsoft.EntityFrameworkCore;
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

        public ClothesServiceTests(IClothesService clothesService)
        {
            _clothesService = clothesService;
        }

        [Fact]
        public void AddAsync_AddedClothes_ReturnedSameClothes()
        {
            var clothes = new Clothes { Name = "Clothes1", Price = 1000 };
            var AddedClothes = _clothesService.AddAsync(clothes).Result;

            Assert.Equal(clothes, AddedClothes);
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
            var clothes = _clothesService.FindAsync(new ClothesDto { Id = 1}).Result;

            _clothesService.DeleteAsync(clothes);

            var notDeleted = _clothesService.IsExistAsync(new ClothesDto { Id = 1 }).Result;
            Assert.False(notDeleted);
        }

        [Fact]
        public void FindAsync_FindClothes_ClothesHasId1()
        {
            int ID = 1;
            var clothes = _clothesService.FindAsync(new ClothesDto { Id = 1 }).Result;
            long clothesID = clothes.Id;

            Assert.Equal(clothesID, ID);
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
