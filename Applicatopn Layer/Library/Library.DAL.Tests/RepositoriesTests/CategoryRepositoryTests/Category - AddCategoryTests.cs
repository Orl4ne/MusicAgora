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
    public class AddCategoryTests
    {
        [TestMethod]
        public void AddCategory_Successful()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<LibraryContext>()
                 .UseInMemoryDatabase(databaseName: MethodBase.GetCurrentMethod().Name)
                 .Options;
            using var context = new LibraryContext(options);
            ICategoryRepository categoryRepository = new CategoryRepository(context);

            //Act
            var category = new CategoryTO { Name = "Musique de films" };
            var result = categoryRepository.Add(category);
            context.SaveChanges();

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Name, "Musique de films");
        }


        [TestMethod]
        public void AddCategory_AddNull_ThrowException()
        {   //Arrange
            var options = new DbContextOptionsBuilder<LibraryContext>()
                 .UseInMemoryDatabase(databaseName: MethodBase.GetCurrentMethod().Name)
                 .Options;
            using var context = new LibraryContext(options);
            ICategoryRepository categoryRepository = new CategoryRepository(context);

            //Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => categoryRepository.Add(null));
        }

        [TestMethod]
        public void AddCategory_AddExistingCategory_DoNotInsertTwiceInDb()
        {//Arrange
            var options = new DbContextOptionsBuilder<LibraryContext>()
                 .UseInMemoryDatabase(databaseName: MethodBase.GetCurrentMethod().Name)
                 .Options;
            using var context = new LibraryContext(options);
            ICategoryRepository categoryRepository = new CategoryRepository(context);

            //Act
            var category = new CategoryTO { Name = "Musique de films" };
            var result = categoryRepository.Add(category);
            var category2 = new CategoryTO { Name = "Musique de films", Id=1 };
            var result2 = categoryRepository.Add(category2);
            context.SaveChanges();

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(categoryRepository.GetAll().Count(), 1);
            Assert.AreEqual(result.Name, "Musique de films");

        }
    }
}
