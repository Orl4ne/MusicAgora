using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.Configuration;
using MusicAgora.Common.Library.Interfaces;
using MusicAgora.Common.Library.Interfaces.UseCases;
using MusicAgora.Common.Library.TransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.BLL.UseCases
{
    public class ChiefUC : IChiefUC
    {
        #region CTOR
        private readonly ILibraryUnitOfWork unitOfWork;
        private readonly IConfiguration config;

        public ChiefUC(ILibraryUnitOfWork iLibraryUnitOfWork, IConfiguration config) 
        {
            this.unitOfWork = iLibraryUnitOfWork ?? throw new System.ArgumentNullException(nameof(iLibraryUnitOfWork));
            this.config = config;
        }
        #endregion

        public SheetTO SetAsCurrentSheet(int SheetId)
        {
            var sheet = unitOfWork.SheetRepository.GetById(SheetId);
            if (sheet.IsCurrent)
            {
                return sheet;
            }
            sheet.IsCurrent = true;
            unitOfWork.SheetRepository.Update(sheet);
            return sheet;
        }

        public List<SheetPartTO> GetAllSheetPartsBySheet(int SheetId)
        {
            var sheetParts = unitOfWork.SheetPartRepository.GetAll().Where(x=>x.Sheet.Id == SheetId);
            return sheetParts.ToList();
        }

        public List<SheetTO> GetAllSheets()
        {
            var sheets = unitOfWork.SheetRepository.GetAll();
            return sheets.ToList();
        }
    }
}
