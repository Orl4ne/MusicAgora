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
    public class UpdateCategoryTests
    {
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
    }
}
