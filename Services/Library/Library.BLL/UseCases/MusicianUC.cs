using Library.DAL;
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
        private LibraryContext context;
        public MusicianUC(LibraryContext libraryContext)
        {
            context = libraryContext;
        }
        #endregion
        #region Get A Current SheetPart
        public SheetPartTO GetACurrentSheetPart(int UserId)
        {
            throw new NotImplementedException();
        }
        #endregion

        public List<SheetPartTO> GetAllCurrentSheetParts(int UserId)
        {
            throw new NotImplementedException();
        }

        public bool UpdateMyAccount(int UserId)
        {
            throw new NotImplementedException();
        }
    }
}
