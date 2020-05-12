﻿using Library.DAL.Repositories;
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
    public class RemoveSheetTests
    {
        [TestMethod]
        public void RemoveSheetByTransferObject_ProvidingNull_ThrowException()
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
            Assert.ThrowsException<KeyNotFoundException>(() => sheetRepository.Remove(sheet));
        }

        [TestMethod]
        public void RemoveSheetByTransferObject_Successful()
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
            var result = sheetRepository.Remove(addedSheet);
            context.SaveChanges();
            //Assert
            Assert.AreEqual(1, sheetRepository.GetAll().Count());
        }

        [TestMethod]
        public void RemoveSheetById_ProvidingNonExistingId_ThrowException()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<LibraryContext>()
                  .UseInMemoryDatabase(databaseName: MethodBase.GetCurrentMethod().Name)
                  .Options;
            using var context = new LibraryContext(options);
            ISheetRepository sheetRepository = new SheetRepository(context);

            //Act & Assert
            Assert.ThrowsException<KeyNotFoundException>(() => sheetRepository.Remove(14));

        }

        [TestMethod]
        public void RemoveSheetById_ProvidingNullId_ThrowException()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<LibraryContext>()
                 .UseInMemoryDatabase(databaseName: MethodBase.GetCurrentMethod().Name)
                 .Options;
            using var context = new LibraryContext(options);
            ISheetRepository sheetRepository = new SheetRepository(context);

            //Act & Assert
            Assert.ThrowsException<NullReferenceException>(() => sheetRepository.Remove(null));
        }

        [TestMethod]
        public void RemoveSheetById_Successful()
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
            var result = sheetRepository.Remove(1);
            context.SaveChanges();
            //Assert
            Assert.AreEqual(1, sheetRepository.GetAll().Count());
        }
    }
}
