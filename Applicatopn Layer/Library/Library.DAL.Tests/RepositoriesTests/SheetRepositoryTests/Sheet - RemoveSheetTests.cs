using Library.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MusicAgora.Common.Library.Interfaces.IRepositories;
using System;
using System.Collections.Generic;
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

            //var category = new SheetTO { Name = "Musique de films" };
            //context.SaveChanges();

            ////Act & Assert
            //Assert.ThrowsException<KeyNotFoundException>(() => categoryRepository.Remove(category));
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

            //var category = new SheetTO { Name = "Musique de films" };
            //var category2 = new SheetTO { Name = "Musique Classique" };
            //var category3 = new SheetTO { Name = "Musique Contemporaine" };
            //var AddedSheet = categoryRepository.Add(category);
            //var AddedSheet2 = categoryRepository.Add(category2);
            //var AddedSheet3 = categoryRepository.Add(category3);
            //context.SaveChanges();

            ////Act
            //var result = categoryRepository.Remove(AddedSheet);
            //context.SaveChanges();
            ////Assert
            //Assert.AreEqual(2, categoryRepository.GetAll().Count());

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

            //var category = new SheetTO { Name = "Musique de films" };
            //context.SaveChanges();

            ////Act & Assert
            //Assert.ThrowsException<KeyNotFoundException>(() => categoryRepository.Remove(14));

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

            //var category = new SheetTO { Name = "Musique de films" };
            //context.SaveChanges();

            ////Act & Assert
            //Assert.ThrowsException<NullReferenceException>(() => categoryRepository.Remove(null));

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

            //var category = new SheetTO { Name = "Musique de films" };
            //var category2 = new SheetTO { Name = "Musique Classique" };
            //var category3 = new SheetTO { Name = "Musique Contemporaine" };
            //var AddedSheet = categoryRepository.Add(category);
            //var AddedSheet2 = categoryRepository.Add(category2);
            //var AddedSheet3 = categoryRepository.Add(category3);
            //context.SaveChanges();

            ////Act
            //var result = categoryRepository.Remove(1);
            //context.SaveChanges();
            ////Assert
            //Assert.AreEqual(2, categoryRepository.GetAll().Count());
        }
    }
}
