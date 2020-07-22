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

namespace Library.DAL.Tests
{
    [TestClass]
    public class CategoryRepositoryTests
    {
        #region Add
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
        {
            //Arrange
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
            var category2 = new CategoryTO { Name = "Musique de films", Id = 1 };
            var result2 = categoryRepository.Add(category2);
            context.SaveChanges();

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, categoryRepository.GetAll().Count());
        }
        #endregion Tests

        #region Delete
        [TestMethod]
        public void DeleteCategory_ProvidingNull_ThrowException()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<LibraryContext>()
                 .UseInMemoryDatabase(databaseName: MethodBase.GetCurrentMethod().Name)
                 .Options;
            using var context = new LibraryContext(options);
            ICategoryRepository categoryRepository = new CategoryRepository(context);

            var category = new CategoryTO { Name = "Musique de films" };

            //Act & Assert
            Assert.ThrowsException<ArgumentException>(() => categoryRepository.Delete(category));
        }

        [TestMethod]
        public void DeleteCategory_Successful()
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
            var result = categoryRepository.Delete(AddedCategory);
            context.SaveChanges();
            //Assert
            Assert.AreEqual(2, categoryRepository.GetAll().Count());

        }
        #endregion

        #region GetById
        [TestMethod]
        public void GetCategoryById_Successful()
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
            var result = categoryRepository.GetById(1);
            var result2 = categoryRepository.GetById(2);
            var result3 = categoryRepository.GetById(3);

            //Assert
            Assert.AreEqual("Musique de films", result.Name);
            Assert.AreEqual("Musique Classique", result2.Name);
            Assert.AreEqual("Musique Contemporaine", result3.Name);
        }

        [TestMethod]
        public void GetCategoryById_ProvidingNonExistingId_ThrowException()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<LibraryContext>()
                 .UseInMemoryDatabase(databaseName: MethodBase.GetCurrentMethod().Name)
                 .Options;
            using var context = new LibraryContext(options);
            ICategoryRepository categoryRepository = new CategoryRepository(context);

            //Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => categoryRepository.GetById(14));
        }
        #endregion

        #region GetAll
        [TestMethod]
        public void GetAllCategories_Successful()
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
            var result = categoryRepository.GetAll();
            //Assert
            Assert.AreEqual(3, result.Count());
        }
        #endregion

        #region Update
        [TestMethod]
        public void UpdateCategory_Successful()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<LibraryContext>()
                 .UseInMemoryDatabase(databaseName: MethodBase.GetCurrentMethod().Name)
                 .Options;
            using var context = new LibraryContext(options);
            ICategoryRepository categoryRepository = new CategoryRepository(context);

            var category = new CategoryTO { Name = "Musiq de flims" };
            var category2 = new CategoryTO { Name = "Musique Classique" };
            var category3 = new CategoryTO { Name = "Musique Contemporaine" };
            var AddedCategory = categoryRepository.Add(category);
            var AddedCategory2 = categoryRepository.Add(category2);
            var AddedCategory3 = categoryRepository.Add(category3);
            context.SaveChanges();

            //Act
            AddedCategory.Name = "Musique de films";
            var test = categoryRepository.Update(AddedCategory);
            context.SaveChanges();

            //Assert
            Assert.AreEqual(3, categoryRepository.GetAll().Count());
            Assert.AreEqual("Musique de films", test.Name);
        }
        [TestMethod]
        public void UpdateCategory_ProvidingNonExistingCategory_ThrowException()
        {
            var options = new DbContextOptionsBuilder<LibraryContext>()
                .UseInMemoryDatabase(databaseName: MethodBase.GetCurrentMethod().Name)
                .Options;
            using var context = new LibraryContext(options);
            ICategoryRepository categoryRepository = new CategoryRepository(context);

            var category = new CategoryTO { Name = "Musique de films" };

            //Act & Assert
            Assert.ThrowsException<ArgumentException>(() => categoryRepository.Update(category));

        }
        #endregion
    }
}
