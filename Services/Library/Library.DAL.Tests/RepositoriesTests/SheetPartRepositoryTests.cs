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
    public class SheetPartRepositoryTests
    {
        #region Add
        [TestMethod]
        public void AddSheetPart_Successful()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<LibraryContext>()
                 .UseInMemoryDatabase(databaseName: MethodBase.GetCurrentMethod().Name)
                 .Options;
            using var context = new LibraryContext(options);
            ISheetRepository sheetRepository = new SheetRepository(context);
            ICategoryRepository categoryRepository = new CategoryRepository(context);
            ISheetPartRepository sheetPartRepository = new SheetPartRepository(context);
            IInstrumentRepository instrumentRepository = new InstrumentRepository(context);

            //Act
            var category = new CategoryTO { Name = "Musique de films" };
            var addedCategory = categoryRepository.Add(category);
            var category2 = new CategoryTO { Name = "Musique de classique" };
            var addedCategory2 = categoryRepository.Add(category2);
            context.SaveChanges();

            var sheet = new SheetTO { Name = "BestOf", Arranger = "Jean-Luc", Category = addedCategory, Composer = "Morricone", IsCurrent = false, IsGarde = false, IsIndependance = true };
            var sheet2 = new SheetTO { Name = "Youg Amadeus", Arranger = "Jan de Haan", Category = addedCategory2, Composer = "Mozart", IsCurrent = true, IsGarde = false, IsIndependance = true };
            var addedSheet = sheetRepository.Add(sheet);
            var addedSheet2 = sheetRepository.Add(sheet2);
            context.SaveChanges();

            var instru = new InstrumentTO { Name = "Saxophone" };
            var instru2 = new InstrumentTO { Name = "Trumpet" };
            var instru3 = new InstrumentTO { Name = "Flute" };
            var addedInstru = instrumentRepository.Add(instru);
            var addedInstru2 = instrumentRepository.Add(instru2);
            var addedInstru3 = instrumentRepository.Add(instru3);
            context.SaveChanges();
            var sheetPart = new SheetPartTO {Instrument= addedInstru, Sheet= addedSheet, Path =@"C:\sheet"  };
            var sheetPart2 = new SheetPartTO { Instrument = addedInstru2, Sheet = addedSheet };
            var sheetPart3 = new SheetPartTO { Instrument = addedInstru, Sheet = addedSheet2 };
            var addedSheetPart = sheetPartRepository.Add(sheetPart);
            var addedSheetPart2 = sheetPartRepository.Add(sheetPart2);
            var addedSheetPart3 = sheetPartRepository.Add(sheetPart3);
            context.SaveChanges();

            //Assert
            Assert.AreEqual(addedSheetPart2.Sheet.Name, "BestOf");
            Assert.AreEqual(3, sheetPartRepository.GetAll().Count());
        }


        [TestMethod]
        public void AddSheetPart_AddNull_ThrowException()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<LibraryContext>()
                 .UseInMemoryDatabase(databaseName: MethodBase.GetCurrentMethod().Name)
                 .Options;
            using var context = new LibraryContext(options);
            ISheetPartRepository sheetPartRepository = new SheetPartRepository(context);

            //Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => sheetPartRepository.Add(null));
        }

        [TestMethod]
        public void AddSheetPart_AddExistingSheetPart_DoNotInsertTwiceInDb()
        {
            var options = new DbContextOptionsBuilder<LibraryContext>()
                 .UseInMemoryDatabase(databaseName: MethodBase.GetCurrentMethod().Name)
                 .Options;
            using var context = new LibraryContext(options);
            ISheetRepository sheetRepository = new SheetRepository(context);
            ICategoryRepository categoryRepository = new CategoryRepository(context);
            ISheetPartRepository sheetPartRepository = new SheetPartRepository(context);
            IInstrumentRepository instrumentRepository = new InstrumentRepository(context);

            //Act
            var category = new CategoryTO { Name = "Musique de films" };
            var addedCategory = categoryRepository.Add(category);
            var category2 = new CategoryTO { Name = "Musique de classique" };
            var addedCategory2 = categoryRepository.Add(category2);
            context.SaveChanges();

            var sheet = new SheetTO { Name = "BestOf", Arranger = "Jean-Luc", Category = addedCategory, Composer = "Morricone", IsCurrent = false, IsGarde = false, IsIndependance = true };
            var sheet2 = new SheetTO { Name = "Youg Amadeus", Arranger = "Jan de Haan", Category = addedCategory2, Composer = "Mozart", IsCurrent = true, IsGarde = false, IsIndependance = true };
            var addedSheet = sheetRepository.Add(sheet);
            var addedSheet2 = sheetRepository.Add(sheet2);
            context.SaveChanges();

            var instru = new InstrumentTO { Name = "Saxophone" };
            var instru2 = new InstrumentTO { Name = "Trumpet" };
            var instru3 = new InstrumentTO { Name = "Flute" };
            var addedInstru = instrumentRepository.Add(instru);
            var addedInstru2 = instrumentRepository.Add(instru2);
            var addedInstru3 = instrumentRepository.Add(instru3);
            context.SaveChanges();
            var sheetPart = new SheetPartTO { Instrument = addedInstru, Sheet = addedSheet, Path = @"C:\sheet" };
            var sheetPart2 = new SheetPartTO {Id=4, Instrument = addedInstru2, Sheet = addedSheet };
            var sheetPart3 = new SheetPartTO { Instrument = addedInstru, Sheet = addedSheet2 };
            var addedSheetPart = sheetPartRepository.Add(sheetPart);
            var addedSheetPart2 = sheetPartRepository.Add(sheetPart2);
            var addedSheetPart3 = sheetPartRepository.Add(sheetPart3);
            context.SaveChanges();

            //Assert
            Assert.AreEqual(2, sheetPartRepository.GetAll().Count());
        }
        #endregion

        #region Delete
        [TestMethod]
        public void DeleteSheetPart_ProvidingNull_ThrowException()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<LibraryContext>()
                 .UseInMemoryDatabase(databaseName: MethodBase.GetCurrentMethod().Name)
                 .Options;
            using var context = new LibraryContext(options);
            ISheetPartRepository sheetPartRepository = new SheetPartRepository(context); IInstrumentRepository instrumentRepository = new InstrumentRepository(context);

            var sheetPart = new SheetPartTO {  };

            //Act & Assert
            Assert.ThrowsException<ArgumentException>(() => sheetPartRepository.Delete(sheetPart));
        }

        [TestMethod]
        public void DeleteSheetPart_Successful()
        {
            var options = new DbContextOptionsBuilder<LibraryContext>()
                 .UseInMemoryDatabase(databaseName: MethodBase.GetCurrentMethod().Name)
                 .Options;
            using var context = new LibraryContext(options);
            ISheetRepository sheetRepository = new SheetRepository(context);
            ICategoryRepository categoryRepository = new CategoryRepository(context);
            ISheetPartRepository sheetPartRepository = new SheetPartRepository(context);
            IInstrumentRepository instrumentRepository = new InstrumentRepository(context);

            //Act
            var category = new CategoryTO { Name = "Musique de films" };
            var addedCategory = categoryRepository.Add(category);
            var category2 = new CategoryTO { Name = "Musique de classique" };
            var addedCategory2 = categoryRepository.Add(category2);
            context.SaveChanges();

            var sheet = new SheetTO { Name = "BestOf", Arranger = "Jean-Luc", Category = addedCategory, Composer = "Morricone", IsCurrent = false, IsGarde = false, IsIndependance = true };
            var sheet2 = new SheetTO { Name = "Youg Amadeus", Arranger = "Jan de Haan", Category = addedCategory2, Composer = "Mozart", IsCurrent = true, IsGarde = false, IsIndependance = true };
            var addedSheet = sheetRepository.Add(sheet);
            var addedSheet2 = sheetRepository.Add(sheet2);
            context.SaveChanges();

            var instru = new InstrumentTO { Name = "Saxophone" };
            var instru2 = new InstrumentTO { Name = "Trumpet" };
            var instru3 = new InstrumentTO { Name = "Flute" };
            var addedInstru = instrumentRepository.Add(instru);
            var addedInstru2 = instrumentRepository.Add(instru2);
            var addedInstru3 = instrumentRepository.Add(instru3);
            context.SaveChanges();
            var sheetPart = new SheetPartTO { Instrument = addedInstru, Sheet = addedSheet, Path = @"C:\sheet" };
            var sheetPart2 = new SheetPartTO { Instrument = addedInstru2, Sheet = addedSheet };
            var sheetPart3 = new SheetPartTO { Instrument = addedInstru, Sheet = addedSheet2 };
            var addedSheetPart = sheetPartRepository.Add(sheetPart);
            var addedSheetPart2 = sheetPartRepository.Add(sheetPart2);
            var addedSheetPart3 = sheetPartRepository.Add(sheetPart3);
            context.SaveChanges();

            var result = sheetPartRepository.Delete(addedSheetPart2);

            //Assert
            Assert.AreEqual(2, sheetPartRepository.GetAll().Count());
        }

        #endregion

        #region GetById
        [TestMethod]
        public void GetSheetPartById_Successful()
        {
            var options = new DbContextOptionsBuilder<LibraryContext>()
                 .UseInMemoryDatabase(databaseName: MethodBase.GetCurrentMethod().Name)
                 .Options;
            using var context = new LibraryContext(options);
            ISheetRepository sheetRepository = new SheetRepository(context);
            ICategoryRepository categoryRepository = new CategoryRepository(context);
            ISheetPartRepository sheetPartRepository = new SheetPartRepository(context);
            IInstrumentRepository instrumentRepository = new InstrumentRepository(context);

            //Act
            var category = new CategoryTO { Name = "Musique de films" };
            var addedCategory = categoryRepository.Add(category);
            var category2 = new CategoryTO { Name = "Musique de classique" };
            var addedCategory2 = categoryRepository.Add(category2);
            context.SaveChanges();

            var sheet = new SheetTO { Name = "BestOf", Arranger = "Jean-Luc", Category = addedCategory, Composer = "Morricone", IsCurrent = false, IsGarde = false, IsIndependance = true };
            var sheet2 = new SheetTO { Name = "Youg Amadeus", Arranger = "Jan de Haan", Category = addedCategory2, Composer = "Mozart", IsCurrent = true, IsGarde = false, IsIndependance = true };
            var addedSheet = sheetRepository.Add(sheet);
            var addedSheet2 = sheetRepository.Add(sheet2);
            context.SaveChanges();

            var instru = new InstrumentTO { Name = "Saxophone" };
            var instru2 = new InstrumentTO { Name = "Trumpet" };
            var instru3 = new InstrumentTO { Name = "Flute" };
            var addedInstru = instrumentRepository.Add(instru);
            var addedInstru2 = instrumentRepository.Add(instru2);
            var addedInstru3 = instrumentRepository.Add(instru3);
            context.SaveChanges();
            var sheetPart = new SheetPartTO { Instrument = addedInstru, Sheet = addedSheet, Path = @"C:\sheet" };
            var sheetPart2 = new SheetPartTO { Instrument = addedInstru2, Sheet = addedSheet };
            var sheetPart3 = new SheetPartTO { Instrument = addedInstru, Sheet = addedSheet2 };
            var addedSheetPart = sheetPartRepository.Add(sheetPart);
            var addedSheetPart2 = sheetPartRepository.Add(sheetPart2);
            var addedSheetPart3 = sheetPartRepository.Add(sheetPart3);
            context.SaveChanges();
            //Act
            var result = sheetPartRepository.GetById(1);
            var result2 = sheetPartRepository.GetById(2);
            var result3 = sheetPartRepository.GetById(3);
            //Assert
            Assert.AreEqual("Saxophone", result.Instrument.Name);
            Assert.AreEqual("Trumpet", result2.Instrument.Name);
            Assert.AreEqual("Saxophone", result3.Instrument.Name);
        }

        [TestMethod]
        public void GetSheetPartById_ProvidingNonExistingId_ThrowException()
        {
            var options = new DbContextOptionsBuilder<LibraryContext>()
                .UseInMemoryDatabase(databaseName: MethodBase.GetCurrentMethod().Name)
                .Options;
            using var context = new LibraryContext(options);
            ISheetPartRepository sheetPartRepository = new SheetPartRepository(context);
            //Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => sheetPartRepository.GetById(14));
        }
        #endregion

        #region GetAll
        [TestMethod]
        public void GetAllSheetParts_Successful()
        {
            var options = new DbContextOptionsBuilder<LibraryContext>()
                 .UseInMemoryDatabase(databaseName: MethodBase.GetCurrentMethod().Name)
                 .Options;
            using var context = new LibraryContext(options);
            ISheetRepository sheetRepository = new SheetRepository(context);
            ICategoryRepository categoryRepository = new CategoryRepository(context);
            ISheetPartRepository sheetPartRepository = new SheetPartRepository(context);
            IInstrumentRepository instrumentRepository = new InstrumentRepository(context);

            //Act
            var category = new CategoryTO { Name = "Musique de films" };
            var addedCategory = categoryRepository.Add(category);
            var category2 = new CategoryTO { Name = "Musique de classique" };
            var addedCategory2 = categoryRepository.Add(category2);
            context.SaveChanges();

            var sheet = new SheetTO { Name = "BestOf", Arranger = "Jean-Luc", Category = addedCategory, Composer = "Morricone", IsCurrent = false, IsGarde = false, IsIndependance = true };
            var sheet2 = new SheetTO { Name = "Youg Amadeus", Arranger = "Jan de Haan", Category = addedCategory2, Composer = "Mozart", IsCurrent = true, IsGarde = false, IsIndependance = true };
            var addedSheet = sheetRepository.Add(sheet);
            var addedSheet2 = sheetRepository.Add(sheet2);
            context.SaveChanges();

            var instru = new InstrumentTO { Name = "Saxophone" };
            var instru2 = new InstrumentTO { Name = "Trumpet" };
            var instru3 = new InstrumentTO { Name = "Flute" };
            var addedInstru = instrumentRepository.Add(instru);
            var addedInstru2 = instrumentRepository.Add(instru2);
            var addedInstru3 = instrumentRepository.Add(instru3);
            context.SaveChanges();
            var sheetPart = new SheetPartTO { Instrument = addedInstru, Sheet = addedSheet, Path = @"C:\sheet" };
            var sheetPart2 = new SheetPartTO { Instrument = addedInstru2, Sheet = addedSheet };
            var sheetPart3 = new SheetPartTO { Instrument = addedInstru, Sheet = addedSheet2 };
            var addedSheetPart = sheetPartRepository.Add(sheetPart);
            var addedSheetPart2 = sheetPartRepository.Add(sheetPart2);
            var addedSheetPart3 = sheetPartRepository.Add(sheetPart3);
            context.SaveChanges();

            var result = sheetPartRepository.GetAll();
            //Assert
            Assert.AreEqual(3, result.Count());
        }
        #endregion

        #region Update
        [TestMethod]
        public void UpdateSheetPart_Successful()
        {
            var options = new DbContextOptionsBuilder<LibraryContext>()
                 .UseInMemoryDatabase(databaseName: MethodBase.GetCurrentMethod().Name)
                 .Options;
            using var context = new LibraryContext(options);
            ISheetRepository sheetRepository = new SheetRepository(context);
            ICategoryRepository categoryRepository = new CategoryRepository(context);
            ISheetPartRepository sheetPartRepository = new SheetPartRepository(context);
            IInstrumentRepository instrumentRepository = new InstrumentRepository(context);

            //Act
            var category = new CategoryTO { Name = "Musique de films" };
            var addedCategory = categoryRepository.Add(category);
            var category2 = new CategoryTO { Name = "Musique de classique" };
            var addedCategory2 = categoryRepository.Add(category2);
            context.SaveChanges();

            var sheet = new SheetTO { Name = "BestOf", Arranger = "Jean-Luc", Category = addedCategory, Composer = "Morricone", IsCurrent = false, IsGarde = false, IsIndependance = true };
            var sheet2 = new SheetTO { Name = "Youg Amadeus", Arranger = "Jan de Haan", Category = addedCategory2, Composer = "Mozart", IsCurrent = true, IsGarde = false, IsIndependance = true };
            var addedSheet = sheetRepository.Add(sheet);
            var addedSheet2 = sheetRepository.Add(sheet2);
            context.SaveChanges();

            var instru = new InstrumentTO { Name = "Saxophone" };
            var instru2 = new InstrumentTO { Name = "Trumpet" };
            var instru3 = new InstrumentTO { Name = "Flute" };
            var addedInstru = instrumentRepository.Add(instru);
            var addedInstru2 = instrumentRepository.Add(instru2);
            var addedInstru3 = instrumentRepository.Add(instru3);
            context.SaveChanges();
            var sheetPart = new SheetPartTO { Instrument = addedInstru, Sheet = addedSheet, Path = @"C:\sheet" };
            var sheetPart2 = new SheetPartTO { Instrument = addedInstru2, Sheet = addedSheet };
            var sheetPart3 = new SheetPartTO { Instrument = addedInstru, Sheet = addedSheet2 };
            var addedSheetPart = sheetPartRepository.Add(sheetPart);
            var addedSheetPart2 = sheetPartRepository.Add(sheetPart2);
            var addedSheetPart3 = sheetPartRepository.Add(sheetPart3);
            context.SaveChanges();

            //ACT
            addedSheetPart.Instrument = addedInstru3;
            var test = sheetPartRepository.Update(addedSheetPart);
            context.SaveChanges();

            //Assert
            Assert.AreEqual(3, sheetPartRepository.GetAll().Count());
            Assert.AreEqual("Flute", test.Instrument.Name);
        }

        [TestMethod]
        public void UpdateSheetPart_ProvidingNonExistingSheetPart_ThrowException()
        {
            var options = new DbContextOptionsBuilder<LibraryContext>()
                 .UseInMemoryDatabase(databaseName: MethodBase.GetCurrentMethod().Name)
                 .Options;
            using var context = new LibraryContext(options);
            ISheetPartRepository sheetPartRepository = new SheetPartRepository(context);

            var sheetPart = new SheetPartTO { };

            //Act & Assert
            Assert.ThrowsException<ArgumentException>(() => sheetPartRepository.Update(sheetPart));
        }
        #endregion

    }
}
