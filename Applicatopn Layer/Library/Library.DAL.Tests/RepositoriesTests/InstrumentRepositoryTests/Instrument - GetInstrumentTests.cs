using Library.DAL.Entities;
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

            var instru = new InstrumentTO { Name = "Saxophone" };
            var instru2 = new InstrumentTO { Name = "Trumpet" };
            var instru3 = new InstrumentTO { Name = "Flute" };
            var AddedInstru = instrumentRepository.Add(instru);
            var AddedInstru2 = instrumentRepository.Add(instru2);
            var AddedInstru3 = instrumentRepository.Add(instru3);
            context.SaveChanges();

            //Act
            var result = instrumentRepository.GetAll();
            //Assert
            Assert.AreEqual(3, result.Count());
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

            var instru = new InstrumentTO { Name = "Saxophone" };
            var instru2 = new InstrumentTO { Name = "Trumpet" };
            var instru3 = new InstrumentTO { Name = "Flute" };
            var AddedInstru = instrumentRepository.Add(instru);
            var AddedInstru2 = instrumentRepository.Add(instru2);
            var AddedInstru3 = instrumentRepository.Add(instru3);
            context.SaveChanges();

            //Act
            var result = instrumentRepository.GetById(1);
            var result2 = instrumentRepository.GetById(2);
            var result3 = instrumentRepository.GetById(3);

            //Assert
            Assert.AreEqual("Saxophone", result.Name);
            Assert.AreEqual("Trumpet", result2.Name);
            Assert.AreEqual("Flute", result3.Name);
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
