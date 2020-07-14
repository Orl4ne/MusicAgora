using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.DAL.Tests.RepositoriesTests
{
    [TestClass]
    public class SheetPartRepositoryTests
    {
        #region Add
        [TestMethod]
        public void AddSheetPart_Successful()
        {

        }


        [TestMethod]
        public void AddSheetPart_AddNull_ThrowException()
        {

        }

        [TestMethod]
        public void AddSheetPart_AddExistingSheetPart_DoNotInsertTwiceInDb()
        {

        }
        #endregion

        #region Delete
        [TestMethod]
        public void DeleteSheetPart_ProvidingNull_ThrowException()
        {

        }

        [TestMethod]
        public void DeleteSheet_Successful()
        {

        }

        #endregion

        #region GetById
        [TestMethod]
        public void GetSheetPartById_Successful()
        {

        }

        [TestMethod]
        public void GetSheetPartById_ProvidingNonExistingId_ThrowException()
        {

        }
        #endregion

        #region GetAll
        [TestMethod]
        public void GetAllSheetParts_Successful()
        {

        }
        #endregion

        #region Update
        [TestMethod]
        public void UpdateSheetPart_Successful()
        {

        }

        [TestMethod]
        public void UpdateSheetPart_ProvidingNonExistingSheetPart_ThrowException()
        {

        }
        #endregion

    }
}
