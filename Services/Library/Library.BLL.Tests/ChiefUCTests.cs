using Library.BLL.UseCases;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MusicAgora.Common.Library.Interfaces;
using MusicAgora.Common.Library.TransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.BLL.Tests
{
    [TestClass]
    public class ChiefUCTests
    {
        #region MocksMethods
        public List<InstrumentTO> MockInstruments()
        {
            var instru = new InstrumentTO { Id = 1, Name = "Saxophone" };
            var instru2 = new InstrumentTO { Id = 2, Name = "Trumpet" };
            var instru3 = new InstrumentTO { Id = 3, Name = "Flute" };
            return new List<InstrumentTO> { instru, instru2, instru3 };
        }
        public List<LibUserTO> MockLibUsers()
        {
            var libUser = new LibUserTO { Id = 1, IdentityUserId = 23, InstrumentIds = new List<int> { 1 } };
            var libUser2 = new LibUserTO { Id = 2, IdentityUserId = 4, InstrumentIds = new List<int> { 2, 3 } };
            var libUser3 = new LibUserTO { Id = 3, IdentityUserId = 6, InstrumentIds = new List<int> { 2 } };
            return new List<LibUserTO> { libUser, libUser2, libUser3 };
        }
        public List<SheetTO> MockSheets()
        {
            var sheet = new SheetTO { Id = 1, Name = "BestOf", Arranger = "Jean-Luc", Composer = "Morricone", IsCurrent = false, IsGarde = false, IsIndependance = true };
            var sheet2 = new SheetTO { Id = 2, Name = "Young Amadeus", Arranger = "Jan de Haan", Composer = "Mozart", IsCurrent = false, IsGarde = false, IsIndependance = true };
            var sheet3 = new SheetTO { Id = 3, Name = "Daft Punk Medley", Arranger = "Mec", Composer = "Daft Punkt", IsCurrent = true, IsGarde = false, IsIndependance = true };
            return new List<SheetTO> { sheet, sheet2, sheet3 };
        }
        public List<SheetPartTO> MockSheetParts()
        {
            var instruments = MockInstruments();
            var sheets = MockSheets();
            var sheetPart = new SheetPartTO { Id = 1, Instrument = instruments.First(i => i.Id == 1), Sheet = sheets.First(s => s.Id == 1), Path = $@"\Files\BestOf\Saxophone" };
            var sheetPart2 = new SheetPartTO { Id = 2, Instrument = instruments.First(i => i.Id == 1), Sheet = sheets.First(s => s.Id == 2), Path = @"\Files\Young Amadeus\Saxophone" };
            var sheetPart3 = new SheetPartTO { Id = 3, Instrument = instruments.First(i => i.Id == 1), Sheet = sheets.First(s => s.Id == 3), Path = @"\Files\Daft Punk Medley\Saxophone" };
            var sheetPart4 = new SheetPartTO { Id = 4, Instrument = instruments.First(i => i.Id == 2), Sheet = sheets.First(s => s.Id == 1), Path = @"\Files\BestOf\Trumpet" };
            var sheetPart5 = new SheetPartTO { Id = 5, Instrument = instruments.First(i => i.Id == 2), Sheet = sheets.First(s => s.Id == 2), Path = @"\Files\Young Amadeus\Trumpet" };
            var sheetPart6 = new SheetPartTO { Id = 5, Instrument = instruments.First(i => i.Id == 2), Sheet = sheets.First(s => s.Id == 3), Path = @"\Files\Daft Punk Medley\Trumpet" };
            var sheetPart7 = new SheetPartTO { Id = 5, Instrument = instruments.First(i => i.Id == 3), Sheet = sheets.First(s => s.Id == 1), Path = @"\Files\BestOf\Flute" };
            var sheetPart8 = new SheetPartTO { Id = 5, Instrument = instruments.First(i => i.Id == 3), Sheet = sheets.First(s => s.Id == 2), Path = @"\Files\Young Amadeus\Flute" };
            var sheetPart9 = new SheetPartTO { Id = 5, Instrument = instruments.First(i => i.Id == 3), Sheet = sheets.First(s => s.Id == 3), Path = @"\Files\Daft Punk Medley\Flute" };
            return new List<SheetPartTO> { sheetPart, sheetPart2, sheetPart3, sheetPart4, sheetPart5, sheetPart6, sheetPart7, sheetPart8, sheetPart9 };
        }
        #endregion
        
        [TestMethod]
        public void SetAsCurrentSheet_Successful()
        {
            var mockUnitOfWork = new Mock<ILibraryUnitOfWork>();
            mockUnitOfWork.Setup(u => u.SheetRepository.GetAll())
            .Returns(MockSheets());

            mockUnitOfWork.Setup(u => u.SheetRepository.GetById(It.IsAny<int>()))
                .Returns(new SheetTO { Id = 2, Name = "Young Amadeus", Arranger = "Jan de Haan", Composer = "Mozart", IsCurrent = false, IsGarde = false, IsIndependance = true });

            var updatedSheet = new SheetTO { Id = 2, Name = "Young Amadeus", Arranger = "Jan de Haan", Composer = "Mozart", IsCurrent = true, IsGarde = false, IsIndependance = true };
            mockUnitOfWork.Setup(u => u.SheetRepository.Update(It.IsAny<SheetTO>()))
                         .Returns(updatedSheet);

            var sut = new ChiefUC(mockUnitOfWork.Object, null);
            var result = sut.SetAsCurrentSheet(2);

            Assert.IsNotNull(result);
            Assert.AreEqual(true, result.IsCurrent);
            Assert.AreEqual(2, result.Id);
            mockUnitOfWork.Verify(u => u.SheetRepository.GetById(2), Times.Once);
            mockUnitOfWork.Verify(u => u.SheetRepository.Update(It.IsAny<SheetTO>()), Times.Once);
        }

        [TestMethod]
        public void SetAsCurrentSheet_SheetAlreadyCurrent_ReturnSheetWithoutUpdateMethod()
        {
            var mockUnitOfWork = new Mock<ILibraryUnitOfWork>();
            mockUnitOfWork.Setup(u => u.SheetRepository.GetAll())
            .Returns(MockSheets());

            mockUnitOfWork.Setup(u => u.SheetRepository.GetById(It.IsAny<int>()))
                .Returns(new SheetTO { Id = 2, Name = "Young Amadeus", Arranger = "Jan de Haan", Composer = "Mozart", IsCurrent = true, IsGarde = false, IsIndependance = true });

            var updatedSheet = new SheetTO { Id = 2, Name = "Young Amadeus", Arranger = "Jan de Haan", Composer = "Mozart", IsCurrent = true, IsGarde = false, IsIndependance = true };
            mockUnitOfWork.Setup(u => u.SheetRepository.Update(It.IsAny<SheetTO>()))
                         .Returns(updatedSheet);

            var sut = new ChiefUC(mockUnitOfWork.Object, null);
            var result = sut.SetAsCurrentSheet(2);

            Assert.IsNotNull(result);
            Assert.AreEqual(true, result.IsCurrent);
            Assert.AreEqual(2, result.Id);
            mockUnitOfWork.Verify(u => u.SheetRepository.GetById(2), Times.Once);
            mockUnitOfWork.Verify(u => u.SheetRepository.Update(It.IsAny<SheetTO>()), Times.Never);
        }

        [TestMethod]
        public void GetAllSheets_Successful()
        {
            var mockUnitOfWork = new Mock<ILibraryUnitOfWork>();
            mockUnitOfWork.Setup(u => u.SheetRepository.GetAll())
            .Returns(MockSheets());

            var sut = new ChiefUC(mockUnitOfWork.Object, null);
            var result = sut.GetAllSheets();

            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Count());
            mockUnitOfWork.Verify(u => u.SheetRepository.GetAll(), Times.Once);
        }
        
        [TestMethod]
        public void GetAllSheetPartsBySheet_Successful()
        {
            var mockUnitOfWork = new Mock<ILibraryUnitOfWork>();
            mockUnitOfWork.Setup(u => u.SheetPartRepository.GetAll())
            .Returns(MockSheetParts());

            var sut = new ChiefUC(mockUnitOfWork.Object, null);
            var result = sut.GetAllSheetPartsBySheet(3);

            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Count());
            mockUnitOfWork.Verify(u => u.SheetPartRepository.GetAll(), Times.Once);
        }


    }
}
