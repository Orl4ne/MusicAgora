using Library.DAL.Entities;
using Library.DAL.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MusicAgora.Common.Library.TransferObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.DAL.Tests
{
    [TestClass]
    public class ExtensionsTests
    {
        [TestMethod]
        public void ToTransfertObject_Successful()
        {
            //ARRANGE
            var instrument = new InstrumentEF { Id = 4, Name = "Saxophone", UserInstruments = new List<UserInstruEF> { new UserInstruEF { InstrumentId = 4, LibUserId = 1 } } };
            var libUser = new LibUserEF { Id = 1, IdentityUserId = 24, UserInstruments = new List<UserInstruEF> { new UserInstruEF { InstrumentId = 4, LibUserId = 1 } } };
            var category = new CategoryEF { Id = 2, Name = "Jazz" };
            var sheet = new SheetEF { Id = 3, Name = "Sam's Dixie", Category = category, IsCurrent = true, IsIndependance = true, IsGarde = false, };
            var sheetPart = new SheetPartEF { Id = 2, Instrument = instrument, Sheet = sheet, Path = "Path" };
            //ACT
            var instrumentTO = instrument.ToTransferObject();
            var libUserTO = libUser.ToTransferObject();
            var categoryTO = category.ToTransferObject();
            var sheetTO = sheet.ToTransferObject();
            var sheetPartTO = sheetPart.ToTransferObject();

            //Assert
            Assert.AreEqual(instrument.Name, instrumentTO.Name);
            Assert.AreEqual(libUser.IdentityUserId, libUserTO.IdentityUserId);
            Assert.AreEqual(category.Name, categoryTO.Name);
            Assert.AreEqual(sheet.Name, sheetTO.Name);
            Assert.AreEqual(sheetPart.Sheet.Name, sheetPartTO.Sheet.Name);
        }

        [TestMethod]
        public void ToTransfertObject_ProvidingNull_ThrowException()
        {
            InstrumentEF instrument = null;
            LibUserEF libUser = null;
            CategoryEF category = null;
            SheetEF sheet = null;
            SheetPartEF sheetPart = null;
            //ACT
            Assert.ThrowsException<ArgumentNullException>(() => instrument.ToTransferObject());
            Assert.ThrowsException<ArgumentNullException>(() => libUser.ToTransferObject());
            Assert.ThrowsException<ArgumentNullException>(() => category.ToTransferObject());
            Assert.ThrowsException<ArgumentNullException>(() => sheet.ToTransferObject());
            Assert.ThrowsException<ArgumentNullException>(() => sheetPart.ToTransferObject());
        }
        [TestMethod]
        public void ToEF_Successful()
        {
            //ARRANGE
            var instrument = new InstrumentTO { Id = 4, Name = "Saxophone" };
            var libUser = new LibUserTO { Id = 1, IdentityUserId = 24 };
            var category = new CategoryTO { Id = 2, Name = "Jazz" };
            var sheet = new SheetTO { Id = 3, Name = "Sam's Dixie", Category = category, IsCurrent = true, IsIndependance = true, IsGarde = false, };
            var sheetPart = new SheetPartTO { Id = 2, Instrument = instrument, Sheet = sheet, Path = "Path" };
            //ACT
            var instrumentEF = instrument.ToEF();
            var libUserEF = libUser.ToEF();
            var categoryEF = category.ToEF();
            var sheetEF = sheet.ToEF();
            var sheetPartEF = sheetPart.ToEF();

            //Assert
            Assert.AreEqual(instrument.Name, instrumentEF.Name);
            Assert.AreEqual(libUser.IdentityUserId, libUserEF.IdentityUserId);
            Assert.AreEqual(category.Name, categoryEF.Name);
            Assert.AreEqual(sheet.Name, sheetEF.Name);
            Assert.AreEqual(sheetPart.Sheet.Name, sheetPartEF.Sheet.Name);
        }

        [TestMethod]
        public void ToTEF_ProvidingNull_ThrowException()
        {
            InstrumentTO instrument = null;
            LibUserTO libUser = null;
            CategoryTO category = null;
            SheetTO sheet = null;
            SheetPartTO sheetPart = null;
            //ACT
            Assert.ThrowsException<ArgumentNullException>(() => instrument.ToEF());
            Assert.ThrowsException<ArgumentNullException>(() => libUser.ToEF());
            Assert.ThrowsException<ArgumentNullException>(() => category.ToEF());
            Assert.ThrowsException<ArgumentNullException>(() => sheet.ToEF());
            Assert.ThrowsException<ArgumentNullException>(() => sheetPart.ToEF());
        }

        [TestMethod]
        public void UpdateFromDetached_ProvidingNullAttachedEF_ThrowException()
        {
            InstrumentEF instrumentAttached = null;
            LibUserEF libUserAttached = null;
            CategoryEF categoryAttached = null;
            SheetEF sheetAttached = null;
            SheetPartEF sheetPartAttached = null;

            var instrument = new InstrumentEF { Id = 4, Name = "Saxophone", UserInstruments = new List<UserInstruEF> { new UserInstruEF { InstrumentId = 4, LibUserId = 1 } } };
            var libUser = new LibUserEF { Id = 1, IdentityUserId = 24, UserInstruments = new List<UserInstruEF> { new UserInstruEF { InstrumentId = 4, LibUserId = 1 } } };
            var category = new CategoryEF { Id = 2, Name = "Jazz" };
            var sheet = new SheetEF { Id = 3, Name = "Sam's Dixie", Category = category, IsCurrent = true, IsIndependance = true, IsGarde = false, };
            var sheetPart = new SheetPartEF { Id = 2, Instrument = instrument, Sheet = sheet, Path = "Path" };

            Assert.ThrowsException<ArgumentNullException>(() => instrumentAttached.UpdateFromDetached(instrument));
            Assert.ThrowsException<ArgumentNullException>(() => libUserAttached.UpdateFromDetached(libUser));
            Assert.ThrowsException<ArgumentNullException>(() => categoryAttached.UpdateFromDetached(category));
            Assert.ThrowsException<ArgumentNullException>(() => sheetAttached.UpdateFromDetached(sheet));
            Assert.ThrowsException<ArgumentNullException>(() => sheetPartAttached.UpdateFromDetached(sheetPart));
        }

        [TestMethod]
        public void UpdateFromDetached_ProvidingNullDetachedEF_ThrowException()
        {
            InstrumentEF instrumentDetached = null;
            LibUserEF libUserDetached = null;
            CategoryEF categoryDetached = null;
            SheetEF sheetDetached = null;
            SheetPartEF sheetPartDetached = null;

            var instrument = new InstrumentEF { Id = 4, Name = "Saxophone", UserInstruments = new List<UserInstruEF> { new UserInstruEF { InstrumentId = 4, LibUserId = 1 } } };
            var libUser = new LibUserEF { Id = 1, IdentityUserId = 24, UserInstruments = new List<UserInstruEF> { new UserInstruEF { InstrumentId = 4, LibUserId = 1 } } };
            var category = new CategoryEF { Id = 2, Name = "Jazz" };
            var sheet = new SheetEF { Id = 3, Name = "Sam's Dixie", Category = category, IsCurrent = true, IsIndependance = true, IsGarde = false, };
            var sheetPart = new SheetPartEF { Id = 2, Instrument = instrument, Sheet = sheet, Path = "Path" };

            Assert.ThrowsException<ArgumentNullException>(() => instrument.UpdateFromDetached(instrumentDetached));
            Assert.ThrowsException<ArgumentNullException>(() => libUser.UpdateFromDetached(libUserDetached));
            Assert.ThrowsException<ArgumentNullException>(() => category.UpdateFromDetached(categoryDetached));
            Assert.ThrowsException<ArgumentNullException>(() => sheet.UpdateFromDetached(sheetDetached));
            Assert.ThrowsException<ArgumentNullException>(() => sheetPart.UpdateFromDetached(sheetPartDetached));
        }
    }
}
