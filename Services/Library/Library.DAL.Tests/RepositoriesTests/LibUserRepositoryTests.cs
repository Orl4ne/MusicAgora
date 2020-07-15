using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
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
