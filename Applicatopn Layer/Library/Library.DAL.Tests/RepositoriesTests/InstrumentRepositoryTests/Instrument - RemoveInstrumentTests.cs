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
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Library.DAL.Tests.RepositoriesTests.InstrumentRepositoryTests
{
    [TestClass]
    public class RemoveInstrumentTests
    {
        [TestMethod]
        public void RemoveInstrumentByTransferObject_ProvidingNull_ThrowException()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<LibraryContext>()
                 .UseInMemoryDatabase(databaseName: MethodBase.GetCurrentMethod().Name)
                 .Options;
            using var context = new LibraryContext(options);
            IInstrumentRepository instrument = new InstrumentRepository(context);
            IInstrumentRepository instrumentRepository = new InstrumentRepository(context);

            var category = new InstrumentTO { Name = "Saxophone" };
            context.SaveChanges();

            //Act & Assert
            Assert.ThrowsException<KeyNotFoundException>(() => instrumentRepository.Remove(category));
        }

        [TestMethod]
        public void RemoveInstrumentByTransferObject_Successful()
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
            var result = instrumentRepository.Remove(AddedInstru);
            context.SaveChanges();
            //Assert
            Assert.AreEqual(2, instrumentRepository.GetAll().Count());
        }

        [TestMethod]
        public void RemoveInstrumentById_ProvidingNonExistingId_ThrowException()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<LibraryContext>()
                 .UseInMemoryDatabase(databaseName: MethodBase.GetCurrentMethod().Name)
                 .Options;
            using var context = new LibraryContext(options);
            IInstrumentRepository instrumentRepository = new InstrumentRepository(context);

            var instru = new InstrumentTO { Name = "Saxophone" };
            context.SaveChanges();

            //Act & Assert
            Assert.ThrowsException<KeyNotFoundException>(() => instrumentRepository.Remove(14));
        }

        [TestMethod]
        public void RemoveInstrumentById_ProvidingNullId_ThrowException()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<LibraryContext>()
                 .UseInMemoryDatabase(databaseName: MethodBase.GetCurrentMethod().Name)
                 .Options;
            using var context = new LibraryContext(options);
            IInstrumentRepository instrumentRepository = new InstrumentRepository(context);

            var instru = new InstrumentTO { Name = "Saxophone" };
            context.SaveChanges();

            //Act & Assert
            Assert.ThrowsException<NullReferenceException>(() => instrumentRepository.Remove(null));
        }

        [TestMethod]
        public void RemoveInstrumentById_Successful()
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
            var result = instrumentRepository.Remove(1);
            context.SaveChanges();
            //Assert
            Assert.AreEqual(2, instrumentRepository.GetAll().Count());
        }
    }
}
