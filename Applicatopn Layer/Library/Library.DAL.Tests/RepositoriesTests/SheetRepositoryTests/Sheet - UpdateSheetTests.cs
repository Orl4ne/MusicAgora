using Library.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MusicAgora.Common.Library.Interfaces.IRepositories;
using MusicAgora.Common.Library.TransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Library.DAL.Tests.RepositoriesTests.SheetRepositoryTests
{
    [TestClass]
    public class UpdateSheetTests
    {
        [TestMethod]
        public void UpdateSheet_Successful()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<LibraryContext>()
                 .UseInMemoryDatabase(databaseName: MethodBase.GetCurrentMethod().Name)
                 .Options;
            using var context = new LibraryContext(options);
            ISheetRepository sheetRepository = new SheetRepository(context);
            ICategoryRepository categoryRepository = new CategoryRepository(context);

            //Act
            var category = new CategoryTO { Name = "Musique de films" };
            var addedCategory = categoryRepository.Add(category);
            var category2 = new CategoryTO { Name = "Musique de classique" };
            var addedCategory2 = categoryRepository.Add(category2);
            context.SaveChanges();

            var sheet = new SheetTO { Name = "BestOf", Arranger = "Jean-Luc", Category = addedCategory, Composer = "Morricone", IsCurrent = false, IsGarde = false, IsIndependance = true };
            var sheet2 = new SheetTO { Name = "Young Amadeus", Arranger = "Jan de Haan", Category = addedCategory2, Composer = "Mozart", IsCurrent = true, IsGarde = false, IsIndependance = true };
            var addedSheet = sheetRepository.Add(sheet);
            var addedSheet2 = sheetRepository.Add(sheet2);
            context.SaveChanges();

            //Act
            addedSheet.Name = "Old Mozart";
            var test = sheetRepository.Update(addedSheet);
            context.SaveChanges();

            //Assert
            Assert.AreEqual(2, sheetRepository.GetAll().Count());
            Assert.AreEqual("Old Mozart", test.Name);
        }
        [TestMethod]
        public void UpdateSheet_ProvidingNonExistingSheet_ThrowException()
        {
            var options = new DbContextOptionsBuilder<LibraryContext>()
                 .UseInMemoryDatabase(databaseName: MethodBase.GetCurrentMethod().Name)
                 .Options;
            using var context = new LibraryContext(options);
            ISheetRepository sheetRepository = new SheetRepository(context);

            //var category = new CategoryTO { Name = "Musique de films" };

            ////Act & Assert
            //Assert.ThrowsException<ArgumentException>(() => categoryRepository.Update(category));

        }
    }
}
