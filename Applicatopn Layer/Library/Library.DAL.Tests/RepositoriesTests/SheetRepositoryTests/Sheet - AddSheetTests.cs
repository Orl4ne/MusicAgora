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
    public class AddSheetTests
    {
        [TestMethod]
        public void AddSheet_Successful()
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

            var sheet = new SheetTO {Name="BestOf", Arranger = "Jean-Luc", CategoryId=1, Composer="Morricone", IsCurrent=false, IsGarde=false, IsIndependance=true  };
            var sheet2 = new SheetTO {Name="Youg Amadeus", Arranger = "Jan de Haan", CategoryId = 2, Composer="Mozart", IsCurrent=true, IsGarde=false, IsIndependance=true  };
            var addedSheet = sheetRepository.Add(sheet);
            var addedSheet2 = sheetRepository.Add(sheet2);
            context.SaveChanges();

            //Assert
            Assert.IsNotNull(sheet);
            Assert.AreEqual(sheet.Name, "BestOf");
            Assert.AreEqual(2, sheetRepository.GetAll().Count());
            Assert.AreEqual(2, categoryRepository.GetAll().Count());
        }

        [TestMethod]
        public void AddSheet_AddNull_ThrowException()
        {   //Arrange
            var options = new DbContextOptionsBuilder<LibraryContext>()
                 .UseInMemoryDatabase(databaseName: MethodBase.GetCurrentMethod().Name)
                 .Options;
            using var context = new LibraryContext(options);
            ISheetRepository sheetRepository = new SheetRepository(context);

            //Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => sheetRepository.Add(null));
        }

        [TestMethod]
        public void AddSheet_AddExistingSheet_DoNotInsertTwiceInDb()
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

            var sheet = new SheetTO { Name = "BestOf", Arranger = "Jean-Luc", CategoryId = 1, Composer = "Morricone", IsCurrent = false, IsGarde = false, IsIndependance = true };
            var sheet2 = new SheetTO { Id = 1,  Name = "BestOf", Arranger = "Jean-Luc", CategoryId = 2, Composer = "Morricone", IsCurrent = false, IsGarde = false, IsIndependance = true };
            var addedSheet = sheetRepository.Add(sheet);
            var addedSheet2 = sheetRepository.Add(sheet2);
            context.SaveChanges();

            //Assert
            Assert.IsNotNull(sheet);
            Assert.AreEqual(1, sheetRepository.GetAll().Count());
        }
    }
}
