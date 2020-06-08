using Library.BLL.Services.MusicianUC;
using Library.DAL;
using Library.DAL.Entities;
using Library.DAL.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MusicAgora.Common.Library.TransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Library.BLL.Tests.UseCasesTests.MusicianUseCasesTests
{
    [TestClass]
    public class GetAllCurrentSheetPartsTests
    {
        [TestMethod]
        public void GetAllCurrentSheetPartsTests_Successful()
        {
            var options = new DbContextOptionsBuilder<LibraryContext>()
                 .UseInMemoryDatabase(databaseName: MethodBase.GetCurrentMethod().Name)
                 .Options;
            using var context = new LibraryContext(options);
            
            //ARRANGE
            //Creating and Add Instrument to DB
            var instru = new InstrumentEF { Name = "Saxophone", UserInstruments = new List<UserInstrumentEF>() };
            var instru2 = new InstrumentEF { Name = "Flute", UserInstruments = new List<UserInstrumentEF>() };
            var addedInstru = context.Add(instru);
            var addedInstru2 = context.Add(instru2);
            context.SaveChanges();

            //Creating and Adding User to DB
            var user = new LibraryUserEF
            {
                FirstName = "Jean-Claude",
                IsIndependance = true,
                UserInstruments = new List<UserInstrumentEF>() {
                    new UserInstrumentEF {
                    InstrumentId = 1} }
            };
            var user2 = new LibraryUserEF
            {
                FirstName = "Sophie",
                IsIndependance = true,
                UserInstruments = new List<UserInstrumentEF>() {
                    new UserInstrumentEF {
                    InstrumentId = 2} }
            };
            var user3 = new LibraryUserEF
            {
                FirstName = "Paulette",
                IsIndependance = true,
                UserInstruments = new List<UserInstrumentEF>()
                {
                    new UserInstrumentEF {InstrumentId = 2},new UserInstrumentEF{InstrumentId = 1}
                }
            };
            var addedUser = context.Add(user);
            var addedUser2 = context.Add(user2);
            var addedUser3 = context.Add(user3);
            context.SaveChanges();

            //Creating and Adding Sheet
            var sheet = new SheetEF { IsCurrent = true, Name = "Medley trop bien" };
            var sheet2 = new SheetEF { IsCurrent = true, Name = "Morceau de Grand-mère" };
            var addedSheet = context.Add(sheet);
            var addedSheet2 = context.Add(sheet2);
            context.SaveChanges();

            //Creating and Adding SheetParts
            var sheetPart = new SheetPartEF { Instrument = instru, Sheet = sheet };
            var sheetPart2 = new SheetPartEF { Instrument = instru2, Sheet = sheet };
            var sheetPart3 = new SheetPartEF { Instrument = instru, Sheet = sheet2 };
            var sheetPart4 = new SheetPartEF { Instrument = instru2, Sheet = sheet2 };
            var addedSheetPart = context.Add(sheetPart);
            var addedSheetPart2 = context.Add(sheetPart2);
            var addedSheetPart3 = context.Add(sheetPart3);
            var addedSheetPart4 = context.Add(sheetPart4);
            context.SaveChanges();
            //ACT
            var musician = new Musician(context);
            var result = musician.GetAllCurrentSheetParts(1);
            var result2 = musician.GetAllCurrentSheetParts(2);
            var result3 = musician.GetAllCurrentSheetParts(3);

            //ASSERT
            Assert.AreEqual(2, result.Count());
            Assert.AreEqual(2, result2.Count());
            Assert.AreEqual(4, result3.Count());
        }
    }
}
