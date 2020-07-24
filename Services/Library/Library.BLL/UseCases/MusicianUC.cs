using Library.DAL;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.Extensions.Configuration;
using MusicAgora.Common.Library.Interfaces;
using MusicAgora.Common.Library.Interfaces.UseCases;
using MusicAgora.Common.Library.TransferObjects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Library.BLL.UseCases
{
    public class MusicianUC : IMusicianUC
    {
        #region CTOR
        private readonly ILibraryUnitOfWork unitOfWork;
        private readonly IConfiguration config;
        public MusicianUC(ILibraryUnitOfWork iLibraryUnitOfWork, IConfiguration config)
        {
            this.unitOfWork = iLibraryUnitOfWork ?? throw new System.ArgumentNullException(nameof(iLibraryUnitOfWork));
            this.config = config;
        }
        #endregion

        public SheetPartTO SeeASheetPartDetails(int SheetPartId)
        {
            return unitOfWork.SheetPartRepository.GetById(SheetPartId);
        }

        public List<SheetPartTO> GetAllMyCurrentSheetParts(int IdentityUserId)
        {
            //Attaching LibUser 
            var libUser = unitOfWork.LibUserRepository.GetByIdentityUserId(IdentityUserId);
            //Get All current sheets
            var currentSheets = unitOfWork.SheetRepository.GetAll().Where(s => s.IsCurrent == true);
            var currentSheetParts = new List<SheetPartTO>();
            //Get All SheetParts of Current sheets
            foreach (var sheet in currentSheets)
            {
                var currentSP = unitOfWork.SheetPartRepository?.GetAll().Where(s => s.Sheet == sheet).ToList();
                currentSheetParts.AddRange(currentSP);
            }
            var result = new List<SheetPartTO>();
            // Getting the SheetParts of the Instruments of the LibUser
            foreach (var inst in libUser.InstrumentIds)
            {
                var currentSheetPartByInstru = currentSheetParts?.Where(sp => sp.Instrument.Id == inst);
                result.AddRange(currentSheetPartByInstru);
            }
            return result;
        }

        public string DowloadSheetPart(int SheetPartId)
        {
            var sheetPart = unitOfWork.SheetPartRepository.GetById(SheetPartId);
            var path = sheetPart.Path;
            return path;
        }
    }
}
