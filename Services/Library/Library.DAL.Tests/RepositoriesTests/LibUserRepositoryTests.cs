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
            IInstrumentRepository instrumentRepository = new InstrumentRepository(context);

            //Act

            var instru = new InstrumentTO { Name = "Saxophone" };
            var addedInstru = instrumentRepository.Add(instru);
            context.SaveChanges();
            var libUser = new LibUserTO { IdentityUserId = 23, InstrumentIds = new List<int> { 1 } };
            var result = libUserRepository.Add(libUser);
            context.SaveChanges();

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.InstrumentIds.First(), 1);
        }


        [TestMethod]
        public void AddLibUser_AddNull_ThrowException()
        {

        }

        [TestMethod]
        public void AddLibUser_AddExistingSheetPart_DoNotInsertTwiceInDb()
        {

        }
        #endregion

        #region Delete
        [TestMethod]
        public void DeleteLibUser_ProvidingNull_ThrowException()
        {

        }

        [TestMethod]
        public void DeleteLibUser_Successful()
        {

        }

        #endregion

        #region GetById
        [TestMethod]
        public void GetLibUsertById_Successful()
        {

        }

        [TestMethod]
        public void GetLibUserById_ProvidingNonExistingId_ThrowException()
        {

        }
        #endregion

        #region GetAll
        [TestMethod]
        public void GetAllLibUsers_Successful()
        {

        }
        #endregion

        #region Update
        [TestMethod]
        public void UpdateLibUser_Successful()
        {

        }

        [TestMethod]
        public void UpdateLibUser_ProvidingNonExistingSheetPart_ThrowException()
        {

        }
        #endregion
    }
}
