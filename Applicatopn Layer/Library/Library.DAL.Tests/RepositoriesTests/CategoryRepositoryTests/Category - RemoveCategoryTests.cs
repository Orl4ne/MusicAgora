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

namespace Library.DAL.Tests.RepositoriesTests.CategoryRepositoryTests
{
    [TestClass]
    public class RemoveCategoryTests
    {
        [TestMethod]
        public void RemoveCategoryByTransferObject_ProvidingNonExistingCategory_ThrowException()
        {

        }

        [TestMethod]
        public void RemoveCategoryByTransferObject_ProvidingNull_ThrowException()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<LibraryContext>()
                 .UseInMemoryDatabase(databaseName: MethodBase.GetCurrentMethod().Name)
                 .Options;
            using var context = new LibraryContext(options);
            ICategoryRepository categoryRepository = new CategoryRepository(context);

            var category = new CategoryTO { Name = "Musique de films" };
            context.SaveChanges();

            //Act & Assert
            Assert.ThrowsException<KeyNotFoundException>(() => categoryRepository.Remove(category));
        }

        [TestMethod]
        public void RemoveCategoryByTransferObject_Successful()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<LibraryContext>()
                 .UseInMemoryDatabase(databaseName: MethodBase.GetCurrentMethod().Name)
                 .Options;
            using var context = new LibraryContext(options);
            ICategoryRepository categoryRepository = new CategoryRepository(context);

            var category = new CategoryTO { Name = "Musique de films" };
            var category2 = new CategoryTO { Name = "Musique Classique" };
            var category3 = new CategoryTO { Name = "Musique Contemporaine" };
            var AddedCategory = categoryRepository.Add(category);
            var AddedCategory2 = categoryRepository.Add(category2);
            var AddedCategory3 = categoryRepository.Add(category3);
            context.SaveChanges();

            //Act
            var result = categoryRepository.Remove(AddedCategory);
            context.SaveChanges();
            //Assert
            Assert.AreEqual(2, categoryRepository.GetAll().Count());

        }

        [TestMethod]
        public void RemoveCategoryById_ProvidingNonExistingId_ThrowException()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<LibraryContext>()
                 .UseInMemoryDatabase(databaseName: MethodBase.GetCurrentMethod().Name)
                 .Options;
            using var context = new LibraryContext(options);
            ICategoryRepository categoryRepository = new CategoryRepository(context);

            var category = new CategoryTO { Name = "Musique de films" };
            context.SaveChanges();

            //Act & Assert
            Assert.ThrowsException<KeyNotFoundException>(() => categoryRepository.Remove(14));

        }

        [TestMethod]
        public void RemoveCategoryById_ProvidingNullId_ThrowException()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<LibraryContext>()
                 .UseInMemoryDatabase(databaseName: MethodBase.GetCurrentMethod().Name)
                 .Options;
            using var context = new LibraryContext(options);
            ICategoryRepository categoryRepository = new CategoryRepository(context);

            var category = new CategoryTO { Name = "Musique de films" };
            context.SaveChanges();

            //Act & Assert
            Assert.ThrowsException<NullReferenceException>(() => categoryRepository.Remove(null));

        }


        [TestMethod]
        public void RemoveCategoryById_Successful()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<LibraryContext>()
                 .UseInMemoryDatabase(databaseName: MethodBase.GetCurrentMethod().Name)
                 .Options;
            using var context = new LibraryContext(options);
            ICategoryRepository categoryRepository = new CategoryRepository(context);

            var category = new CategoryTO { Name = "Musique de films" };
            var category2 = new CategoryTO { Name = "Musique Classique" };
            var category3 = new CategoryTO { Name = "Musique Contemporaine" };
            var AddedCategory = categoryRepository.Add(category);
            var AddedCategory2 = categoryRepository.Add(category2);
            var AddedCategory3 = categoryRepository.Add(category3);
            context.SaveChanges();

            //Act
            var result = categoryRepository.Remove(1);
            context.SaveChanges();
            //Assert
            Assert.AreEqual(2, categoryRepository.GetAll().Count());
        }
    }
}
