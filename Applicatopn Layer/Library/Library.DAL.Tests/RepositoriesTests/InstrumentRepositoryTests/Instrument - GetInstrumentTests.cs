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
    public class GetInstrumentTests
    {
        [TestMethod]
        public void GetAllInstruments_Successful()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<LibraryContext>()
                 .UseInMemoryDatabase(databaseName: MethodBase.GetCurrentMethod().Name)
                 .Options;
            using var context = new LibraryContext(options);
            IInstrumentRepository instrumentRepository = new InstrumentRepository(context);

            //var category = new CategoryTO { Name = "Musique de films" };
            //var category2 = new CategoryTO { Name = "Musique Classique" };
            //var category3 = new CategoryTO { Name = "Musique Contemporaine" };
            //var AddedCategory = categoryRepository.Add(category);
            //var AddedCategory2 = categoryRepository.Add(category2);
            //var AddedCategory3 = categoryRepository.Add(category3);
            //context.SaveChanges();

            ////Act
            //var result = categoryRepository.GetAll();
            ////Assert
            //Assert.AreEqual(3, result.Count());
        }

        [TestMethod]
        public void GetInstrumentById_Successful()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<LibraryContext>()
                 .UseInMemoryDatabase(databaseName: MethodBase.GetCurrentMethod().Name)
                 .Options;
            using var context = new LibraryContext(options);
            IInstrumentRepository instrumentRepository = new InstrumentRepository(context);

            //var category = new CategoryTO { Name = "Musique de films" };
            //var category2 = new CategoryTO { Name = "Musique Classique" };
            //var category3 = new CategoryTO { Name = "Musique Contemporaine" };
            //var AddedCategory = categoryRepository.Add(category);
            //var AddedCategory2 = categoryRepository.Add(category2);
            //var AddedCategory3 = categoryRepository.Add(category3);
            //context.SaveChanges();

            ////Act
            //var result = categoryRepository.GetById(1);
            //var result2 = categoryRepository.GetById(2);
            //var result3 = categoryRepository.GetById(3);

            ////Assert
            //Assert.AreEqual("Musique de films", result.Name);
            //Assert.AreEqual("Musique Classique", result2.Name);
            //Assert.AreEqual("Musique Contemporaine", result3.Name);
        }

        [TestMethod]
        public void GetInstrumentById_ProvidingNonExistingId_ThrowException()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<LibraryContext>()
                 .UseInMemoryDatabase(databaseName: MethodBase.GetCurrentMethod().Name)
                 .Options;
            using var context = new LibraryContext(options);
            IInstrumentRepository instrumentRepository = new InstrumentRepository(context);

            //Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => instrumentRepository.GetById(14));
        }
    }
}
