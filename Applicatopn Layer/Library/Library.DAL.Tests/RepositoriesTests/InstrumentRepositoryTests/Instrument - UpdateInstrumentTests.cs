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

            var instru = new InstrumentEF { Name = "Saxophone" };
            var instru2 = new InstrumentEF { Name = "Trumpet" };
            var instru3 = new InstrumentEF { Name = "Flute" };
            var AddedInstru = context.Add(instru);
            var AddedInstru2 = context.Add(instru2);
            var AddedInstru3 = context.Add(instru3);
            context.SaveChanges();

            //Act
            instru.Name = "PouetPouet";
            var entityToUpdate = context.Instruments.Find(1);
            entityToUpdate.Name = "PouetPouet";
            context.Instruments.Update(entityToUpdate);
            //var test = context.Update(AddedInstru);
            context.SaveChanges();

            var result = context.Instruments.Find(1);

            //Assert
            Assert.AreEqual(3, context.Instruments.Count());
            Assert.AreEqual("PouetPouet", result.Name);
        }
        
        [TestMethod]
        public void UpdateInstrument_ProvidingNonExistingInstrument_ThrowException()
        {
            var options = new DbContextOptionsBuilder<LibraryContext>()
                .UseInMemoryDatabase(databaseName: MethodBase.GetCurrentMethod().Name)
                .Options;
            using var context = new LibraryContext(options);

            var instru = new InstrumentEF { Name = "Saxophone" };

            //Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => context.Update(null));
        }
    }
}
