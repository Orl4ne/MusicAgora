using Library.DAL;
using MusicAgora.Common.Library.Interfaces;
using MusicAgora.Common.Library.Interfaces.UseCases;
using MusicAgora.Common.Library.TransferObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.BLL.UseCases
{
    public class MusicianUC : IMusicianUC
    {
        #region CTOR
        private readonly ILibraryUnitOfWOrk unitOfWork;
        public MusicianUC(ILibraryUnitOfWOrk iLibraryUnitOfWork)
        {
            this.unitOfWork = iLibraryUnitOfWork ?? throw new System.ArgumentNullException(nameof(iLibraryUnitOfWork));
        }
        #endregion
        #region Get A Current SheetPart
        public SheetPartTO GetACurrentSheetPart(int UserId)
        {
            throw new NotImplementedException();
        }
        #endregion

        public List<SheetPartTO> GetAllMyCurrentSheetParts(int UserId)
        {
            throw new NotImplementedException();
        }

        public bool UpdateMyAccount(int UserId)
        {
            throw new NotImplementedException();
        }
    }
}
