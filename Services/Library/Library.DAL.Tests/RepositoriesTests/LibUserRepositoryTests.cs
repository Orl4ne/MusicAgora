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

namespace Library.DAL.Tests.RepositoriesTests
{
    [TestClass]
    public class LibUserRepositoryTests
    {
        #region Add
        [TestMethod]
        public void AddLibUser_Successful()
        {
            var options = new DbContextOptionsBuilder<LibraryContext>()
                .UseInMemoryDatabase(databaseName: MethodBase.GetCurrentMethod().Name)
                .Options;
            using var context = new LibraryContext(options);
            ILibUserRepository libUserRepository = new LibUserRepository(context);

            //Act
            var libUser = new LibUserTO { IdentityUserId = 23, InstrumentIds = new List<int> { 1 } };
            var result = libUserRepository.Add(libUser);
            context.SaveChanges();

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.InstrumentIds.First(), 1);
            Assert.AreEqual(result.IdentityUserId, 23);
        }

        [TestMethod]
        public void AddLibUser_AddNull_ThrowException()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<LibraryContext>()
                 .UseInMemoryDatabase(databaseName: MethodBase.GetCurrentMethod().Name)
                 .Options;
            using var context = new LibraryContext(options);
            ILibUserRepository libUserRepository = new LibUserRepository(context);
            //Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => libUserRepository.Add(null));
        }

        [TestMethod]
        public void AddLibUser_AddExistingSheetPart_DoNotInsertTwiceInDb()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<LibraryContext>()
                 .UseInMemoryDatabase(databaseName: MethodBase.GetCurrentMethod().Name)
                 .Options;
            using var context = new LibraryContext(options);
            ILibUserRepository libUserRepository = new LibUserRepository(context);

            //Act
            var libUser = new LibUserTO { IdentityUserId = 23, InstrumentIds = new List<int> { 1 } };
            var libUser2 = new LibUserTO {Id=2, IdentityUserId = 23, InstrumentIds = new List<int> { 1 } };
            var addedLibUser = libUserRepository.Add(libUser);
            var addedLibUser2 = libUserRepository.Add(libUser2);
            context.SaveChanges();

            //Assert
            Assert.AreEqual(1, libUserRepository.GetAll().Count());
        }
        #endregion

        #region Delete
        [TestMethod]
        public void DeleteLibUser_ProvidingNull_ThrowException()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<LibraryContext>()
                 .UseInMemoryDatabase(databaseName: MethodBase.GetCurrentMethod().Name)
                 .Options;
            using var context = new LibraryContext(options);
            ILibUserRepository libUserRepository = new LibUserRepository(context);

            var libUser = new LibUserTO { IdentityUserId = 23, InstrumentIds = new List<int> { 1 } };

            //Act & Assert
            Assert.ThrowsException<ArgumentException>(() => libUserRepository.Delete(libUser));
        }

        [TestMethod]
        public void DeleteLibUser_Successful()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<LibraryContext>()
                 .UseInMemoryDatabase(databaseName: MethodBase.GetCurrentMethod().Name)
                 .Options;
            using var context = new LibraryContext(options);
            ILibUserRepository libUserRepository = new LibUserRepository(context);

            var libUser = new LibUserTO { IdentityUserId = 23, InstrumentIds = new List<int> { 1 } };
            var libUser2 = new LibUserTO { IdentityUserId = 4, InstrumentIds = new List<int> { 7, 4 } };
            var libUser3 = new LibUserTO { IdentityUserId = 6, InstrumentIds = new List<int> { 2 } };
            var addedLibUser = libUserRepository.Add(libUser);
            var addedLibUser2 = libUserRepository.Add(libUser2);
            var addedLibUser3 = libUserRepository.Add(libUser3);
            context.SaveChanges();

            //Act
            var result = libUserRepository.Delete(addedLibUser);
            context.SaveChanges();
            //Assert
            Assert.AreEqual(2, libUserRepository.GetAll().Count());
        }

        #endregion

        #region GetById
        [TestMethod]
        public void GetLibUsertById_Successful()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<LibraryContext>()
                 .UseInMemoryDatabase(databaseName: MethodBase.GetCurrentMethod().Name)
                 .Options;
            using var context = new LibraryContext(options);
            ILibUserRepository libUserRepository = new LibUserRepository(context);

            var libUser = new LibUserTO { IdentityUserId = 23, InstrumentIds = new List<int> { 1 } };
            var libUser2 = new LibUserTO { IdentityUserId = 4, InstrumentIds = new List<int> { 7, 4 } };
            var libUser3 = new LibUserTO { IdentityUserId = 6, InstrumentIds = new List<int> { 2 } };
            var addedLibUser = libUserRepository.Add(libUser);
            var addedLibUser2 = libUserRepository.Add(libUser2);
            var addedLibUser3 = libUserRepository.Add(libUser3);
            context.SaveChanges();

            //Act
            var result = libUserRepository.GetById(1);
            var result2 = libUserRepository.GetById(2);
            var result3 = libUserRepository.GetById(3);

            //Assert
            Assert.AreEqual(23, result.IdentityUserId);
            Assert.AreEqual(4, result2.IdentityUserId);
            Assert.AreEqual(6, result3.IdentityUserId);
        }

        [TestMethod]
        public void GetLibUserById_ProvidingNonExistingId_ThrowException()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<LibraryContext>()
                 .UseInMemoryDatabase(databaseName: MethodBase.GetCurrentMethod().Name)
                 .Options;
            using var context = new LibraryContext(options);
            ILibUserRepository libUserRepository = new LibUserRepository(context);

            //Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => libUserRepository.GetById(32));
        }
        #endregion

        #region GetAll
        [TestMethod]
        public void GetAllLibUsers_Successful()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<LibraryContext>()
                 .UseInMemoryDatabase(databaseName: MethodBase.GetCurrentMethod().Name)
                 .Options;
            using var context = new LibraryContext(options);
            ILibUserRepository libUserRepository = new LibUserRepository(context);

            var libUser = new LibUserTO { IdentityUserId = 23, InstrumentIds = new List<int> { 1 } };
            var libUser2 = new LibUserTO { IdentityUserId = 4, InstrumentIds = new List<int> { 7, 4 } };
            var libUser3 = new LibUserTO { IdentityUserId = 6, InstrumentIds = new List<int> { 2 } };
            var addedLibUser = libUserRepository.Add(libUser);
            var addedLibUser2 = libUserRepository.Add(libUser2);
            var addedLibUser3 = libUserRepository.Add(libUser3);
            context.SaveChanges();

            //Act
            var result = libUserRepository.GetAll();
            //Assert
            Assert.AreEqual(3, result.Count());
        }
        #endregion

        #region Update
        [TestMethod]
        public void UpdateLibUser_Successful()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<LibraryContext>()
                 .UseInMemoryDatabase(databaseName: MethodBase.GetCurrentMethod().Name)
                 .Options;
            using var context = new LibraryContext(options);
            ILibUserRepository libUserRepository = new LibUserRepository(context);

            var libUser = new LibUserTO { IdentityUserId = 23, InstrumentIds = new List<int> { 1 } };
            var libUser2 = new LibUserTO { IdentityUserId = 4, InstrumentIds = new List<int> { 7, 4 } };
            var libUser3 = new LibUserTO { IdentityUserId = 6, InstrumentIds = new List<int> { 2 } };
            var addedLibUser = libUserRepository.Add(libUser);
            var addedLibUser2 = libUserRepository.Add(libUser2);
            var addedLibUser3 = libUserRepository.Add(libUser3);
            context.SaveChanges();

            //Act
            addedLibUser.IdentityUserId = 29;
            var test = libUserRepository.Update(addedLibUser);
            context.SaveChanges();

            //Assert
            Assert.AreEqual(3, libUserRepository.GetAll().Count());
            Assert.AreEqual(29, test.IdentityUserId);
        }

        [TestMethod]
        public void UpdateLibUser_ProvidingNonExistingSheetPart_ThrowException()
        {

        }
        #endregion
    }
}
