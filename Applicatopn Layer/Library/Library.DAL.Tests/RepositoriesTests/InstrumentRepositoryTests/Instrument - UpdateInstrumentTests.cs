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
    public class UpdateInstrumentTests
    {
        [TestMethod]
        public void UpdateInstrument_Successful()
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
            AddedInstru.Name = "PouetPouet";
            var test = instrumentRepository.Update(AddedInstru);
            context.SaveChanges();

            //Assert
            Assert.AreEqual(3, instrumentRepository.GetAll().Count());
            Assert.AreEqual("PouetPouet", test.Name);
        }

        [TestMethod]
        public void UpdateInstrument_ProvidingNonExistingCategory_ThrowException()
        {
            var options = new DbContextOptionsBuilder<LibraryContext>()
                .UseInMemoryDatabase(databaseName: MethodBase.GetCurrentMethod().Name)
                .Options;
            using var context = new LibraryContext(options);
            IInstrumentRepository instrumentRepository = new InstrumentRepository(context);

            var instru = new InstrumentTO { Name = "Saxophone" };

            //Act & Assert
            Assert.ThrowsException<ArgumentException>(() => instrumentRepository.Update(instru));
        }
    }
}
