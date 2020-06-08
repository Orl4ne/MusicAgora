using Library.DAL.Entities;
using Library.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Library.DAL.Tests.RepositoriesTests.InstrumentRepositoryTests
{
    [TestClass]
    public class AddInstrumentTests
    {
        // Warning, Using Identity, I can't make a repositoryPattern for the Users. As Users and Instruments are connected via UserInstrument 
        // I use directly EntityFramework without repository pattern for Instruments AND Users.
        [TestMethod]
        public void AddInstrument_Successful()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<LibraryContext>()
                 .UseInMemoryDatabase(databaseName: MethodBase.GetCurrentMethod().Name)
                 .Options;
            using var context = new LibraryContext(options);

            //Act
            var instru = new InstrumentEF { Name = "Saxophone", UserInstruments= new List<UserInstrumentEF>()};
            var result = context.Add(instru);
            context.SaveChanges();

            var user = new LibraryUserEF { FirstName = "Jean-Claude", IsIndependance = true, 
                UserInstruments=new List<UserInstrumentEF>() { 
                    new UserInstrumentEF {
                    InstrumentId = 1} } };
            var addedUser = context.Add(user);
            context.SaveChanges();

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Entity.Name, "Saxophone");
            Assert.AreEqual(user.UserInstruments.First().Instrument.Name, "Saxophone");
        }

        [TestMethod]
        public void AddInstrument_AddNull_ThrowException()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<LibraryContext>()
                 .UseInMemoryDatabase(databaseName: MethodBase.GetCurrentMethod().Name)
                 .Options;
            using var context = new LibraryContext(options);

            //Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => context.Add(null));
        }

        [TestMethod]
        public void AddInstrument_AddExistingInstrument_DoNotInsertTwiceInDb()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<LibraryContext>()
                 .UseInMemoryDatabase(databaseName: MethodBase.GetCurrentMethod().Name)
                 .Options;
            using var context = new LibraryContext(options);

            //Act
            var instru = new InstrumentEF { Name = "Saxophone" };
            var result = context.Add(instru);
            var instru2 = new InstrumentEF { Name = "Saxophone", Id=1 };
            //var result2 = context.Add(instru2);
            context.SaveChanges();

            //Assert
            Assert.ThrowsException<InvalidOperationException>(() => context.Add(instru2));
            Assert.IsNotNull(result);
            Assert.AreEqual(1, context.Instruments.Count());
        }
    }
}
