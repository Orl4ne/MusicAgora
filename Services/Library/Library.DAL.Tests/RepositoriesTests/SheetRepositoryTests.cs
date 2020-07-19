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

namespace Library.DAL.Tests.RepositoriesTests
{
    [TestClass]
    public class SheetRepositoryTests
    {
        #region Add
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

            var sheet = new SheetTO { Name = "BestOf", Arranger = "Jean-Luc", Category = addedCategory, Composer = "Morricone", IsCurrent = false, IsGarde = false, IsIndependance = true };
            var sheet2 = new SheetTO { Name = "Youg Amadeus", Arranger = "Jan de Haan", Category = addedCategory2, Composer = "Mozart", IsCurrent = true, IsGarde = false, IsIndependance = true };
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

            var sheet = new SheetTO { Name = "BestOf", Arranger = "Jean-Luc", Category = addedCategory, Composer = "Morricone", IsCurrent = false, IsGarde = false, IsIndependance = true };
            var sheet2 = new SheetTO { Id = 1, Name = "BestOf", Arranger = "Jean-Luc", Category = addedCategory2, Composer = "Morricone", IsCurrent = false, IsGarde = false, IsIndependance = true };
            var addedSheet = sheetRepository.Add(sheet);
            var addedSheet2 = sheetRepository.Add(sheet2);
            context.SaveChanges();

            //Assert
            Assert.IsNotNull(sheet);
            Assert.AreEqual(1, sheetRepository.GetAll().Count());
        }
        #endregion

        #region Delete
        [TestMethod]
        public void DeleteSheet_ProvidingNull_ThrowException()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<LibraryContext>()
                 .UseInMemoryDatabase(databaseName: MethodBase.GetCurrentMethod().Name)
                 .Options;
            using var context = new LibraryContext(options);
            ISheetRepository sheetRepository = new SheetRepository(context);
            ICategoryRepository categoryRepository = new CategoryRepository(context);

            var category = new CategoryTO { Name = "Musique de films" };
            var addedCategory = categoryRepository.Add(category);
            var category2 = new CategoryTO { Name = "Musique de classique" };
            var addedCategory2 = categoryRepository.Add(category2);
            context.SaveChanges();

            var sheet = new SheetTO { Name = "BestOf", Arranger = "Jean-Luc", Category = addedCategory, Composer = "Morricone", IsCurrent = false, IsGarde = false, IsIndependance = true };
            var sheet2 = new SheetTO { Name = "Young Amadeus", Arranger = "Jan de Haan", Category = addedCategory2, Composer = "Mozart", IsCurrent = true, IsGarde = false, IsIndependance = true };
            //Act & Assert
            Assert.ThrowsException<ArgumentException>(() => sheetRepository.Delete(sheet));
        }

        [TestMethod]
        public void DeleteSheet_Successful()
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
            var result = sheetRepository.Delete(addedSheet);
            context.SaveChanges();
            //Assert
            Assert.AreEqual(1, sheetRepository.GetAll().Count());
        }
        #endregion

        #region GetById
        [TestMethod]
        public void GetSheetById_Successful()
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
            var result = sheetRepository.GetById(1);
            var result2 = sheetRepository.GetById(2);

            //Assert
            Assert.AreEqual("BestOf", result.Name);
            Assert.AreEqual("Young Amadeus", result2.Name);
            Assert.AreEqual(2, categoryRepository.GetAll().Count());
        }

        [TestMethod]
        public void GetSheetById_ProvidingNonExistingId_ThrowException()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<LibraryContext>()
                  .UseInMemoryDatabase(databaseName: MethodBase.GetCurrentMethod().Name)
                  .Options;
            using var context = new LibraryContext(options);
            ISheetRepository sheetRepository = new SheetRepository(context);

            //Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => sheetRepository.GetById(14));
        }
        #endregion

        #region GetAll
        [TestMethod]
        public void GetAllSheets_Successful()
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
            var result = categoryRepository.GetAll();
            //Assert
            Assert.AreEqual(2, result.Count());
        }
        #endregion

        #region Update
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
            addedSheet.Category = addedCategory2;
            var test = sheetRepository.Update(addedSheet);
            context.SaveChanges();

            //Assert
            Assert.AreEqual(2, sheetRepository.GetAll().Count());
            Assert.AreEqual("Musique de classique", test.Category.Name);
        }

        [TestMethod]
        public void UpdateSheet_ProvidingNonExistingSheet_ThrowException()
        {
            var options = new DbContextOptionsBuilder<LibraryContext>()
                 .UseInMemoryDatabase(databaseName: MethodBase.GetCurrentMethod().Name)
                 .Options;
            using var context = new LibraryContext(options);
            ISheetRepository sheetRepository = new SheetRepository(context);

            var sheet = new SheetTO { Name = "BestOf", Arranger = "Jean-Luc", Composer = "Morricone", IsCurrent = false, IsGarde = false, IsIndependance = true };

            //Act & Assert
            Assert.ThrowsException<ArgumentException>(() => sheetRepository.Update(sheet));
        }
        #endregion
    }
}
