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

            var instrument = new InstrumentEF { Name = "Saxophone" };

            //Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => context.Instruments.Remove(null));
        }

        [TestMethod]
        public void RemoveInstrumentByTransferObject_Successful()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<LibraryContext>()
                 .UseInMemoryDatabase(databaseName: MethodBase.GetCurrentMethod().Name)
                 .Options;
            using var context = new LibraryContext(options);

            var instru = new InstrumentEF { Name = "Saxophone" };
            var instru2 = new InstrumentEF { Name = "Trumpet" };
            var instru3 = new InstrumentEF { Name = "Flute" };
            var AddedInstru = context.Instruments.Add(instru);
            var AddedInstru2 = context.Instruments.Add(instru2);
            var AddedInstru3 = context.Instruments.Add(instru3);
            context.SaveChanges();

            //Act
            var result = context.Instruments.Remove(instru);
            context.SaveChanges();
            //Assert
            Assert.AreEqual(2, context.Instruments.Count());
        }

        [TestMethod]
        public void RemoveInstrumentById_ProvidingNonExistingId_ThrowException()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<LibraryContext>()
                 .UseInMemoryDatabase(databaseName: MethodBase.GetCurrentMethod().Name)
                 .Options;
            using var context = new LibraryContext(options);

            var instru = new InstrumentTO { Name = "Saxophone" };
            context.SaveChanges();

            var entity = context.Instruments.Find(14);

            //Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => context.Instruments.Remove(entity));
        }

        [TestMethod]
        public void RemoveInstrumentById_ProvidingNullId_ThrowException()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<LibraryContext>()
                 .UseInMemoryDatabase(databaseName: MethodBase.GetCurrentMethod().Name)
                 .Options;
            using var context = new LibraryContext(options);

            var instru = new InstrumentTO { Name = "Saxophone" };
            context.SaveChanges();

            //Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => context.Instruments.Remove(null));
        }

        [TestMethod]
        public void RemoveInstrumentById_Successful()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<LibraryContext>()
                 .UseInMemoryDatabase(databaseName: MethodBase.GetCurrentMethod().Name)
                 .Options;
            using var context = new LibraryContext(options);

            var instru = new InstrumentEF { Name = "Saxophone" };
            var instru2 = new InstrumentEF { Name = "Trumpet" };
            var instru3 = new InstrumentEF { Name = "Flute" };
            var AddedInstru = context.Add(instru);
            var AddedInstru2 = context.Add(instru2);
            var AddedInstru3 = context.Add(instru3);
            context.SaveChanges();

            var entity = context.Instruments.Find(1);

            //Act
            var result = context.Instruments.Remove(entity);
            context.SaveChanges();
            //Assert
            Assert.AreEqual(2, context.Instruments.Count());
        }
    }
}
