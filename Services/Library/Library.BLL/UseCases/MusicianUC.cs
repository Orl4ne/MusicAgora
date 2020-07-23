using Library.DAL;
using Microsoft.CodeAnalysis.CSharp;
using MusicAgora.Common.Library.Interfaces;
using MusicAgora.Common.Library.Interfaces.UseCases;
using MusicAgora.Common.Library.TransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.BLL.UseCases
{
    public class MusicianUC : IMusicianUC
    {
        #region CTOR
        private readonly ILibraryUnitOfWork unitOfWork;
        public MusicianUC(ILibraryUnitOfWork iLibraryUnitOfWork)
        {
            this.unitOfWork = iLibraryUnitOfWork ?? throw new System.ArgumentNullException(nameof(iLibraryUnitOfWork));
        }
        #endregion

        public SheetPartTO SeeASheetPartDetails(int IdentityUserId, int SheetPartId)
        {
            return unitOfWork.SheetPartRepository.GetById(SheetPartId);
        }

        public List<SheetPartTO> GetAllMyCurrentSheetParts(int IdentityUserId)
        {
            var libUser = unitOfWork.LibUserRepository.GetByIdentityUserId(IdentityUserId);
            var currentSheets = unitOfWork.SheetRepository.GetAll().Where(s => s.IsCurrent == true);
            var currentSheetParts = new List<SheetPartTO>();
            foreach (var sheet in currentSheets)
            {
                var currentSP = unitOfWork.SheetPartRepository?.GetAll().Where(s => s.Sheet == sheet).ToList();
                currentSheetParts.AddRange(currentSP);
            }
            var result = new List<SheetPartTO>();
            foreach (var inst in libUser.InstrumentIds)
            {
                var currentSheetPartByInstru = currentSheetParts?.Where(sp => sp.Instrument.Id == inst);
                result.AddRange(currentSheetPartByInstru);
            }
            return result;
        }

    }
}
