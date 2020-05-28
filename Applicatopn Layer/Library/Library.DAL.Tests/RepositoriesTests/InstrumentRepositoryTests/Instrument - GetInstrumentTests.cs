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

            var instru = new InstrumentEF { Name = "Saxophone" };
            var instru2 = new InstrumentEF { Name = "Trumpet" };
            var instru3 = new InstrumentEF { Name = "Flute" };
            var AddedInstru = context.Add(instru);
            var AddedInstru2 = context.Add(instru2);
            var AddedInstru3 = context.Add(instru3);
            context.SaveChanges();

            //Act
            var result = context.Instruments.Count();
            //Assert
            Assert.AreEqual(3, result);
        }

        [TestMethod]
        public void GetInstrumentById_Successful()
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

            //Act
            var result = context.Instruments.Find(1);
            var result2 = context.Instruments.Find(2);
            var result3 = context.Instruments.Find(3);

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

            //Act & Assert
            Assert.ThrowsException<ArgumentException>(() => context.Instruments.Find("huigyvi"));
        }
    }
}
