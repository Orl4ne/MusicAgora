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

namespace Library.DAL.Tests
{
    [TestClass]
    public class InstrumentRepositoryTests
    {
        #region Add
        [TestMethod]
        public void AddInstrument_Successful()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<LibraryContext>()
                 .UseInMemoryDatabase(databaseName: MethodBase.GetCurrentMethod().Name)
                 .Options;
            using var context = new LibraryContext(options);
            IInstrumentRepository instrumentRepository = new InstrumentRepository(context);

            //Act
            var instru = new InstrumentTO { Name = "Saxophone" };
            var result = instrumentRepository.Add(instru);
            context.SaveChanges();

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Name, "Saxophone");
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
        public void AddInstrument_AddExistingInstrument_DoNotInsertTwiceInDb()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<LibraryContext>()
                 .UseInMemoryDatabase(databaseName: MethodBase.GetCurrentMethod().Name)
                 .Options;
            using var context = new LibraryContext(options);
            IInstrumentRepository instrumentRepository = new InstrumentRepository(context);

            //Act
            var instru = new InstrumentTO { Name = "Saxophone" };
            var instru2 = new InstrumentTO {Id=2, Name = "Saxophone" };
            var result = instrumentRepository.Add(instru);
            var result2 = instrumentRepository.Add(instru2);
            context.SaveChanges();

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(1, instrumentRepository.GetAll().Count());
        }
        #endregion

        #region Delete
        [TestMethod]
        public void DeleteInstrument_ProvidingNull_ThrowException()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<LibraryContext>()
                 .UseInMemoryDatabase(databaseName: MethodBase.GetCurrentMethod().Name)
                 .Options;
            using var context = new LibraryContext(options);
            IInstrumentRepository instrumentRepository = new InstrumentRepository(context);

            var instrument = new InstrumentTO { Name = "Saxophone" };

            //Act & Assert
            Assert.ThrowsException<ArgumentException>(() => instrumentRepository.Delete(instrument));
        }

        [TestMethod]
        public void DeleteInstrument_Successful()
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
            var result = instrumentRepository.Delete(AddedInstru);
            context.SaveChanges();
            //Assert
            Assert.AreEqual(2, instrumentRepository.GetAll().Count());
        }
        #endregion

        #region GetById
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
        #endregion

        #region GetAll
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
        #endregion

        #region Update
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
        #endregion
    }
}
