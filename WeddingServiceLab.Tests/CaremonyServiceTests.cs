using Microsoft.EntityFrameworkCore;
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

        public CaremonyServiceTests(ICeremoniesService ceremoniesService)
        {
            _ceremoniesService = ceremoniesService;
        }

        [Fact]
        public void AddAsync_AddedCeremony_ReturnedSameCeremony()
        {
            var ceremony = new Ceremonies() { Name = "Ceremony1", Price = 100 };
            var AddedCeremony = _ceremoniesService.AddAsync(ceremony).Result;

            Assert.Equal(ceremony, AddedCeremony);
        }

        [Fact]
        public void UpdateAsync_UpdateCeremony_UpdatedCeremonyInDB()
        {
            var ceremony = _ceremoniesService.FindAsync(new CeremoniesDto { Id = 1 }).Result;

            ceremony.Name = "UpdatedCeremony";

            _ceremoniesService.UpdateAsync(ceremony);

            var newCeremony = _ceremoniesService.FindAsync(new CeremoniesDto { Id = 1 }).Result;

            Assert.Equal(ceremony, newCeremony);
        }

        [Fact]
        public void DeleteAsync_DeleteCeremony_DeletedCeremonyInDB()
        {
            var ceremony = _ceremoniesService.FindAsync(new CeremoniesDto { Id = 1 }).Result;

            _ceremoniesService.DeleteAsync(ceremony);

            var notDeleted = _ceremoniesService.IsExistAsync(new CeremoniesDto { Id = 1 }).Result;
            Assert.False(notDeleted);
        }

        [Fact]
        public void FindAsync_FindCeremony_CeremonyHasId1()
        {
            int ID = 1;
            var ceremony = _ceremoniesService.FindAsync(new CeremoniesDto { Id = 1 }).Result;
            long ceremonyID = ceremony.Id; 
            Assert.Equal(ceremonyID, ID);
        }

        [Fact]
        public void IsExistAsync_CheckCeremony_ExistCeremonyId1()
        {
            var exist = _ceremoniesService.IsExistAsync(new CeremoniesDto { Id = 1 }).Result;

            Assert.True(exist);
        }

        [Fact]
        public void GetAsync_GetList_NotEmptyList()
        {
            var list = _ceremoniesService.GetAsync().Result;

            Assert.NotEmpty(list);
        }
    }
}
