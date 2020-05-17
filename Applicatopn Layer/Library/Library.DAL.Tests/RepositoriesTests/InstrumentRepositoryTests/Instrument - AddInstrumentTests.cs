using Library.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MusicAgora.Common.Library.Interfaces.IRepositories;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Library.DAL.Tests.RepositoriesTests.InstrumentRepositoryTests
{
    [TestClass]
    public class AddInstrumentTests
    {
        [TestMethod]
        public void AddInstrument_Successful()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<LibraryContext>()
                 .UseInMemoryDatabase(databaseName: MethodBase.GetCurrentMethod().Name)
                 .Options;
            using var context = new LibraryContext(options);
            IInstrumentRepository instrumentRepository = new InstrumentRepository(context);

            ////Act
            //var category = new CategoryTO { Name = "Musique de films" };
            //var result = categoryRepository.Add(category);
            //context.SaveChanges();

            ////Assert
            //Assert.IsNotNull(result);
            //Assert.AreEqual(result.Name, "Musique de films");
        }

        [TestMethod]
        public void AddInstrument_AddNull_ThrowException()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<LibraryContext>()
                 .UseInMemoryDatabase(databaseName: MethodBase.GetCurrentMethod().Name)
                 .Options;
            using var context = new LibraryContext(options);
            IInstrumentRepository instrumentRepository = new InstrumentRepository(context);

            //Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => instrumentRepository.Add(null));
        }

        [TestMethod]
        public void AddInstrument_AddExistingCategory_DoNotInsertTwiceInDb()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<LibraryContext>()
                 .UseInMemoryDatabase(databaseName: MethodBase.GetCurrentMethod().Name)
                 .Options;
            using var context = new LibraryContext(options);
            IInstrumentRepository instrumentRepository = new InstrumentRepository(context);

            ////Act
            //var category = new CategoryTO { Name = "Musique de films" };
            //var result = categoryRepository.Add(category);
            //var category2 = new CategoryTO { Name = "Musique de films", Id = 1 };
            //var result2 = categoryRepository.Add(category2);
            //context.SaveChanges();

            ////Assert
            //Assert.IsNotNull(result);
            //Assert.AreEqual(1, categoryRepository.GetAll().Count());
        }
    }
}
